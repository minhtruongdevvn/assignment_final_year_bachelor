using AAM.AgentSuggestion.Constants;
using AAM.AgentSuggestion.Entities;
using AAM.AgentSuggestion.Interfaces;
using Microsoft.Data.Analysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.ML;
using Microsoft.ML.AutoML;
using Microsoft.ML.Trainers.FastTree;
using System.Data;

namespace AAM.AgentSuggestion.Implements;

internal class Trainer : Common, ITrainer
{
    const string _labelColName = "result";
    const double _intendedAccuracyPercentage = 80;
    readonly IPredictor _predictor;
    readonly ILogger<Trainer> _logger;

    public Trainer(
        IConfiguration configuration,
        IPredictor predictor,
        ILogger<Trainer> logger
    ) : base(configuration)
    {
        _logger = logger;
        _predictor = predictor;
    }

    public async Task TrainAsync(bool onlyIfOutdatedModel = false)
    {
        var dataProps = _meanOfFeatures.Select(x => new MeanOfFeature(x.Name, (double)x.Mean!)).ToList();
        dataProps.Add(new MeanOfFeature(_labelColName));

        if (onlyIfOutdatedModel && await ExamineModelAsync(dataProps))
            return;

        MLContext mlContext = new();

        var trainData = await DatasetExtension.GetDataFromDBAsync(
            dataProps,
            _connectionString,
            "GetDataForTrainer",
            lableName: _labelColName
        );

        // a point where we dont need the seed data
        if (trainData.Rows.Count < 10000)
            trainData.Append(GetSeededData(dataProps.Select(x => x.Name)).Rows, inPlace: true);

        var tunnedModel = GetTunedModel(mlContext, trainData);

        if (tunnedModel == null) { 
            _logger.LogWarning("Cannot tune model to meet the expected accuracy");
            return;
        }

        mlContext.Model.Save(tunnedModel, (trainData as IDataView).Schema, ServiceEnvironment.PredictModelPath);
        _predictor.Reset();
    }

    /// <summary>
    /// Check if an model is outdated
    /// </summary>
    /// <returns>
    /// False if the model is outdated, True if the model is good
    /// </returns>
    private async Task<bool> ExamineModelAsync(IEnumerable<MeanOfFeature> dataProps)
    {
        try
        {
            var validatingData = await DatasetExtension.GetDataFromDBAsync(
                dataProps,
                _connectionString,
                "GetDataForTrainer",
                lableName: _labelColName
            );

            var actualResult = validatingData.Columns.First(x => x.Name == _labelColName).Cast<bool>().ToArray();
            validatingData.Columns.Remove(_labelColName);
            var predictedResult = _predictor.GetAgentPredictResult(validatingData).Select(x => x.Success).ToArray();

            int totalItem = actualResult.Length;
            int totalMatch = 0;
            for (int i = 0; i < totalItem; i++)
            {
                if (actualResult[i] == predictedResult[i])
                    totalMatch++;
            }

            return totalMatch * 100 / totalItem >= _intendedAccuracyPercentage;
        }
        catch
        {
            return false;
        }
    }

    private DataFrame GetSeededData(IEnumerable<string> dataProps)
    {
        var rawSeededData = DataFrame.LoadCsv(ServiceEnvironment.PreSeedDataPath);
        var rawSeededDataCount = rawSeededData.Rows.Count;

        var seededData = new DataFrame();
        foreach (var prop in dataProps)
        {
            var rawColumn = rawSeededData.Columns.FirstOrDefault(col => col.Name == prop);
            if (rawColumn == null)
                seededData.Columns.Add(DataFrameColumn.Create(prop, new int[rawSeededDataCount]));
            else
                seededData.Columns.Add(rawColumn);
        }

        return seededData;
    }

    private ITransformer? GetTunedModel(MLContext context, IDataView trainData) {
        var experiment = context.Auto().CreateBinaryClassificationExperiment(600);
        var result = experiment.Execute(trainData, _labelColName);
        if (meetIntendedAccuracy()) return result.BestRun.Estimator.Fit(trainData);
        else {
            // retry
            experiment = context.Auto().CreateBinaryClassificationExperiment((uint)TimeSpan.FromDays(1).TotalSeconds);
            result = experiment.Execute(trainData, _labelColName);
            if (meetIntendedAccuracy()) 
                return result.BestRun.Estimator.Fit(trainData);
            else
                return null;
        }

        bool meetIntendedAccuracy() =>           
            result!.BestRun.ValidationMetrics.Accuracy * 100 >= _intendedAccuracyPercentage &&
                result.BestRun.ValidationMetrics.F1Score * 100 >= _intendedAccuracyPercentage;
        
    }
}



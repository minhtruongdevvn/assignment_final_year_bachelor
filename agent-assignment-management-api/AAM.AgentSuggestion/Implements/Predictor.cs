using AAM.AgentSuggestion.Constants;
using AAM.AgentSuggestion.Entities;
using AAM.AgentSuggestion.Extensions;
using AAM.AgentSuggestion.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.ML;
using Microsoft.ML.Data;
using NumSharp.Utilities.Linq;
using System.Data;
using System.Reflection;

namespace AAM.AgentSuggestion.Implements;

internal class Predictor : Common, IPredictor
{
    ITransformer? _predictModelStore = null;
    ITransformer _predictModel
    {
        get
        {
            if (_predictModelStore == null)
            {
                try
                {
                    var mlContext = new MLContext();
                    _predictModelStore =
                        mlContext.Model.Load(ServiceEnvironment.PredictModelPath, out var _);
                }
                catch
                {
                    throw new Exception("No trained model is available");
                }

            }

            return _predictModelStore;
        }
    }

    Type? _inputTypeStore = null;
    Type _inputType
    {
        get
        {
            if (_inputTypeStore == null)
            {
                _inputTypeStore = ReflectionExtensions.CreateDynamicType(
                        _features.Select(x => new DynamicTypeProperty(x, typeof(Single))).ToList());
            }

            return _inputTypeStore!;
        }
    }

    public Predictor(IConfiguration configuration)
        : base(configuration) { }

    public async Task<List<PredictResult>> GetAgentPredictResultAsync(string questId, bool excludeFail = true)
    {
        var props = _meanOfFeatures.Select(x => new MeanOfFeature(x.Name, (double)x.Mean!)).ToList();
        props.Add(new MeanOfFeature("id"));
        var agentsInfo = await DatasetExtension.GetDataFromDBAsync(
            props,
            _connectionString,
            "GetDataForPredictor",
            param: new { questId }
        );

        return PredictMultiple(agentsInfo, excludeFail);
    }

    public List<PredictResult> GetAgentPredictResult(IDataView agentsInfo, bool excludeFail = false)
        => PredictMultiple(agentsInfo, excludeFail);


    public async Task<PredictResult> GetPredictResultByAgentIdAsync(string questId, string agentId)
    {
        var connection = new SqlConnection(_connectionString);
        var agentInfo = await connection.QuerySingleAsync<object>(
            "GetDataForPredictor",
            new { questId, agentId },
            commandType: CommandType.StoredProcedure
        );

        return Predict(agentInfo);
    }

    private List<PredictResult> PredictMultiple(IDataView agentsInfo, bool excludeFail)
    {
        var predictResult = _predictModel.Transform(agentsInfo);
        var labels = predictResult.GetColumn<bool>("PredictedLabel").ToArray();
        var resultLength = labels.Count();

        string[] ids;
        if (agentsInfo.Schema.Any(x => x.Name == "id"))
        {
            ids = predictResult.GetColumn<string>("id").ToArray();
        }
        else
        {
            ids = new string[resultLength];
        }

        var scores = predictResult.GetColumn<float>("Score").ToArray();
        var probas = predictResult.GetColumn<float>("Probability").ToArray();

        var result = new List<PredictResult>();
        for (int i = 0; i < resultLength; i++)
        {
            if (excludeFail && !labels[i]) continue;
            result.Add(new()
            {
                AgentId = ids[i] == null ? null : new Guid(ids[i]),
                Score = scores[i],
                Probability = probas[i],
                Success = labels[i]
            });
        }

        return result;
    }

    private PredictResult Predict(object agentInfo)
    {
        var mlContext = new MLContext();

        var genericPredictionMethod = mlContext.Model
            .GetType()
            .GetMethod("CreatePredictionEngine",
                new[] {
                                    typeof(ITransformer),
                                    typeof(bool),
                                    typeof(SchemaDefinition),
                                    typeof(SchemaDefinition)
                });

        var predictionMethod = genericPredictionMethod!.MakeGenericMethod(_inputType, typeof(PredictResult));
        var engine = predictionMethod.Invoke(mlContext.Model, new object[] { _predictModel, true, null!, null! });

        var data = Activator.CreateInstance(_inputType);
        var inputDic = (IDictionary<string, object>)agentInfo!;
        foreach (var fea in _features)
        {
            ReflectionExtensions.SetProperty(data!, fea, Convert.ToSingle(inputDic[fea]));
        }

        var predictMethod = (engine!.GetType().GetMethods() as MethodInfo[])!
            .First(
                x => x.Name == "Predict" &&
                !x.Attributes.HasFlag(MethodAttributes.Virtual)
            );

        dynamic result = predictMethod.Invoke(engine, new[] { data })!;
        return new PredictResult
        {
            AgentId = (Guid)inputDic["id"],
            Score = result.Score,
            Probability = result.Probability,
            Success = result.Success,
        };
    }

    public void Reset()
    {
        var meanOfFeatures = DatasetConstant.ConstFeatures.Select(x => new MeanOfFeature(x, 0)).ToList();
        meanOfFeatures.AddRange(DatasetExtension.GetSkillAsync(_connectionString).Result);
        _meanOfFeatureStore = meanOfFeatures;

        _inputTypeStore = ReflectionExtensions.CreateDynamicType(
            _features.Select(x => new DynamicTypeProperty(x, typeof(Single))).ToList());

        var mlContext = new MLContext();
        _predictModelStore =
            mlContext.Model.Load(ServiceEnvironment.PredictModelPath, out var _);
    }
}


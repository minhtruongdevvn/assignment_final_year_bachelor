using AAM.AgentSuggestion.Constants;
using AAM.AgentSuggestion.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AAM.AgentSuggestion.Implements;

internal class Common
{
    protected readonly IConfiguration _configuration;

    protected List<MeanOfFeature>? _meanOfFeatureStore = null;

    protected List<MeanOfFeature> _meanOfFeatures
    {
        get
        {
            if (_meanOfFeatureStore == null)
            {
                var meanOfFeatures = DatasetConstant.ConstFeatures.Select(x => new MeanOfFeature(x, 0)).ToList();
                meanOfFeatures.AddRange(DatasetExtension.GetSkillAsync(_connectionString).Result);
                _meanOfFeatureStore = meanOfFeatures;
            }

            return _meanOfFeatureStore;
        }
    }

    protected List<string> _features
    {
        get
        {
            return _meanOfFeatures.Select(x => x.Name).ToList();
        }
    }

    protected string _connectionString
    {
        get
        {
            return _configuration.GetConnectionString("aamdb");
        }
    }

    public Common(IConfiguration configuration)
    {
        _configuration = configuration;
    }
}


using Microsoft.ML.Data;

namespace AAM.AgentSuggestion.Entities;

public class PredictResult
{

    [ColumnName("PredictedLabel")]
    public bool Success { get; set; }

    public float Score { get; set; }

    public float Probability { get; set; }

    [NoColumn]
    public Guid? AgentId { get; set; }
}


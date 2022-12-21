namespace AAM.AgentSuggestion.Entities;

public class MeanOfFeature
{
    public string Name { get; set; } = string.Empty;
    public double? Mean { get; set; } = null;

    public MeanOfFeature(string name, double mean)
    {
        Name = name;
        Mean = mean;
    }

    public MeanOfFeature(string name)
    {
        Name = name;
    }
}


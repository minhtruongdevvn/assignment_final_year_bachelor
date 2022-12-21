namespace AAM.AgentSuggestion.Constants;

internal static class ServiceEnvironment
{
    static readonly string _predictorFolderPath
        = Directory.GetParent(Directory.GetCurrentDirectory())?.FullName + "\\AAM.AgentSuggestion";

    public static string PreSeedDataPath
    {
        get
        {
            return _predictorFolderPath + "\\seed_with_defined_logic.csv";
        }
    }

    public static string PredictModelPath
    {
        get
        {
            return _predictorFolderPath + "\\Models\\" + "ForestClassifierModel.zip";
        }
    }
}


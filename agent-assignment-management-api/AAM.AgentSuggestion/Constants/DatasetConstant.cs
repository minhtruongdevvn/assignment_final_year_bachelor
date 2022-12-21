namespace AAM.AgentSuggestion.Constants;

internal static class DatasetConstant
{
    public static List<string> ConstFeatures
    {
        get
        {
            return new() {
                "age", "self_discipline", "height", "sex",
                "iq", "eq", "stamina", "strength", "reaction_time",
                "category_code", "num_success", "num_agent", "necessity"
            };
        }
    }
}


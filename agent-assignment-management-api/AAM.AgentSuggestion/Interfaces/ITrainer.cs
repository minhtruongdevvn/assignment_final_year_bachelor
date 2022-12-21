namespace AAM.AgentSuggestion.Interfaces;

public interface ITrainer
{
    Task TrainAsync(bool onlyIfOutdatedModel = false);
}



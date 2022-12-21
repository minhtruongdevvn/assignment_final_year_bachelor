using AAM.AgentSuggestion.Implements;
using AAM.AgentSuggestion.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AAM.AgentSuggestion;

public static class ServiceCollectionExtensions
{
    public static void AddPredictor(this IServiceCollection services)
    {
        services.AddScoped<ITrainer, Trainer>();
        services.AddSingleton<IPredictor, Predictor>();
    }
}

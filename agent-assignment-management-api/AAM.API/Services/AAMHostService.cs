using AAM.AgentSuggestion.Interfaces;

namespace AAM.API;

public class AAMHostService : BackgroundService
{
    readonly IServiceProvider _serviceProvider;

    public AAMHostService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var timer = new PeriodicTimer(TimeSpan.FromDays(30));
        while (await timer.WaitForNextTickAsync())
        {
            Thread thread = new Thread(RunReTrainModel);
            thread.IsBackground = true;
            thread.Priority = ThreadPriority.BelowNormal;
            thread.Start();
        }
    }

    private async void RunReTrainModel()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            ITrainer trainer = scope.ServiceProvider.GetRequiredService<ITrainer>();
            await trainer.TrainAsync(true);
        }
    }
}

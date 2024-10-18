namespace Services.Interfaces
{
    public interface IArticleBackgroundService
    {
        //public Task ExecuteAsync(CancellationToken stoppingToken);
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
    }
}

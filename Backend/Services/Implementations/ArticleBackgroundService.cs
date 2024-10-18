using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Interfaces;

namespace Services.Implementations
{
    public class ArticleBackgroundService : BackgroundService, IArticleBackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILoggerHelper _logger;
        public ArticleBackgroundService(IServiceProvider serviceProvider, ILoggerHelper logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Article background service is starting...");
            _logger.LogInfo("Article background service is starting...");
            return base.StartAsync(cancellationToken);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Article background service is starting...");
            _logger.LogInfo("Article background service is starting...");

            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine($"Fetching and processing RSS feeds at: {DateTimeOffset.UtcNow}");
                _logger.LogInfo($"Fetching and processing RSS feeds at: {DateTimeOffset.UtcNow}");

                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var rssFeedService = scope.ServiceProvider.GetRequiredService<IRssFeedService>();
                    await rssFeedService.FetchAndProcessRssFeedsAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex + "\nAn error occurred while fetching and processing RSS feeds!");
                    _logger.LogError(ex, "An error occurred while fetching and processing RSS feeds!");
                }

                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }

            Console.WriteLine("Article background service is stopping...");
            _logger.LogInfo("Article background service is stopping...");
        }
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Article background service is stopping...");
            _logger.LogInfo("Article background service is stopping...");
            await base.StopAsync(cancellationToken);
        }
    }
}

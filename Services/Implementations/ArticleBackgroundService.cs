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
            _logger.LogInfo("Article background service is starting...");
            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInfo("Article background service is starting...");

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInfo($"Fetching and processing RSS feeds at: {DateTimeOffset.UtcNow}");

                try
                {
                    //await _rssFeedService.FetchAndProcessRssFeedsAsync();
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var rssFeedService = scope.ServiceProvider.GetRequiredService<IRssFeedService>();
                        await rssFeedService.FetchAndProcessRssFeedsAsync();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while fetching and processing RSS feeds!");
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }

            _logger.LogInfo("Article background service is stopping...");
        }
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInfo("Article background service is stopping...");
            await base.StopAsync(cancellationToken);
        }
    }
}

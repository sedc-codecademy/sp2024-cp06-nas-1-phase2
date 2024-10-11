using Microsoft.Extensions.Logging;
using Services.Interfaces;

namespace Services.Implementations
{
    public class LoggerHelper<T> : ILoggerHelper
    {
        private readonly ILogger<T> _logger;
        public LoggerHelper(ILogger<T> logger)
        {
            _logger = logger;
        }
        public void LogInfo(string message)
        {
            _logger.LogInformation("LogInfo: {Message}", message);
        }

        public void LogError(Exception ex, string message)
        {
            _logger.LogError(ex, "LogError: {Message}", message);
        }
    }
}

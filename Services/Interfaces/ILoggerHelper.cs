namespace Services.Interfaces
{
    public interface ILoggerHelper
    {
        void LogInfo(string message);
        void LogError(Exception ex, string message);
    }
}

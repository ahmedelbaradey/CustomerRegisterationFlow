namespace CustomerRegisterationFlow.Application.Contracts.Infrastructure
{
    public interface ILoggerManager
    {
        void LogInfo(string message, string method, string url);
        void LogWarn(string message);
        void LogDebug(string message);
        void LogError(string message);
    }
}

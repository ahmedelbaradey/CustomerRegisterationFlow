using CustomerRegisterationFlow.Application.Contracts.Infrastructure;
using NLog;

namespace CustomerRegisterationFlow.LoggerService
{
    public class LoggerManager : ILoggerManager
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        public LoggerManager()
        { }
        public void LogDebug(string message) => logger.Debug(message);
        public void LogError(string message) => logger.Error(message);
        public void LogInfo(string message,string method,string url)
        {
            LogEventInfo info = new LogEventInfo(LogLevel.Info, "_database", message);
            info.Properties["URL"] = url;
            info.Properties["date"] = DateTime.Now;
            info.Properties["UserId"] = 4;
            info.Properties["exception"] = null;
            logger.Info(message);
        }
        public void LogWarn(string message) => logger.Warn(message);
       
    }
}

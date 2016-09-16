using System;

namespace Log.Interface
{
    public interface ILogger
    {
        void LogError(Exception exception);
        void LogWarn(Exception exception);
        void LogWarn(string message);
        void LogInfo(string message);
        void LogTrace(string message);
    }
}

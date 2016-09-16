using System;
using Log.Interface;
using NLog;

namespace Log
{
    public class Logger : Log.Interface.ILogger
    {
        private static readonly NLog.Logger Log;
        static Logger()
        {
            Log = LogManager.GetCurrentClassLogger();
        }

        public void LogError(Exception exception)
        {
            Log.Error(exception.Message, exception);
        }

        public void LogWarn(Exception exception)
        {
            Log.Warn(exception.Message, exception);
        }

        public void LogWarn(string message)
        {
            Log.Warn(message);
        }

        public void LogInfo(string message)
        {
            Log.Info(message);
        }

        public void LogTrace(string message)
        {
            Log.Trace(message);
        }
    }
}

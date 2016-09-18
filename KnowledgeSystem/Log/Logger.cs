using System;
using Log.Interface;
using NLog;

namespace Log
{
    /// <summary>
    /// Class for logining system messagges
    /// </summary>
    public class Logger : Log.Interface.ILogger
    {
        private static readonly NLog.Logger Log;
        static Logger()
        {
            Log = LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// Log exception
        /// </summary>
        /// <param name="exception"></param>
        public void LogError(Exception exception)
        {
            Log.Error(exception.Message, exception);
        }

        /// <summary>
        /// Log warning
        /// </summary>
        /// <param name="exception"></param>
        public void LogWarn(Exception exception)
        {
            Log.Warn(exception.Message, exception);
        }

        /// <summary>
        /// Log warning message 
        /// </summary>
        /// <param name="message"></param>
        public void LogWarn(string message)
        {
            Log.Warn(message);
        }

        /// <summary>
        /// Log info message
        /// </summary>
        /// <param name="message"></param>
        public void LogInfo(string message)
        {
            Log.Info(message);
        }

        /// <summary>
        /// Log trace message
        /// </summary>
        /// <param name="message"></param>
        public void LogTrace(string message)
        {
            Log.Trace(message);
        }
    }
}

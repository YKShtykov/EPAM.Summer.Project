using System;
using NLog;

namespace BLL
{
    /// <summary>
    /// Service class for logging
    /// </summary>
    public static class Logger
    {
        private static readonly NLog.Logger Log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Log Error
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exception"></param>
        public static void LogError<T>(T exception) where T : Exception
        {
            Log.Error(exception.Message, exception);
        }

        /// <summary>
        /// Log information
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exception"></param>
        public static void LogInfo<T>(T exception) where T : Exception
        {
            Log.Info(exception.Message, exception);
        }

        /// <summary>
        /// Log warning
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exception"></param>
        public static void LogWarn<T>(T exception) where T : Exception
        {
            Log.Warn(exception.Message, exception);
        }

        /// <summary>
        /// Log trace
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exception"></param>
        public static void LogTrace<T>(T exception) where T : Exception
        {
            Log.Trace(exception.Message, exception);
        }
    }
}

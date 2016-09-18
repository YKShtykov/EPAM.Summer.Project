using System;

namespace Log.Interface
{
    /// <summary>
    /// Interface for loggining classes
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Log exception
        /// </summary>
        /// <param name="exception"></param>
        void LogError(Exception exception);

        /// <summary>
        /// Log warning
        /// </summary>
        /// <param name="exception"></param>
        void LogWarn(Exception exception);

        /// <summary>
        /// Log warning message
        /// </summary>
        /// <param name="message"></param>
        void LogWarn(string message);

        /// <summary>
        /// Log info message
        /// </summary>
        /// <param name="message"></param>
        void LogInfo(string message);

        /// <summary>
        /// Log trace message
        /// </summary>
        /// <param name="message"></param>
        void LogTrace(string message);
    }
}

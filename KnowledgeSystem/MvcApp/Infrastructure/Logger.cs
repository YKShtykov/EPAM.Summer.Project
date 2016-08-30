using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLog;

namespace MvcApp.Infrastructure
{
    public static class Logger
    {
        private static readonly NLog.Logger Log = LogManager.GetCurrentClassLogger();

        public static void LogError<T>(T exception) where T : Exception
        {
            Log.Error(exception.Message, exception);
        }

        public static void LogInfo<T>(T exception) where T : Exception
        {
            Log.Info(exception.Message, exception);
        }

        public static void LogInfo(string info)
        {
            Log.Info(info);
        }

        public static void LogWarn<T>(T exception) where T : Exception
        {
            Log.Warn(exception.Message, exception);
        }

        public static void LogTrace<T>(T exception) where T : Exception
        {
            Log.Trace(exception.Message, exception);
        }

        public static void Test()
        {
            Log.Info("This is a test message");
        }
    }
}
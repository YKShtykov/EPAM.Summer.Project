using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace BLL
{
    public static class Logger
    {
        private static readonly NLog.Logger Log = LogManager.GetCurrentClassLogger();

        public static void LogError<T>(T exception) where T : Exception
        {
            Log.Error(exception.Message, exception);
            //using (var file = new System.IO.StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"log.txt"), true))
            //{
            //    file.WriteLine("{0}: {1}{2}", DateTime.Now, message, Environment.NewLine);
            //}
        }

        public static void LogInfo<T>(T exception) where T : Exception
        {
            Log.Info(exception.Message, exception);
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
            Log.Info("This is a test message");
            Log.Info("This is a test message");
            Log.Info("This is a test message");
            Log.Info("This is a test message");
            Log.Info("This is a test message");
            Log.Info("This is a test message");
            Log.Info("This is a test message");
            Log.Info("This is a test message");
        }
    }
}

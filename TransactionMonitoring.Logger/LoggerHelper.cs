using System;
using System.Collections.Generic;
using System.Text;
using NLog;
using System.Runtime.CompilerServices;
using System.IO;

namespace TransactionMonitoring.Logger
{
    public class LoggerHelper : ILoggerHelper
    {
        private static readonly ILogger DefaultLogger = LogManager.GetCurrentClassLogger();

        static LoggerHelper()
        {
            LogManager.AutoShutdown = true;
        }

        public void TraceLog(string message = "", [CallerFilePath] string callerFilePath = null)
        {
            CreateLogEvent(LogLevel.Trace, callerFilePath, message);
        }

        public void ErrorLog(Exception ex, string callerFilePath)
        {
            string name = !string.IsNullOrWhiteSpace(callerFilePath)
                ? Path.GetFileNameWithoutExtension(callerFilePath)
                : null;
            var logger = string.IsNullOrWhiteSpace(name) ? DefaultLogger : LogManager.GetLogger(name);

            LogEventInfo logEvent = new LogEventInfo();


            if (ex.InnerException != null && ex.InnerException.InnerException != null)
            {
                logEvent.Properties.Add("InnerExceptionMessage", ex.InnerException.InnerException.Message);
            }
            else if (ex.InnerException != null)
            {
                logEvent.Properties.Add("InnerExceptionMessage", ex.InnerException.Message);
            }
            logEvent.Properties.Add("StackTrace", ex.StackTrace);

            logEvent.LoggerName = logger.Name;
            logEvent.Level = LogLevel.Error;
            logEvent.Message = ex.Message;

            WriteLog(logEvent);
        }

        public void Shutdown() => LogManager.Shutdown();

        private void CreateLogEvent(LogLevel logLevel, string callerFilePath, string message)
        {
            string name = !string.IsNullOrWhiteSpace(callerFilePath)
                ? Path.GetFileNameWithoutExtension(callerFilePath)
                : null;
            var logger = string.IsNullOrWhiteSpace(name) ? DefaultLogger : LogManager.GetLogger(name);

            LogEventInfo logEvent = new LogEventInfo();
            logEvent.LoggerName = logger.Name;
            logEvent.Level = logLevel;
            logEvent.Message = message;

            WriteLog(logEvent);
        }

        private void WriteLog(LogEventInfo logEvent)
        {
            DefaultLogger.Log(logEvent);
        }

    }
}

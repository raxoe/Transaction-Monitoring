using System;
using System.Runtime.CompilerServices;

namespace TransactionMonitoring.Logger
{
    public interface ILoggerHelper
    {
        void TraceLog(string message = "", [CallerFilePath] string callerFilePath = null);
        void ErrorLog(Exception ex, [CallerFilePath] string callerFilePath = null);
        void Shutdown();
    }
}
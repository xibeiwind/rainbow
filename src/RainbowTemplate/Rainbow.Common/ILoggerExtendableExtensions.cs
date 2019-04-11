using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Rainbow.Common
{
    public static class ILoggerExtendableExtensions
    {
        public static void LogInfo(this ILoggerExtendable target, string message,
            [CallerMemberName] string methodName = default,
            [CallerLineNumber] int lineNumber = default)
        {
            target.Logger.Log(LogLevel.Information, $"[{lineNumber}]({methodName}): {message}");
        }

        public static void LogError(this ILoggerExtendable target,
            Exception exception,
            string message,
            [CallerMemberName] string methodName = default,
            [CallerLineNumber] int lineNumber = default)
        {
            target.Logger.Log(LogLevel.Error, exception, $"[{lineNumber}]({methodName}): {message}");
        }

        public static void ActionWithLog(this ILoggerExtendable target, Action action,
            string message,
            string errorMessage = default,
            [CallerMemberName] string methodName = default,
            [CallerLineNumber] int lineNumber = default
        )
        {
            try
            {
                action.Invoke();
                target.Logger.Log(LogLevel.Information, $"[{lineNumber}]({methodName}): {message}");
            }
            catch (Exception ex)
            {
                target.Logger.Log(LogLevel.Error, ex, $"[{lineNumber}]({methodName}): {errorMessage ?? ex.Message}");
                throw;
            }
        }
    }
}

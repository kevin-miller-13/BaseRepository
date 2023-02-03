using System;
using System.Collections.Generic;
using System.Text;

namespace Korbitec.Licensing.Common
{
    public static class ExceptionExtensions
    {
        public static string BuildExceptionString(this Exception exception)
        {
            return exception == null
                ? string.Empty
                : BuildExceptionString(exception, true, true, true);
        }

        private static void BuildExceptionString(StringBuilder sb, string exceptionContext, Exception exception, bool includeExceptionType, bool includeExceptionMessage)
        {
            if (includeExceptionType)
                sb.AppendLine($"{exception.GetType()} ({exceptionContext})");
            if (includeExceptionMessage)
                sb.AppendLine(exception.Message);
            sb.AppendLine(exception.StackTrace);
        }

        private static string BuildExceptionString(Exception exception, bool includeExceptionType, bool includeExceptionMessage, bool includeInnerExceptions)
        {
            var sb = new StringBuilder();

            var exceptions = new List<Exception> { exception };

            if (includeInnerExceptions)
            {
                exception = exception.InnerException;
                while (exception != null)
                {
                    exceptions.Add(exception);
                    exception = exception.InnerException;
                }

                for (var i = exceptions.Count - 1; i > 0; i--)
                {
                    BuildExceptionString(sb, "inner exception", exceptions[i], includeExceptionType, includeExceptionMessage);
                    sb.AppendLine();
                }
            }

            BuildExceptionString(sb, "exception", exceptions[0], includeExceptionType, includeExceptionMessage);

            return sb.ToString();
        }
    }
}

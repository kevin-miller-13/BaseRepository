using System;

namespace Korbitec.Licensing.Application.Exceptions
{
    public class BaseObjectException : Exception
    {
        protected BaseObjectException(string message, int code)
            : base(message)
        {
            Message = message;
            Code = code;
        }

        public BaseObjectException(string message, Exception exception)
            : base(message, exception)
        {
            Message = message;
        }

        public new string Message { get; set; }
        public int Code { get; set; }
    }
}

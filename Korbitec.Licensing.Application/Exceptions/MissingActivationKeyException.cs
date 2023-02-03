using System;

namespace Korbitec.Licensing.Application.Exceptions
{
    public class MissingActivationKeyException : Exception
    {
        public new string Message = "Activation key not provided.";
        public int Code = 1;
    }
}

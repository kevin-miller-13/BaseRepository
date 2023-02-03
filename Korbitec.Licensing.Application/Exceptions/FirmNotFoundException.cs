using System;

namespace Korbitec.Licensing.Application.Exceptions
{
    public class FirmNotFoundException : Exception
    {
        public new string Message = "Firm not found.";
        public int Code = 5;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Korbitec.Licensing.Application.Exceptions
{
    public class MissingServerIdException : Exception
    {
        public new string Message = "Server id not provided.";
        public int Code = 4;
    }
}

namespace Korbitec.Licensing.Application.Exceptions
{
    public class ServerIdNotFoundException : System.Exception
    {
        public new string Message = "Server id not found.";
        public int Code = 3;
    }
}

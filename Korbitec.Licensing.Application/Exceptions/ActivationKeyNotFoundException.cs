namespace Korbitec.Licensing.Application.Exceptions
{
    public class ActivationKeyNotFoundException : System.Exception
    {
        public new string Message = "Activation key not found.";
        public int Code = 2;
    }
}

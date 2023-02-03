namespace Korbitec.Licensing.Common
{
    public class DeserializeObjectException : System.Exception
    {
        public new string Message = "Invalid Resource Response. Response could not be parsed.";
        public int Code = 6;
    }
}

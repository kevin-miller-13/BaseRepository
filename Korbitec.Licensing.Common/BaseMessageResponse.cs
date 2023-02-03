namespace Korbitec.Licensing.Common
{
    public class BaseMessageResponse
    {
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
    }
}

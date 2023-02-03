using Microsoft.Azure.Cosmos.Table;

namespace Korbitec.Licensing.Common.Persistence.TableStorage.DTOs
{
    public class ExceptionEntity : TableEntity
    {
        public ExceptionEntity(
            string id,
            string time,
            string title,
            string type,
            string request,
            string stackTrace)
        {
            PartitionKey = id;
            RowKey = time;
            Title = title;
            Type = type;
            Request = request;
            StackTrace = stackTrace;
        }

        public string Title { get; set; }
        public string Type { get; set; }
        public string Request { get; set; }
        public string StackTrace { get; set; }
    }
}

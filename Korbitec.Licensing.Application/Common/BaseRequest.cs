using System;
using System.Text.Json.Serialization;
using System.Threading;
using Microsoft.Azure.WebJobs;

namespace Korbitec.Licensing.Application
{
    public class BaseRequest
    {
        public Guid RequestId { get; set; }

        [JsonIgnore]
        public string FunctionName { get; set; }

        [JsonIgnore]
        public IBinder Binder { get; set; }

        [JsonIgnore]
        public CancellationToken Token { get; set; }

        public BaseRequest()
        {
            
        }

        public BaseRequest(Guid requestId, string functionName, IBinder binder)
        {
            RequestId = requestId;

            FunctionName = functionName;

            Binder = binder;

            Token = new CancellationToken();
        }
    }
}

using System;
using MediatR;
using Microsoft.Azure.WebJobs;

namespace Korbitec.Licensing.Application.Queries.ValidateKeyDeactivation
{

    public class ValidateKeyDeactivationRequest : BaseRequest, IRequest<ValidateKeyDeactivationResponse>
    {
        public ValidateKeyDeactivationRequest(Guid requestId, string functionName, IBinder binder, string activationKey)
            : base(requestId, functionName, binder)
        {
            ActivationKey = activationKey;
        }

        public string ActivationKey { get; set; }
      
    }
}

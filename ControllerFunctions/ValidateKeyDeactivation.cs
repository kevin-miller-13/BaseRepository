using Korbitec.Licensing.Application.Queries.ValidateKeyDeactivation;
using Korbitec.Licensing.Enums;
using Korbitec.Licensing.FunctionApplication.Dtos;
using Korbitec.Licensing.FunctionApplication.Exception;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace Korbitec.Licensing.FunctionApplication.ControllerFunctions
{
    public class ValidateKeyDeactivation
    {
        private readonly IMediator _mediator;

        private const string ActivationKeyQueryKey = "activationkey";

        public ValidateKeyDeactivation(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("ValidateKeyDeactivation")]
        public async System.Threading.Tasks.Task<IActionResult> RunValidateKeyDeactivationAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            Binder binder,
            ExecutionContext context)
        {
            var activationKey = req.Query[ActivationKeyQueryKey];

            try
            {
                var response = await _mediator.Send(new ValidateKeyDeactivationRequest(
                    requestId: context.InvocationId,
                    functionName: context.FunctionName,
                    binder: binder,
                    activationKey: activationKey));

                var keyStatus = !response.ServerDeleted && response.LicensingServerStatus() == LicensingServerStatus.NotActivated;

                return new OkObjectResult(new ValidateKeyDeactivationResponseDto(keyStatus));
            }

            catch (System.Exception e)
            {
                return e.Build();
            }
        }
    }
}


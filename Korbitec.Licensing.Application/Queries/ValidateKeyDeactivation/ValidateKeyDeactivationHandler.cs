using Korbitec.Licensing.Application.Queries.ValidateKeyDeactivation.InfrastructureContracts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Korbitec.Licensing.Application.Queries.ValidateKeyDeactivation
{
    public class ValidateKeyDeactivationHandler : IRequestHandler<ValidateKeyDeactivationRequest, ValidateKeyDeactivationResponse>
    {
        public ValidateKeyDeactivationHandler(IValidateKeyDeactivationRepository getServerIdRepository)
        {
            _repository = getServerIdRepository;
        }

        public IValidateKeyDeactivationRepository _repository;

        public async Task<ValidateKeyDeactivationResponse> Handle(ValidateKeyDeactivationRequest request, CancellationToken cancellationToken)
        {
            var status = await _repository.GetActivationKeyStatus(request.ActivationKey);

            ValidateKeyDeactivationResponse response = new ValidateKeyDeactivationResponse
            {
                ServerStatus = status.ServerStatus,
                ServerDeleted = status.ServerDeleted
            };

            return response;
        }
    }
}

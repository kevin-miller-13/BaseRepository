using Korbitec.Licensing.Application.Queries.ValidateKeyDeactivation.InfrastructureContracts.Models;
using System.Threading.Tasks;

namespace Korbitec.Licensing.Application.Queries.ValidateKeyDeactivation.InfrastructureContracts
{
    public interface IValidateKeyDeactivationRepository
    {
        Task<ActivationKeyStatusModel> GetActivationKeyStatus(string activationKey);
    }
}

using Korbitec.Licensing.Application.Exceptions;
using Korbitec.Licensing.Application.Queries.ValidateKeyDeactivation.InfrastructureContracts;
using Korbitec.Licensing.Application.Queries.ValidateKeyDeactivation.InfrastructureContracts.Models;
using Korbitec.Licensing.Persistence.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Korbitec.Licensing.Persistence
{
    public class ValidateKeyDeactivationRepository : IValidateKeyDeactivationRepository
    {
        private readonly LicensingContext _context;

        public ValidateKeyDeactivationRepository(LicensingContext context) 
        {
            _context = context;
        }

        public async Task<ActivationKeyStatusModel> GetActivationKeyStatus(string activationKey)
        {
            if (string.IsNullOrWhiteSpace(activationKey))
                throw new MissingActivationKeyException();

            var result =
                from ac in _context.ActivationCode
                join lsac in _context.LicensingServerActivationCode on ac.Id equals lsac.ActivationCodeId
                join ls in _context.LicensingServer on lsac.ServerId equals ls.Id
                where ac.Code.ToLower().Equals(activationKey.ToLower())
                select new ActivationKeyStatusModel
                {
                    ServerStatus = ls.Status,
                    ServerDeleted = ls.Deleted
                };

            if (await result.AnyAsync())
                return result.First();

            throw new ActivationKeyNotFoundException();
        }
    }
}
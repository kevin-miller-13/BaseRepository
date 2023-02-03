using Korbitec.Licensing.Application.Queries.ValidateKeyDeactivation.InfrastructureContracts;
using Korbitec.Licensing.Persistence.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Korbitec.Licensing.Persistence.Configuration
{
    public static class LicensingInfrastructureConfiguration
    {
        public static IServiceCollection ConfigureLicensingInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<LicensingContext>();
            services.AddScoped<IValidateKeyDeactivationRepository, ValidateKeyDeactivationRepository>();

            return services;
        }
    }
}

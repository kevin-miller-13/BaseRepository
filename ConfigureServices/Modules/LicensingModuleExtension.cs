using Korbitec.Licensing.Application;
using Korbitec.Licensing.Application.Queries.ValidateKeyDeactivation;
using Korbitec.Licensing.Persistence.Configuration;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Korbitec.Licensing.FunctionApplication.ConfigureServices.Modules
{
    public static class LicensingModuleExtension
    {
        public static IServiceCollection AddLicensingModule(this IServiceCollection services)
        {
            services.ConfigureLicensingInfrastructure();
            
            services.AddMediatR(typeof(LicensingApplicationAssembly).GetTypeInfo().Assembly);

            services.AddTransient<ValidateKeyDeactivationHandler>();

            return services;
        }
    }
}

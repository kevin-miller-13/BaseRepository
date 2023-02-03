using Korbitec.Licensing.FunctionApplication.ConfigureServices.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace Korbitec.Licensing.FunctionApplication.ConfigureServices
{
    public static class ModulesExtension
    {
        public static IServiceCollection AddModules(this IServiceCollection services)
        {
            services.AddLicensingModule();

            return services;
        }
    }
}

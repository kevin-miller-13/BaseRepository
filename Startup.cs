using System;
using System.Linq;
using Korbitec.Common.Persistence.Configuration;
using Korbitec.Licensing.Common;
using Korbitec.Licensing.FunctionApplication;
using Korbitec.Licensing.FunctionApplication.ConfigureServices;
using MediatR.Pipeline;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace Korbitec.Licensing.FunctionApplication
{
    public class Startup : FunctionsStartup
    {
        //01
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            builder.ConfigurationBuilder.LoadCloudSettings();
        }

        //02
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configuration = builder.GetContext()?.Configuration ??
                
            (IConfiguration)builder.Services.First(x => x.ServiceType == typeof(IConfiguration)).ImplementationInstance;

            builder.Services.Configure<DatabaseSettings>(options =>
            {
                options.DBConnectionString = Environment.GetEnvironmentVariable(SettingKeys.Licensing_Database_ConnectionString);
            });

            builder.Services.AddModules();

            builder.Services.AddTransient(typeof(IRequestExceptionHandler<,,>), typeof(GeneralRequestExceptionHandler<,,>));
        }
    }
}

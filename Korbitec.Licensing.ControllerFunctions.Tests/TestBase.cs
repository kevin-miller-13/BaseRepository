using Korbitec.Licensing.Application.Queries.ValidateKeyDeactivation.InfrastructureContracts;
using Korbitec.Licensing.Common;
using Korbitec.Licensing.FunctionApplication;
using Korbitec.Licensing.FunctionApplication.ConfigureServices;
using Korbitec.Licensing.FunctionApplication.ControllerFunctions;
using Korbitec.Licensing.Persistence;
using Korbitec.Licensing.Persistence.EntityFrameworkCore;
using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace Korbitec.Licensing.ControllerFunctions.Tests
{
    public class TestBase
    {
        public class HostFixture : IDisposable
        {
            public readonly DbTools.DbContext DbContext;

            //public readonly GetFirmName GetFirmName;
            //public readonly GetFirmNameList GetFirmNameList;
            //public readonly GetServerId GetServerId;
            public readonly ValidateKeyDeactivation ValidateKeyDeactivation;

            public HostFixture()
            {
                var startup = new Startup();

                var host = new HostBuilder()
                    .ConfigureHostConfiguration(TestConfiguration)
                    .ConfigureWebJobs(startup.Configure)
                    .ConfigureServices(services =>
                    {
                        services.AddScoped<LicensingContext>();
                        services.AddScoped<IValidateKeyDeactivationRepository, ValidateKeyDeactivationRepository>();

                        var requestExceptionHandler = services.First(x => x.ServiceType == typeof(IRequestExceptionHandler<,,>));
                        services.Remove(requestExceptionHandler);
                    })
                    .Build();

                var services = host.Services;

                var licensingContext = services.GetService<LicensingContext>();
                DbContext = new DbTools.DbContext(licensingContext.Database.GetDbConnection());

                var mediator = services.GetRequiredService<IMediator>();
                //GetFirmName = new GetFirmName(mediator);
                //GetFirmNameList = new GetFirmNameList(mediator);
                //GetServerId = new GetServerId(mediator);
                ValidateKeyDeactivation = new ValidateKeyDeactivation(mediator);
            }

            public void Dispose()
            {
                // Cleanup
                DbContext?.Dispose();
            }

            private void TestConfiguration(IConfigurationBuilder builder)
            {
                //Settings found in AppSettings

                Environment.SetEnvironmentVariable(SettingKeys.Debug_Managed_Identity_Tenant_Id, "f35b5e26-c0b8-4234-91ea-880038dee0b7");

                Environment.SetEnvironmentVariable(SettingKeys.Licensing_Vault_URI, "https://kv-acllicensing-dev.vault.azure.net/");

                builder.LoadCloudSettings();
            }
        }
    }
}

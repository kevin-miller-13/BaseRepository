using Korbitec.Licensing.ControllerFunctions.Tests.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using static Korbitec.Licensing.ControllerFunctions.Tests.TestBase;

namespace Korbitec.Licensing.ControllerFunctions.Tests.Helpers
{
    public static class DbHelpers
    {
        public static async Task<LicensingServer[]> GenerateAndInsertLicensingServerRecordsAsync(HostFixture fixture, byte licensingServerStatus = 1, int recordsToGenerate = 1)
        {
            const string FirmName = "Integration Test Firm";
            const string ServerName = "Integration Test Server";
            const string Username = "Username";
            const string Password = "Password";
            const string EmailAddress = "email@address.com";

            var random = new Random(DateTime.Now.Millisecond);

            LicensingServer[] licensingServers = Enumerable.Range(0, recordsToGenerate).Select(_ =>
            {
                int rnd = random.Next(1000) + 1;

                string firmName = $"{FirmName} {rnd}";
                string serverName = $"{ServerName} {rnd}";
                string username = $"{Username} {rnd}";

                return new LicensingServer(firmName, serverName, username, Password, EmailAddress, licensingServerStatus);

            }).ToArray();

            var licensingServersWithIds = await fixture.DbContext.InsertRecordsReturnIdentitiesAsync<LicensingServer, int>(licensingServers);

            // Update ServerIDs
            foreach (LicensingServer ls in licensingServers)
                ls.ServerId = licensingServersWithIds[ls];

            return licensingServers;
        }

        public static async Task<ActivationCode[]> GenerateAndInsertActivationCodeRecordsAsync(HostFixture fixture, int recordsToGenerate = 1)
        {
            const string ActivationCodeTemplate = "THIS-ISAN-INTE-GRAT-IONT-EST-{0}";

            var random = new Random(DateTime.Now.Millisecond);

            ActivationCode[] activationCodes = Enumerable.Range(0, recordsToGenerate).Select(_ =>
            {
                int rnd = random.Next(9999);

                return new ActivationCode
                {
                    Code = string.Format(ActivationCodeTemplate, rnd.ToString("D4"))
                };

            }).ToArray();

            var activationCodesWithIds = await fixture.DbContext.InsertRecordsReturnIdentitiesAsync<ActivationCode, int>(activationCodes);

            // Update ActivationCodeIDs
            foreach (ActivationCode av in activationCodes)
                av.ActivationCodeId = activationCodesWithIds[av];

            return activationCodes;
        }

        public static async Task<int> InsertLicensingServerActivationCodeRecordsAsync(HostFixture fixture, params LicensingServerActivationCode[] licensingServerActivationCodes)
        {
            return await fixture.DbContext.InsertRecordsAsync(licensingServerActivationCodes);
        }

        public static async Task DeleteLicensingServerRecordsAsync(HostFixture fixture, params LicensingServer[] licensingServers)
        {
            if (licensingServers == null)
                return;

            foreach (LicensingServer server in licensingServers)
            {
                int serverId = server.ServerId;
                var serverConfiguration = new LicensingServerConfiguration(serverId);

                await fixture.DbContext.DeleteRecordAsync(serverConfiguration);
                await fixture.DbContext.DeleteRecordAsync(server);
            }
        }

        public static async Task DeleteLicensingServerActivationCodeRecordsAsync(HostFixture fixture, params LicensingServerActivationCode[] licensingServerActivationCodes)
        {
            if (licensingServerActivationCodes == null)
                return;

            foreach (LicensingServerActivationCode lsActivationCode in licensingServerActivationCodes)
            {
                await fixture.DbContext.DeleteRecordAsync(lsActivationCode);

                await fixture.DbContext.DeleteRecordAsync(new ActivationCode
                {
                    ActivationCodeId = lsActivationCode.ActivationCodeId
                });
            }
        }
    }
}

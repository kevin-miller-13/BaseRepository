using System;
using Microsoft.Extensions.Configuration;
using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Korbitec.Licensing.Common;

namespace Korbitec.Licensing.FunctionApplication.ConfigureServices
{
    public static class CloudSettingsExtension
    {
        public static IConfigurationBuilder LoadCloudSettings(this IConfigurationBuilder builder)
        {
            var credential = GetDefaultAzureCredential();

            GetVaultSecrets(credential);

            return builder;
        }

        private static DefaultAzureCredential GetDefaultAzureCredential()
        {

#if DEBUG
            //Notes: VS User requires "App Configuration Data Reader" role to run this FunctionApp in debug and fetch settings from AppConfig
            var credOptions = new DefaultAzureCredentialOptions();

            var debugTenantId = Environment.GetEnvironmentVariable(SettingKeys.Debug_Managed_Identity_Tenant_Id);

            if (debugTenantId == null)
                throw new InvalidOperationException("Debug tenant Id not found");

            credOptions.VisualStudioTenantId = debugTenantId;
            credOptions.SharedTokenCacheTenantId = debugTenantId;

            return new DefaultAzureCredential(credOptions);
#else
            return new DefaultAzureCredential();
#endif
        }

        private static void GetVaultSecrets(DefaultAzureCredential credential)
        {
            var uri = Environment.GetEnvironmentVariable(SettingKeys.Licensing_Vault_URI);

            if (uri == null)
                throw new InvalidOperationException("Vault uri not found");

            var secretClient = new SecretClient(new Uri(uri), credential);

            //Load Licensing secrets

            LoadSecretSettingIntoEnvironment(secretClient, FunctionVaultSecretKeys.Licensing_Storage_Connection, isConnectionSettingPath: true);

#if DEBUG
            Environment.SetEnvironmentVariable("AzureWebJobsStorage".GetConnectionSettingPath(), Environment.GetEnvironmentVariable(FunctionVaultSecretKeys.Licensing_Storage_Connection));
#endif
        }

        private static void LoadSecretSettingIntoEnvironment(SecretClient client, string key, bool isConnectionSettingPath = false)
        {
            var secretGetServerIdUri = client.GetSecret(key);

            if (secretGetServerIdUri == null)
                throw new InvalidOperationException("Secret not found");

            Environment.SetEnvironmentVariable(key, secretGetServerIdUri.Value.Value);

            //Also store connectionSettingPath for Table client
            if (isConnectionSettingPath)
                Environment.SetEnvironmentVariable(key.GetConnectionSettingPath(), secretGetServerIdUri.Value.Value);
        }
    }
}

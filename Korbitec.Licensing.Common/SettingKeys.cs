using System;

namespace Korbitec.Licensing.Common

{
    public class SettingKeys
    {
        public const string Licensing_Vault_URI = "LicensingVaultUri";
        public const string Debug_Managed_Identity_Tenant_Id = "Debug_Managed_Identity_Tenant_Id";

        public const string LicensingLogException_Table_Name = "licensingtablelogexception";

        public const string Licensing_Database_ConnectionString = "LicensingDatabaseConnectionString";
    }

    public class FunctionVaultSecretKeys
    {
        public const string Licensing_Storage_Connection = "LicensingStorageConnectionString";
    }

    public static class SettingsListExtensions
    {
        public static string GetConnectionSettingPath(this string key)
        {
            if (key == null)
                throw new InvalidOperationException("Key empty");

            return $"ConnectionStrings:{key}";
        }
    }
}

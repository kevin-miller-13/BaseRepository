using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.WebJobs;

namespace Korbitec.Licensing.Common.Persistence.TableStorage
{
    public class TableOperations
    {
        public static string GetUtcTimeKey(DateTime date)
        {
            var dateTime = date.ToUniversalTime();

            return dateTime.Ticks.ToString();
        }

        public static async Task Operation_InsertAsync(IBinder binder, ITableEntity entity, string tableName)
        {

            var operation = TableOperation.Insert(entity);

            var table = await binder.BindAsync<CloudTable>(new TableAttribute(tableName) { Connection = FunctionVaultSecretKeys.Licensing_Storage_Connection });

            await table.ExecuteAsync(operation);
        }
    }
}

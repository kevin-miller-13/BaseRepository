using Korbitec.Common.Persistence.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Korbitec.Licensing.Common.Persistence.EntityFrameworkCore
{
    public abstract class BaseDbContext : DbContext
    {
        public BaseDbContext(IOptions<DatabaseSettings> settings)
            : base(BuildEntityFrameworkOptions(settings.Value))
        {
        }

        private static DbContextOptions<DbContext> BuildEntityFrameworkOptions(DatabaseSettings settings)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbContext>();

            optionsBuilder.UseSqlServer(new SqlConnection(settings.DBConnectionString));

            return optionsBuilder.Options;
        }
    }
}

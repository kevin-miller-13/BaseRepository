using Korbitec.Common.Persistence.Configuration;
using Korbitec.Licensing.Common.Persistence.EntityFrameworkCore;
using Korbitec.Licensing.Persistence.EntityFrameworkCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Korbitec.Licensing.Persistence.EntityFrameworkCore
{
  
    public class LicensingContext : BaseDbContext
    {
        public LicensingContext(IOptions<DatabaseSettings> options)
          : base(options)
        {
        }

        public DbSet<ActivationCode> ActivationCode { get; set; }
        public DbSet<LicensingServerActivationCode> LicensingServerActivationCode { get; set; }
        public DbSet<LicensingServer> LicensingServer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActivationCode>()
                .HasKey(c => new { c.Id });

            modelBuilder.Entity<LicensingServerActivationCode>()
                .HasKey(c => new { c.ServerId, c.ActivationCodeId });

            modelBuilder.Entity<LicensingServer>()
                .HasKey(c => new { c.Id });
        }
    }
}

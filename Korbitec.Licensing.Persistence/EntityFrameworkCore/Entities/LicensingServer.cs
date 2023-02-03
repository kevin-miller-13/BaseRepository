using System.ComponentModel.DataAnnotations.Schema;

namespace Korbitec.Licensing.Persistence.EntityFrameworkCore.Entities
{
    [Table("LicensingServers")]
    public class LicensingServer
    {
        [Column("ServerID")]
        public int Id { get; set; }
        public byte Status { get; set; }
        public bool Deleted { get; set; }
        public string FirmName { get; set; }
    }
}

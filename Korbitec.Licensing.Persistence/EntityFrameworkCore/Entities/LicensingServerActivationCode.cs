using System.ComponentModel.DataAnnotations.Schema;

namespace Korbitec.Licensing.Persistence.EntityFrameworkCore.Entities
{
    [Table("LicensingServerActivationCodes")]
    public class LicensingServerActivationCode
    {
        [Column("ActivationCodeID")]
        public int ActivationCodeId { get; set; }
        [Column("ServerID")]
        public int ServerId { get; set; }

        public LicensingServer LicensingServer { get; set; }
    }
}

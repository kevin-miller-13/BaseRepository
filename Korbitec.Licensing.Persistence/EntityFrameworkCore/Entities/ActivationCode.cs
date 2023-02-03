using System.ComponentModel.DataAnnotations.Schema;

namespace Korbitec.Licensing.Persistence.EntityFrameworkCore.Entities
{
    [Table("ActivationCodes")]
    public class ActivationCode
    {
        [Column("ActivationCodeID")]
        public int Id { get; set; }
        [Column("ActivationCode")]
        public string Code { get; set; }

        public LicensingServerActivationCode LicensingServerActivationCode { get; set; }
    }
}

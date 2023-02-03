using Korbitec.Licensing.ControllerFunctions.Tests.DbTools;

namespace Korbitec.Licensing.ControllerFunctions.Tests.Models
{
    public class ActivationCode : ModelBase
    {
        public int ActivationCodeId { get; set; }
        public string Code { get; set; }

        public override string InsertQuery =>
            "INSERT INTO ActivationCodes(ActivationCode) VALUES (@Code)";

        public override string DeleteQuery =>
            "DELETE FROM ActivationCodes WHERE ActivationCodeID = @ActivationCodeId";
    }
}

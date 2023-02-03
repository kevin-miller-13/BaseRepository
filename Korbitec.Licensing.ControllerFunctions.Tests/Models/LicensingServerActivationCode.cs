using Korbitec.Licensing.ControllerFunctions.Tests.DbTools;

namespace Korbitec.Licensing.ControllerFunctions.Tests.Models
{
    public class LicensingServerActivationCode : ModelBase
    {
        public int ActivationCodeId { get; set; }
        public int ServerId { get; set; }

        public override string InsertQuery =>
            "INSERT INTO LicensingServerActivationCodes(ActivationCodeID, ServerID) " +
            "VALUES (@ActivationCodeId, @ServerId)";

        public override string DeleteQuery =>
            "DELETE FROM LicensingServerActivationCodes WHERE ActivationCodeID = @ActivationCodeId AND ServerID = @ServerId";

        public LicensingServerActivationCode(int activationCodeId, int serverId)
        {
            ActivationCodeId = activationCodeId;
            ServerId = serverId;
        }
    }
}

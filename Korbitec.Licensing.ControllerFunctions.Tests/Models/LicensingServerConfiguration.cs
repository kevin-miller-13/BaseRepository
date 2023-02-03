using Korbitec.Licensing.ControllerFunctions.Tests.DbTools;

namespace Korbitec.Licensing.ControllerFunctions.Tests.Models
{
    public class LicensingServerConfiguration : ModelBase
    {
        public int ServerId { get; }

        public override string DeleteQuery => "DELETE FROM LicensingServerConfiguration WHERE ServerID = @ServerId";

        public LicensingServerConfiguration(int serverId)
        {
            ServerId = serverId;
        }
    }
}

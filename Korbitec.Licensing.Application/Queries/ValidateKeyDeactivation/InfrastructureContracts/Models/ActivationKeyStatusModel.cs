using Korbitec.Licensing.Enums;
using System;

namespace Korbitec.Licensing.Application.Queries.ValidateKeyDeactivation.InfrastructureContracts.Models
{
    public class ActivationKeyStatusModel
    {
        public byte ServerStatus { get; set; }
        public bool ServerDeleted { get; set; }

        public LicensingServerStatus LicensingServerStatus()
        {
            var status = Convert.ToInt32(ServerStatus);

            return Enum.IsDefined(typeof(LicensingServerStatus), status)
                ? (LicensingServerStatus) status
                : Enums.LicensingServerStatus.Unknown;
        }
    }
}

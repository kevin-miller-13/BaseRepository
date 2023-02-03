using Korbitec.Licensing.ControllerFunctions.Tests.DbTools;

namespace Korbitec.Licensing.ControllerFunctions.Tests.Models
{
    public class LicensingServer : ModelBase
    {
        public int ServerId { get; set; }
        public string FirmName { get; }
        public string ServerName { get; }
        public string UserName { get; }
        public string Password { get; }
        public string EmailAddress { get; }
        public byte Status { get; }
        public int SubmissionInterval { get; }
        public int Synchronise { get; }
        public bool Deleted { get; }

        public override string InsertQuery =>
            "INSERT INTO LicensingServers(FirmName, ServerName, UserName, Password, EmailAddress, Status, SubmissionInterval, Synchronise, Deleted) " +
            "VALUES (@FirmName, @ServerName, @UserName, @Password, @EmailAddress, @Status, @SubmissionInterval, @Synchronise, @Deleted)";

        public override string DeleteQuery => "DELETE FROM LicensingServers WHERE ServerID = @ServerId";

        public LicensingServer(string firmName, string serverName, string userName, string password, string emailAddress, byte status = 1, int submissionInterval = 60, int synchronise = 0, bool deleted = false)
        {
            FirmName = firmName;
            ServerName = serverName;
            UserName = userName;
            Password = password;
            EmailAddress = emailAddress;
            Status = status;
            SubmissionInterval = submissionInterval;
            Synchronise = synchronise;
            Deleted = deleted;
        }
    }
}

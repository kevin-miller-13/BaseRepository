using System;

namespace Korbitec.Licensing.ControllerFunctions.Tests.DbTools
{
    public abstract class ModelBase
    {
        public virtual string SelectAllQuery => throw new NotImplementedException();

        public virtual string InsertQuery => throw new NotImplementedException();

        public virtual string UpdateQuery => throw new NotImplementedException();

        public virtual string DeleteQuery => throw new NotImplementedException();

        public virtual string ReturnIdentityQuery => "SELECT CAST(SCOPE_IDENTITY() AS INT)";
    }
}

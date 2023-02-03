using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace Korbitec.Licensing.ControllerFunctions.Tests.DbTools
{
    public class DbContext : IDisposable
    {
        private SqlConnection _sqlConnection;

        public DbContext(DbConnection dbConnection)
        {
            _sqlConnection = (SqlConnection)dbConnection;
            _sqlConnection.Open();
        }

        public Task<IEnumerable<T>> GetAllRecordsAsync<T>() where T : ModelBase, new() =>
            _sqlConnection.QueryAsync<T>(new T().SelectAllQuery);

        public Task<int> InsertRecordAsync<T>(T entity) where T : ModelBase =>
            _sqlConnection.ExecuteAsync(entity.InsertQuery, entity);

        public async Task<int> InsertRecordsAsync<T>(IEnumerable<T> entities) where T : ModelBase
        {
            int totalInserted = 0;

            foreach (T entity in entities)
                totalInserted += await InsertRecordAsync(entity);

            return totalInserted;
        }

        public Task<IdentityType> InsertRecordReturnIdentityAsync<T,IdentityType>(T entity) where T : ModelBase =>
            _sqlConnection.QuerySingleAsync<IdentityType>(entity.InsertQuery + ";" + entity.ReturnIdentityQuery, entity);

        public async Task<Dictionary<T,IdentityType>> InsertRecordsReturnIdentitiesAsync<T,IdentityType>(IEnumerable<T> entities) where T : ModelBase
        {
            var entitiesWithIdentities = new Dictionary<T,IdentityType>();

            foreach (T entity in entities)
            {
                IdentityType identity = await InsertRecordReturnIdentityAsync<T,IdentityType>(entity);
                entitiesWithIdentities.Add(entity, identity);
            }

            return entitiesWithIdentities;
        }

        public Task<int> UpdateRecordAsync<T>(T entity) where T : ModelBase =>
            _sqlConnection.ExecuteAsync(entity.UpdateQuery, entity);

        public Task<int> DeleteRecordAsync<T>(T entity) where T : ModelBase =>
            _sqlConnection.ExecuteAsync(entity.DeleteQuery, entity);

        public Task<int> ExecuteAsync(string sql, object param = null) =>
            _sqlConnection.ExecuteAsync(sql, param);

        #region IDisposable Members

        private bool _isDisposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _sqlConnection.Close();
                    _sqlConnection.Dispose();
                    _sqlConnection = null;
                }

                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Members
    }
}

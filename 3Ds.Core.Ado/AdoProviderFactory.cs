using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace _3Ds.Core.Ado
{
    public class AdoProviderFactory : DbProviderFactory
    {

        private DbProviderFactory _dbProviderFactory;
        private string _connectionString;
        

        public AdoProviderFactory(string connectionString, string invariant)        
        {
            _connectionString = connectionString;
            _dbProviderFactory = DbProviderFactories.GetFactory(invariant);
        }        

        public override DbCommand CreateCommand()
        {
            return _dbProviderFactory.CreateCommand();
        }

        public override DbCommandBuilder CreateCommandBuilder()
        {
            return _dbProviderFactory.CreateCommandBuilder();
        }

        public override DbConnection CreateConnection()
        {
            var connection = _dbProviderFactory.CreateConnection();
            connection.ConnectionString = _connectionString;
            return connection;
        }

        public override DbConnectionStringBuilder CreateConnectionStringBuilder()
        {
            return _dbProviderFactory.CreateConnectionStringBuilder();
        }

        public override DbDataAdapter CreateDataAdapter()
        {
            return _dbProviderFactory.CreateDataAdapter();
        }

        public override DbDataSourceEnumerator CreateDataSourceEnumerator()
        {
            return _dbProviderFactory.CreateDataSourceEnumerator();
        }

        public override DbParameter CreateParameter()
        {
            return _dbProviderFactory.CreateParameter();
        }

        public override CodeAccessPermission CreatePermission(PermissionState state)
        {
            return _dbProviderFactory.CreatePermission(state);
        }

        public override bool Equals(object obj)
        {
            return _dbProviderFactory.Equals(obj);
        }

        public override int GetHashCode()
        {
            return _dbProviderFactory.GetHashCode();
        }

        public override string ToString()
        {
            return _dbProviderFactory.ToString();
        }

        public override bool CanCreateDataSourceEnumerator
        {
            get
            {
                return _dbProviderFactory.CanCreateDataSourceEnumerator;
            }
        }

        public static void RegisterDbProvider(string invariant, string description, string name, string type)
        {
            DataSet ds = ConfigurationManager.GetSection("system.data") as DataSet;
            var table = ds.Tables[0];
            var row = table.Select(String.Format("InvariantName = '{0}'", invariant)).SingleOrDefault();
            if (row == null)
            {
                table.Rows.Add(name, description, invariant, type);
            }
            else
            {
                row["invariant"] = invariant;
                row["description"] = description;
                row["name"] = name;
                row["type"] = name;
            }
        }

    }

}


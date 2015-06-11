using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace _3Ds.Core.Ado
{
    public class AdoProviderFactory : DbProviderFactory
    {

        private DbProviderFactory _dbProviderFactory;

        public AdoProviderFactory(string connectionString, string invariant)
        {
            _dbProviderFactory = DbProviderFactories.GetFactory(invariant);
        }

        public static void RegisterDbProvider(string invariant, string description, string name, string type)
        {

            DataSet ds = ConfigurationManager.GetSection("system.data") as DataSet;
            var table = ds.Tables[0];
            var row = table.Select("InvariantName = " + invariant).SingleOrDefault();
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


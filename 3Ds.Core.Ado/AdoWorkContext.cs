using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace _3Ds.Core.Ado
{
    public class AdoWorkContext : IWorkContext
    {

        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public Guid Id
        {
            get { throw new NotImplementedException(); }
        }
        
        public DbConnection OpenConnection()
        {
            throw new NotImplementedException();
        }

        internal void CloseConnection(DbConnection connection)
        {
            throw new NotImplementedException();
        }

        
    }
}

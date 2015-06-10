using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _3Ds.Utils.AutoType;
using System.Data.Common;
using Dapper;

namespace _3Ds.Core.Ado
{
    public class AdoDefaultRepository<T> : IRepository<T>, IAsyncRepository<T>
        where T: class, IEntity, new()
    {
        private AdoWorkContext _context;
        private AutoTypeInspector<T> _typeInspector;

        /// <summary>
        /// The default repository that assumes that the entity is stored in a table 
        /// with the same name as the class. It will try to map all values that 
        /// can be mapped.       
        /// </summary>
        /// <param name="uow"></param>
        public AdoDefaultRepository(AdoWorkContext context)
        {
            this._context = context;
            this._typeInspector = new AutoTypeInspector<T>();
        }

        public IEnumerable<T> All()
        {
            DbConnection connection;
            var result = new List<T>();
            try
            {
                var all = "select * from {0}";               
                connection = _context.OpenConnection();
                result = connection.Query<T>(String.Format(all, _typeInspector.TypeName)).ToList();
            }
            finally
            {
                _context.CloseConnection(connection);
            }
            return result;
        }

        public T Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public T Find(Specification spec)
        {
            throw new NotImplementedException();
        }

        public void Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }
    }
}

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
        private AdoUoW _uow;
        private AutoTypeInspector<T> _typeInspector;

        /// <summary>
        /// The default repository that assumes that the entity is stored in a table 
        /// with the same name as the class. It will try to map all values that 
        /// can be mapped.       
        /// </summary>
        /// <param name="uow"></param>
        public AdoDefaultRepository(AdoUoW context)
        {
            this._uow = context;
            this._typeInspector = new AutoTypeInspector<T>();
        }

        public virtual IEnumerable<T> All()
        {
            DbConnection connection = null;
            var result = new List<T>();
            try
            {
                var all = "select * from {0}";               
                connection = _uow.OpenConnection();
                result = connection.Query<T>(String.Format(all, _typeInspector.TypeName)).ToList();
            }
            finally
            {
                _uow.CloseConnection();
            }
            return result;
        }

        public IEnumerable<T> Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Find(Specification spec)
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

        public IEnumerable<T> AsyncAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> AsyncFind(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> AsyncFind(Specification spec)
        {
            throw new NotImplementedException();
        }

        public void AsyncInsert(T entity)
        {
            throw new NotImplementedException();
        }

        public void AsyncDelete(T entity)
        {
            throw new NotImplementedException();
        }
    }
}

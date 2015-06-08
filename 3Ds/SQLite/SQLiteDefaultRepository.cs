using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3Ds.Core.SQLite
{
    public class SQLiteDefaultRepository<T> : IRepository<T>
        where T: IEntity
    {
        private SQLiteUoW _uow;

        /// <summary>
        /// The default repository assumes that the entity is stored in a table with the same name as the class. That all values can be mapped and so on.
        ///
        /// </summary>
        /// <param name="uow"></param>
        public SQLiteDefaultRepository(SQLiteUoW uow)
        {
            // TODO: Complete member initialization
            this._uow = uow;
        }

        public IEnumerable<T> All()
        {
            throw new NotImplementedException();
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

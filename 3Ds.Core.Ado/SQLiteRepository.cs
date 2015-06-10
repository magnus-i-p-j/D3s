using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3Ds.Core.SQLite
{
    
    public class SQLiteRepository<T> : IRepository<T> where T: class, IEntity
    {

        private SQLiteUoW _uow;

        public SQLiteRepository(SQLiteUoW uow)
        {
            _uow = uow;
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

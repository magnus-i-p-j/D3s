using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3Ds.Core
{
    interface IAsyncRepository<T>
        where T : class, IEntity
    {
        IEnumerable<T> AsyncAll();
        T AsyncFind(Guid id);
        T AsyncFind(Specification spec);
        void AsyncInsert(T entity);
        void AsyncDelete(T entity);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3Ds.Core
{
    public interface IAsyncRepository<T>
        where T : class, IEntity
    {
        IEnumerable<T> AsyncAll();
        IEnumerable<T> AsyncFind(Guid id);
        IEnumerable<T> AsyncFind(Specification spec);
        void AsyncInsert(T entity);
        void AsyncDelete(T entity);
    }
}

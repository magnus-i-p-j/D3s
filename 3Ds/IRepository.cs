using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3Ds.Core
{  
    public interface IRepository<T>
        where T: class, IEntity
    {
        IEnumerable<T> All();
        EntityView<T> Find(Guid id);
        EntityView<T> Find(Specification spec);
        void Insert(T entity);
        void Delete(T entity);        
    }
}

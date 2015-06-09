using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3Ds.Core
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : class, IEntity;
        IEnumerable<Type> KnownTypes { get; }        
        void AcceptChanges();           
    }    
}

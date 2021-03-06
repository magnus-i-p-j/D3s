﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3Ds.Core
{  
    public interface IRepository<T>
        where T: class, IEntity
    {
        IEnumerable<T> All();
        T Find(Guid id);
        IEnumerable<T> Find(Specification<T> spec);
        void Insert(T entity);
        void Delete(T entity);        
    }
}

﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3Ds.Core
{
    public class EntityView<T> : IEnumerable<T>, INotifyPropertyChanged, INotifyCollectionChanged
        where T : class, IEntity
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public EntityView(Specification<T> set)
        {
            throw new NotImplementedException();
        }        

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

    }
}

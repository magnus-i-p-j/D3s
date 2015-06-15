using _3Ds.Utils.AutoType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3Ds.Core
{
    public class Specification<T> where T: class
    {
        private AutoTypeInspector<T> _typeInspector;

        public Specification()
        {
            this._typeInspector = new AutoTypeInspector<T>();
        }

        public Func<TProperty, bool> this<TProperty>[string name]
        {
            get;
            set;
        }


    }
    
}

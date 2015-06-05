using _3Ds.Utils;
using _3Ds.Utils.AutoType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3Ds.Core
{
    public abstract class ValueObject<T> : ICloneable, IEquatable<ValueObject<T>>
        where T : ValueObject<T>
    {

        protected AutoTypeInspector<T> _inspector;

        public ValueObject()
        {
            _inspector = new AutoTypeInspector<T>();
        }
               
        public object Clone()
        {
            var clone = _inspector.CreateInstance();            
            foreach (var property in _inspector.Properties)
            {               
                var getter = property.Getter;
                var value = getter((T)this);
                var setter = property.Setter;
                setter(clone, value);
            }
            return clone;
        }
       
        public bool Equals(ValueObject<T> other)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}

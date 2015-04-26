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
            var clone = (T)Activator.CreateInstance(typeof(T));            
            foreach (var property in _inspector.PropertyDefinitions)
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
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

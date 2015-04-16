using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3Ds.Core
{
    public abstract class ValueObject : ICloneable, IComparable, IEquatable<ValueObject>
    {
        
        public ValueObject()
        {
        
        }

               

        public object Clone()
        {
            throw new NotImplementedException();
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public bool Equals(ValueObject other)
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

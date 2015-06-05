using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3Ds.Core
{
    [Serializable]
    public class UnitOfWorkException : Exception
    {
        public UnitOfWorkException() { }
        public UnitOfWorkException(string message) : base(message) { }
        public UnitOfWorkException(string message, Exception inner) : base(message, inner) { }
        protected UnitOfWorkException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}

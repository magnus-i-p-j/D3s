using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3Ds.Core
{
    [Serializable]
    public class NoRepositoryFoundException : Exception
    {

        private Type _type;

        public NoRepositoryFoundException(Type type) 
        {
            _type = type;
        }

        public Type Type { get; set; }

        public NoRepositoryFoundException(Type type, string message) : base(message) { _type = type; }
        public NoRepositoryFoundException(Type type, string message, Exception inner) : base(message, inner) { _type = type; }
        protected NoRepositoryFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3Ds.Utils.AutoType
{
    [Serializable]
    public class AutoTypeInspectorException : Exception
    {
        public AutoTypeInspectorException() { }
        public AutoTypeInspectorException(string message) : base(message) { }
        public AutoTypeInspectorException(string message, Exception inner) : base(message, inner) { }
        protected AutoTypeInspectorException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}

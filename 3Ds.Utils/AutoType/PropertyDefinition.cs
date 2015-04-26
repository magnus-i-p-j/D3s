using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3Ds.Utils.AutoType
{
    public class PropertyDefinition<TInstance>
    {        
        public string Name { get; internal set; }
        public Type ValueType { get; internal set; }
        public Func<TInstance, object> Getter { get; internal set; }
        public Action<TInstance, object> Setter { get; internal set; }
        public Func<object, object, bool> EqualTo { get; internal set; }
    }
}

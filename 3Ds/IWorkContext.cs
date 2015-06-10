using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3Ds.Core
{
    public interface IWorkContext
    {
        public string Name { get; }
        public Guid Id { get; }
    }
}

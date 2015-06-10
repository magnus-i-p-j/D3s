using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3Ds.Core
{
    public interface IWorkContext
    {
        string Name { get; }
        Guid Id { get; }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3Ds.Core
{
    public class EntitySet<T> : IEnumerable<T>
        where T: IEntity
    {

        public DateTime TimeStamp { get; set; }
        public Guid Id { get; set; }        

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}

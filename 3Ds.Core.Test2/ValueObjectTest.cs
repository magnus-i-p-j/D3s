using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3Ds.Core.Test2
{
    [TestFixture]
    public class ValueObjectTest
    {

        public class AValueObject : ValueObject<AValueObject>
        {
            public int IntProperty { get; set; }
            public string StringProperty { get; set; }
        }

        [Test]
        public void Should_clone()
        {
            var valueObject = new AValueObject()
            {
                IntProperty = 1,
                StringProperty = "A string"
            };
            var clone = (AValueObject) valueObject.Clone();
            Assert.That(clone, Is.InstanceOf<AValueObject>());            
            Assert.That(clone.IntProperty, Is.EqualTo(valueObject.IntProperty));
            Assert.That(clone.StringProperty, Is.EqualTo(valueObject.StringProperty));
        }

    }
}

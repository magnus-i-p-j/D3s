using _3Ds.Utils;
using _3Ds.Utils.AutoType;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3Ds.Utils.Test
{
    [TestFixture]
    public class AutoTypeInspectorTest
    {

        public class AClass
        {
            public int IntProperty { get; set; }
            public bool BoolProperty { get; set; }
            public string StringProperty { get; set; }
        }

        [Test]
        public void Should_extract_all_getters()
        {
            var inspector = new AutoTypeInspector<AClass>();
            Assert.That(inspector.Getter("IntProperty"), Is.Not.Null);
            Assert.That(inspector.Getter("BoolProperty"), Is.Not.Null);
            Assert.That(inspector.Getter("StringProperty"), Is.Not.Null);
        }

        [Test]
        public void Should_extract_all_setters()
        {
            var inspector = new AutoTypeInspector<AClass>();
            Assert.That(inspector.Setter("IntProperty"), Is.Not.Null);
            Assert.That(inspector.Setter("BoolProperty"), Is.Not.Null);
            Assert.That(inspector.Setter("StringProperty"), Is.Not.Null);
        }

        [Test]
        public void Should_create_a_new_instance()
        {
            var inspector = new AutoTypeInspector<AClass>();
            var clone = inspector.CreateInstance();
            Assert.That(clone, Is.InstanceOf<AClass>());
        }



    }
}

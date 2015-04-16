using _3Ds.Utils;
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
            Assert.That(inspector.GetGetter<int>("IntProperty"), Is.Not.Null);
            Assert.That(inspector.GetGetter<bool>("BoolProperty"), Is.Not.Null);
            Assert.That(inspector.GetGetter<string>("StringProperty"), Is.Not.Null);
        }

    }
}

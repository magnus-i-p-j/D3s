using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3Ds.Core.Test2
{
    [TestFixture]
    public class SpecificationTest
    {

        public class MockEntity : IEntity
        {
            public Guid Id { get; set; }
            public int AnInt { get; set; }
            public string AString { get; set; }

            public override bool Equals(object obj)
            {
                var other = obj as IEntity;
                if (other != null)
                {
                    return Id == other.Id;
                }
                return false;
            }

        }


        [Test]
        public void Should_keep_a_list_of_specified_properties()
        {
            var spec = new Specification<MockEntity>();
            spec["AnInt"] = i => i < 10;
            Assert.That(spec.Details.Count, Is.EqualTo(0));
        }

        [Test]
        public void Should_throw_an_exception_when_specifying_unknown_property()
        {
            var spec = new Specification<MockEntity>();
            spec["AnInt"] = i => i < 10;
            Assert.That(spec.Details.Count, Is.EqualTo(0));
        }


    }
}

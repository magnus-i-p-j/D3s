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
        public void Should_call_is_satisfied_recursively()
        {
            var spec = new Specification<MockEntity>()
                .Property(e => e.AnInt).EqualTo(3)
                .And().Property(e => e.AString).LessThan("abc");

            var entity = new MockEntity
            {
                AnInt = 4,
                AString = "ddd"
            };
            Assert.That(spec.IsSatisfiedBy(entity), Is.True);
        }

         [Test]
        public void Should_call_is_satisfied_in_correct_nested_order()
        {
            var spec = new Specification<MockEntity>()
                .Property(e => e.AnInt).EqualTo(3)
                .And().Property(e => e.AString).LessThan("abc");
           
            //spec["AnInt"] = i => i < 10;
            //Assert.That(spec.Details.Count, Is.EqualTo(0));
        }


    }
}

using _3Ds.Core.Ado;
using FakeItEasy;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3Ds.Core.Test2
{
    //These test should be passed by all uows.
    [TestFixture]
    public class AdoUoWTest
    {

        public class MockEntity : IEntity
        {
            public Guid Id { get; set; }

            public bool Equals(IEntity other)
            {
                return Id == other.Id;
            }
        }


        [Test]
        [ExpectedException(typeof(NoRepositoryFoundException))]
        public void Should_throw_exception_when_cannot_find_repository()
        {
            var factory = A.Fake<AdoRepositoryFactory>();
            var uow = new AdoUoW(null, factory);
            var repo = uow.GetRepository<MockEntity>();            
        }

        [Test]
        public void Should_call_get_repository_on_factory()
        {
            var factory = new AdoRepositoryFactory();            
            factory.ConfigureRepository<MockEntity>();
            var uow = new AdoUoW(null, factory);            
            var repo = uow.GetRepository<MockEntity>();
            Assert.That(repo, Is.InstanceOf<IRepository<MockEntity>>());
        }

        [Test]
        public void Should_call_get_repository_on_factory_once()
        {
            var factory = new AdoRepositoryFactory();
            factory.ConfigureRepository<MockEntity>();
            var uow = new AdoUoW(null, factory);
            var repo_1 = uow.GetRepository<MockEntity>();
            var repo_2 = uow.GetRepository<MockEntity>();            
            Assert.That(repo_1, Is.EqualTo(repo_2));
        }

        [Test]
        public void Should_locate_repository_by_type()
        {
            Assert.That(false, Is.True);
        }

        [Test]
        public void Should_locate_repository_by_interface()
        {
            Assert.That(false, Is.True);
        }

    }
}

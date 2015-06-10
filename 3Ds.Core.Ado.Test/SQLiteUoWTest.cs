using _3Ds.Core.SQLite;
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
    public class SQLiteUoWTest
    {

        public class MockEntity : IEntity
        {

            public Guid Id { get; set; }
        }


        [Test]
        [ExpectedException(typeof(NoRepositoryFoundException))]
        public void Should_throw_exception_when_cannot_find_repository()
        {
            var factory = A.Fake<SQLiteRepositoryFactory>();
            var uow = new SQLiteUoW(null, factory);
            var repo = uow.GetRepository<MockEntity>();            
        }

        [Test]
        public void Should_call_get_repository_on_factory()
        {
            var factory = new SQLiteRepositoryFactory();            
            factory.ConfigureRepository<MockEntity>();
            var uow = new SQLiteUoW(null, factory);            
            var repo = uow.GetRepository<MockEntity>();
            Assert.That(repo, Is.InstanceOf<IRepository<MockEntity>>());
        }

        [Test]
        public void Should_call_get_repository_on_factory_once()
        {
            var factory = new SQLiteRepositoryFactory();
            factory.ConfigureRepository<MockEntity>();
            var uow = new SQLiteUoW(null, factory);
            var repo_1 = uow.GetRepository<MockEntity>();
            var repo_2 = uow.GetRepository<MockEntity>();            
            Assert.That(repo_1, Is.EqualTo(repo_2));
        }
        
    }
}

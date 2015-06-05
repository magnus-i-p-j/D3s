using _3Ds.Core.SQLite;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3Ds.Core.Test2
{
    [TestFixture]
    public class SQLiteUoWTest
    {

        public class MockEntity : IEntity
        {

            public Guid Id
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }
        }

        public class MockRepository : SQLiteRepository<MockEntity>{
            public MockRepository(SQLiteUoW uow): base(uow)
            {

            }
        }
       
        [Test]
        public void Should_get_repository()
        {
            var factory = new SQLiteUoWFactory("");
            factory.ConfigureRepository<MockRepository, MockEntity>();
            var uow = factory.CreateUnitOfWork();            
            var repo = uow.GetRepository<MockEntity>();
            Assert.That(repo, Is.Not.Null);
        }

        
    }
}

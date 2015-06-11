using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3Ds.Core.Test2.SQLite
{
    [TestFixture]
    public class SQLiteDefaultRepositoryTest
    {

        public SQLiteConnection Connection { get; set; }
        public SQLiteUoW UoW { get; set; }
        public string ConnectionString { get; set; }

        public class MockEntity : IEntity
        {
            public Guid Id { get; set; }
        }

        [SetUp]
        public void SetUp()
        {
            var repositoryFactory = new SQLiteRepositoryFactory();
            repositoryFactory.ConfigureRepository<MockEntity>();
            //ConnectionString = "Data Source=:memory:";
            
            ConnectionString = "FullUri=file::memory:?cache=shared";
            Connection = new SQLiteConnection(ConnectionString);
            Connection.Open();
            UoW = new SQLiteUoW(ConnectionString, repositoryFactory);
            AddEntities();
        }

        private void AddEntities()
        {
            var table = "create table if not exists MockEntity (Id BLOB, AnInt INTEGER, AString TEXT)";
            var addTable = new SQLiteCommand
            {
                Connection = Connection,
                CommandText = table
            };
            addTable.ExecuteNonQuery();
            var entities = "insert into MockEntity values('{0}',{1},'{2}')";
            for (int i = 1; i <= 10; i++)
            {
                var insertEntity = new SQLiteCommand{
                    Connection = Connection,
                    CommandText = String.Format(entities, Guid.NewGuid(), i, i)
                };
                insertEntity.ExecuteNonQuery();
            }
        }

        [TearDown]
        public void Teardown()
        {
            Connection.Close();
        }

        [Test]
        public void Should_get_all_entities()
        {
            var repo = UoW.GetRepository<MockEntity>();
            var all = repo.All();
            Assert.That(all.Count(), Is.EqualTo(10));
        }

    }
}

using _3Ds.Core.Ado;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3Ds.Core.Test2.Ado
{
    [TestFixture]
    public class AdopDefaultRepositoryTest
    {

        public SQLiteConnection Connection { get; set; }
        public string ConnectionString { get { return "FullUri=file::memory:?cache=shared"; } }// ":memory:?cache=shared"; } }

        public AdoUoW UoW { get; set; }

        public class MockEntity : IEntity
        {
            public Guid Id { get; set; }
            public int AnInt { get; set; }
            public string AString { get; set; }
        }

        [SetUp]
        public void SetUp()
        {
            AdoProviderFactory.RegisterDbProvider(
                "System.Data.SQLite", ".Net Framework Data Provider for SQLite",
                "SQLite Data Provider",
                "System.Data.SQLite.SQLiteFactory, System.Data.SQLite");

            var providerFactory = new AdoProviderFactory(
                ConnectionString,
                "System.Data.SQLite");
            
            var repositoryFactory = new AdoRepositoryFactory();
            repositoryFactory.ConfigureRepository<MockEntity>();

            UoW = new AdoUoW(providerFactory, repositoryFactory);
            
            Connection = new SQLiteConnection(ConnectionString);
            Connection.Open();            
            AddEntities();
        }

        private void AddEntities()
        {
            var table = "create table if not exists MockEntity (Id GUID, AnInt INTEGER, AString TEXT)";
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

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
    public class AdoDefaultRepositoryTest
    {

        public SQLiteConnection Connection { get; set; }
        public string ConnectionString { get { return "FullUri=file::memory:?cache=shared"; } }// ":memory:?cache=shared"; } }

        public AdoUoW UoW { get; set; }
        public List<MockEntity> Entities { get; set; }

        public class MockEntity : IEntity
        {
            public Guid Id { get; set; }
            public int AnInt { get; set; }
            public string AString { get; set; }

            public override bool Equals(object obj)
            {
                var other = obj as IEntity;
                if(other != null){
                    return Id == other.Id;
                }
                return false;
            }

        }

        [TestFixtureSetUp]
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
            Entities = new List<MockEntity>();

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
                var entity = new MockEntity
                {
                    AnInt = i,
                    AString = i.ToString(),
                    Id = Guid.NewGuid()
                };
                Entities.Add(entity);
                var insertEntity = new SQLiteCommand{
                    Connection = Connection,
                    CommandText = String.Format(entities, entity.Id, entity.AnInt, entity.AString)
                };
                insertEntity.ExecuteNonQuery();
            }
        }

        [TestFixtureTearDown]
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
            Assert.That(all, Is.EquivalentTo(Entities));
        }

        [Test]
        public void Should_find_specific_entity()
        {
            var repo = UoW.GetRepository<MockEntity>();
            var i = 6;
            var found = repo.Find(Entities[i].Id);
            Assert.That(found, Is.EqualTo(Entities[i]));
        }

        [Test]
        public void Should_find_all_specified_entities()
        {
            var repo = UoW.GetRepository<MockEntity>();
            var spec = new Specification<MockEntity>();
            var found = repo.Find(spec);
            Assert.That(found, Is.EqualTo(Entities.Select(e => e.AnInt > 3 || e.AnInt < 7)));
        }

    }
}

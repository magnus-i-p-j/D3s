using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _3Ds.Core.SQLite
{
    public class SQLiteUoWFactory : IUnitOfWorkFactory
    {
        private string _connectionString;
        private Dictionary<Type, Type> _repositories;

        public SQLiteUoWFactory(string connectionString)
        {
            _connectionString = connectionString;
            _repositories = new Dictionary<Type, Type>();
        }

        public SQLiteUoWFactory ConfigureRepository<TRepository, TEntity>()
            where TEntity : IEntity
            where TRepository : SQLiteRepository<TEntity>
        {
            _repositories[typeof(TEntity)] = typeof(TRepository);
            return this;
        }


        public IUnitOfWork CreateUnitOfWork()
        {
            var uow = new SQLiteUoW(new SQLiteConnection(_connectionString));
            var addRepository = 
                uow.GetType().GetMethod("AddRepository", 
                BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var repo in _repositories)
            {
                addRepository.MakeGenericMethod(repo.Key)
                    .Invoke(uow, new object[]{ Activator.CreateInstance(repo.Value, uow)});
            }
            return uow;
        }
    }
}

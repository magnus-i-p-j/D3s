using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3Ds.Core.Ado
{
    public class AdoRepositoryFactory
    {

        private Dictionary<Guid, Func<AdoUoW, object>> _repositories;

        public AdoRepositoryFactory()
        {
            _repositories = new Dictionary<Guid, Func<AdoUoW, object>>();
        }

        public IEnumerable<Type> KnownTypes { get { return _repositories.Values.Select(r => r.GetType()); } }

        public void ConfigureRepository<TEntity>()
            where TEntity : class, IEntity, new()
        {
            _repositories[GetTypeKey<TEntity>()] = uow => new AdoDefaultRepository<TEntity>(uow);
        }

        public void ConfigureRepository<TEntity, TRepository>()
            where TEntity : class, IEntity, new()
            where TRepository : AdoDefaultRepository<TEntity>
        {
            _repositories[GetTypeKey<TEntity>()] =
                uow => Activator.CreateInstance(typeof(TRepository), new Object[] { uow });
        }

        public IRepository<TEntity> CreateRepository<TEntity>(AdoUoW uow)
             where TEntity : class, IEntity, new()
        {
            var key = GetTypeKey<TEntity>();
            if (!_repositories.ContainsKey(key))
            {
                throw new NoRepositoryFoundException(typeof(TEntity));
            }
            
            return (IRepository<TEntity>) _repositories[key](uow);
        }

        private Guid GetTypeKey<T>()
        {
            return typeof(T).GUID;
        }        

    }
}

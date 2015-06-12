using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _3Ds.Core.Ado
{    

    public class AdoUoW: IUnitOfWork
    {
        private AdoRepositoryFactory _repositoryFactory;
        private AdoProviderFactory _providerFactory;
        private Dictionary<Guid, object> _repositories;
        private List<Action<DbConnection>> _actions;        
        private Object thisLock;

        private DbConnection _connection;

        public AdoUoW(AdoProviderFactory providerFactory,
            AdoRepositoryFactory repositoryFactory)
        {
            Id = Guid.NewGuid();
            _providerFactory = providerFactory;
            _repositoryFactory = repositoryFactory;
            _repositories = new Dictionary<Guid, object>();
            _actions = new List<Action<DbConnection>>();
            thisLock = new Object();            
        }
        
        internal void AddAction(Action<DbConnection> action)
        {
            _actions.Add(action);
        }

        internal DbConnection OpenConnection()
        {
            CloseConnection();
            _connection = _providerFactory.CreateConnection();
            _connection.Open();
            return _connection;
        }

        internal void CloseConnection()
        {
            if (_connection != null)
            {
                _connection.Close();
            }
        }        

        public string Name
        {
            get; set;
        }

        public Guid Id
        {
            get; private set;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity, new()
        {            
            var key = GetTypeKey<TEntity>();
            if (!_repositories.ContainsKey(key))
            {
                _repositories[key] = _repositoryFactory.CreateRepository<TEntity>(this);
            }
            return (IRepository<TEntity>)_repositories[key];
        }

        private Guid GetTypeKey<T>()
        {
            return typeof(T).GUID;
        }        

        public IEnumerable<Type> KnownTypes
        {
            get { return _repositoryFactory.KnownTypes; }
        }

        public void AcceptChanges()
        {
            try
            {
                List<Action<DbConnection>> actions;
                lock (thisLock)
                {
                    actions = _actions;
                    _actions = new List<Action<DbConnection>>();
                }
            }
            catch (Exception ex)
            {
                throw new UnitOfWorkException("Unable to accept changes.", ex);
            }

        }
    }
}

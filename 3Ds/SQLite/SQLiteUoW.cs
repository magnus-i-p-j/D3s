using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3Ds.Core.SQLite
{
    public class SQLiteUoW: IUnitOfWork
    {

        private string _connectionString;        
        private SQLiteRepositoryFactory _factory;
        private Dictionary<Guid, object> _repositories;
        private List<SQLiteCommand> _commands;        
        private Object thisLock;        
       
        public SQLiteUoW(string connectionString, SQLiteRepositoryFactory factory)
        {
            _connectionString = connectionString;
            _factory = factory;
            _repositories = new Dictionary<Guid, object>();
            _commands = new List<SQLiteCommand>();
            thisLock = new Object();            
        }

        internal string ConnectionString
        {
            get
            {
                return _connectionString;
            }
        }

        internal void AddCommand(SQLiteCommand command)
        {
            _commands.Add(command);
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity
        {            
            var key = GetTypeKey<TEntity>();
            if (!_repositories.ContainsKey(key))
            {
                _repositories[key] = _factory.CreateRepository<TEntity>(this);
            }
            return (IRepository<TEntity>)_repositories[key];
        }

        private Guid GetTypeKey<T>()
        {
            return typeof(T).GUID;
        }        

        public IEnumerable<Type> KnownTypes
        {
            get { return _factory.KnownTypes; }
        }

        public void AcceptChanges()
        {
            try
            {
                List<SQLiteCommand> commands;
                lock (thisLock)
                {
                    commands = _commands;
                    _commands = new List<SQLiteCommand>();
                }
            }
            catch (Exception ex)
            {
                throw new UnitOfWorkException("Unable to accept changes.", ex);
            }

        }
    }
}

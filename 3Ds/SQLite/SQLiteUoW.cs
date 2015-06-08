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

        private SQLiteConnection _connection;
        private List<SQLiteCommand> _commands;
        private SQLiteRepositoryFactory _factory;
        private Dictionary<Type, dynamic> _repositories;

        internal SQLiteUoW(SQLiteConnection connection)
        {        
            _connection = connection;
        }

        public SQLiteUoW(SQLiteConnection connection, SQLiteRepositoryFactory factory)
        {
            _connection = connection;
            _factory = factory;
        }

        internal void AddCommand(SQLiteCommand command)
        {
            _commands.Add(command);
        }

        public IRepository<T> GetRepository<T>() where T : IEntity
        {
            return _factory.CreateRepository<T>(this);
        }

        public IEnumerable<Type> KnownTypes
        {
            get { return _factory.KnownTypes; }
        }

        public void AcceptChanges()
        {
            try
            {
            }
            catch (Exception ex)
            {
                throw new UnitOfWorkException("Unable to accept changes.", ex);
            }

        }
    }
}

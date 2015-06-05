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
        private Dictionary<Type, dynamic> _repositories;

        internal SQLiteUoW(SQLiteConnection connection)
        {        
            _connection = connection;
        }

        internal void AddRepository<T>(SQLiteRepository<T> repo)
            where T : IEntity
        {

        }

        internal void AddCommand(SQLiteCommand command)
        {
            _commands.Add(command);
        }

        public IRepository<T> GetRepository<T>() where T : IEntity
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Type> KnownTypes
        {
            get { throw new NotImplementedException(); }
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

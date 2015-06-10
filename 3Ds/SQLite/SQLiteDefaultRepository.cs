using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using _3Ds.Utils.AutoType;

namespace _3Ds.Core.SQLite
{
    public class SQLiteDefaultRepository<T> : IRepository<T>, IAsyncRepository<T>
        where T: class, IEntity, new()
    {
        private SQLiteUoW _uow;
        private AutoTypeInspector<T> _typeInspector;

        /// <summary>
        /// The default repository assumes that the entity is stored in a table 
        /// with the same name as the class. It will try to map all values that 
        /// can be mapped.       
        /// </summary>
        /// <param name="uow"></param>
        public SQLiteDefaultRepository(SQLiteUoW uow)
        {
            // TODO: Complete member initialization
            this._uow = uow;
            this._typeInspector = new AutoTypeInspector<T>();
        }

        public IEnumerable<T> All()
        {
            var connection = new SQLiteConnection(_uow.ConnectionString);
            var result = new List<T>();
            try
            {
                var all = "select * from {0}";
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = String.Format(all, _typeInspector.TypeName)
                };
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var current = _typeInspector.CreateInstance();
                        var values = reader.GetValues();
                        if(values != null){
                            foreach (var key in values.AllKeys)
                            {
                                if (_typeInspector.HasProperty(key))
                                {
                                    _typeInspector.Setter(key)(current, values[key]);
                                }
                            }
                        }
                        result.Add(current);
                    }
                }
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        public T Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public T Find(Specification spec)
        {
            throw new NotImplementedException();
        }

        public void Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }
    }
}

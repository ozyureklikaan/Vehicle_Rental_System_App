using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRentalSystem.Commons.Concretes.Data;
using VehicleRentalSystem.Commons.Concretes.Helpers;
using VehicleRentalSystem.Commons.Concretes.Logger;
using VehicleRentalSystem.DataAccess.Abstractions;
using VehicleRentalSystem.Models.Concretes;

namespace VehicleRentalSystem.DataAccess.Concretes
{
    public class PersonsRepository : IRepository<Persons>, IDisposable
    {
        private string _connectionString;
        private string _dbProviderName;
        private DbProviderFactory _dbProviderFactory;
        private int _rowsAffected, _errorCode;
        private bool _bDisposed;

        public PersonsRepository()
        {
            _connectionString = DBHelper.GetConnectionString();
            _dbProviderName = DBHelper.GetConnectionProvider();
            _dbProviderFactory = DbProviderFactories.GetFactory(_dbProviderName);
        }

        public bool DeletedById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("DELETE ");
                query.Append("FROM [dbo].[tPersons] ");
                query.Append("WHERE");
                query.Append("[personID] = @id ");
                query.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                    {
                        throw new ArgumentException("dbCommand" + "The db SelectById command for entity [tPersons] can't be null");
                    }

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                        {
                            throw new ArgumentException("dbCommand" + " The db SelectById command for entity [tPersons] can't be null.");
                        }

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        DBHelper.AddParameter(dbCommand, "@id", Types.Int, ParameterDirection.Input, id);

                        DBHelper.AddParameter(dbCommand, "@intErrorCode", Types.Int, ParameterDirection.Output, null);

                        if (dbConnection.State != ConnectionState.Open)
                        {
                            dbConnection.Open();
                        }

                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            throw new Exception("Deleting Error for entity [tPersons] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("CustomersRepository::Insert:Error occured.", ex);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool bDisposing)
        {
            if (!_bDisposed)
            {
                if (bDisposing)
                {
                    _dbProviderFactory = null;
                }

                _bDisposed = true;
            }
        }

        public bool Insert(Persons entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("INSERT [dbo].[tPersons] ");
                query.Append("([personName], [personLastName], [age], [username], [password]) ");
                query.Append("VALUES ");
                query.Append("(@PersonName, @PersonLastName, @Age, @Username, @Password) ");
                query.Append("SELECT @intErrorCode=@@ERROR;");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                    {
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");
                    }

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                        {
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tPersons] can't be null. ");
                        }

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        DBHelper.AddParameter(dbCommand, "@PersonName", Types.String, ParameterDirection.Input, entity.PersonName);
                        DBHelper.AddParameter(dbCommand, "@PersonLastName", Types.String, ParameterDirection.Input, entity.PersonLastName);
                        DBHelper.AddParameter(dbCommand, "@Age", Types.Int, ParameterDirection.Input, entity.Age);
                        DBHelper.AddParameter(dbCommand, "@Username", Types.String, ParameterDirection.Input, entity.Username);
                        DBHelper.AddParameter(dbCommand, "@Password", Types.String, ParameterDirection.Input, entity.Password);

                        DBHelper.AddParameter(dbCommand, "@intErrorCode", Types.Int, ParameterDirection.Output, null);

                        if (dbConnection.State != ConnectionState.Open)
                        {
                            dbConnection.Open();
                        }

                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            throw new Exception("Inserting Error for entity [tVehicle] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("CustomersRepository::Insert:Error occured.", ex);
            }
        }

        public IList<Persons> SelectAll()
        {
            _errorCode = 0;
            _rowsAffected = 0;

            IList<Persons> persons = new List<Persons>();

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append("[personID], [personName], [personLastName], [age], [username], [password] ");
                query.Append("FROM [dbo].[tPersons] ");
                query.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                    {
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");
                    }

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                        {
                            throw new ArgumentNullException("dbCommand" + " The db SelectById command for entity [tPersons] can't be null. ");
                        }

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        DBHelper.AddParameter(dbCommand, "@intErrorCode", Types.Int, ParameterDirection.Output, null);

                        if (dbConnection.State != ConnectionState.Open)
                        {
                            dbConnection.Open();
                        }

                        using (var reader = dbCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var entity = new Persons();
                                    entity.PersonID = reader.GetInt32(0);
                                    entity.PersonName = reader.GetString(1);
                                    entity.PersonLastName = reader.GetString(2);
                                    entity.Age = reader.GetInt32(3);
                                    entity.Username = reader.GetString(4);
                                    entity.Password = reader.GetString(5);
                                    persons.Add(entity);
                                }
                            }

                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            throw new Exception("Selecting All Error for entity [tPersons] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }
                return persons;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("CustomersRepository::SelectAll:Error occured.", ex);
            }
        }

        public Persons SelectedById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            Persons persons = null;

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append("[personID], [personName], [personLastName], [age], [username], [password] ");
                query.Append("FROM [dbo].[tPersons] ");
                query.Append("WHERE ");
                query.Append("[personID] = @id ");
                query.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                    {
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");
                    }

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                        {
                            throw new ArgumentNullException("dbCommand" + " The db SelectById command for entity [tPersons] can't be null.");
                        }

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        DBHelper.AddParameter(dbCommand, "@id", Types.Int, ParameterDirection.Input, id);

                        DBHelper.AddParameter(dbCommand, "@intErrorCode", Types.Int, ParameterDirection.Output, null);

                        if (dbConnection.State != ConnectionState.Open)
                        {
                            dbConnection.Open();
                        }

                        using (var reader = dbCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var entity = new Persons();
                                    entity.PersonID = reader.GetInt32(0);
                                    entity.PersonName = reader.GetString(1);
                                    entity.PersonLastName = reader.GetString(2);
                                    entity.Age = reader.GetInt32(3);
                                    entity.Username = reader.GetString(4);
                                    entity.Password = reader.GetString(5);
                                    persons = entity;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            throw new Exception("Selecting Error for entity [tPersons] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }
                return persons;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("CustomersRepository::SelectById:Error occured.", ex);
            }
        }

        public bool Update(Persons entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("UPDATE [dbo].[tPersons] ");
                query.Append("SET [personName] = @PersonName, [personLastName] = @PersonLastName, [age] = @Age, [username] = @Username, [password] = @Password ");
                query.Append("WHERE "); 
                query.Append("[personID] = @PersonID ");
                query.Append("SELECT @intErrorCode = @@ERROR; ");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                    {
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");
                    }

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                        {
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tPersons] can't be null. ");
                        }

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        DBHelper.AddParameter(dbCommand, "@PersonID", Types.Int, ParameterDirection.Input, entity.PersonID);
                        DBHelper.AddParameter(dbCommand, "@PersonName", Types.String, ParameterDirection.Input, entity.PersonName);
                        DBHelper.AddParameter(dbCommand, "@PersonLastName", Types.String, ParameterDirection.Input, entity.PersonLastName);
                        DBHelper.AddParameter(dbCommand, "@Age", Types.Int, ParameterDirection.Input, entity.Age);
                        DBHelper.AddParameter(dbCommand, "@Username", Types.String, ParameterDirection.Input, entity.Username);
                        DBHelper.AddParameter(dbCommand, "@Password", Types.String, ParameterDirection.Input, entity.Password);

                        DBHelper.AddParameter(dbCommand, "@intErrorCode", Types.Int, ParameterDirection.Output, null);

                        if (dbConnection.State != ConnectionState.Open)
                        {
                            dbConnection.Open();
                        }

                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            throw new Exception("Updating Error for entity [tPersons] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("CustomersRepository::Update:Error occured.", ex);
            }
        }
    }
}

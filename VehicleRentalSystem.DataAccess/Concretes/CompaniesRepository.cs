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
    public class CompaniesRepository : IRepository<Companies>, IDisposable
    {
        private string _connectionString;
        private string _dbProviderName;
        private DbProviderFactory _dbProviderFactory;
        private int _rowsAffected, _errorCode;
        private bool _bDisposed;

        public CompaniesRepository()
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
                query.Append("FROM [dbo].[tCompany] ");
                query.Append("WHERE");
                query.Append("[companyID] = @id ");
                query.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                    {
                        throw new ArgumentException("dbCommand" + "The db SelectById command for entity [tCompany] can't be null");
                    }

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                        {
                            throw new ArgumentException("dbCommand" + " The db SelectById command for entity [tCompany] can't be null.");
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
                            throw new Exception("Deleting Error for entity [tCompany] reported the Database ErrorCode: " + _errorCode);
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

        public bool Insert(Companies entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("INSERT [dbo].[tCompany] ");
                query.Append("([companyName], [password], [city], [address], [vehicleNumber], [companyScore]) ");
                query.Append("VALUES ");
                query.Append("(@CompanyName, @Password, @City, @Address, @VehicleNumber, @CompanyScore) ");
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
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tCompany] can't be null. ");
                        }

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        DBHelper.AddParameter(dbCommand, "@CompanyName", Types.String, ParameterDirection.Input, entity.CompanyName);
                        DBHelper.AddParameter(dbCommand, "@Password", Types.String, ParameterDirection.Input, entity.Password);
                        DBHelper.AddParameter(dbCommand, "@City", Types.String, ParameterDirection.Input, entity.City);
                        DBHelper.AddParameter(dbCommand, "@Address", Types.String, ParameterDirection.Input, entity.Address);
                        DBHelper.AddParameter(dbCommand, "@VehicleNumber", Types.Int, ParameterDirection.Input, entity.VehicleNumber);
                        DBHelper.AddParameter(dbCommand, "@CompanyScore", Types.Int, ParameterDirection.Input, entity.CompanyScore);

                        DBHelper.AddParameter(dbCommand, "@intErrorCode", Types.Int, ParameterDirection.Output, null);

                        if (dbConnection.State != ConnectionState.Open)
                        {
                            dbConnection.Open();
                        }

                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            throw new Exception("Inserting Error for entity [tCompany] reported the Database ErrorCode: " + _errorCode);
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

        public IList<Companies> SelectAll()
        {
            _errorCode = 0;
            _rowsAffected = 0;

            IList<Companies> companies = new List<Companies>();

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append("[companyID], [companyName], [password], [city], [address], [vehicleNumber], [companyScore] ");
                query.Append("FROM [dbo].[tCompany] ");
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
                            throw new ArgumentNullException("dbCommand" + " The db SelectById command for entity [tCompany] can't be null. ");
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
                                    var entity = new Companies();
                                    entity.CompanyID = reader.GetInt32(0);
                                    entity.CompanyName = reader.GetString(1);
                                    entity.Password = reader.GetString(2);
                                    entity.City = reader.GetString(3);
                                    entity.Address= reader.GetString(4);
                                    entity.VehicleNumber = reader.GetInt32(5);
                                    entity.CompanyScore = reader.GetInt32(6);
                                    companies.Add(entity);
                                }
                            }

                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            throw new Exception("Selecting All Error for entity [tCompany] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }
                return companies;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("CustomersRepository::SelectAll:Error occured.", ex);
            }
        }

        public Companies SelectedById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            Companies company = null;

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                query.Append("[companyID], [companyName], [password], [city], [address], [vehicleNumber], [companyScore] ");
                query.Append("FROM [dbo].[tCompany] ");
                query.Append("WHERE ");
                query.Append("[companyID] = @id ");
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
                            throw new ArgumentNullException("dbCommand" + " The db SelectById command for entity [tCompany] can't be null.");
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
                                    var entity = new Companies();
                                    entity.CompanyID = reader.GetInt32(0);
                                    entity.CompanyName = reader.GetString(1);
                                    entity.Password = reader.GetString(2);
                                    entity.City = reader.GetString(3);
                                    entity.Address = reader.GetString(4);
                                    entity.VehicleNumber = reader.GetInt32(5);
                                    entity.CompanyScore = reader.GetInt32(6);
                                    company = entity;
                                    break;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            throw new Exception("Selecting Error for entity [tCompany] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }
                return company;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("CustomersRepository::SelectById:Error occured.", ex);
            }
        }

        public bool Update(Companies entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append(" UPDATE [dbo].[tCompany] ");
                query.Append(" SET [companyName] = @CompanyName, [password] = @Password, [city] =  @City, [address] = @Address, [vehicleNumber] = @VehicleNumber, [companyScore] = @CompanyScore ");
                query.Append(" WHERE ");
                query.Append(" [companyID] = @CompanyID ");
                query.Append(" SELECT @intErrorCode = @@ERROR; ");

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
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tCompany] can't be null. ");
                        }

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        DBHelper.AddParameter(dbCommand, "@CompanyID", Types.Int, ParameterDirection.Input, entity.CompanyID);
                        DBHelper.AddParameter(dbCommand, "@CompanyName", Types.String, ParameterDirection.Input, entity.CompanyName);
                        DBHelper.AddParameter(dbCommand, "@Password", Types.String, ParameterDirection.Input, entity.Password);
                        DBHelper.AddParameter(dbCommand, "@City", Types.String, ParameterDirection.Input, entity.City);
                        DBHelper.AddParameter(dbCommand, "@Address", Types.String, ParameterDirection.Input, entity.Address);
                        DBHelper.AddParameter(dbCommand, "@VehicleNumber", Types.Int, ParameterDirection.Input, entity.VehicleNumber);
                        DBHelper.AddParameter(dbCommand, "@CompanyScore", Types.Int, ParameterDirection.Input, entity.CompanyScore);

                        DBHelper.AddParameter(dbCommand, "@intErrorCode", Types.Int, ParameterDirection.Output, null);

                        if (dbConnection.State != ConnectionState.Open)
                        {
                            dbConnection.Open();
                        }

                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            throw new Exception("Updating Error for entity [tCompany] reported the Database ErrorCode: " + _errorCode);
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

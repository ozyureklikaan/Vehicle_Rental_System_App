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
    public class RentalTransactionsRepository : IRepository<RentalTransactions>, IDisposable
    {
        private string _connectionString;
        private string _dbProviderName;
        private DbProviderFactory _dbProviderFactory;
        private int _rowsAffected, _errorCode;
        private bool _bDisposed;

        public RentalTransactionsRepository()
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
                query.Append("FROM [dbo].[tRentalTransactions] ");
                query.Append("WHERE");
                query.Append("[rentalID] = @id ");
                query.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                    {
                        throw new ArgumentException("dbCommand" + "The db SelectById command for entity [tRentalTransactions] can't be null");
                    }

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                        {
                            throw new ArgumentException("dbCommand" + " The db SelectById command for entity [tRentalTransactions] can't be null.");
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
                            throw new Exception("Deleting Error for entity [tRentalTransactions] reported the Database ErrorCode: " + _errorCode);
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

        public bool Insert(RentalTransactions entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("INSERT [dbo].[tRentalTransactions] ");
                //query.Append("([personID], [vehicleID], [rentingDate]) ");
                query.Append("([personID], [vehicleID] ) ");
                query.Append("VALUES ");
                query.Append("(@PersonID, @VehicleID) ");
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
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tRentalTransactions] can't be null. ");
                        }

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        DBHelper.AddParameter(dbCommand, "@PersonID", Types.Int, ParameterDirection.Input, entity.PersonID);
                        DBHelper.AddParameter(dbCommand, "@VehicleID", Types.Int, ParameterDirection.Input, entity.VehicleID);
                        //DBHelper.AddParameter(dbCommand, "@RentingDate", Types.DateTime, ParameterDirection.Input, entity.RentingDate);

                        DBHelper.AddParameter(dbCommand, "@intErrorCode", Types.Int, ParameterDirection.Output, null);

                        if (dbConnection.State != ConnectionState.Open)
                        {
                            dbConnection.Open();
                        }

                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            throw new Exception("Inserting Error for entity [tRentalTransactions] reported the Database ErrorCode: " + _errorCode);
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

        public IList<RentalTransactions> SelectAll()
        {
            _errorCode = 0;
            _rowsAffected = 0;

            IList<RentalTransactions> rentalTransactions = new List<RentalTransactions>();

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                //query.Append("[rentalID], [personID], [vehicleID], [rentingDate] ");
                query.Append("[rentalID], [personID], [vehicleID] ");
                query.Append("FROM [dbo].[tRentalTransactions] ");
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
                            throw new ArgumentNullException("dbCommand" + " The db SelectById command for entity [tRentalTransactions] can't be null. ");
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
                                    var entity = new RentalTransactions();
                                    entity.RentalID = reader.GetInt32(0);
                                    entity.PersonID = reader.GetInt32(1);
                                    entity.VehicleID = reader.GetInt32(2);
                                    //entity.RentingDate = reader.GetDateTime(2);
                                    rentalTransactions.Add(entity);
                                }
                            }

                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            throw new Exception("Selecting All Error for entity [tRentalTransactions] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }
                return rentalTransactions;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("CustomersRepository::SelectAll:Error occured.", ex);
            }
        }

        public RentalTransactions SelectedById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            RentalTransactions rentalTransaction = null;

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                //query.Append("[rentalID], [personID], [vehicleID], [rentingDate] ");
                query.Append("[rentalID], [personID], [vehicleID] ");
                query.Append("FROM [dbo].[tRentalTransactions] ");
                query.Append("WHERE ");
                query.Append("[rentalID] = @id ");
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
                            throw new ArgumentNullException("dbCommand" + " The db SelectById command for entity [tRentalTransactions] can't be null.");
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
                                    var entity = new RentalTransactions();
                                    entity.RentalID = reader.GetInt32(0);
                                    entity.PersonID = reader.GetInt32(1);
                                    entity.VehicleID = reader.GetInt32(2);
                                    //entity.RentingDate = reader.GetDateTime(3);
                                    rentalTransaction = entity;
                                    break;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            throw new Exception("Selecting Error for entity [tRentalTransactions] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }

                return rentalTransaction;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("CustomersRepository::SelectById:Error occured.", ex);
            }
        }

        public bool Update(RentalTransactions entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append(" UPDATE [dbo].[tRentalTransactions] ");
                //query.Append(" SET [personID] = @PersonID, [vehicleID] = @VehicleID, [rentingDate] =  @RentingDate ");
                query.Append(" SET [personID] = @PersonID, [vehicleID] = @VehicleID ");
                query.Append(" WHERE ");
                query.Append(" [rentalID] = @RentalID ");
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
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tRentalTransactions] can't be null. ");
                        }

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        DBHelper.AddParameter(dbCommand, "@RentalID", Types.Int, ParameterDirection.Input, entity.RentalID);
                        DBHelper.AddParameter(dbCommand, "@PersonID", Types.Int, ParameterDirection.Input, entity.PersonID);
                        DBHelper.AddParameter(dbCommand, "@VehicleID", Types.Int, ParameterDirection.Input, entity.VehicleID);
                        //DBHelper.AddParameter(dbCommand, "@RentingDate", Types.DateTime, ParameterDirection.Input, entity.RentingDate);

                        DBHelper.AddParameter(dbCommand, "@intErrorCode", Types.Int, ParameterDirection.Output, null);

                        if (dbConnection.State != ConnectionState.Open)
                        {
                            dbConnection.Open();
                        }

                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            throw new Exception("Updating Error for entity [tRentalTransactions] reported the Database ErrorCode: " + _errorCode);
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

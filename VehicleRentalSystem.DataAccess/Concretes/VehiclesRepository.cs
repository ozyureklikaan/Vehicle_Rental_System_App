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
    public class VehiclesRepository : IRepository<Vehicles>, IDisposable
    {
        private string _connectionString;
        private string _dbProviderName;
        private DbProviderFactory _dbProviderFactory;
        private int _rowsAffected, _errorCode;
        private bool _bDisposed;

        public VehiclesRepository()
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
                query.Append("FROM [dbo].[tVehicle] ");
                query.Append("WHERE");
                query.Append("[vehicleID] = @id ");
                query.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = query.ToString();
                query.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                    {
                        throw new ArgumentException("dbCommand" + "The db SelectById command for entity [tVehicle] can't be null");
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

        public bool Insert(Vehicles entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("INSERT [dbo].[tVehicle] ");
                query.Append("([vehicleBrand], [vehicleModel], [requiredAgeOfDrivingLicense], [minimumAgeLimit], [dailyKilometerLimit], [instantKilometerOfTheVehicle], " +
                                "[airbag], [baggageVolume], [numberOfSeats], [dailyRentPrice], [leasingStatus], [companyID]) ");
                query.Append("VALUES ");
                query.Append("(@VehicleBrand, @VehicleModel, @RequiredAgeOfDrivingLicense, @MinimumAgeLimit, @DailyKilometerLimit, @InstantKilometerOfTheVehicle, " +
                                "@Airbag, @BaggageVolume, @NumberOfSeats, @DailyRentPrice, @LeasingStatus, @CompanyID) ");
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

                        DBHelper.AddParameter(dbCommand, "@VehicleBrand", Types.String, ParameterDirection.Input, entity.VehicleBrand);
                        DBHelper.AddParameter(dbCommand, "@VehicleModel", Types.String, ParameterDirection.Input, entity.VehicleModel);
                        DBHelper.AddParameter(dbCommand, "@RequiredAgeOfDrivingLicense", Types.Int, ParameterDirection.Input, entity.RequiredAgeOfDrivingLicense);
                        DBHelper.AddParameter(dbCommand, "@MinimumAgeLimit", Types.Int, ParameterDirection.Input, entity.MinimumAgeLimit);
                        DBHelper.AddParameter(dbCommand, "@DailyKilometerLimit", Types.Int, ParameterDirection.Input, entity.DailyKilometerLimit);
                        DBHelper.AddParameter(dbCommand, "@InstantKilometerOfTheVehicle", Types.Int, ParameterDirection.Input, entity.InstantKilometerOfTheVehicle);
                        DBHelper.AddParameter(dbCommand, "@Airbag", Types.Boolean, ParameterDirection.Input, entity.Airbag);
                        DBHelper.AddParameter(dbCommand, "@BaggageVolume", Types.Int, ParameterDirection.Input, entity.BaggageVolume);
                        DBHelper.AddParameter(dbCommand, "@NumberOfSeats", Types.Int, ParameterDirection.Input, entity.NumberOfSeats);
                        DBHelper.AddParameter(dbCommand, "@DailyRentPrice", Types.Decimal, ParameterDirection.Input, entity.DailyRentPrice);
                        DBHelper.AddParameter(dbCommand, "@LeasingStatus", Types.Boolean, ParameterDirection.Input, entity.LeasingStatus);
                        DBHelper.AddParameter(dbCommand, "@CompanyID", Types.Int, ParameterDirection.Input, entity.CompanyID);

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

        public IList<Vehicles> SelectAll()
        {
            _errorCode = 0;
            _rowsAffected = 0;

            IList<Vehicles> vehicles = new List<Vehicles>();

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                /*query.Append("[vehicleID], [vehicleBrand], [vehicleModel], [inputDate], [outputDate], [requiredAgeOfDrivingLicense], [minimumAgeLimit], [dailyKilometerLimit], " +
                                "[instantKilometerOfTheVehicle], [airbag], [baggageVolume], [numberOfSeats], [dailyRentPrice], [leasingStatus], [companyID] ");*/
                query.Append("[vehicleID], [vehicleBrand], [vehicleModel], [requiredAgeOfDrivingLicense], [minimumAgeLimit], [dailyKilometerLimit], " +
                                "[instantKilometerOfTheVehicle], [airbag], [baggageVolume], [numberOfSeats], [dailyRentPrice], [leasingStatus], [companyID] ");
                query.Append("FROM [dbo].[tVehicle] ");
                //query.Append("WHERE [leasingStatus] = 0 ");
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
                            throw new ArgumentNullException("dbCommand" + " The db SelectById command for entity [tVehicle] can't be null. ");
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
                                    var entity = new Vehicles();
                                    entity.VehicleID = reader.GetInt32(0);
                                    entity.VehicleBrand = reader.GetString(1);
                                    entity.VehicleModel = reader.GetString(2);
                                    /*entity.InputDate = reader.GetDateTime(3);
                                    entity.OutputDate = reader.GetDateTime(4);
                                    entity.RequiredAgeOfDrivingLicense = reader.GetInt32(5);
                                    entity.MinimumAgeLimit = reader.GetInt32(6);
                                    entity.DailyKilometerLimit = reader.GetInt32(7);
                                    entity.InstantKilometerOfTheVehicle = reader.GetInt32(8);
                                    entity.Airbag = reader.GetBoolean(9);
                                    entity.BaggageVolume = reader.GetInt32(10);
                                    entity.NumberOfSeats = reader.GetInt32(11);
                                    entity.DailyRentPrice = reader.GetDecimal(12);
                                    entity.LeasingStatus = reader.GetBoolean(13);
                                    entity.CompanyID = reader.GetInt32(14);*/
                                    entity.RequiredAgeOfDrivingLicense = reader.GetInt32(3);
                                    entity.MinimumAgeLimit = reader.GetInt32(4);
                                    entity.DailyKilometerLimit = reader.GetInt32(5);
                                    entity.InstantKilometerOfTheVehicle = reader.GetInt32(6);
                                    entity.Airbag = reader.GetBoolean(7);
                                    entity.BaggageVolume = reader.GetInt32(8);
                                    entity.NumberOfSeats = reader.GetInt32(9);
                                    entity.DailyRentPrice = reader.GetDecimal(10);
                                    entity.LeasingStatus = reader.GetBoolean(11);
                                    entity.CompanyID = reader.GetInt32(12);
                                    CompaniesRepository companiesRepository = new CompaniesRepository();
                                    entity.Company = companiesRepository.SelectedById(entity.CompanyID);
                                    vehicles.Add(entity);
                                }
                            }

                        }
                        
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            throw new Exception("Selecting All Error for entity [tVehicle] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }
                return vehicles;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("CustomersRepository::SelectAll:Error occured.", ex);
            }
        }

        public Vehicles SelectedById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            Vehicles vehicle = null;

            try
            {
                var query = new StringBuilder();
                query.Append("SELECT ");
                /*query.Append("[vehicleID], [vehicleBrand], [vehicleModel], [inputDate], [outputDate], [requiredAgeOfDrivingLicense], [minimumAgeLimit], [dailyKilometerLimit], [instantKilometerOfTheVehicle], " +
                                "[airbag], [baggageVolume], [numberOfSeats], [dailyRentPrice], [leasingStatus] ");*/
                query.Append("[vehicleID], [vehicleBrand], [vehicleModel], [requiredAgeOfDrivingLicense], [minimumAgeLimit], [dailyKilometerLimit], [instantKilometerOfTheVehicle], " +
                                "[airbag], [baggageVolume], [numberOfSeats], [dailyRentPrice], [leasingStatus], [companyID] ");
                query.Append("FROM [dbo].[tVehicle] ");
                query.Append("WHERE ");
                query.Append("[vehicleID] = @id ");
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
                            throw new ArgumentNullException("dbCommand" + " The db SelectById command for entity [tVehicle] can't be null.");
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
                                    var entity = new Vehicles();
                                    entity.VehicleID = reader.GetInt32(0);
                                    entity.VehicleBrand = reader.GetString(1);
                                    entity.VehicleModel = reader.GetString(2);
                                    /*entity.InputDate = reader.GetDateTime(3);
                                    entity.OutputDate = reader.GetDateTime(4);
                                    entity.RequiredAgeOfDrivingLicense = reader.GetInt32(5);
                                    entity.MinimumAgeLimit = reader.GetInt32(6);
                                    entity.DailyKilometerLimit = reader.GetInt32(7);
                                    entity.InstantKilometerOfTheVehicle = reader.GetInt32(8);
                                    entity.Airbag = reader.GetBoolean(9);
                                    entity.BaggageVolume = reader.GetInt32(10);
                                    entity.NumberOfSeats = reader.GetInt32(11);
                                    entity.DailyRentPrice = reader.GetDecimal(12);
                                    entity.LeasingStatus = reader.GetBoolean(13);*/
                                    entity.RequiredAgeOfDrivingLicense = reader.GetInt32(3);
                                    entity.MinimumAgeLimit = reader.GetInt32(4);
                                    entity.DailyKilometerLimit = reader.GetInt32(5);
                                    entity.InstantKilometerOfTheVehicle = reader.GetInt32(6);
                                    entity.Airbag = reader.GetBoolean(7);
                                    entity.BaggageVolume = reader.GetInt32(8);
                                    entity.NumberOfSeats = reader.GetInt32(9);
                                    entity.DailyRentPrice = reader.GetDecimal(10);
                                    entity.LeasingStatus = reader.GetBoolean(11);
                                    entity.CompanyID = reader.GetInt32(12);
                                    vehicle = entity;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            throw new Exception("Selecting Error for entity [tVehicle] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }
                return vehicle;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("CustomersRepository::SelectById:Error occured.", ex);
            }
        }

        public bool Update(Vehicles entity)
        {
            _rowsAffected = 0;
            _errorCode = 0;

            try
            {
                var query = new StringBuilder();
                query.Append("UPDATE [dbo].[tVehicle] ");
                /*query.Append("SET [vehicleBrand] = @VehicleBrand, [vehicleModel] =  @VehicleModel, [inputDate] = @InputDate, [outputDate] = @OutputDate, " +
                                "[requiredAgeOfDrivingLicense] = @RequiredAgeOfDrivingLicense, [minimumAgeLimit] = @MinimumAgeLimit, [dailyKilometerLimit] = @DailyKilometerLimit, " +
                                "[instantKilometerOfTheVehicle] = @InstantKilometerOfTheVehicle, [airbag] = @Airbag, [baggageVolume] = @BaggageVolume, " +
                                "[numberOfSeats] = @NumberOfSeats, [dailyRentPrice] = @DailyRentPrice, [leasingStatus] = @LeasingStatus, [companyID] = @CompanyID ");*/
                query.Append("SET [vehicleBrand] = @VehicleBrand, [vehicleModel] =  @VehicleModel, " +
                                "[requiredAgeOfDrivingLicense] = @RequiredAgeOfDrivingLicense, [minimumAgeLimit] = @MinimumAgeLimit, [dailyKilometerLimit] = @DailyKilometerLimit, " +
                                "[instantKilometerOfTheVehicle] = @InstantKilometerOfTheVehicle, [airbag] = @Airbag, [baggageVolume] = @BaggageVolume, " +
                                "[numberOfSeats] = @NumberOfSeats, [dailyRentPrice] = @DailyRentPrice, [leasingStatus] = @LeasingStatus, [companyID] = @CompanyID ");
                query.Append(" WHERE ");
                query.Append(" [vehicleID] = @VehicleID ");
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
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [tVehicle] can't be null. ");
                        }

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        DBHelper.AddParameter(dbCommand, "@VehicleID", Types.String, ParameterDirection.Input, entity.VehicleID);
                        DBHelper.AddParameter(dbCommand, "@VehicleBrand", Types.String, ParameterDirection.Input, entity.VehicleBrand);
                        DBHelper.AddParameter(dbCommand, "@VehicleModel", Types.String, ParameterDirection.Input, entity.VehicleModel);
                        //DBHelper.AddParameter(dbCommand, "@InputDate", Types.DateTime, ParameterDirection.Input, entity.InputDate);
                        //DBHelper.AddParameter(dbCommand, "@OutputDate", Types.DateTime, ParameterDirection.Input, entity.OutputDate);
                        DBHelper.AddParameter(dbCommand, "@RequiredAgeOfDrivingLicense", Types.Int, ParameterDirection.Input, entity.RequiredAgeOfDrivingLicense);
                        DBHelper.AddParameter(dbCommand, "@MinimumAgeLimit", Types.Int, ParameterDirection.Input, entity.MinimumAgeLimit);
                        DBHelper.AddParameter(dbCommand, "@DailyKilometerLimit", Types.Int, ParameterDirection.Input, entity.DailyKilometerLimit);
                        DBHelper.AddParameter(dbCommand, "@InstantKilometerOfTheVehicle", Types.Int, ParameterDirection.Input, entity.InstantKilometerOfTheVehicle);
                        DBHelper.AddParameter(dbCommand, "@Airbag", Types.Boolean, ParameterDirection.Input, entity.Airbag);
                        DBHelper.AddParameter(dbCommand, "@BaggageVolume", Types.Int, ParameterDirection.Input, entity.BaggageVolume);
                        DBHelper.AddParameter(dbCommand, "@NumberOfSeats", Types.Int, ParameterDirection.Input, entity.NumberOfSeats);
                        DBHelper.AddParameter(dbCommand, "@DailyRentPrice", Types.Decimal, ParameterDirection.Input, entity.DailyRentPrice);
                        DBHelper.AddParameter(dbCommand, "@LeasingStatus", Types.Boolean, ParameterDirection.Input, entity.LeasingStatus);
                        DBHelper.AddParameter(dbCommand, "@CompanyID", Types.Int, ParameterDirection.Input, entity.CompanyID);

                        DBHelper.AddParameter(dbCommand, "@intErrorCode", Types.Int, ParameterDirection.Output, null);

                        if (dbConnection.State != ConnectionState.Open)
                        {
                            dbConnection.Open();
                        }

                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            throw new Exception("Updating Error for entity [tVehicle] reported the Database ErrorCode: " + _errorCode);
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

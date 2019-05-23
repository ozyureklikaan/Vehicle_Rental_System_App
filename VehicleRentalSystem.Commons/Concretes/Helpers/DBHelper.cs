using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using VehicleRentalSystem.Commons.Concretes.Data;
using VehicleRentalSystem.Commons.Concretes.Logger;

namespace VehicleRentalSystem.Commons.Concretes.Helpers
{
    public static class DBHelper
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        }

        public static string GetConnectionProvider()
        {
            return ConfigurationManager.ConnectionStrings["Default"].ProviderName;
        }

        public static void AddParameter(DbCommand command, string paramName, Types DataType,
            ParameterDirection direction, object value)
        {
            if (command == null)
                throw new ArgumentNullException("command", "The AddParameter's Command value is null.");

            try
            {
                DbParameter parameter = command.CreateParameter();
                parameter.ParameterName = paramName;
                parameter.DbType = CSharpDbTypeConverter(DataType);
                parameter.Value = value ?? DBNull.Value;
                parameter.Direction = direction;
                command.Parameters.Add(parameter);
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("DBHelper::AddParameter::Error occured.", ex);
            }
        }

        private static DbType CSharpDbTypeConverter(Types DataType)
        {
            var dbType = DbType.String;
            switch (DataType)
            {
                case Types.String:
                    dbType = DbType.String;
                    break;
                case Types.Int:
                    dbType = DbType.Int32;
                    break;
                case Types.Long:
                    dbType = DbType.Int64;
                    break;
                case Types.Double:
                    dbType = DbType.Double;
                    break;
                case Types.Decimal:
                    dbType = DbType.Decimal;
                    break;
                case Types.DateTime:
                    dbType = DbType.DateTime;
                    break;
                case Types.Boolean:
                    dbType = DbType.Boolean;
                    break;
                case Types.Short:
                    dbType = DbType.Int16;
                    break;
                case Types.Guid:
                    dbType = DbType.Guid;
                    break;
                case Types.ByteArray:
                case Types.Binary:
                    dbType = DbType.Binary;
                    break;
            }
            return dbType;
        }
    }
}

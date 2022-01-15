using System;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;

namespace Wisol.MES
{
    public class DBHelper
    {
        public static string GenerateEntityConnectionString(string ip, string port, string db, string userId, string password, string metadata)
        {
            try
            {
                return MssqlEntityConnectionString(ip, db, userId, password, metadata);
                throw new Exception("DB Type is not exists.");
            }
            catch (Exception exception) { throw exception; }
        }

        private static string MssqlEntityConnectionString(string ip, string db, string userId, string password, string metadata)
        {
            try
            {
                var sqlBuilder = new SqlConnectionStringBuilder()
                {
                    DataSource = ip,
                    InitialCatalog = db,
                    UserID = userId,
                    Password = password,
                    IntegratedSecurity = false,
                    MultipleActiveResultSets = true,
                    ApplicationName = "EntityFramework",
                    PersistSecurityInfo = true,
                };
                var entityBuilder = new EntityConnectionStringBuilder(sqlBuilder.ConnectionString)
                {
                    Provider = "System.Data.SqlClient",
                    ProviderConnectionString = sqlBuilder.ConnectionString,
                    Metadata = metadata,
                };
                return entityBuilder.ToString();
            }
            catch (Exception exception) { throw exception; }
        }

        public static string GenerateConnectionString(string dataSource, string initialCatalog, string userId, string password)
        {
            try
            {
                var connectString = "Data Source=;Initial Catalog=;Persist Security Info=True;User ID=;Password=";
                var sqlBuilder = new SqlConnectionStringBuilder(connectString)
                {
                    DataSource = dataSource,
                    InitialCatalog = initialCatalog,
                    PersistSecurityInfo = true,
                    UserID = userId,
                    Password = password,
                };
                return sqlBuilder.ToString();
            }
            catch (Exception exception) { throw exception; }
        }

        public static string ConnectionString(PlantType plantType)
        {
            return null;
        }
    }

    public class ConnectionStrings
    {

    }

    public enum PlantType
    {

    }

    public enum OperationType
    {
        Prodction,
        Dev,
    }

    public enum DBType
    {
        Oracle,
        Mssql
    }
}

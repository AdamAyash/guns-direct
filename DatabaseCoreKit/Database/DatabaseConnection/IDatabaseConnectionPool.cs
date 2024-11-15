namespace DatabaseCoreKit
{
    using Microsoft.Data.SqlClient;

    public interface IDatabaseConnectionPool
    {
        public SqlConnection? GetDatabaseConnection();
        public bool ReleaseConnection(SqlConnection databaseConnection);
    }
}

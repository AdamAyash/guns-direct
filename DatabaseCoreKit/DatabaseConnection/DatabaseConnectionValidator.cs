namespace DatabaseCoreKit.DatabaseConnectionValidator
{
    using Microsoft.Data.SqlClient;

    public class DatabaseConnectionValidator
    {
        public DatabaseConnectionValidator()
        {
        }

        public bool IsConnectionOpen(SqlConnection databaseConnection)
        {
            return databaseConnection.State == System.Data.ConnectionState.Open;
        }

        public bool InvalidateDatabaseConnection(SqlConnection databaseConnection)
        {
            bool isConnectionValid = true;

            CancellationToken canncelationToken = new CancellationToken();
            Task databaseConnectionTask = databaseConnection.OpenAsync(canncelationToken);
            databaseConnectionTask.Wait();

            if (!databaseConnectionTask.IsCompletedSuccessfully)
                isConnectionValid = false;

            if (!IsConnectionOpen(databaseConnection))
                isConnectionValid = false;

            return isConnectionValid;
        }
    }
}

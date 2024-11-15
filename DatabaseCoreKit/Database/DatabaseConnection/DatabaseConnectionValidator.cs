namespace DatabaseCoreKit
{
    using Common;
    using Microsoft.Data.SqlClient;

    public class DatabaseConnectionValidator
    {
        private readonly Logger _logger;

        public DatabaseConnectionValidator()
        {
            this._logger = Logger.GetLoggerInstance();
        }

        public bool IsConnectionOpen(SqlConnection databaseConnection)
        {
            return databaseConnection.State == System.Data.ConnectionState.Open;
        }

        public bool InvalidateDatabaseConnection(SqlConnection databaseConnection)
        {
            bool isConnectionValid = true;

            CancellationToken canncelationToken = new();
            Task databaseConnectionTask;
            try
            {
                databaseConnectionTask = databaseConnection.OpenAsync(canncelationToken);
                databaseConnectionTask.Wait();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, Messages.DATABASE_CONNECTION_FAILED_TO_OPEN_MESSAGE);
                return false;
            }

            if (!databaseConnectionTask.IsCompletedSuccessfully)
            {
               this._logger.LogError(Messages.DATABASE_CONNECTION_FAILED_TO_OPEN_MESSAGE);
                isConnectionValid = false;
            }

            if (!IsConnectionOpen(databaseConnection))
                isConnectionValid = false;

            return isConnectionValid;
        }
    }
}

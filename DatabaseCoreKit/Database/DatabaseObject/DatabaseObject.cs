using Common;
using Microsoft.Data.SqlClient;

namespace DatabaseCoreKit.Database.DatabaseObject
{
    public class DatabaseObject
    {
        //-------------------------
        //Constants:
        //-------------------------

        //-------------------------
        //Members:
        //-------------------------

        protected SqlConnection? _databaseConnection;
        protected SqlTransaction? _transactionContext;
        protected bool _isTransactionActive;
        protected bool _isLocalSession;

        protected Logger _logger = Logger.GetLoggerInstance();

        //-------------------------
        //Properties:
        //-------------------------

        //-------------------------
        //Constructor/Destructor:
        //-------------------------
        protected DatabaseObject()
        {
            _isLocalSession = true;
        }

        protected DatabaseObject(SqlConnection connection)
        {
            _databaseConnection = connection;
            _isLocalSession = false;
        }

        //-------------------------
        //Methods:
        //-------------------------

        public void StartTransaction()
        {
            if (_transactionContext != null)
                return;

            _transactionContext = _databaseConnection.BeginTransaction();
            _isTransactionActive = true;
        }

        public bool CommitTransaction()
        {
            try
            {
                if (_isTransactionActive)
                    _transactionContext.Commit();
            }
            catch (SqlException exception)
            {
                //_logger.LogError(exception);
                Rollback();
                return false;
            }
            return true;
        }

        protected void OpenLocalConnection()
        {
            if (_databaseConnection != null)
                return;

            DatabaseConnectionPool databaseConnectionPool = DatabaseConnectionPool.GetDatabaseConnectionInstance();
            _databaseConnection = databaseConnectionPool.GetDatabaseConnection();
        }

        protected void Rollback()
        {
            try
            {
                _isTransactionActive = false;
                _transactionContext.Rollback();
            }
            catch (SqlException exception)
            {
                //_logger.LogError(exception);
            }
        }

        protected bool CloseLocalConnection()
        {
            if (!_isLocalSession)
                return true;

            if (!CommitTransaction())
                Rollback();

            DatabaseConnectionPool databaseConnectionPool = DatabaseConnectionPool.GetDatabaseConnectionInstance();

            if (!databaseConnectionPool.ReleaseConnection(_databaseConnection!))
                return false;

            _databaseConnection = null;

            return true;
        }

        public SqlTransaction GetTransactionContext()
        {
            return this._transactionContext;
        }

        public void SetTransactionContext(SqlTransaction sqlTransactionContext)
        {
            this._transactionContext = sqlTransactionContext;
        }

        //-------------------------
        //Overrides:
        //-------------------------
    }
}

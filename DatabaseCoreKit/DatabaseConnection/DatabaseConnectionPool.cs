namespace DatabaseCoreKit.DatabaseConnectionPool
{
    using Common.ConfigurationManager;
    using DatabaseCoreKit.DatabaseConnection.ConfigurationSettings;
    using DatabaseCoreKit.DatabaseConnectionValidator;
    using Microsoft.Data.SqlClient;

    public sealed class DatabaseConnectionPool
    {
        private static DatabaseConnectionPool? _databaseConnectionInstance = null;

        private int? _maxPoolSize = 10; //Max connections by default
        private string? _connectionString;

        private DatabaseConnectionValidator _databaseConnectionValidator;
        private ConfigurationManager _configurationManager;

        private List<SqlConnection> _databaseConnectionsPool;
        private List<SqlConnection> _databaseCurrentlyUsedConnections;

        public int AvailableConnectionsCount => _databaseConnectionsPool.Count;
        public int CurrentlyUsedConnections => this._databaseCurrentlyUsedConnections.Count;

        private DatabaseConnectionPool()
        {
            this._databaseConnectionsPool = new List<SqlConnection>();
            this._databaseCurrentlyUsedConnections = new List<SqlConnection>();
            this._databaseConnectionValidator = new DatabaseConnectionValidator();
            this._configurationManager  = new ConfigurationManager();

            RetrieveDatabseConfiguration();
            InitializeConnectionPool();
        }
        private void RetrieveDatabseConfiguration()
        {
            DatabaseSettings? databaseSettings = _configurationManager.GetConfiguration<DatabaseSettings>("DatabaseSettings");

            this._connectionString = _configurationManager.GetConnectionString("GunsDirectDatabase");
            this._maxPoolSize = databaseSettings.MaxPoolConnections;
        }

        private void InitializeConnectionPool()
        {
            for (short index = 0;  index < _maxPoolSize; ++index)
            {
                var databaseConnection = CreateNewConnection();

                if (databaseConnection == null)
                    continue;

                _databaseConnectionsPool.Add(databaseConnection);
            }
        }
        private SqlConnection? CreateNewConnection()
        {
            var databaseConnection = new SqlConnection(_connectionString);

            if(!_databaseConnectionValidator.InvalidateDatabaseConnection(databaseConnection))
                return null;

            return databaseConnection;
        }

        public SqlConnection? GetDatabaseConnection()
        {
            var databaseConnection = _databaseConnectionsPool.FirstOrDefault() ?? CreateNewConnection();

            if (databaseConnection == null)
                return null;

            this._databaseConnectionsPool.Remove(databaseConnection);
            this._databaseCurrentlyUsedConnections.Add(databaseConnection);

            return databaseConnection;
        }
        public bool ReleaseUsedConnection(SqlConnection databaseConnection)
        {
            if(!this._databaseConnectionValidator.IsConnectionOpen(databaseConnection))
            {
                if(!_databaseConnectionValidator.InvalidateDatabaseConnection(databaseConnection))
                    return false;
            }

            this._databaseConnectionsPool.Add(databaseConnection);
            bool isRemoved = this._databaseCurrentlyUsedConnections.Remove(databaseConnection);

            return isRemoved;
        }

        public static DatabaseConnectionPool GetDatabaseConnectionInstance()
        {
            if (_databaseConnectionInstance == null)
                _databaseConnectionInstance = new DatabaseConnectionPool();

            return _databaseConnectionInstance;
        }
    }
}

namespace DatabaseCoreKit.Database.DatabaseConnection
{
    using Common.ConfigurationManager;
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
        public int CurrentlyUsedConnections => _databaseCurrentlyUsedConnections.Count;

        private DatabaseConnectionPool()
        {
            _databaseConnectionsPool = new List<SqlConnection>();
            _databaseCurrentlyUsedConnections = new List<SqlConnection>();
            _databaseConnectionValidator = new DatabaseConnectionValidator();
            _configurationManager = new ConfigurationManager();

            RetrieveDatabseConfiguration();
            InitializeConnectionPool();
        }
        private void RetrieveDatabseConfiguration()
        {
            DatabaseSettings? databaseSettings = _configurationManager.GetConfiguration<DatabaseSettings>("DatabaseSettings");

            _connectionString = _configurationManager.GetConnectionString("GunsDirectDatabase");
            _maxPoolSize = databaseSettings.MaxPoolConnections;
        }

        private void InitializeConnectionPool()
        {
            for (short index = 0; index < _maxPoolSize; ++index)
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

            if (!_databaseConnectionValidator.InvalidateDatabaseConnection(databaseConnection))
                return null;

            return databaseConnection;
        }

        public SqlConnection? GetDatabaseConnection()
        {
            var databaseConnection = _databaseConnectionsPool.FirstOrDefault() ?? CreateNewConnection();

            if (databaseConnection == null)
                return null;

            _databaseConnectionsPool.Remove(databaseConnection);
            _databaseCurrentlyUsedConnections.Add(databaseConnection);

            return databaseConnection;
        }
        public bool ReleaseUsedConnection(SqlConnection databaseConnection)
        {
            if (!_databaseConnectionValidator.IsConnectionOpen(databaseConnection))
            {
                if (!_databaseConnectionValidator.InvalidateDatabaseConnection(databaseConnection))
                    return false;
            }

            _databaseConnectionsPool.Add(databaseConnection);
            bool isRemoved = _databaseCurrentlyUsedConnections.Remove(databaseConnection);

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

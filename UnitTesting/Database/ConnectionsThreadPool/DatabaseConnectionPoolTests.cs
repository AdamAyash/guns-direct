namespace UnitTesting.Database.ConnectionPool
{
    using DatabaseCoreKit.Database.DatabaseConnection;

    [TestClass]
    public class DatabaseConnectionPoolTests
    {
        private DatabaseConnectionPool _databaseConnectionPool;
        private const int maxDatabaseConnections = 10;
        
        public DatabaseConnectionPoolTests()
        {
            this._databaseConnectionPool = DatabaseConnectionPool.GetDatabaseConnectionInstance();
        }

        [TestMethod]
        public void DatabaseConnectionPoolInstance()
        {
            Assert.IsNotNull(this._databaseConnectionPool);
        }

        [TestMethod]
        public void DatabaseConnectionNotvalidTest()
        {
            Assert.IsNotNull(this._databaseConnectionPool.GetDatabaseConnection());
        }

        [TestMethod]
        public void TestAvailableDatabaseConnections()
        {
            this._databaseConnectionPool = DatabaseConnectionPool.GetDatabaseConnectionInstance();

            for(int index = 0; index < maxDatabaseConnections; index++)
            {
                var connection = _databaseConnectionPool.GetDatabaseConnection();
            }

            Assert.AreEqual(_databaseConnectionPool.AvailableConnectionsCount, 0);
        }

        [TestMethod]
        public void TestDatbaseConnectionPoolCapacityGrow()
        {
            this._databaseConnectionPool = DatabaseConnectionPool.GetDatabaseConnectionInstance();

            for (int index = 0; index < maxDatabaseConnections; index++)
            {
                var connection = _databaseConnectionPool.GetDatabaseConnection();
            }

            var maxCapacityConnection = _databaseConnectionPool.GetDatabaseConnection();

            Assert.IsTrue(_databaseConnectionPool.CurrentlyUsedConnections > maxDatabaseConnections);
        }

        [TestMethod]
        public void TestDatbaseConnectionRelease()
        {
            this._databaseConnectionPool = DatabaseConnectionPool.GetDatabaseConnectionInstance();

            var connection = _databaseConnectionPool.GetDatabaseConnection();
            if (connection == null)
                return;

            bool isReleased = _databaseConnectionPool.ReleaseUsedConnection(connection);

            Assert.IsTrue(isReleased);
        }
    }
}

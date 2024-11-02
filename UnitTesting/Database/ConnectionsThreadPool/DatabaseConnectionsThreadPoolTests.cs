namespace UnitTesting.Database.ConnectionsThreadPool
{
    using DatabaseCoreKit.DatabaseConnectionsThreadPool;
    using Microsoft.Data.SqlClient;

    [TestClass]
    public class DatabaseConnectionsThreadPoolTests
    {
        private DatabaseConnectionsThreadPool _databaseConnectionsThreadPool;
        private const int maxDatabaseConnections = 10;
        
        public DatabaseConnectionsThreadPoolTests()
        {
            this._databaseConnectionsThreadPool = DatabaseConnectionsThreadPool.GetDatabaseConnectionInstance();
        }

        [TestMethod]
        public void DatabaseConnectionsThreadPoolInstance()
        {
            Assert.IsNotNull(this._databaseConnectionsThreadPool);
        }

        [TestMethod]
        public void DatabaseConnectionNotvalidTest()
        {
            Assert.IsNotNull(this._databaseConnectionsThreadPool.GetDatabaseConnection());
        }

        [TestMethod]
        public void TestAvailableDatabaseConnections()
        {
            this._databaseConnectionsThreadPool = DatabaseConnectionsThreadPool.GetDatabaseConnectionInstance();

            for(int index = 0; index < maxDatabaseConnections; index++)
            {
                var connection = _databaseConnectionsThreadPool.GetDatabaseConnection();
            }

            Assert.AreEqual(_databaseConnectionsThreadPool.AvailableConnectionsCount, 0);
        }

        [TestMethod]
        public void TestDatbaseConnectionPoolCapacityGrow()
        {
            const int maxConnections = 10; // Max connections by default
            this._databaseConnectionsThreadPool = DatabaseConnectionsThreadPool.GetDatabaseConnectionInstance();

            for (int index = 0; index < maxConnections; index++)
            {
                var connection = _databaseConnectionsThreadPool.GetDatabaseConnection();
            }

            var maxCapacityConnection = _databaseConnectionsThreadPool.GetDatabaseConnection();

            Assert.IsTrue(_databaseConnectionsThreadPool.CurrentlyUsedConnections > maxConnections);
        }


        [TestMethod]
        public void TestDatbaseConnectionRelease()
        {
            this._databaseConnectionsThreadPool = DatabaseConnectionsThreadPool.GetDatabaseConnectionInstance();

            var connection = _databaseConnectionsThreadPool.GetDatabaseConnection();
            if (connection == null)
                return;

            bool isReleased = _databaseConnectionsThreadPool.ReleaseUsedConnection(connection);

            Assert.IsTrue(isReleased);
        }
    }
}

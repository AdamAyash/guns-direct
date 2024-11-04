namespace UnitTesting.Database.DatabaseConnectionTest
{
    using DatabaseCoreKit.Database;
    using DatabaseCoreKit.Database.DatabaseConnection;

    [TestClass]
    public class DatabaseConnectionValidatorTests
    {
        private readonly DatabaseConnectionValidator _databaseConnectionValidator;
        private readonly DatabaseConnectionPool _databaseConnectionsThreadPool;
        public DatabaseConnectionValidatorTests()
        {
            this._databaseConnectionValidator = new DatabaseConnectionValidator();
            this._databaseConnectionsThreadPool = DatabaseConnectionPool.GetDatabaseConnectionInstance();
        }

        [TestMethod]
        public void TestIfClosedConnectionIsValid()
        {
            var databaseConnection = _databaseConnectionsThreadPool.GetDatabaseConnection();
            if (databaseConnection == null)
                return;

            databaseConnection.Close();

            bool isConnectionOpened =  _databaseConnectionValidator.IsConnectionOpen(databaseConnection);
            Assert.IsFalse(isConnectionOpened);
        }
    }
}

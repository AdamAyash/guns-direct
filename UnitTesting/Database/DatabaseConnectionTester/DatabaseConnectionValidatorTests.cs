namespace UnitTesting.Database.DatabaseConnectionTest
{
    using DatabaseCoreKit.DatabaseConnectionsThreadPool;
    using DatabaseCoreKit.DatabaseConnectionValidator;

    [TestClass]
    public class DatabaseConnectionValidatorTests
    {
        private readonly DatabaseConnectionValidator _databaseConnectionValidator;
        private readonly DatabaseConnectionsThreadPool _databaseConnectionsThreadPool;
        public DatabaseConnectionValidatorTests()
        {
            this._databaseConnectionValidator = new DatabaseConnectionValidator();
            this._databaseConnectionsThreadPool = DatabaseConnectionsThreadPool.GetDatabaseConnectionInstance();
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

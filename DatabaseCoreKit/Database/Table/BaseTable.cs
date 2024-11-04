namespace DatabaseCoreKit.Database.Table
{
    using DatabaseCoreKit.Database.DatabaseConnection;

    public class BaseTable<RecordType>
        where RecordType : class
    {
        private readonly DatabaseConnectionPool _databaseConnectionPool;

        public BaseTable()
        {
            _databaseConnectionPool = DatabaseConnectionPool.GetDatabaseConnectionInstance();
        }

        ~BaseTable()
        {

        }
    }
}

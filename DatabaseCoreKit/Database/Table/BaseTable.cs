namespace DatabaseCoreKit
{
    using Common;
    using Microsoft.Data.SqlClient;

    public class BaseTable<RecordType, IdentityKey>
        where RecordType : DomainObject , new()
    {
        private readonly DatabaseConnectionPool _databaseConnectionPool;
        private readonly TableBindingsData _SQLTableBindingsData;

        private Logger _logger = Logger.GetLoggerInstance();
        public string TableName { get; private set; }

        public BaseTable(string tableName)
        {
            _databaseConnectionPool = DatabaseConnectionPool.GetDatabaseConnectionInstance();
            _SQLTableBindingsData = TableBindingsDataMap.GetInstance().GetTableBindingsData(tableName);

            this.TableName = tableName;
        }

        ~BaseTable()
        {
        }

        public virtual bool SelectAll(ICollection<RecordType> domainObjectsList)
        {
            var databaseConnection  = this._databaseConnectionPool.GetDatabaseConnection();

            if (databaseConnection == null)
                return false;

            SQLComplexKey complexKey = new SQLComplexKey();
            complexKey.SetTableName(this.TableName);

            SqlCommand command = new SqlCommand(complexKey.GenerateWhereStatement(), databaseConnection);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return false;
            }

            var domainObjectMapper = new SQLToDomainObjectMapper<RecordType>(_SQLTableBindingsData);
            using (SqlDataReader databaseResultSet = command.ExecuteReader())
            {
                while(databaseResultSet.Read())
                {
                    RecordType domainObject = new();
                    if (!domainObjectMapper.MapDomainObject(databaseResultSet, domainObject))
                        return false;

                    domainObjectsList.Add(domainObject);
                }
            }

            this._databaseConnectionPool.ReleaseConnection(databaseConnection);

            return true;
        }


        public virtual bool SelectByIdentifier(IdentityKey indentity) 
        {
            return true;
        }

        public virtual bool SelectByComplexKey(SQLComplexKey complexKey, ICollection<RecordType> domainObjectsList)
        {
            var databaseConnection = this._databaseConnectionPool.GetDatabaseConnection();

            if (databaseConnection == null)
                return false;

            complexKey.SetTableName(TableName);

            SqlCommand command = new SqlCommand(complexKey.GenerateWhereStatement(), databaseConnection);
            command.ExecuteNonQuery();

            var domainObjectMapper = new SQLToDomainObjectMapper<RecordType>(_SQLTableBindingsData);
            using (SqlDataReader databaseResultSet = command.ExecuteReader())
            {
                while (databaseResultSet.Read())
                {
                    RecordType domainObject = new();
                    if (!domainObjectMapper.MapDomainObject(databaseResultSet, domainObject))
                        return false;

                    domainObjectsList.Add(domainObject);
                }
            }

            this._databaseConnectionPool.ReleaseConnection(databaseConnection);

            return true;
        }
    }
}

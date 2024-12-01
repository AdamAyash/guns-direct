namespace DatabaseCoreKit
{
    using Common;
    using Microsoft.Data.SqlClient;
    using System.Reflection;

    internal class SQLToDomainObjectMapper<RecordType>
        where RecordType : DomainObject, new()
    {
        private TableBindingsData _SQLTableBindingsData;
        private Logger _logger = Logger.GetLoggerInstance();

        public SQLToDomainObjectMapper(TableBindingsData SQLTableBindingsData)
        {
            this._SQLTableBindingsData = SQLTableBindingsData;
        }

        public bool MapDomainObject(SqlDataReader databaseResultSet, RecordType domainObject)
        {
            List<PropertyInfo> propertiesInfoList = domainObject.GetType().GetProperties()
                .OrderBy(property => property.MetadataToken)
                .ToList();

            if (propertiesInfoList.Count != _SQLTableBindingsData.GetColumnCount())
                return false;

            var databaseColumns = _SQLTableBindingsData.DatabaseColumns;

            for (int index = 0; index < propertiesInfoList.Count(); index++)
            {
                DatabaseColumnInfo databaseColumn = databaseColumns[index];
                PropertyInfo propertyInfo = propertiesInfoList[index];

                try
                {
                    propertyInfo.SetValue(domainObject, databaseResultSet[databaseColumn.Name]);
                }
                catch (Exception exception)
                {
                    this._logger.LogError(exception, Messages.DOMAIN_OBJECT_COLUMN_MAPPING_ERROR, databaseColumn.Name, _SQLTableBindingsData.TableName);
                    return false;
                }
            }

            return true;

        }
    }
}

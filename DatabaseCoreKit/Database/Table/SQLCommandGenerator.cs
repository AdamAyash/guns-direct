using Common;
using DatabaseCoreKit.Database.SQLCommandGenerator;
using Microsoft.Extensions.Primitives;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace DatabaseCoreKit.Database.Table
{
    internal class SQLCommandGenerator<RecordType> : BaseSQLCommandGenerator
        where RecordType : DomainObject, new()
    {
        public SQLCommandGenerator(TableBindingsData SQLTableBindingsData) 
            : base(SQLTableBindingsData)
        {
        }

        public string? GenerateInsertStatement(RecordType domainObject)
        {
            List<PropertyInfo> propertiesInfoList = domainObject.GetType().GetProperties()
               .OrderBy(property => property.MetadataToken)
               .ToList();

            if (propertiesInfoList.Count != _SQLTableBindingsData.GetColumnCount())
            {
                _logger.LogError(Messages.DIFFERENCE_IN_DOMAIN_OBJECT_FIELDS_AND_TABLE);
                return null;
            }

            StringBuilder insertStatementSource = new StringBuilder();
            insertStatementSource.Append(@"INSERT INTO " + _SQLTableBindingsData.TableName + " WITH(UPDLOCK)\n");
            insertStatementSource.Append("VALUES(");

            var databaseColumns = _SQLTableBindingsData.DatabaseColumns;

            for (int index = 0; index < propertiesInfoList.Count(); index++)
            {
                DatabaseColumnInfo databaseColumn = databaseColumns[index];
                PropertyInfo propertyInfo = propertiesInfoList[index];

                var propertyValue = propertyInfo.GetValue(domainObject, null);
                insertStatementSource.Append(ProcessKeyValue(propertyValue!));

                if (index != propertiesInfoList.Count() - 1)
                    insertStatementSource.Append(",");
            }

            insertStatementSource.Append(")");
            return insertStatementSource.ToString();
        }

        public string? GenerateUpdateStatement(RecordType domainObject)
        {
            List<PropertyInfo> propertiesInfoList = domainObject.GetType().GetProperties()
               .OrderBy(property => property.MetadataToken)
               .ToList();

            if (propertiesInfoList.Count != _SQLTableBindingsData.GetColumnCount())
            {
                _logger.LogError(Messages.DIFFERENCE_IN_DOMAIN_OBJECT_FIELDS_AND_TABLE);
                return null;
            }

            StringBuilder updateStatement = new StringBuilder();
            updateStatement.Append(@"UPDATE " + _SQLTableBindingsData.TableName + " WITH(UPDLOCK)\n");
            updateStatement.Append("SET \n");

            var databaseColumns = _SQLTableBindingsData.DatabaseColumns;

            for (int index = 0; index < propertiesInfoList.Count(); index++)
            {
                DatabaseColumnInfo databaseColumn = databaseColumns[index];
                PropertyInfo propertyInfo = propertiesInfoList[index];

                var propertyValue = propertyInfo.GetValue(domainObject, null);

                updateStatement.Append(databaseColumn.Name);
                updateStatement.Append("\t = ");
                updateStatement.Append("\t");
                updateStatement.Append(ProcessKeyValue(propertyValue!));

                if (index != propertiesInfoList.Count() - 1)
                    updateStatement.Append(",");
            }

            var primaryKeyColumn = this._SQLTableBindingsData.GetPrimaryKeyColumnName();
            if (primaryKeyColumn == null)
            {
                this._logger.LogError("Missing primary key column.");
                return null;
            }

            var identifier = Utilities.GetPropertyValue<RecordType>(primaryKeyColumn,domainObject);
            if (identifier == null)
            {
                this._logger.LogError("Missing identiifer.");
                return null;
            }

            updateStatement.AppendFormat("\n WHERE {0} = {1}", primaryKeyColumn, identifier);
            return updateStatement.ToString();
        }

    }
}

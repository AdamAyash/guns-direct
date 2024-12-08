namespace DatabaseCoreKit
{
    using Common;
    using Common.Exceptions;
    using Microsoft.IdentityModel.Tokens;
    using System.Xml;

    public sealed class DatabaseXMLSchemeParser : IDatabaseSchemeParser
    {
        private const string TABLE_ELEMENT = "table";

        private const string DATA_TYPE_ATTRIBUTE = "dataType";
        private const string SIZE_ATTRIBUTE = "size";
        private const string NAME_ATTRIBUTE = "name";
        private const string IS_PRIMARY_KEY_ATTRIBUTE = "isPrimaryKey";

        private readonly Logger _logger;
        private readonly XmlDocument _xmlDatabaseScheme;

        private TableBindingsDataMap _SQLTableBindingsDataMap;

        private ConfigurationManager _configurationManager;

        public DatabaseXMLSchemeParser()
        {
            this._xmlDatabaseScheme = new XmlDocument();
            this._SQLTableBindingsDataMap = TableBindingsDataMap.GetInstance();
            this._configurationManager = new ConfigurationManager();
            this._logger = Logger.GetLoggerInstance();
        }

        public bool Process()
        {
            if (!LoadDatabaseScheme())
                return false;

            if(!ParseScheme())
                return false;


            return true;
        }

        private bool LoadDatabaseScheme()
        {
            DatabaseSettings database = _configurationManager.GetConfiguration<DatabaseSettings>("DatabaseSettings")!;

            try
            {
                string? databaseXMLSchemePath = database.DatabaseXMLSchemePath;

                if (databaseXMLSchemePath == null || databaseXMLSchemePath.IsNullOrEmpty())
                    return false;

                _xmlDatabaseScheme.Load(databaseXMLSchemePath);
            }
            catch (Exception exception)
            {
                this._logger.LogFatal(exception, Messages.FAILED_TO_LOAD_DATABASE_SCHEME_MESSAGE);
                Utilities.ShutDownApplication();

                return false;
            }

            return true;
        }

        private bool ParseScheme()
        {
            XmlNodeList tables = _xmlDatabaseScheme.GetElementsByTagName(TABLE_ELEMENT);
            try
            {
                foreach (XmlNode tableNode in tables)
                {
                    ParseTable(tableNode);
                }
            }
            catch(InvalidDatabaseSchemeException exception)
            {
                _logger.LogError(exception, exception.Message);
                Utilities.ShutDownApplication();
            }
            return true;
        }

        private bool ParseTable(XmlNode table)
        {
            bool isTableDataValid = true;
            string validationMessage = String.Empty;

            TableBindingsData SQLTableBindingData = new();

            if (table.Attributes == null)
            {
                isTableDataValid = false;
                validationMessage = Messages.TABLE_SCHEME_HAS_NO_ATTRIBUTES;
            }

            if (table.Attributes[NAME_ATTRIBUTE] == null)
            {
                isTableDataValid = false;
                validationMessage = Messages.TABLE_SCHEME_HAS_NO_NAME_ATTRIBUTE;
            }

            var tableName = table.Attributes[NAME_ATTRIBUTE]?.Value;

            if (tableName.IsNullOrEmpty())
            {
                isTableDataValid = false;
                validationMessage = Messages.TABLE_SCHEME_HAS_NO_NAME_ATTRIBUTE;
            }

            SQLTableBindingData.TableName = tableName;

            if (!isTableDataValid)
                throw new InvalidDatabaseSchemeException(validationMessage);

            var tableColumns = table.ChildNodes;
            foreach (XmlNode column in tableColumns)
            {
                ParseColumn(SQLTableBindingData, column);
            }

           var primaryKeyColumns =  SQLTableBindingData.DatabaseColumns.Where(column => column.IsPrimaryKey).ToList();
            if (primaryKeyColumns.Count() > 1)
                throw new InvalidDatabaseSchemeException(Messages.MORE_THAN_ONE_PRIMARY_KEYS_DEFINED_IN_TABLE_SCHEME);

            _SQLTableBindingsDataMap.Add(tableName, SQLTableBindingData);

            return isTableDataValid;
        }

        private bool ParseColumn(TableBindingsData SQLTableBindingData, XmlNode tableColumn)
        {
            int size = -1;
            bool isColumnDataValid = true;
            string validationMessage = String.Empty;

            if (tableColumn.Attributes == null)
            {
                isColumnDataValid = false;
                validationMessage = Messages.TABLE_COLUMN_SCHEME_HAS_NO_NAME_ATTRIBUTES;
            }

            //Да проверяваме дали вечя няма таква колона
            if (tableColumn.Attributes[NAME_ATTRIBUTE] == null || tableColumn.Attributes[NAME_ATTRIBUTE].Value.IsNullOrEmpty())
            {
                isColumnDataValid = false;
                validationMessage = Messages.TABLE_COLUMN_SCHEME_HAS_NO_NAME_ATTRIBUTES;
            }

            string columnName = tableColumn.Attributes[NAME_ATTRIBUTE]!.Value;
            if(!SQLTableBindingData.DatabaseColumns.Exists(column => column.Name == columnName))
            {
                isColumnDataValid = false;
                validationMessage = Messages.TABLE_COLUMN_SCHEME_ALREADY_EXISTS;
            }

            if (tableColumn.Attributes[DATA_TYPE_ATTRIBUTE] == null || tableColumn.Attributes[DATA_TYPE_ATTRIBUTE]?.Value == null)
            {
                isColumnDataValid = false;
                validationMessage = Messages.TABLE_COLUMN_SCHEME_HAS_NO_DATA_ATTRIBUTES;
            }

            string dataType = tableColumn.Attributes[DATA_TYPE_ATTRIBUTE]!.Value;

            bool isPrimaryKey = false;

            if (tableColumn.Attributes[IS_PRIMARY_KEY_ATTRIBUTE] != null || tableColumn.Attributes[IS_PRIMARY_KEY_ATTRIBUTE]?.Value != null)
                isPrimaryKey = bool.Parse(tableColumn.Attributes[IS_PRIMARY_KEY_ATTRIBUTE]!.Value);

            if (!isColumnDataValid)
                throw new InvalidDatabaseSchemeException(validationMessage);

            XmlAttribute? sizeAttribute = tableColumn.Attributes[SIZE_ATTRIBUTE];

            if (sizeAttribute != null && sizeAttribute.Value != null)
                size = int.Parse(sizeAttribute.Value);

            SQLTableBindingData.AddColumn(new DatabaseColumnInfo(columnName, dataType, size, isPrimaryKey));

            return true;
        }
    }
}

namespace Common
{
    /// <summary></summary>
    public class Messages
    {
        public const string COONECTION_POOL_INITIAZLIZED_MESSAGE = "Connection pool initialized successfully.";
        public const string DATABASE_CONNECTION_FAILED_TO_OPEN_MESSAGE = "Database connection failed to open.";
        public const string FAILED_TO_LOAD_DATABASE_SCHEME_MESSAGE = "An error ocurred while trying to load the database scheme.";
        public const string MORE_THAN_ONE_PRIMARY_KEYS_DEFINED_IN_TABLE_SCHEME = "Table scheme has more than on primary keys defined.";
        public const string TABLE_SCHEME_HAS_NO_ATTRIBUTES = "Table scheme has no attributes.";
        public const string TABLE_SCHEME_HAS_NO_NAME_ATTRIBUTE = "Table scheme has no name attribute.";
        public const string TABLE_COLUMN_SCHEME_HAS_NO_NAME_ATTRIBUTES = "Table column scheme has no name attribute.";
        public const string APPLICATION_SHUT_DOWN_WITH_ERROR_MESSAGE = "The application has encountered an error and has been shutdown.";
        public const string TABLE_COLUMN_SCHEME_HAS_NO_DATA_ATTRIBUTES = "Table column scheme has no data type attribute.";
        public const string DOMAIN_OBJECT_MAPPING_ERROR = "An error occured while trying to map a domain object of table {0}";
        public const string DOMAIN_OBJECT_COLUMN_MAPPING_ERROR = "An error occured while trying to map a domain object column {0} of table {1}";
    }
}

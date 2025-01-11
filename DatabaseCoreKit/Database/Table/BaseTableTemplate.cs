using Microsoft.Data.SqlClient;

namespace DatabaseCoreKit.Database.Table
{
    public class BaseTableTemplate<RecordType> : BaseTable<RecordType, int>
        where RecordType : DomainObject, new()
    {
        protected BaseTableTemplate(string tableName) 
            : base(tableName)
        {
        }

        protected BaseTableTemplate(SqlConnection sqlConnection, string tableName)
           : base(sqlConnection, tableName)
        {
        }
    }
}

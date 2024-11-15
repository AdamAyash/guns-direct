namespace DatabaseCoreKit.Database.Table
{
    public class BaseTableTemplate<RecordType> : BaseTable<RecordType, int>
        where RecordType : DomainObject, new()
    {
        public BaseTableTemplate(string tableName) 
            : base(tableName)
        {
        }
    }
}

namespace DatabaseCoreKit.Database.Table.Implementations
{
    internal class Counter : DomainObject
    {
        public int ID { get; set; }
        public string TableName { get; set; }
        public string PrimaryKeyColumnName { get; set; }
        public int CurrentId { get; set; }
        public int IncrementBy { get; set; }

        public int IncrementID()
        {
            return CurrentId += IncrementBy;
        }
    }
}

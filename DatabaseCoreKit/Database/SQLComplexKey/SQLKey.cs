namespace DatabaseCoreKit
{
    public class SQLKey
    {
        public string ColumnName { get; set; }
        public dynamic KeyValue { get; set; }
        public CompareTypes CompareType { get; set; }

        public SQLKey(string columnName, dynamic key, CompareTypes compareType = CompareTypes.EQUALS)
        {
            this.ColumnName = columnName;
            this.KeyValue = key;
            this.CompareType = compareType;
        }
    }
}

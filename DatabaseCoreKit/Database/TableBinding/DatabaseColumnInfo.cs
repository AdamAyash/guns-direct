namespace DatabaseCoreKit
{
    public class DatabaseColumnInfo
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public int Size { get; set; }
        public bool IsPrimaryKey { get; set; }

        public DatabaseColumnInfo(string name, string dataType, int size, bool isPrimaryKey = false)
        {
            this.Name = name;
            this.DataType = dataType;
            this.Size = size;
            this.IsPrimaryKey = isPrimaryKey;
        }
    }
}

namespace DatabaseCoreKit
{
    using System.Text;

    internal class TableBindingsData
    {
        private const string UPDATE_COUNTER_COLUMN_NAME = "UPDATE_COUNTER";

        private List<DatabaseColumnInfo> _tableColumns;
        public TableBindingsData()
        {
            _tableColumns = new List<DatabaseColumnInfo>();
        }

        public string TableName { get; set; }

        public List<DatabaseColumnInfo> DatabaseColumns
        {
            get
            {
                return _tableColumns;
            }
        }
        public void AddColumn(DatabaseColumnInfo databaseColumn)
        {
            _tableColumns.Add(databaseColumn);
        }

        public string GetColumnsInfo()
        {
            StringBuilder columnsInfo = new StringBuilder();

            for(int index = 0; index < _tableColumns.Count; index++)
            {
                DatabaseColumnInfo column = _tableColumns.ElementAt(index);
                columnsInfo.Append(column.Name + ",");

                if(index == _tableColumns.Count - 1)
                    columnsInfo.Append(column.Name);
            }

            return columnsInfo.ToString();
        }

        public bool SupportsUpdateCounter()
        {
            return _tableColumns.FirstOrDefault(column => column.Name == UPDATE_COUNTER_COLUMN_NAME) != null;
        }

        public DatabaseColumnInfo? GetPrimaryKeyColumn()
        {
            return _tableColumns.FirstOrDefault(column => column.IsPrimaryKey);
        }

        public string? GetPrimaryKeyColumnName()
        {
            return this.GetPrimaryKeyColumn()?.Name;
        }

        public int GetColumnCount()
        {
            return this._tableColumns.Count();
        }
    }
}

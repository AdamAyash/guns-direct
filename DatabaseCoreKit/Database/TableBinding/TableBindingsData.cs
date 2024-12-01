namespace DatabaseCoreKit
{
    using System.Text;

    internal class TableBindingsData
    {
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

        public DatabaseColumnInfo? GetPrimaryKeyColumn()
        {
            return _tableColumns.FirstOrDefault(column => column.IsPrimaryKey);
        }

        public int GetColumnCount()
        {
            return this._tableColumns.Count();
        }
    }
}

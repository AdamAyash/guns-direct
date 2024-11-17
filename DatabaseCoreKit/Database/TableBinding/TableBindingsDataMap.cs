namespace DatabaseCoreKit
{ 
    internal class TableBindingsDataMap
    {
        private static TableBindingsDataMap? _TableBindingsDataMapInstance = null;
        private Dictionary<string, TableBindingsData> _TableBindingsDataMap;

        private TableBindingsDataMap()
        {
            this._TableBindingsDataMap = new Dictionary<string, TableBindingsData>();
        }

        ~TableBindingsDataMap()
        {
            this._TableBindingsDataMap.Clear();
        }

        public static TableBindingsDataMap GetInstance()
        {
            if (_TableBindingsDataMapInstance == null)
                _TableBindingsDataMapInstance = new();

            return _TableBindingsDataMapInstance;
        }

        public void Add(string tableName, TableBindingsData SQLTableBindingsData)
        {
            _TableBindingsDataMap.Add(tableName, SQLTableBindingsData);
        }

        public TableBindingsData GetTableBindingsData(string tableName)
        {
            return _TableBindingsDataMap[tableName];
        }
    }
}

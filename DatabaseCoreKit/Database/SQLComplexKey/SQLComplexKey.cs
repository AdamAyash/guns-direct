namespace DatabaseCoreKit
{
    using Common;
    using Microsoft.IdentityModel.Tokens;
    using System.Text;

    public class SQLComplexKey
    {
        private List<SQLKey> _keysList;
        private readonly LockTypes _lockType;

        private string? _tableName;
        public SQLComplexKey(string columnName, dynamic key, CompareTypes compareType = CompareTypes.EQUALS, 
            LockTypes lockType = LockTypes.NOLOCK)
        {
            this._keysList = [new SQLKey(columnName, key, compareType)];
            this._lockType = lockType;
        }

        public SQLComplexKey()
        {
            this._keysList = new List<SQLKey>();
            this._lockType = LockTypes.NOLOCK;
        }

        public void SetTableName(string tableName)
        {
            this._tableName = tableName;
        }

        public string GenerateWhereStatement()
        {
            StringBuilder SQLStatement = new StringBuilder();
            SQLStatement.Append("SELECT * FROM " + _tableName + " WITH(" + this._lockType.ToString() + ") ");

            if(!_keysList.IsNullOrEmpty())
                SQLStatement.Append("WHERE ");

            for(int index = 0; index < _keysList.Count; index++)
            {
                SQLKey key = _keysList[index];

                string compareType = ProcessCompareType(key.CompareType);
                string keyValue = ProcessKeyValue(key.KeyValue);

                SQLStatement.Append(key.ColumnName + compareType + keyValue);

                if (index < _keysList.Count - 1)
                    SQLStatement.Append(" AND ");
            }

            return SQLStatement.ToString();
        }

        private string ProcessKeyValue(dynamic keyValue)
        {
            string processedValue = Utilities.ConvertObject<string>(keyValue);

            if(keyValue is string)
                processedValue = "\'" + processedValue + "\'";

            return processedValue;
        }

        private string ProcessCompareType(CompareTypes compareType) 
        {
            switch(compareType)
            {
                case CompareTypes.EQUALS:
                    return " = ";

                default:
                    return String.Empty;
            }    
        }

        public void AddKey(SQLKey key) 
        {
            this._keysList.Add(key);
        }
    }
}

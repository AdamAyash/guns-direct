using Common;
using Microsoft.Data.SqlClient;

namespace DatabaseCoreKit.Database.Table.Implementations
{
    internal class CountersTable : BaseTableTemplate<Counter>
    {
        private const int COUNTER_DOES_NOT_EXIST = -1;

        enum CounterTableColumn
        {
            Table_Name
        }

        public CountersTable(SqlConnection sqlConnection) 
            : base(sqlConnection, "COUNTERS")
        {
        }

        public int GetNextUniqueIdentifier(string tableName)
        {
            Counter counter = new Counter();
            SQLComplexKey complexKey = new SQLComplexKey(CounterTableColumn.Table_Name.ToString(), tableName);

            if(!this.SelectByComplexKey(complexKey, ref counter))
            {
                this._logger.LogError(Messages.COUNTER_DOES_NO_EXIST_ERROR, TableName);
                return COUNTER_DOES_NOT_EXIST;
            }

            return counter.IncrementID();
        }

        public bool UpdateNextUniqueIdentifier(string tableName, int nextUniqueIndentifier)
        {
            Counter counter = new Counter();
            SQLComplexKey complexKey = new SQLComplexKey(CounterTableColumn.Table_Name.ToString(), tableName);

            if (!this.SelectByComplexKey(complexKey, ref counter))
            {
                this._logger.LogError(Messages.COUNTER_DOES_NO_EXIST_ERROR, TableName);
                return false;
            }

            if (nextUniqueIndentifier <= counter.CurrentId)
            {
                this._logger.LogError(Messages.COUNTER_ALREADY_UPDATED, TableName);
                return false;
            }

            counter.CurrentId = nextUniqueIndentifier;

            if (!this.Update(counter))
                return false;

            return true;
        }
    }
}

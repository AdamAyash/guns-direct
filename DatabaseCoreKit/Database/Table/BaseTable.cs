namespace DatabaseCoreKit
{
    using Common;
    using DatabaseCoreKit.Database.DatabaseObject;
    using DatabaseCoreKit.Database.Table;
    using DatabaseCoreKit.Database.Table.Implementations;
    using Microsoft.Data.SqlClient;
    using System;

    public class BaseTable<RecordType, IdentityKey> : DatabaseObject
        where RecordType : DomainObject, new()
    {
        private readonly TableBindingsData _SQLTableBindingsData;
        private readonly bool _isSupportingCounter;

        public string TableName { get; private set; }

        protected string? PrimaryKeyColumnName => this._SQLTableBindingsData?.GetPrimaryKeyColumnName();

        protected BaseTable(string tableName, bool isSupportingCounter = true)
        {
            this._SQLTableBindingsData = TableBindingsDataMap.GetInstance().GetTableBindingsData(tableName);
            this._isSupportingCounter = isSupportingCounter;
            this.TableName = tableName;
        }

        protected BaseTable(SqlConnection databaseConnection, string tableName, bool isSupportingCounter = true)
            : base(databaseConnection)
        {
            this._SQLTableBindingsData = TableBindingsDataMap.GetInstance().GetTableBindingsData(tableName);
            this._isSupportingCounter = isSupportingCounter;
            this.TableName = tableName;
        }

        ~BaseTable()
        {
            CloseLocalConnection();
        }

        public virtual bool SelectAll(ICollection<RecordType> domainObjectsList)
        {
            this.OpenLocalConnection();

            if (_databaseConnection == null)
                return false;

            SQLComplexKey complexKey = new SQLComplexKey();
            complexKey.SetTableName(this.TableName);

            SqlCommand command = new SqlCommand(complexKey.GenerateWhereStatement(), _databaseConnection);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return false;
            }

            var domainObjectMapper = new SQLToDomainObjectMapper<RecordType>(_SQLTableBindingsData);
            using (SqlDataReader databaseResultSet = command.ExecuteReader())
            {
                while (databaseResultSet.Read())
                {
                    RecordType domainObject = new();
                    if (!domainObjectMapper.MapDomainObject(databaseResultSet, domainObject))
                    {
                        this._logger.LogError(Messages.DOMAIN_OBJECT_MAPPING_ERROR, _SQLTableBindingsData.TableName);
                        return false;
                    }

                    domainObjectsList.Add(domainObject);
                }
            }

            if (!CloseLocalConnection())
                return false;

            return true;
        }

        public virtual bool SelectByPrimaryKey(IdentityKey identityKey, RecordType domainObject)
        {
            string? primaryKeyColumnName = this._SQLTableBindingsData.GetPrimaryKeyColumnName();
            if (primaryKeyColumnName == null)
                return false;

            SQLComplexKey complexKey = new SQLComplexKey(primaryKeyColumnName, identityKey);
            if (!SelectByComplexKey(complexKey, domainObject))
                return false;

            return true;
        }

        public virtual bool SelectByComplexKey(SQLComplexKey complexKey, ICollection<RecordType> domainObjectsList)
        {
            this.OpenLocalConnection();

            if (_databaseConnection == null)
                return false;

            complexKey.SetTableName(TableName);

            SqlCommand command = new SqlCommand(complexKey.GenerateWhereStatement(), _databaseConnection);
            command.ExecuteNonQuery();

            var domainObjectMapper = new SQLToDomainObjectMapper<RecordType>(_SQLTableBindingsData);
            using (SqlDataReader databaseResultSet = command.ExecuteReader())
            {
                while (databaseResultSet.Read())
                {
                    RecordType domainObject = new();
                    if (!domainObjectMapper.MapDomainObject(databaseResultSet, domainObject))
                    {
                        this._logger.LogError(Messages.DOMAIN_OBJECT_MAPPING_ERROR, _SQLTableBindingsData.TableName);
                        return false;

                    }

                    domainObjectsList.Add(domainObject);
                }
            }

            if (!CloseLocalConnection())
                return false;

            return true;
        }

        public virtual bool SelectByComplexKey(SQLComplexKey complexKey, ref RecordType domainObject)
        {
            OpenLocalConnection();

            if (_databaseConnection == null)
                return false;

            complexKey.SetTableName(TableName);

            SqlCommand command = new SqlCommand(complexKey.GenerateWhereStatement(), _databaseConnection);
            command.Transaction = _transactionContext;
            command.ExecuteNonQuery();

            var domainObjectMapper = new SQLToDomainObjectMapper<RecordType>(_SQLTableBindingsData);
            using (SqlDataReader databaseResultSet = command.ExecuteReader())
            {
                if (databaseResultSet.Read())
                {
                    if (!domainObjectMapper.MapDomainObject(databaseResultSet, domainObject))
                    {
                        this._logger.LogError(Messages.DOMAIN_OBJECT_MAPPING_ERROR, _SQLTableBindingsData.TableName);
                        return false;
                    }
                }

                if (!CloseLocalConnection())
                    return false;

                return true;
            }
        }

        public virtual bool SelectByComplexKey(SQLComplexKey complexKey, RecordType domainObject)
        {
            OpenLocalConnection();

            if (_databaseConnection == null)
                return false;

            complexKey.SetTableName(TableName);

            SqlCommand command = new SqlCommand(complexKey.GenerateWhereStatement(), _databaseConnection);
            command.Transaction = _transactionContext;
            command.ExecuteNonQuery();

            var domainObjectMapper = new SQLToDomainObjectMapper<RecordType>(_SQLTableBindingsData);
            using (SqlDataReader databaseResultSet = command.ExecuteReader())
            {
                if (databaseResultSet.Read())
                {
                    if (!domainObjectMapper.MapDomainObject(databaseResultSet, domainObject))
                    {
                        this._logger.LogError(Messages.DOMAIN_OBJECT_MAPPING_ERROR, _SQLTableBindingsData.TableName);
                        return false;
                    }
                }

                if (!CloseLocalConnection())
                    return false;

                return true;
            }
        }

        public virtual bool Insert(RecordType domainObject)
        {
            OpenLocalConnection();
            StartTransaction();

            if (_databaseConnection == null)
                return false;

            int nextUniqueId = 0;

            if (_isSupportingCounter)
            {
                var primaryKeyColumn = this.PrimaryKeyColumnName;
                nextUniqueId = GetNextUniqueIdentifier();

                if (nextUniqueId <= 0)
                    return false;
                try
                {
                    domainObject.GetType().GetProperty(primaryKeyColumn)?.SetValue(domainObject, nextUniqueId);
                }
                catch (Exception exception)
                {
                    return false;
                }
            }
            string? insertCommandString;

            try
            {
                SQLCommandGenerator<RecordType> sqlCommandGenerator = new SQLCommandGenerator<RecordType>(this._SQLTableBindingsData);
                insertCommandString = sqlCommandGenerator.GenerateInsertStatement(domainObject);

            }
            catch(Exception)
            {
                return false;
            }

            if (insertCommandString == null)
                return false;

                SqlCommand insertCommand = new SqlCommand(insertCommandString, _databaseConnection);
            try
            {
                insertCommand.Transaction = _transactionContext;
                insertCommand.ExecuteNonQuery();
            }
            catch(Exception exception)
            {
                Rollback();
                return false;
            }

            if (_isSupportingCounter && !UpdateNextUniqueIdentifier(nextUniqueId))
            {
                Rollback();
                return false;
            }

            if (!CloseLocalConnection())
                return false;

            return true;
        }

        public virtual bool Update(RecordType domainObject)
        {
            if (!_isSupportingCounter)
                return false;

             OpenLocalConnection();
             StartTransaction();

            if (_databaseConnection == null)
                return false;

            var primaryKeyColumn = this.PrimaryKeyColumnName;
            if (primaryKeyColumn == null)
            {
                this._logger.LogError("Missing primary key column.");
                return false;
            }

            var identifier = Utilities.GetPropertyValue<RecordType>(primaryKeyColumn, domainObject);
            if (identifier == null)
            {
                this._logger.LogError("Missing identiifer.");
                return false;
            }

            RecordType databseDomainObject = new RecordType();
            SQLCommandGenerator<RecordType> sqlCommandGenerator = new SQLCommandGenerator<RecordType>(this._SQLTableBindingsData);

            string? updateCommandString = sqlCommandGenerator.GenerateUpdateStatement(domainObject);
            if (updateCommandString == null)
                return false;

            SqlCommand updateCommand = new SqlCommand(updateCommandString, _databaseConnection);
            updateCommand.Transaction = _transactionContext;

            try
            {
                updateCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Rollback();
                return false;
            }

            if (!CloseLocalConnection())
            {
                Rollback();
                return false;
            }

            return true;
        }

        private int GetNextUniqueIdentifier()
        {
            CountersTable countersTable = new CountersTable(_databaseConnection);
            countersTable.SetTransactionContext(this._transactionContext);

            return countersTable.GetNextUniqueIdentifier(TableName);
        }

        private bool UpdateNextUniqueIdentifier(int nextUniqueIdentifier)
        {
            CountersTable countersTable = new CountersTable(_databaseConnection);
            countersTable.SetTransactionContext(this._transactionContext);

            return countersTable.UpdateNextUniqueIdentifier(TableName, nextUniqueIdentifier);
        }
    }
}

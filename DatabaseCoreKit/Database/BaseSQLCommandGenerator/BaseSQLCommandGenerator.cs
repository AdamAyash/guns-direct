using Common;

namespace DatabaseCoreKit.Database.SQLCommandGenerator
{
    internal class BaseSQLCommandGenerator
    {
        // --------------------------------------------------------------------------
        // Constants
        // --------------------------------------------------------------------------

        // --------------------------------------------------------------------------
        // Members
        // --------------------------------------------------------------------------

        protected TableBindingsData _SQLTableBindingsData;
        protected Logger _logger = Logger.GetLoggerInstance();

        // --------------------------------------------------------------------------
        // Properties
        // --------------------------------------------------------------------------

        // --------------------------------------------------------------------------
        // Constructor
        // --------------------------------------------------------------------------
        protected BaseSQLCommandGenerator(TableBindingsData SQLTableBindingsData)
        {
            this._SQLTableBindingsData = SQLTableBindingsData;
        }

        // --------------------------------------------------------------------------
        // Methods
        // --------------------------------------------------------------------------

        protected string ProcessKeyValue(dynamic keyValue)
        {
            string processedValue = Utilities.ConvertObject<string>(keyValue);

            if (keyValue is string)
            {
                processedValue = "N\'" + processedValue + "\'";

            }
            else if (keyValue is DateTime)
            {
                DateTime dateValue = keyValue;
                string formattedDate = Utilities.FormatDate(dateValue);
                processedValue = this.ProcessKeyValue(formattedDate);
            }
            else if(keyValue is byte[])
            {
                processedValue = System.Text.Encoding.Default.GetString(keyValue);
            }

            return processedValue;
        }

        // --------------------------------------------------------------------------
        // Overrides
        // --------------------------------------------------------------------------
    }
}

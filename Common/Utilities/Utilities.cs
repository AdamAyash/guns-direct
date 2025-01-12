namespace Common
{
    #region
    using Serilog;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text.Json;
    #endregion

    /// <summary></summary>
    public static class Utilities
    {
        // --------------------------------------------------------------------------
        // Constants
        // --------------------------------------------------------------------------

        private const string DEFAULT_DATE_FORMAT = "yyyy-MM-dd HH:mm:ss.fff";

        // --------------------------------------------------------------------------
        // Members
        // --------------------------------------------------------------------------

        // --------------------------------------------------------------------------
        // Properties
        // --------------------------------------------------------------------------

        // --------------------------------------------------------------------------
        // Constructor
        // --------------------------------------------------------------------------

        // --------------------------------------------------------------------------
        // Methods
        // --------------------------------------------------------------------------

        public static T ConvertObject<T>(object input)
           where T : class
        {
            return (T)Convert.ChangeType(input, typeof(T));
        }

        public static DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }

        public static DateTime GetCurrentDate()
        {
            return DateTime.Now.Date;
        }

        public static string GetCurrentDateString()
        {
            return DateTime.Now.Date.ToString("dd-MM-yyyy");
        }

        public static string FormatDate(DateTime date)
        {
            return date.ToString(DEFAULT_DATE_FORMAT);
        }

        public static void ShutDownApplication()
        {
            Log.Logger.Fatal(Messages.APPLICATION_SHUT_DOWN_WITH_ERROR_MESSAGE);
            Log.CloseAndFlush();
            System.Threading.Thread.Sleep(1000);
            Environment.Exit(0);
        }

        public static object? GetPropertyValue<Type>(string properyName, Type @object)
        {
            return @object.GetType().GetProperty(properyName)?.GetValue(@object);
        }

        // --------------------------------------------------------------------------
        // Overrides
        // --------------------------------------------------------------------------
    }
}

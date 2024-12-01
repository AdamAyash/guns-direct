namespace Common
{
    #region
    using Serilog;
    #endregion

    /// <summary></summary>
    public sealed class Logger
    {
        // --------------------------------------------------------------------------
        // Constants
        // --------------------------------------------------------------------------

        // --------------------------------------------------------------------------
        // Members
        // --------------------------------------------------------------------------

        private static Logger? m_LoggerInstance = null;
        private static LogSettings? _logSettings;

        private ConfigurationManager _configurationManager;

        // --------------------------------------------------------------------------
        // Properties
        // --------------------------------------------------------------------------

        // --------------------------------------------------------------------------
        // Constructor
        // --------------------------------------------------------------------------

        private Logger()
        {
            this._configurationManager = new ConfigurationManager();
            _logSettings = GetConfiguration();

            Log.Logger = new LoggerConfiguration()
            .WriteTo.Seq(_logSettings.SeqServerUrl)
            .CreateLogger();
        }

        // --------------------------------------------------------------------------
        // Methods
        // --------------------------------------------------------------------------

        public static Logger GetLoggerInstance()
        {
            if (m_LoggerInstance == null)
                m_LoggerInstance = new Logger();

            return m_LoggerInstance;
        }

        private LogSettings? GetConfiguration()
        {
            return _configurationManager?.GetConfiguration<LogSettings>("LoggerSettings");
        }

        public void LogInformation(string message)
        {
            Log.Logger.Information(message);
        }

        public void LogError(string message)
        {
            Log.Logger.Error(message);
        }

        public void LogError(string message, params object?[]? propertyValue)
        {
            Log.Logger.Error(message, propertyValue);
        }

        public void LogError(Exception? exception, string messageTemplate, params object?[]? propertyValue)
        {
            Log.Logger.Error(exception, messageTemplate, propertyValue);
        }

        public void LogFatal(string message)
        {
            Log.Logger.Fatal(message);
        }

        public void LogFatal(Exception? exception, string messageTemplate)
        {
            Log.Logger.Fatal(exception, messageTemplate);
        }

        // --------------------------------------------------------------------------
        // Overrides
        // --------------------------------------------------------------------------
    }
}

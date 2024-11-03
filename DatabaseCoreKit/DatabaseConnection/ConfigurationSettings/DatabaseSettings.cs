namespace DatabaseCoreKit.DatabaseConnection.ConfigurationSettings
{
    using Common.ConfigurationManager;

    internal class DatabaseSettings : IConfigurationSettings
    {
        public int MaxPoolConnections { get; set; }
    }
}

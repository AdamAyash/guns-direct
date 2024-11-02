using Common.ConfigurationManager;

namespace DatabaseCoreKit.DatabaseConnection.ConfigurationSettings
{
    internal class DatabaseSettings : IConfigurationSettings
    {
        public int MaxPoolConnections { get; set; }
    }
}

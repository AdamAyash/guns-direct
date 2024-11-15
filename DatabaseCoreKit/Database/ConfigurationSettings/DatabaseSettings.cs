namespace DatabaseCoreKit
{
    using Common;

    internal class DatabaseSettings : IConfigurationSettings
    {
        public int MaxPoolConnections { get; set; }
        public string? DatabaseXMLSchemePath { get; set; }
    }
}

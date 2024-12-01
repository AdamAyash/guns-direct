namespace DatabaseCoreKit
{
    #region
    using Common;
    #endregion

    /// <summary></summary>
    internal class DatabaseSettings : IConfigurationSettings
    {
        public int MaxPoolConnections { get; set; }
        public string? DatabaseXMLSchemePath { get; set; }
    }
}

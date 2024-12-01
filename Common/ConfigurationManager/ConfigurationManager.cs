namespace Common
{
    #region
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.FileProviders;
    using Serilog;
    #endregion

    /// <summary></summary>s
    public sealed class ConfigurationManager
    {
        // --------------------------------------------------------------------------
        // Constants
        // --------------------------------------------------------------------------
        
        private const string m_strFileName = "appsettings.json";

        // --------------------------------------------------------------------------
        // Members
        // --------------------------------------------------------------------------
       
        private IConfigurationRoot? m_oConfigurationRoot;
        private IFileProvider? m_oConfigurationFileProvider;

        private string? m_strFilePath;

        private readonly bool m_bIsConfigurationOptional = false;
        private readonly bool m_bIsReloadOnChange = false;

        // --------------------------------------------------------------------------
        // Properties
        // --------------------------------------------------------------------------

        // --------------------------------------------------------------------------
        // Constructor
        // --------------------------------------------------------------------------
        public ConfigurationManager()
        {
            BuildConfiguration();
        }

        // --------------------------------------------------------------------------
        // Methods
        // --------------------------------------------------------------------------
        private void BuildConfiguration()
        {
            m_strFilePath = Path.Combine(Directory.GetCurrentDirectory(), m_strFileName);

            m_oConfigurationRoot = new ConfigurationBuilder()
               .AddJsonFile(m_strFilePath, m_bIsConfigurationOptional, m_bIsReloadOnChange).Build();
        }

        public ConfigurationSettings? GetConfiguration<ConfigurationSettings>(string strConfigurationSectionName)
            where ConfigurationSettings : IConfigurationSettings
        {
            return m_oConfigurationRoot!.GetRequiredSection(strConfigurationSectionName).Get<ConfigurationSettings>();
        }

        public string? GetConnectionString(string connectionStringName)
        {
            return m_oConfigurationRoot?.GetConnectionString(connectionStringName);
        }

        // --------------------------------------------------------------------------
        // Overrides
        // --------------------------------------------------------------------------
    }
}

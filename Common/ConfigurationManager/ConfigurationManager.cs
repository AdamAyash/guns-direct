using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

namespace Common.ConfigurationManager
{
    public class ConfigurationManager
    {
        private const string m_strFileName = "appsettings.json";

        private IConfigurationRoot m_oConfigurationRoot;
        private IFileProvider m_oConfigurationFileProvider;

        private string m_strFilePath;

        private readonly bool m_bIsConfigurationOptional = false;
        private readonly bool m_bIsReloadOnChange = false;

        public ConfigurationManager()
        {
            BuildConfiguration();
        }

        private void BuildConfiguration()
        {
            m_strFilePath = Path.Combine(Directory.GetCurrentDirectory(), m_strFileName);
            
            m_oConfigurationRoot = new ConfigurationBuilder()
               .AddJsonFile(m_strFilePath, m_bIsConfigurationOptional, m_bIsReloadOnChange).Build();
        }

        public ConfigurationSettings? GetConfiguration<ConfigurationSettings>(string strConfigurationSectionName)
            where ConfigurationSettings : IConfigurationSettings
        {
            return m_oConfigurationRoot.GetRequiredSection(strConfigurationSectionName).Get<ConfigurationSettings>();
        }

        public string? GetConnectionString(string connectionStringName)
        {
            return m_oConfigurationRoot?.GetConnectionString(connectionStringName); 
        }
    }
}

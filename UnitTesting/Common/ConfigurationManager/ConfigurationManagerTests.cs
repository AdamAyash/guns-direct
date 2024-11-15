using Common;

namespace UnitTesting.Common
{

    [TestClass]
    public class ConfigurationManagerTests
    {
        private class TestSettingsConfiguration : IConfigurationSettings
        {
        }

        private ConfigurationManager _configurationManager;
        public ConfigurationManagerTests()
        {
            _configurationManager = new ConfigurationManager();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetConfigurationTest()
        {
            var testConfig = _configurationManager.GetConfiguration<TestSettingsConfiguration>("TestConfiguration");
        }

        [TestMethod]
        public void GetConncetionStringTest()
        {
            Assert.IsNotNull(_configurationManager.GetConnectionString("GunsDirectDatabase"));
        }

    }
}

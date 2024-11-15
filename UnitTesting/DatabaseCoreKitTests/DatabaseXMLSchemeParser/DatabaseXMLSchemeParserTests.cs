using DatabaseCoreKit;

namespace UnitTesting.DatabaseCoreKit
{
    [TestClass]
    public class DatabaseXMLSchemeParserTests
    {
        private readonly DatabaseXMLSchemeParser _XMLSchemeParser;

        public DatabaseXMLSchemeParserTests()
        {
            _XMLSchemeParser = new DatabaseXMLSchemeParser();
        }

        [TestMethod]
        public void LoadSchemeTest()
        {
            Assert.IsTrue(_XMLSchemeParser.Process());
        }
    }
}

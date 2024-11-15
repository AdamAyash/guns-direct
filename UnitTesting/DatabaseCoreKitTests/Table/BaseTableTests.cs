using DatabaseCoreKit;

namespace UnitTesting.DatabaseCoreKit
{
    [TestClass]
    public class BaseTableTests
    {
        //private BaseTable<UnitTest, int> _baseTable;

        public BaseTableTests()
        {
            DatabaseXMLSchemeParser databaseXMLSchemeParser = new DatabaseXMLSchemeParser();
            databaseXMLSchemeParser.Process();

            //this._baseTable = new BaseTable<UnitTest, null>("UNIT_TESTS");
        }

        [TestMethod]
        public void SelectAllRecordsTest()
        {
            //var unitTests = new List<UnitTest>();
            //Assert.IsTrue(this._baseTable.SelectAll(unitTests));
        }
    }
}

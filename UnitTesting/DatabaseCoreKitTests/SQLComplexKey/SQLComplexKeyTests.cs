using DatabaseCoreKit;

namespace UnitTesting.DatabaseCoreKit
{
    [TestClass]
    public class SQLComplexKeyTests
    {
        [TestMethod]
        public void SelectAllRecordsWithNoLockComplexKeyTest()
        {
            SQLComplexKey oComplexKey = new SQLComplexKey();
            oComplexKey.SetTableName("TEST");

            string expectedResult = "SELECT * FROM TEST WITH(NOLOCK)";
            string actualResult = oComplexKey.GenerateWhereStatement().Trim();

            CollectionAssert.AreEqual(expectedResult.ToCharArray(), actualResult.ToCharArray());
        }

        [TestMethod]
        public void SelectRecordByIdComplexKeyTest()
        {
            SQLComplexKey oComplexKey = new SQLComplexKey("ID", 1);
            oComplexKey.SetTableName("TEST");

            string expectedResult = "SELECT * FROM TEST WITH(NOLOCK) WHERE ID = 1";
            string actualResult = oComplexKey.GenerateWhereStatement().Trim();

            CollectionAssert.AreEqual(expectedResult.ToCharArray(), actualResult.ToCharArray());
        }

        [TestMethod]
        public void SelectRecordByIdWithUpdateLockComplexKeyTest()
        {
            SQLComplexKey oComplexKey = new SQLComplexKey("ID", 1, CompareTypes.EQUALS ,LockTypes.UPDLOCK);
            oComplexKey.SetTableName("TEST");

            string expectedResult = "SELECT * FROM TEST WITH(UPDLOCK) WHERE ID = 1";
            string actualResult = oComplexKey.GenerateWhereStatement().Trim();

            CollectionAssert.AreEqual(expectedResult.ToCharArray(), actualResult.ToCharArray());
        }

        [TestMethod]
        public void SelectRecordByIdAndNameComplexKeyTest()
        {
            SQLComplexKey oComplexKey = new SQLComplexKey("ID", 1, CompareTypes.EQUALS, LockTypes.UPDLOCK);
            oComplexKey.AddKey(new SQLKey("NAME", "TEST_NAME"));

            oComplexKey.SetTableName("TEST");

            string expectedResult = "SELECT * FROM TEST WITH(UPDLOCK) WHERE ID = 1 AND NAME = \'TEST_NAME\'";
            string actualResult = oComplexKey.GenerateWhereStatement().Trim();

            CollectionAssert.AreEqual(expectedResult.ToCharArray(), actualResult.ToCharArray());
        }
    }
}

using DynVarManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Tests
{
    [TestClass]
    public class Wrong
    {
        private Test GetDefaultTest()
        {
            return new Test(true, false, true);
        }

        [TestMethod]
        [TestCategory("Wrong")]
        public void WrongTestCase()
        {
            GetDefaultTest().runTest(new[] { "../../../Wrong Test Cases/Case X/CG.cs" });            
        }


        [TestMethod]
        [TestCategory("Wrong")]
        public void WrongEDTestCase()
        {
            DynVarOptions.Instance.EverythingDynamic = true;
            GetDefaultTest().runTest(new[] { "../../../Wrong Test Cases/Case X/CG.cs" });
            DynVarOptions.Instance.EverythingDynamic = false;
        }
    }
}

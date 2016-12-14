using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Tests.Evaluation
{
    [TestClass]
    public class Points
    {
        private Test GetDefaultTest()
        {
            return new Test(true, false, true);
        }
        
        [TestMethod]
        [TestCategory("Evaluation"), TestCategory("Points")]
        public void PointsSP()
        {
            GetDefaultTest().runTest();
        }
    }
}

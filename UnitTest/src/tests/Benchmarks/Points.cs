using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Tests.Benchmarks
{
    [TestClass]
    public class Points
    {
        private Test GetDefaultTest()
        {
            return new Test(true, false, true);
        }

        [TestMethod]
        [TestCategory("Benchmarks"), TestCategory("Points")]
        public void Points01()
        {            
            GetDefaultTest().runTest();
        }

        [TestMethod]
        [TestCategory("Benchmarks"), TestCategory("Points")]
        public void Points02()
        {
            GetDefaultTest().runTest();
        }

       
        //[TestMethod]
        [TestCategory("Benchmarks"), TestCategory("Points")]
        public void PointsFull()
        {
            GetDefaultTest().runTest();
        }
    }
}

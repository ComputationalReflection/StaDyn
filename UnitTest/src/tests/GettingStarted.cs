using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Tests
{
    [TestClass]
    public class GettingStarted
    {
        private Test GetDefaultTest()
        {
            return new Test(false, false, false);
        }

        [TestMethod]
        [TestCategory("GettingStarted")]
        public void Sample1()
        {
            GetDefaultTest().runTest();
        }

        [TestMethod]
        [TestCategory("GettingStarted")]
        public void Sample2()
        {
            GetDefaultTest().runTest();
        }

        [TestMethod]
        [TestCategory("GettingStarted")]
        public void Sample3()
        {
            GetDefaultTest().runTest();
        }

        [TestMethod]
        [TestCategory("GettingStarted")]
        public void Sample3b()
        {
            GetDefaultTest().runTest();
        }

        [TestMethod]
        [TestCategory("GettingStarted")]
        public void Sample4()
        {
            GetDefaultTest().runTest();
        }

        [TestMethod]
        [TestCategory("GettingStarted")]
        public void Sample5()
        {
            GetDefaultTest().runTest();
        }

        [TestMethod]
        [TestCategory("GettingStarted")]
        public void Sample6()
        {
            GetDefaultTest().runTest();
        }

        [TestMethod]
        [TestCategory("GettingStarted")]
        public void Sample7()
        {
            GetDefaultTest().runTest();
        }
    }
}

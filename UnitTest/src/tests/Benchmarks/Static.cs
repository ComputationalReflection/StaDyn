using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Tests.Benchmarks
{
    [TestClass]
    public class Static

    {
        private Test GetDefaultTest()
        {
            return new Test(false, false, false);
        }

        [TestMethod]
        [TestCategory("Benchmarks"), TestCategory("Static")]
        public void Pystone()
        {
            GetDefaultTest().runTest();            
        }

        [TestMethod]
        [TestCategory("Benchmarks"), TestCategory("Static"), TestCategory("JavaGrande")]
        public void JGFFFT()
        {
            GetDefaultTest().runTest();
        }

        [TestMethod]
        [TestCategory("Benchmarks"), TestCategory("Static"), TestCategory("JavaGrande")]
        public void JGFNumericSortTest()
        {
            GetDefaultTest().runTest();
        }


        /*
        *   NOTE: To try this test it's necessary to add "process.StartInfo.CreateNoWindow = true" to src/compiler/Program ln 403
        */
        [TestMethod]
        [TestCategory("Benchmarks"), TestCategory("Static"), TestCategory("JavaGrande")]
        public void JGFRayTracer()
        {
            GetDefaultTest().runTest();
        }

        [TestMethod]
        [TestCategory("Benchmarks"), TestCategory("Static"), TestCategory("JavaGrande")]
        public void JGFSparseMatmult()
        {
            GetDefaultTest().runTest();
        }


    }
}

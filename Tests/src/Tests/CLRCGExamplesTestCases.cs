using System.IO;
using DynVarManagement;
using TargetPlatforms;

namespace Tests
{
    class CLRCGExamplesTestCases : Test
    {
            #region Constructor

        /// GenerateCode = true
        /// Run = true
        /// Dynamic = true
        /// Target = "clr"
        /// </summary>
        public CLRCGExamplesTestCases() : base(true, true, true, TargetPlatform.CLR, true) {}
        #endregion


        #region runTest(string[])
        /// <summary>
        /// A code genertion tests calls the run tests the name of the output file
        /// as the name of the first input file, with exe extension
        /// </summary>
        /// <param name="fileNames"></param>
        protected void runTest(string[] fileNames)
        {
            this.runTest(fileNames, Path.ChangeExtension(fileNames[0], ".exe"));
        }
        #endregion

        public void testFigure04()
        {
            runTest(new string[] { "tests/examples/Figure04.stadyn" });
        }
        public void testFigure04b()
        {
            runTest(new string[] { "tests/examples/Figure04b.stadyn" });
        }
        public void testFigure05()
        {
            runTest(new string[] { "tests/examples/Figure05.stadyn" });
        }
        public void testFigure05b()
        {
            runTest(new string[] { "tests/examples/Figure05b.stadyn" });
        }
        public void testFigure07()
        {
            runTest(new string[] { "tests/examples/Figure07.stadyn" });
        }
        public void testFigure10()
        {
            runTest(new string[] { "tests/examples/Figure10.stadyn" });
        }
        public void testFigure10b()
        {
            runTest(new string[] { "tests/examples/Figure10b.stadyn" });
        }
        public void testFigure12()
        {
            runTest(new string[] { "tests/examples/Figure12.stadyn" });
        }
        public void testFigure12b()
        {
            runTest(new string[] { "tests/examples/Figure12b.stadyn" });
        }
        public void testFigure13()
        {
            runTest(new string[] { "tests/examples/Figure13.stadyn" });
        }

	}
}

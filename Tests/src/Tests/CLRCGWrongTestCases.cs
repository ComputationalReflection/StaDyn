using System.IO;
using DynVarManagement;
using TargetPlatforms;

namespace Tests
{
    class CLRCGWrongTestCases : Test
    {
        #region Constructor

        /// GenerateCode = true
        /// Run = true
        /// Dynamic = true
        /// Target = "clr"
        /// </summary>
        public CLRCGWrongTestCases() : base(true, true, true, TargetPlatform.CLR, false)
        {
            this.specialized = true;
        }
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

        public void testX()
        {            
            DynVarOptions.Instance.EverythingDynamic = true;
            runTest(new string[] { "../../../Wrong Test Cases/Case X/CG.cs" });
            DynVarOptions.Instance.EverythingDynamic = false;
        }       
	}
}

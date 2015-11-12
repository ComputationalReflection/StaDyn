using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TargetPlatforms;

namespace UnitTest.Tests.CLRCG
{    
    class CLRCGTest : Test
    {
        #region Constructor
        /// <summary>
        /// GenerateCode = true
        /// Run = true
        /// Target = "clr"
        /// </summary>
        public CLRCGTest(): base(true, true, false, TargetPlatform.CLR, false) { }
        #endregion


        #region runTest(string[])
        /// <summary>
        /// A code genertion tests calls the run tests the name of the output file
        /// as the name of the first input file, with exe extension
        /// </summary>
        /// <param name="fileNames"></param>
        public void runTest(string[] fileNames)
        {
            this.runTest(fileNames, Path.ChangeExtension(fileNames[0], ".exe"));            
        }
        #endregion

    }
}
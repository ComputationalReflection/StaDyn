using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
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


        public void runTest()
        {
            MethodBase method = new StackTrace().GetFrame(1).GetMethod();
            Type declaringType = method.DeclaringType;
            String methodName = declaringType.Namespace.Substring(declaringType.Namespace.LastIndexOf('.')+1) + "/" + declaringType.Name + "/" + method.Name + ".cs";
            this.runTest(new []{ TESTS_PATH + methodName});
            Assert.AreEqual(this.ExpectedErrors, this.ToError - this.FromError, this.ExpectedErrors + " errors expected, " + (this.ToError - this.FromError) + " found.");
            Assert.IsTrue(this.Success); 
        }

        #endregion

    }
}
//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                            
// -------------------------------------------------------------------------- 
// File: CLRCodeGenerationTest.cs                                                             
// Author: Francisco Ortin  -  francisco.ortin@gmail.com                    
// Description:                                                               
//    Abstract class that generalizes all the code generation tests to
//            generate IL code for the CLR.                    
// -------------------------------------------------------------------------- 
// Create date: 22-08-2007                                                    
// Modification date: 22-08-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using ErrorManagement;
using TargetPlatforms;

namespace Tests {
    abstract class CLRCodeGenerationTest : Test {
        #region Constructor
        /// GenerateCode = true
        /// Run = true
        /// Target = "clr"
        /// </summary>
        public CLRCodeGenerationTest(): base(true, true,false, TargetPlatform.CLR,false) {
        }
        #endregion


        #region runTest(string[])
        /// <summary>
        /// A code genertion tests calls the run tests the name of the output file
        /// as the name of the first input file, with exe extension
        /// </summary>
        /// <param name="fileNames"></param>
        protected void runTest(string[] fileNames) {
            this.runTest(fileNames, Path.ChangeExtension(fileNames[0], ".exe"));
        }
        #endregion

   }
}

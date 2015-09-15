//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                            
// -------------------------------------------------------------------------- 
// File: SemanticTest.cs                                                             
// Author: Francisco Ortin  -  francisco.ortin@gmail.com                    
// Description:                                                               
//    Abstract class that generalizes all the semantic tests.                                     
// -------------------------------------------------------------------------- 
// Create date: 22-08-2007                                                    
// Modification date: 22-08-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using ErrorManagement;
using TargetPlatforms;

namespace Tests {
    abstract class SemanticTest: Test {
        #region Constructor
        /// <summary>
        /// GenerateCode = false
        /// Run = false
        /// Target = null
        /// </summary>
        public SemanticTest() :base(false,false,false,TargetPlatform.CLR) {
        }
        #endregion

        #region runTest(string[])
        /// <summary>
        /// A semantic test calls the run tests without an output filename
        /// </summary>
        /// <param name="fileNames"></param>
        protected void runTest(string[] fileNames) {
            this.runTest(fileNames, null);
        }
        #endregion
   }
}

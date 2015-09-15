//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                            
// -------------------------------------------------------------------------- 
// File: MainTest.cs                                                             
// Author: Francisco Ortin  -  francisco.ortin@gmail.com                    
// Description:                                                               
//    This test only adds the Main method of the whole testing project.                                     
// -------------------------------------------------------------------------- 
// Create date: 01-06-2007                                                    
// Modification date: 01-06-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using ErrorManagement;

namespace Tests {
    class SemanticMainTest : SemanticTest {
        public void testMain() {
            runTest(new string[] { "tests/semantic/test.Main.cs" });
        }
    }
}
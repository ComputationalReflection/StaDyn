//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                            
// -------------------------------------------------------------------------- 
// File: WrongExplicitTests.cs                                                             
// Author: Francisco Ortin  -  francisco.ortin@gmail.com                    
// Description:                                                               
//    Testing of wrong programs without implicit type inference.
// -------------------------------------------------------------------------- 
// Create date: 12-04-2007                                                    
// Modification date: 12-04-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace Tests {
    class WrongExplicitTests : SemanticTest {
        public void testWrongBCLBuiltInTypes() {
            runTest(new string[] { "tests/wrong/testWrongExplicit.BCLBuiltInTypes.cs" });
        }


    }
}
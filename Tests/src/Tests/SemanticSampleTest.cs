//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: ASTTest.cs                                                             
// Author: Francisco Ortin  -  francisco.ortin@gmail.com                    
// Description:                                                               
//    A sample file to rapidly test parts of the compiler.
// -------------------------------------------------------------------------- 
// Create date: 04-04-2007                                                    
// Modification date: 04-04-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace Tests {
    class SemanticSampleTest : SemanticTest {

        public  void testSample() {
            runTest(new string[] { 
                //"tests/semantic/VarWrap.cs", 
                "tests/semantic/sample.cs",  
                       });
        }



    }
}

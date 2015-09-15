//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                            
// -------------------------------------------------------------------------- 
// File: WrongDynamicsTests.cs                                                             
// Author: Francisco Ortin  -  francisco.ortin@gmail.com                    
// Description:                                                               
//    Testing of wrong dynamic implicit var references.                                     
// -------------------------------------------------------------------------- 
// Create date: 18-05-2007                                                    
// Modification date: 18-05-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace Tests {
    class WrongDynamicsTests : SemanticTest {

        public void testWrongDynamicsCommonOperatorConstraints() {
            runTest(new string[] { "tests/wrong/testWrongDynamics.CommonOperators.cs" });
        }

        public void testWrongDynamicsArray() {
            runTest(new string[] { "tests/wrong/testWrongDynamics.Array.cs" });
        }

        public void testWrongDynamicsAttributes() {
            runTest(new string[] { "tests/wrong/Figures.cs", "tests/wrong/testWrongDynamics.Attributes.cs" });
        }

        public void testWrongDynamicsParameters() {
            runTest(new string[] { "tests/wrong/Figures.cs", "tests/wrong/testWrongDynamics.Parameters.cs" });
        }

        public void testWrongDynamicsPromotion() {
            runTest(new string[] { "tests/wrong/Figures.cs", "tests/wrong/testWrongDynamics.Promotion.cs" });
        }

    }
}
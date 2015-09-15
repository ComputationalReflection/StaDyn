//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                            
// -------------------------------------------------------------------------- 
// File: DynamicsTest.cs                                                             
// Author: Francisco Ortin  -  francisco.ortin@gmail.com                    
// Description:                                                               
//    Testing of dynamic implicit var references.                                     
// -------------------------------------------------------------------------- 
// Create date: 18-05-2007                                                    
// Modification date: 18-05-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace Tests {
    class SemanticDynamicsTest : SemanticTest {

        public void testDynamicsCommonOperatorConstraints() {
            runTest(new string[] { "tests/semantic/testDynamics.CommonOperators.cs" });
        }

        public void testDynamicsArray() {
            runTest(new string[] { "tests/semantic/testDynamics.Array.cs" });
        }


        public void testDynamicsAttributes() {
            runTest(new string[] { "tests/semantic/Figures.cs", "tests/semantic/testDynamics.Attributes.cs" });
        }

        public void testDynamicsParameters() {
            runTest(new string[] { "tests/semantic/Figures.cs", "tests/semantic/testDynamics.Parameters.cs" });
        }

        public void testDynamicsPromotion() {
            runTest(new string[] { "tests/semantic/Figures.cs", "tests/semantic/testDynamics.Promotion.cs" });
        }

    }
}
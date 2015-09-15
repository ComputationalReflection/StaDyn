//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: ExplicitTest.cs                                                             
// Author: Francisco Ortin  -  francisco.ortin@gmail.com                    
// Description:                                                               
//    Testing of semantic analysis of explicit types (exluding type variables)
// -------------------------------------------------------------------------- 
// Create date: 04-04-2007                                                    
// Modification date: 04-04-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace Tests {
    class SemanticExplicitTest : SemanticTest {

        public void testBCLBuiltInTypes() {
            runTest(new string[] { "tests/semantic/testExplicit.BCLBuiltInTypes.cs" });
        }

        public void testOverload() {
            runTest(new string[] { "tests/semantic/testExplicit.Overload.cs" });
        }

        public void testBCL() {
            runTest(new string[] { "tests/semantic/testExplicit.BCL.cs" });
        }

        public void testBase() {
            runTest(new string[] { "tests/semantic/testExplicit.Base.cs" });
        }

        public void testTernary() {
            runTest(new string[] { "tests/semantic/testExplicit.Ternary.cs" });
        }

        public void testStringAddition() {
            runTest(new string[] { "tests/semantic/testExplicit.StringAddition.cs" });
        }

        public void testNamespacesAndMembers() {
            runTest(new string[] { "tests/semantic/testExplicit.NamespaceAndStatic.cs" });
        }

        public void testCast() {
            runTest(new string[] { "tests/semantic/testExplicit.Cast.cs" });
       }

    }
}

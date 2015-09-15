//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: CLRCGVarSemanticTests.cs                                                             
// Author: Cristina Gonzalez Muñoz - cristi.gm@gmail.com                     
// Description:                                                               
//    Testing of the implicit features of the language, compiled
//            into the CLR platform.
// -------------------------------------------------------------------------- 
// Create date: 29-10-2007                                                    
// Modification date: 29-10-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Tests {
    class CLRCGVarSemanticTests : CLRCodeGenerationTest {

        public void testCGSemanticVarDynamicsAttributes() {
            runTest(new string[] { "tests/code.generation/Semantic/CG.testDynamics.Attributes.cs", "tests/code.generation/Semantic/Figures.cs" });
        }

        public void testCGSemanticVarDynamicsPromotion() {
            runTest(new string[] { "tests/code.generation/Semantic/CG.testDynamics.Promotion.cs", "tests/code.generation/Semantic/Figures.cs" });
        }

        public void testCGSemanticVarDynamicsArray() {
            runTest(new string[] { "tests/code.generation/Semantic/CG.testDynamics.Array.cs" });
        }

        public void testCGSemanticVarDynamicsArray2() {
            runTest(new string[] { "tests/code.generation/Semantic/CG.testDynamics.Array2.cs" });
        }

        public void testCGVarSemanticParametersAndReturn() {
            runTest(new string[] { "tests/code.generation/Semantic/CG.testVar.ParametersAndReturn.cs" });
        }

        public void testCGVarSemanticStaticUnionTypes1() {
            runTest(new string[] { "tests/code.generation/Semantic/CG.testVar.StaticUnionTypes1.cs", "tests/code.generation/Semantic/Figures.cs" });
        }

        public void testCGVarSemanticDynamicsCommonOperators() {
            runTest(new string[] { "tests/code.generation/Semantic/CG.testDynamics.CommonOperators.cs" });
        }

        public void testCGVarSemanticExplicitBase() {
            runTest(new string[] { "tests/code.generation/Semantic/CG.testExplicit.Base.cs" });
        }

        public void testCGVarSemanticExplicitCast() {
            runTest(new string[] { "tests/code.generation/Semantic/CG.testExplicit.Cast.cs" });
        }

        public void testCGVarSemanticExplicitOverload() {
            runTest(new string[] { "tests/code.generation/Semantic/CG.testExplicit.Overload.cs" });
        }

        public void testCGVarSemanticExplicitStringAddition() {
            runTest(new string[] { "tests/code.generation/Semantic/CG.testExplicit.StringAddition.cs" });
        }

        public void testCGVarSemanticExplicitTernary() {
            runTest(new string[] { "tests/code.generation/Semantic/CG.testExplicit.Ternary.cs" });
        }

        public void testCGVarSemanticExplicitBCL() {
            runTest(new string[] { "tests/code.generation/Semantic/CG.testExplicit.BCL.cs" });
        }
        //TODO: Este test deberia salir correcgto el problemq es que hay varios entry-point definidos
        //public void testCGVar() {
        //    runTest(new string[] { "tests/code.generation/Semantic/" });
        //}


    }
}

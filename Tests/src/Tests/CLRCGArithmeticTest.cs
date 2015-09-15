////////////////////////////////////////
// -------------------------------------------------------------------------- /
// Project Stadyn                                                             /
// -------------------------------------------------------------------------- /
// File: CLRArithmeticTest .cs                                                /
// Author: Daniel Zapico Rodríguez daniel.zapico@rodriguez                    /
// Description:                                                               /
//    Testing of code arithmetic operations                                   /
// additionalfeatures of the language, compiled into the CLR 2.0 platform.                                            /
// -------------------------------------------------------------------------- /
// Create date: 20-11-2010                                                    /
////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Tests {
    class CLRCGArithmeticTest : CLRCodeGenerationTest
    {    
        public void testCGArithmeticLiterals()
        {
            runTest(new string[] { "tests/code.generation/Arithmetic/CG.ArithmeticLiterals.cs" });
        }

        public void testCGArithmeticUnionAndLiteral()
        {
            runTest(new string[] { "tests/code.generation/Arithmetic/CG.ArithmeticUnionAndLiteral.cs" });
        }

        public void testCGArithmeticIds()
        {
            runTest(new string[] { "tests/code.generation/Arithmetic/CG.ArithmeticIds.cs" });
        }
        public void testArithmPlusChar()
        {
            runTest(new string[] { "tests/code.generation/Arithmetic/CG.ArithmPlusChar.cs" });
        }


        public void testBugSemantic()
        {
            runTest(new string[] { "tests/code.generation/Arithmetic/CG.ArithmeticUnionAndLiteral.cs.no.funciona.con.metodos.semantic.error.cs" });
        }

        public void testCGCommonOperatorConstraints()
        {
            runTest(new string[] { "tests/code.generation/Arithmetic/CG.Var.CommonOperatorConstraints.cs" });
        }

        public void testCGExplicitStringAddition()
        {
            runTest(new string[] { "tests/code.generation/Arithmetic/CG.Explicit.StringAddition.cs" });
        }
        
    }      
}


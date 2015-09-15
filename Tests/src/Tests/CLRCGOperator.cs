////////////////////////////////////////
// -------------------------------------------------------------------------- /
// Project Stadyn                                                             /
// -------------------------------------------------------------------------- /
// File: CLRCGParameterTest .cs                                               /
// Author: Daniel Zapico Rodríguez daniel.zapico@rodriguez                    /
// Description:                                                               /
//    Testing of operation features                                           /
// additionalfeatures of the language, compiled into the CLR 2.0 platform.    /
// -------------------------------------------------------------------------- /
// Create date: 30-11-2010                                                    /
////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Tests {
    class CLRCGOperatorTest : CLRCodeGenerationTest
    {
        public void testCGExplicitTernary()
        {
            runTest(new string[] { "tests/code.generation/Operator/CG.Explicit.Ternary.cs" });
        }

        public void testCGVarBracketConstraints()
        {
            runTest(new string[] { "tests/code.generation/Operator/CG.Var.BracketConstraints.cs" });
        }

        public void testCGVarBracketConstraints2()
        {
            runTest(new string[] { "tests/code.generation/Operator/CG.Var.BracketConstraints2.cs" });
        }   
    }      
}


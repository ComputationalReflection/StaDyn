////////////////////////////////////////// -------------------------------------------------------------------------- /
// Project Stadyn                                                             /
// -------------------------------------------------------------------------- /
// File: CLRCGCastingTest .cs                                                /
// Author: Daniel Zapico Rodr¡guez daniel.zapico@rodriguez                    /
// Description:                                                               /
//    Checking the behaviour of the generation of casting code/
// compiled into the CLR 2.0 platform.                                            /
// -------------------------------------------------------------------------- /
// Create date: 20-11-2010                                                    /
////////////////////////////////////////

using System;

using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Tests
{
    class CLRCGCastingTest : CLRCodeGenerationTest
    {
        public void testCCGCasting()
        {
            runTest(new string[] { "tests/code.generation/Cast/CG.Casting.cs" });
        }
        public void testCCGImplicitConversion()
        {
            runTest(new string[] { "tests/code.generation/Cast/CG.ImplicitConversion.cs" });
        }
        public void testCCGImplicitConversion2()
        {
            runTest(new string[] { "tests/code.generation/Cast/CG.ImplicitConversion2.cs" });
        }
        public void testCGExplicitCast()
        {
            runTest(new string[] { "tests/code.generation/Cast/CG.Explicit.Cast.cs" });
        }
        public void testCGVarCast()
        {
            runTest(new string[] { "tests/code.generation/Cast/CG.Var.Cast.cs" });
        }
    }
}
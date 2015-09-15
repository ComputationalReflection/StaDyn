////////////////////////////////////////// -------------------------------------------------------------------------- /
// Project Stadyn                                                             /
// -------------------------------------------------------------------------- /
// File: CLRArithmeticTest .cs                                                /
// Author: Daniel Zapico Rodr¡guez daniel.zapico@rodriguez                    /
// Description:                                                               /
//    Generating code in property access// compiled into the CLR 2.0 platform.                                            /
// -------------------------------------------------------------------------- /
// Create date: 20-11-2010                                                    /
////////////////////////////////////////

using System;

using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Tests
{
    class CLRCGPropertyTest : CLRCodeGenerationTest
    {
        public void testCGProperties()
        {
            runTest(new string[] { "tests/code.generation/Property/CG.Properties.cs" });
        }

        public void testCGProperty()
        {
            runTest(new string[] { "tests/code.generation/Property/CG.property.cs" });
        }

        public void testCGDynamicsAttributes()
        {
            runTest(new string[] { "tests/code.generation/Property/CG.Dynamics.Attributes.cs" });
        }
        public void testCGVarAttributes()
        {
            runTest(new string[] { "tests/code.generation/Property/CG.Var.Attributes.cs" });
        }        
	}
}

////////////////////////////////////////// -------------------------------------------------------------------------- /
// Project Stadyn                                                             /
// -------------------------------------------------------------------------- /
// File: CLRCGScopeTest .cs                                                /
// Author: Daniel Zapico Rodr¡guez daniel.zapico@rodriguez                    /
// Description:                                                               /
//    Sample code generation with scope resolution// compiled into the CLR 2.0 platform.                                            /
// -------------------------------------------------------------------------- /
// Create date: 20-11-2010                                                    /
////////////////////////////////////////

using System;

using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Tests
{
    class CLRCGScopeTest : CLRCodeGenerationTest
    {

		public void testCcgscope1() {
			runTest(new string[] { "tests/code.generation/Scope/cg.scope1.cs" });
		}
		public void testCcgscope2() {
			runTest(new string[] { "tests/code.generation/Scope/cg.scope2.cs" });
		}
		public void testCCGScope3() {
			runTest(new string[] { "tests/code.generation/Scope/CG.Scope3.cs" });
		}
	}
}

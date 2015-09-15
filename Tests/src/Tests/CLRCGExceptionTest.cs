////////////////////////////////////////// -------------------------------------------------------------------------- /
// Project Stadyn                                                             /
// -------------------------------------------------------------------------- /
// File: CLRCGExceptionTest .cs                                                /
// Author: Daniel Zapico Rodr¡guez daniel.zapico@rodriguez                    /
// Description:                                                               /
//    Checking the correct behaviour of conditionals// compiled into the CLR 2.0 platform.                                            /
// -------------------------------------------------------------------------- /
// Create date: 20-11-2010                                                    /
////////////////////////////////////////

using System;

using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Tests
{
    class CLRCGExceptionTest : CLRCodeGenerationTest
    {
		public void testCCGException1() {
			runTest(new string[] { "tests/code.generation/Exception/CG.Exception1.cs" });
		}
		public void testCCGException2() {
			runTest(new string[] { "tests/code.generation/Exception/CG.Exception2.cs" });
		}
		public void testCCGException3() {
			runTest(new string[] { "tests/code.generation/Exception/CG.Exception3.cs" });
		}
        public void testCCGException4()
        {
            runTest(new string[] { "tests/code.generation/Exception/CG.Exception4.cs" });
        }
        public void testCcgexception5()
        {
            runTest(new string[] { "tests/code.generation/Exception/cg.exception5.cs" });
        }
        public void testCCGException6()
        {
            runTest(new string[] { "tests/code.generation/Exception/CG.Exception6.cs" });
        }
        public void testCCGException7()
        {
            runTest(new string[] { "tests/code.generation/Exception/CG.Exception7.cs" });
        }
        public void testCCGException8()
        {
            runTest(new string[] { "tests/code.generation/Exception/CG.Exception8.cs" });
        }
        public void testCCGException9()
        {
            runTest(new string[] { "tests/code.generation/Exception/CG.Exception9.cs" });
        }
	}
}

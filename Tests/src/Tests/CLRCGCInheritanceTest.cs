////////////////////////////////////////// -------------------------------------------------------------------------- /
// Project Stadyn                                                             /
// -------------------------------------------------------------------------- /
// File: CLRCGInheritanceTest .cs                                                /
// Author: Daniel Zapico Rodr¡guez daniel.zapico@rodriguez                    /
// Description:                                                               /
//    Generating code in inheritance trees// compiled into the CLR 2.0 platform.                                            /
// -------------------------------------------------------------------------- /
// Create date: 20-11-2010                                                    /
////////////////////////////////////////

using System;

using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Tests
{
    class CLRCGInheritanceTest : CLRCodeGenerationTest
    {

		public void testCCGFigures() {
			runTest(new string[] { "tests/code.generation/Inheritance/CG.Figures.cs" });
		}
        public void testCCGInterfaceInheritance1()
        {
            runTest(new string[] { "tests/code.generation/Inheritance/CG.InterfaceInheritance1.cs" });
        }
        public void testCCGInterfaceInheritance2()
        {
            runTest(new string[] { "tests/code.generation/Inheritance/CG.InterfaceInheritance2.cs" });
        }
        public void testCCGInterfaceInheritance3()
        {
            runTest(new string[] { "tests/code.generation/Inheritance/CG.InterfaceInheritance3.cs" });
        }
        public void testCCGInterfaceInheritance4()
        {
            runTest(new string[] { "tests/code.generation/Inheritance/CG.InterfaceInheritance4.cs" });
        }
        public void testCCGInterfaceVar1()
        {
            runTest(new string[] { "tests/code.generation/Inheritance/CG.InterfaceVar1.cs" });
        }
        public void testCCGSimpleAbstractInheritance1()
        {
            runTest(new string[] { "tests/code.generation/Inheritance/CG.SimpleAbstractInheritance1.cs" });
        }
        public void testCCGSimpleAbstractInheritance2()
        {
            runTest(new string[] { "tests/code.generation/Inheritance/CG.SimpleAbstractInheritance2.cs" });
        }
        public void testCCGSimpleAbstractInheritance3()
        {
            runTest(new string[] { "tests/code.generation/Inheritance/CG.SimpleAbstractInheritance3.cs" });
        }
	}
}

////////////////////////////////////////// -------------------------------------------------------------------------- /
// Project Stadyn                                                             /
// -------------------------------------------------------------------------- /
// File: CLRArrayTest .cs                                                /
// Author: Daniel Zapico Rodr¡guez daniel.zapico@rodriguez                    /
// Description:                                                               /
//    Testing of codegeneation using arrays/
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
    class CLRCGArrayTest : CLRCodeGenerationTest
    {
        public void testCGarray()
        {
            runTest(new string[] { "tests/code.generation/Arrays/cg.array.cs" });
        }
        public void testCGarray2()
        {
            runTest(new string[] { "tests/code.generation/Arrays/CG.array2.cs" });
        }
        public void testCGarray3()
        {
            runTest(new string[] { "tests/code.generation/Arrays/CG.array3.cs" });
        }
        public void testCGarray4()
        {
            runTest(new string[] { "tests/code.generation/Arrays/CG.array4.cs" });
        }
        public void testCGarray5()
        {
            runTest(new string[] { "tests/code.generation/Arrays/CG.array5.cs" });
        }
        public void testCGArrayConstruction()
        {
            runTest(new string[] { "tests/code.generation/Arrays/CG.ArrayConstruction.cs" });
        }
        public void testCGArraySize()
        {
            runTest(new string[] { "tests/code.generation/Arrays/CG.ArraySize.cs" });
        }
        public void testCGArrayTest()
        {
            runTest(new string[] { "tests/code.generation/Arrays/CG.ArrayTest.cs" });
        }
        public void testCGtestDynamicsArray2()
        {
            runTest(new string[] { "tests/code.generation/Arrays/CG.Dynamics.Array2.cs" });
        }
        public void testCGVarAbstractInheritance1()
        {
            runTest(new string[] { "tests/code.generation/Arrays/CG.VarAbstractInheritance1.cs" });
        }
        public void testCGVarAbstractInheritance2()
        {
            runTest(new string[] { "tests/code.generation/Arrays/CG.VarAbstractInheritance2.cs" });
        }

        public void testCGVarAbstractInheritance2a()
        {
            runTest(new string[] { "tests/code.generation/Arrays/CG.VarAbstractInheritance2a.cs" });
        }
        public void testCGVarAbstractInheritance2b()
        {
            runTest(new string[] { "tests/code.generation/Arrays/CG.VarAbstractInheritance2b.cs" });
        }

        public void testCGVarParameterArray()
        {
            runTest(new string[] { "tests/code.generation/Arrays/CG.VarParameterArray.cs" });
        }

        public void testCGVarAbstractInheritance3()
        {

            runTest(new string[] { "tests/code.generation/Arrays/CG.VarAbstractInheritance3.cs" });
        }

        public void testCGVarAbstractInheritance4()
        {
            runTest(new string[] { "tests/code.generation/Arrays/CG.VarAbstractInheritance4.cs" });
        }
	}
}

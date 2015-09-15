////////////////////////////////////////// -------------------------------------------------------------------------- /
// Project Stadyn                                                             /
// -------------------------------------------------------------------------- /
// File: CLRCGUnionTypes.cs                                                /
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
    class CLRCGUnionTypes : CLRCodeGenerationTest {

        public void testCCGUnionTypes1()
        {
            runTest(new string[] { "tests/code.generation/UnionTypes/CG.UnionTypes1.cs" });
        }
        public void testCCGUnionTypes2()
        {
            runTest(new string[] { "tests/code.generation/UnionTypes/CG.UnionTypes2.cs" });
        }

        public void testCCGUnionTypes3()
        {
            runTest(new string[] { "tests/code.generation/UnionTypes/CG.UnionTypes3.cs" });
        }
        
        public void testCCGUnionTypes4()
        {
            runTest(new string[] { "tests/code.generation/UnionTypes/CG.UnionTypes4.cs" });
        }
        public void testCCGUnionTypes5()
        {
            runTest(new string[] { "tests/code.generation/UnionTypes/CG.UnionTypes5.cs" });
        }
        public void testCCGUnionTypes6()
        {
            runTest(new string[] { "tests/code.generation/UnionTypes/CG.UnionTypes6.cs" });
        }
        public void testCCGUnionTypes7()
        {
            runTest(new string[] { "tests/code.generation/UnionTypes/CG.UnionTypes7.cs" });
        }
        public void testCCGUnionTypes8()
        {
            runTest(new string[] { "tests/code.generation/UnionTypes/CG.UnionTypes8.cs" });
        }
        public void testCCGvarUnifiedReferenceNoUnion()
        {
            runTest(new string[] { "tests/code.generation/UnionTypes/CG.var.UnifiedReferenceNoUnion.cs" });
        }
        public void testCCGvarUnifiedReferenceNoUnion2()
        {
            runTest(new string[] { "tests/code.generation/UnionTypes/CG.var.UnifiedReferenceNoUnion2.cs" });
        }
        public void testCCGvarUnionTypeDyn()
        {
            runTest(new string[] { "tests/code.generation/UnionTypes/CG.var.UnionTypeDyn.cs" });
        }
        public void testCCGvarUnionTypeSta()
        {
            runTest(new string[] { "tests/code.generation/UnionTypes/CG.var.UnionTypeSta.cs" });
        }
        public void testCGVarDynamicUnionTypes()
        {
            runTest(new string[] { "tests/code.generation/UnionTypes/CG.Var.DynamicUnionTypes.cs" });
        }
        public void testCGVarStaticUnionTypes()
        {
            runTest(new string[] { "tests/code.generation/UnionTypes/CG.Var.StaticUnionTypes.cs" });
        }

    }
}

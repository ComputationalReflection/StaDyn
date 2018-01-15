////////////////////////////////////////// -------------------------------------------------------------------------- /
// Project Stadyn                                                             /
// -------------------------------------------------------------------------- /
// File: CLRCGCollectionsTest .cs                                                /
// Author: Daniel Zapico Rodr¡guez daniel.zapico@rodriguez                    /
// Description:                                                               /
//    Using of Collections/
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
    class CLRCGCollectionsTest : CLRCodeGenerationTest
    {        
        public void testCGCollections()
        {
            runTest(new string[] { "tests/code.generation/Collections/CG.Collections.cs" });
        }
        public void testCGCollectionsClass()
        {
            runTest(new string[] { "tests/code.generation/Collections/CG.CollectionsClass.cs" });
        }
        public void testCGCollectionsFields()
        {
            runTest(new string[] { "tests/code.generation/Collections/CG.CollectionsFields.cs" });
        }
        public void testCGCustomStack()
        {
            runTest(new string[] { "tests/code.generation/Collections/CG.CustomStack.cs" });
        }

        //public void testCGVARCustomStack()
        //{
        //    runTest(new string[] { "tests/code.generation/Collections/CG.Var.CustomStack.cs" });
        //}
        //public void testCGVARCustomStack2()
        //{
        //    runTest(new string[] { "tests/code.generation/Collections/CG.Var.CustomStack2.cs" });
        //}
	}
}

////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: CLRCGTest .cs                                                        //
// Author: Daniel Zapico Rodríguez -- eldani@gmail.com                        //
// Description:                                                               //
//    Testing of code generation without implicit features of the language,   //
// compiled into the CLR platform.                                            //
// -------------------------------------------------------------------------- //
// Create date: 30-11-2010                                                    //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Tests
{
    class CLRCGTYpes : CLRCodeGenerationTest {
        public void testCGExplicitBCLBuiltInTypes() {
            runTest(new string[] { "tests/code.generation/Types/CG.Explicit.BCLBuiltInTypes.cs" });
        }
        public void testCGExplicitBase() {
            runTest(new string[] { "tests/code.generation/Types/CG.Explicit.Base.cs" });
        }
        public void testCGExplicitBCL() {
            runTest(new string[] { "tests/code.generation/Types/CG.Explicit.BCL.cs" });
        }

   }
}

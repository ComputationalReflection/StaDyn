////////////////////////////////////////
// -------------------------------------------------------------------------- /
// Project Stadyn                                                             /
// -------------------------------------------------------------------------- /
// File: CLRCGParameterTest .cs                                               /
// Author: Daniel Zapico Rodríguez daniel.zapico@rodriguez                    /
// Description:                                                               /
//    Testing of promotion operations features                                /
// additionalfeatures of the language, compiled into the CLR 2.0 platform.    /
// -------------------------------------------------------------------------- /
// Create date: 30-11-2010                                                    /
////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Tests {
    class CLRCGPromotionTest : CLRCodeGenerationTest
    {
        public void testCGDynamicsParameterscs()
        {
            runTest(new string[] { "tests/code.generation/Parameter/CG.Dynamics.Parameters.cs" });
        }

        public void testCGVarParametersAndReturn()
        {
            runTest(new string[] { "tests/code.generation/Parameter/CG.Var.ParametersAndReturn.cs" });
        }

        
    }      
}


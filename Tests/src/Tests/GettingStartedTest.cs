//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                            
// -------------------------------------------------------------------------- 
// File: GettingStarted.cs                                                             
// Author: Francisco Ortin  -  francisco.ortin@gmail.com                    
// Description:                                                               
//    Testing the getting started files.                                     
// -------------------------------------------------------------------------- 
// Create date: 22-01-2008                                                    
// Modification date: 22-01-2008                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace Tests {
    class GettingStartedTest : CLRCodeGenerationTest {
        public void testSample11()
        {
            runTest(new string[] { "tests/getting.started/sample11.cs" });
        }

        public void testSample1()
        {
            runTest(new string[] { "tests/getting.started/sample1.cs" });
        }

        public void testSample2()
        {
            runTest(new string[] { "tests/getting.started/sample2.cs" });
        }

        public void testSample3()
        {
            runTest(new string[] { "tests/getting.started/sample3.cs" });
        }

        public void testSample4()
        {
            runTest(new string[] { "tests/getting.started/sample4.cs" });
        }

        public void testSample5()
        {
            runTest(new string[] { "tests/getting.started/sample5.cs" });
        }

        public void testSample6()
        {
            runTest(new string[] { "tests/getting.started/sample6.cs" });
        }

        public void testSample7()
        {
            runTest(new string[] { "tests/getting.started/sample7.cs" });
        }


    }
}
//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: CLRCGSampleTest .cs                                                             
// Author: Francisco Ortin  -  francisco.ortin@gmail.com                    
// Description:                                                               
//    A sample file to rapidly test CLR code generation.
// -------------------------------------------------------------------------- 
// Create date: 04-04-2007                                                    
// Modification date: 04-04-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Tests
{
   class CLRCGSampleTest : CLRCodeGenerationTest
   {
       public void testCGSample()
       {
           runTest(new string[] { "tests/code.generation/Sample/cg.sample.cs", });
       }
       public void testCGSample1()
       {
           runTest(new string[] { "tests/code.generation/Sample/cg.sample1.cs", });
       }
       public void testCGSample2()
       {
           runTest(new string[] { "tests/code.generation/Sample/cg.sample2.cs", });
       }
       public void testCGSample3()
       {
           runTest(new string[] { "tests/code.generation/Sample/cg.sample3.cs", });
       }

       public void testCGSample4() {
           runTest(new string[] { "tests/code.generation/Sample/cg.sample4.cs", });
       }
       public void testCGSample5() {
           runTest(new string[] { "tests/code.generation/Sample/cg.sample5.cs", });
       }              
       public void testCGSample7() {
           runTest(new string[] { "tests/code.generation/Sample/cg.sample7.cs", });
       }
       public void testCGSample8() {
           runTest(new string[] { "tests/code.generation/Sample/cg.sample8.cs", });
       }
       public void testCGScope1() {
           runTest(new string[] { "tests/code.generation/Sample/cg.scope1.cs", });
       }
       public void testCGScope2() {
           runTest(new string[] { "tests/code.generation/Sample/cg.scope2.cs", });
       }
   }
}

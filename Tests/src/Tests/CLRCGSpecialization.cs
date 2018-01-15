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
using DynVarManagement;

namespace Tests
{
    class CLRCGSpecialization : CLRCodeGenerationTest {

        public CLRCGSpecialization()
        {
            this.specialized = true;
            this.dynamic = true;            
        }
        //public void testCGSpecialization1()
        //{
        //    runTest(new string[] { "tests/code.generation/Specialization/CG.Specialization1.cs" });
        //}
        //public void testCGSpecialization2()
        //{
        //    runTest(new string[] { "tests/code.generation/Specialization/CG.Specialization2.cs" });
        //}
        //public void testCGSpecialization3()
        //{
        //    runTest(new string[] { "tests/code.generation/Specialization/CG.Specialization3.cs" });
        //}
        //public void testCGSpecialization4()
        //{
        //    runTest(new string[] { "tests/code.generation/Specialization/CG.Specialization4.cs" });
        //}
        //public void testCGSpecialization5()
        //{
        //    runTest(new string[] { "tests/code.generation/Specialization/CG.Specialization5.cs" });
        //}
        //public void testCGSpecialization6()
        //{
        //    runTest(new string[] { "tests/code.generation/Specialization/CG.Specialization6.cs" });
        //}
        //public void testCGSpecialization7()
        //{
        //    runTest(new string[] { "tests/code.generation/Specialization/CG.Specialization7.cs" });
        //}
        //public void testCGSpecialization8()
        //{
        //    runTest(new string[] { "tests/code.generation/Specialization/CG.Specialization8.cs" });
        //}
        //public void testCGSpecialization9()
        //{
        //    runTest(new string[] { "tests/code.generation/Specialization/CG.Specialization9.cs" });
        //}
        //public void testCGSpecialization10()
        //{
        //    runTest(new string[] { "tests/code.generation/Specialization/CG.Specialization10.cs" });
        //}
        //public void testCGSpecialization11()
        //{
        //    runTest(new string[] { "tests/code.generation/Specialization/CG.Specialization11.cs" });
        //}
        //public void testCGSpecialization12()
        //{
        //    runTest(new string[] { "tests/code.generation/Specialization/CG.Specialization12.cs" });
        //}
        //public void testCGSpecialization13()
        //{
        //    runTest(new string[] { "tests/code.generation/Specialization/CG.Specialization13.cs" });
        //}
        //public void testCGSpecialization14()
        //{
        //    runTest(new string[] { "tests/code.generation/Specialization/CG.Specialization14.cs" });
        //}
        //public void testCGSpecialization15()
        //{
        //    runTest(new string[] { "tests/code.generation/Specialization/CG.Specialization15.cs" });
        //}
        //public void testCGSpecialization16()
        //{
        //    runTest(new string[] { "tests/code.generation/Specialization/CG.Specialization16.cs" });
        //}
        //public void testCGSpecialization17()
        //{
        //    runTest(new string[] { "tests/code.generation/Specialization/CG.Specialization17.cs" });
        //}
        //public void testCGSpecialization18()
        //{
        //    runTest(new string[] { "tests/code.generation/Specialization/CG.Specialization18.cs" });
        //}
        //public void testCGSpecialization19()
        //{
        //    runTest(new string[] { "tests/code.generation/Specialization/CG.Specialization19.cs" });
        //}

        public void testX()
        {
            runTest(new string[] { "../../../Wrong Test Cases/Case X/CG.cs" });
        }       
   }
}

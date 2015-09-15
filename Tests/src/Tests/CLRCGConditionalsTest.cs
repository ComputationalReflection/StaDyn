////////////////////////////////////////// -------------------------------------------------------------------------- /
// Project Stadyn                                                             /
// -------------------------------------------------------------------------- /
// File: CLRCGConditionalsTest .cs                                                /
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
    class CLRCGConditionalsTest : CLRCodeGenerationTest
    {
        public void testCCGIfStat()
        {
            runTest(new string[] { "tests/code.generation/Conditionals/CG.IfStat.cs" });
        }
        public void testCCGIfStat2()
        {
            runTest(new string[] { "tests/code.generation/Conditionals/CG.IfStat2.cs" });
        }
        public void testCCGIfStat3()
        {
            runTest(new string[] { "tests/code.generation/Conditionals/CG.IfStat3.cs" });
        }
        public void testCCGSwitchStat()
        {
            runTest(new string[] { "tests/code.generation/Conditionals/CG.SwitchStat.cs" });
        }
        public void testCCGSwitchStat2()
        {
            runTest(new string[] { "tests/code.generation/Conditionals/CG.SwitchStat2.cs" });
        }
        public void testCCGTestDo()
        {
            runTest(new string[] { "tests/code.generation/Conditionals/CG.TestDo.cs" });
        }
        public void testCCGTestFor()
        {
            runTest(new string[] { "tests/code.generation/Conditionals/CG.TestFor.cs" });
        }
        public void testCCGTestFor2()
        {
            runTest(new string[] { "tests/code.generation/Conditionals/CG.TestFor2.cs" });
        }
        public void testCCGTestIf0()
        {
            runTest(new string[] { "tests/code.generation/Conditionals/CG.TestIf0.cs" });
        }
        public void testCCGTestIf1()
        {
            runTest(new string[] { "tests/code.generation/Conditionals/CG.TestIf1.cs" });
        }
        public void testCCGTestSwitch()
        {
            runTest(new string[] { "tests/code.generation/Conditionals/CG.TestSwitch.cs" });
        }
        public void testCCGTestSwitch2()
        {
            runTest(new string[] { "tests/code.generation/Conditionals/CG.TestSwitch2.cs" });
        }
        public void testCCGTestSwitch3()
        {
            runTest(new string[] { "tests/code.generation/Conditionals/CG.TestSwitch3.cs" });
        }
        public void testCCGTestWhile()
        {
            runTest(new string[] { "tests/code.generation/Conditionals/CG.TestWhile.cs" });
        }
        public void testCGVarRecursion()
        {
            runTest(new string[] { "tests/code.generation/Conditionals/CG.Var.Recursion.cs" });
        }
        public void testCGVarSSAdo()
        {
            runTest(new string[] { "tests/code.generation/Conditionals/CG.Var.SSA.do.cs" });
        }
        public void testCGVarSSAfor()
        {
            runTest(new string[] { "tests/code.generation/Conditionals/CG.Var.SSA.for.cs" });
        }
        public void testCGVarSSAif()
        {
            runTest(new string[] { "tests/code.generation/Conditionals/CG.Var.SSA.if.cs" });
        }
        public void testCGVarSequential()
        {
            runTest(new string[] { "tests/code.generation/Conditionals/CG.Var.SSA.sequential.cs" });
        }
        
        public void testCGVarSSAnested()
        {
            runTest(new string[] { "tests/code.generation/Conditionals/CG.Var.SSA.nested.cs" });
        }

        public void testCGVarSSAswitch()
        {
            runTest(new string[] { "tests/code.generation/Conditionals/CG.Var.SSA.switch.cs" });
        }
	}
}

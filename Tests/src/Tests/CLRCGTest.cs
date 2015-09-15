////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: CLRCGTest .cs                                                        //
// Author: Cristina Gonzalez Muñoz - cristi.gm@gmail.com                      //
// Description:                                                               //
//    Testing of code generation without implicit features of the language,   //
// compiled into the CLR platform.                                            //
// -------------------------------------------------------------------------- //
// Create date: 14-09-2007                                                    //
// Modification date: 14-09-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Tests
{
    class CLRCGTest : CLRCodeGenerationTest {
        public void testCGConstants() {
            runTest(new string[] { "tests/code.generation/CG.Constants.cs" });
        }

        public void testCGConstructors() {
            runTest(new string[] { "tests/code.generation/CG.Constructors.cs" });
        }

        public void testCGCalls() {
            runTest(new string[] { "tests/code.generation/CG.Calls.cs" });
        }

        public void testCGClassAccess() {
            runTest(new string[] { "tests/code.generation/CG.ClassAccess.cs" });
        }

        public void testCGThisBase() {
            runTest(new string[] { "tests/code.generation/CG.ThisBase.cs" });
        }

        public void testCGProperties() {
            runTest(new string[] { "tests/code.generation/CG.Properties.cs" });
        }

        public void testCGFields() {
            runTest(new string[] { "tests/code.generation/CG.Fields2.cs" });
        }

        public void testCGMethods() {
            runTest(new string[] { "tests/code.generation/CG.OperationsMethods.cs" });
        }

        public void testCGOperations3() {
            runTest(new string[] { "tests/code.generation/CG.Operations3.cs" });
        }

        public void testCGOperationsLocal() {
            runTest(new string[] { "tests/code.generation/CG.OperationsLocal.cs" });
        }

        public void testCGOperationsLocal2() {
            runTest(new string[] { "tests/code.generation/CG.OperationsLocal2.cs" });
        }

        public void testCGOperationsField() {
            runTest(new string[] { "tests/code.generation/CG.OperationsField.cs" });
        }

        public void testCGOperationsField2() {
            runTest(new string[] { "tests/code.generation/CG.OperationsField2.cs" });
        }

        public void testCGOperationsFieldThis() {
            runTest(new string[] { "tests/code.generation/CG.OperationsFieldThis.cs" });
        }

        public void testCGOperationsFieldThis2() {
            runTest(new string[] { "tests/code.generation/CG.OperationsFieldThis2.cs" });
        }

        public void testCGOperationsFieldStatic() {
            runTest(new string[] { "tests/code.generation/CG.OperationsFieldStatic.cs" });
        }

        public void testCGOperationsFieldStatic2() {
            runTest(new string[] { "tests/code.generation/CG.OperationsFieldStatic2.cs" });
        }

        public void testCGOperations2() {
            runTest(new string[] { "tests/code.generation/CG.Operations2.cs" });
        }

        public void testCGOperationsFields() {
            runTest(new string[] { "tests/code.generation/CG.OperationsFields.cs" });
        }

        public void testCGOperationsParam() {
            runTest(new string[] { "tests/code.generation/CG.OperationsParam.cs" });
        }

        public void testCGOperationsExps() {
            runTest(new string[] { "tests/code.generation/CG.OperationsExps.cs" });
        }

        public void testCGArrayConstruction() {
            runTest(new string[] { "tests/code.generation/CG.ArrayConstruction.cs" });
        }

        public void testCGArrayTest() {
            runTest(new string[] { "tests/code.generation/CG.ArrayTest.cs" });
        }

        public void testCGArrays() {
            runTest(new string[] { "tests/code.generation/CG.OpArrays.cs" });
        }

        public void testCGArraysFields() {
            runTest(new string[] { "tests/code.generation/CG.OpArraysFields.cs" });
        }

        public void testCGArraysFieldsThis() {
            runTest(new string[] { "tests/code.generation/CG.OpArraysFieldsThis.cs" });
        }

        public void testCGArraysParam() {
            runTest(new string[] { "tests/code.generation/CG.OpArraysParam.cs" });
        }
        public void testA() {
            runTest(new string[] { "tests/code.generation/A.cs" });
        }

        public void testCGArraySize() {
            runTest(new string[] { "tests/code.generation/CG.ArraySize.cs" });
        }

        public void testCGMoreOperations() {
            runTest(new string[] { "tests/code.generation/CG.MoreOperations.cs" });
        }

        public void testCGMoreOperations2() {
            runTest(new string[] { "tests/code.generation/CG.MoreOperations2.cs" });
        }

        public void testCGMoreOperations3() {
            runTest(new string[] { "tests/code.generation/CG.MoreOperations3.cs" });
        }

        public void testCGMoreOperations4() {
            runTest(new string[] { "tests/code.generation/CG.MoreOperations4.cs" });
        }

        public void testCGIf0() {
            runTest(new string[] { "tests/code.generation/CG.TestIf0.cs" });
        }

        public void testCGIf1() {
            runTest(new string[] { "tests/code.generation/CG.TestIf1.cs" });
        }

        public void testCGIfStat() {
            runTest(new string[] { "tests/code.generation/CG.IfStat.cs" });
        }

        public void testCGIfStat2() {
            runTest(new string[] { "tests/code.generation/CG.IfStat2.cs" });
        }

        public void testCGIfStat3() {
            runTest(new string[] { "tests/code.generation/CG.IfStat3.cs" });
        }

        public void testCGShowArray() {
            runTest(new string[] { "tests/code.generation/CG.ShowArray.cs" });
        }

        public void testCGSwitchStat() {
            runTest(new string[] { "tests/code.generation/CG.SwitchStat.cs" });
        }

        public void testCGSwitchStat2() {
            runTest(new string[] { "tests/code.generation/CG.SwitchStat2.cs" });
        }

        public void testCGTestWhile() {
            runTest(new string[] { "tests/code.generation/CG.TestWhile.cs" });
        }

        public void testCGTestDo() {
            runTest(new string[] { "tests/code.generation/CG.TestDo.cs" });
        }

        public void testCGTestFor() {
            runTest(new string[] { "tests/code.generation/CG.TestFor.cs" });
        }

        public void testCGTestFor2() {
            runTest(new string[] { "tests/code.generation/CG.TestFor2.cs" });
        }

        public void testCGTestSwitch() {
            runTest(new string[] { "tests/code.generation/CG.TestSwitch.cs" });
        }

        public void testCGTestSwitch2() {
            runTest(new string[] { "tests/code.generation/CG.TestSwitch2.cs" });
        }

        public void testCGTestSwitch3() {
            runTest(new string[] { "tests/code.generation/CG.TestSwitch3.cs" });
        }

        public void testCGTestStats() {
            runTest(new string[] { "tests/code.generation/CG.TestStats.cs" });
        }

        public void testCGTestStats2() {
            runTest(new string[] { "tests/code.generation/CG.TestStats2.cs" });
        }

        public void testCGTestStat3() {
            runTest(new string[] { "tests/code.generation/CG.TestStat3.cs" });
        }

        public void testCGTestsStats2() {
            runTest(new string[] { "tests/code.generation/CG.TestsStats2.cs" });
        }

        public void testCGTestsStats2Vers2() {
            runTest(new string[] { "tests/code.generation/CG.TestsStats2Vers2.cs" });
        }

        public void testCGUnaryStats() {
            runTest(new string[] { "tests/code.generation/CG.UnaryStats.cs" });
        }

        public void testCGUnaryStats2() {
            runTest(new string[] { "tests/code.generation/CG.UnaryStats2.cs" });
        }

        public void testCGCollections() {
            runTest(new string[] { "tests/code.generation/CG.Collections.cs" });
        }

        public void testCGCollectionsClass() {
            runTest(new string[] { "tests/code.generation/CG.CollectionsClass.cs" });
        }

        public void testCGCollectionsParam() {
            runTest(new string[] { "tests/code.generation/CG.CollectionsParam.cs" });
        }

        public void testCGCollectiosFields() {
            runTest(new string[] { "tests/code.generation/CG.CollectionsFields.cs" });
        }

        public void testCGImplicitConversion() {
            runTest(new string[] { "tests/code.generation/CG.ImplicitConversion.cs" });
        }

        public void testCGImplicitConversion2() {
            runTest(new string[] { "tests/code.generation/CG.ImplicitConversion2.cs" });
        }

        public void testCGReturns() {
            runTest(new string[] { "tests/code.generation/CG.Returns.cs" });
        }

        public void testCGTreeModsParser() {
            runTest(new string[] { "tests/code.generation/CG.TreeModsParser.cs" });
        }

        public void testValueTypes() {
            runTest(new string[] { "tests/code.generation/CG.value.types.cs" });
        }
    }
}

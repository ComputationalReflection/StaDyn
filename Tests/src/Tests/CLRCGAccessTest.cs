////////////////////////////////////////// -------------------------------------------------------------------------- /
// Project Stadyn                                                             /
// -------------------------------------------------------------------------- /
// File: CLRArithmeticTest .cs                                                /
// Author: Daniel Zapico Rodr¡guez daniel.zapico@rodriguez                    /
// Description:                                                               /
//    Testing of code access operations to different levels of protection of  /
//  and features of the language
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
    class CLRCGAccessTest : CLRCodeGenerationTest
    {
        public void testCCGCalls()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.Calls.cs" });
        }
        public void testCCGClassAccess()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.ClassAccess.cs" });
        }
        public void testCCGConstants()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.Constants.cs" });
        }
        public void testCCGConstructors()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.Constructors.cs" });
        }
        public void testCCGFields2()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.Fields2.cs" });
        }
        public void testCCGMoreOperations()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.MoreOperations.cs" });
        }
        public void testCCGMoreOperations2()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.MoreOperations2.cs" });
        }
        public void testCCGMoreOperations3()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.MoreOperations3.cs" });
        }
        public void testCCGMoreOperations4()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.MoreOperations4.cs" });
        }
        public void testCCGOpArrays()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.OpArrays.cs" });
        }
        public void testCCGOpArraysFields()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.OpArraysFields.cs" });
        }
        public void testCCGOpArraysFieldsThis()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.OpArraysFieldsThis.cs" });
        }
        public void testCCGOpArraysParam()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.OpArraysParam.cs" });
        }
        public void testCCGOperations2()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.Operations2.cs" });
        }
        public void testCCGOperations3()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.Operations3.cs" });
        }
        public void testCCGOperationsExps()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.OperationsExps.cs" });
        }
        public void testCCGOperationsField()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.OperationsField.cs" });
        }
        public void testCCGOperationsField2()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.OperationsField2.cs" });
        }
        public void testCCGOperationsFields()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.OperationsFields.cs" });
        }
        public void testCCGOperationsFieldStatic()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.OperationsFieldStatic.cs" });
        }
        public void testCCGOperationsFieldStatic2()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.OperationsFieldStatic2.cs" });
        }
        public void testCCGOperationsFieldThis()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.OperationsFieldThis.cs" });
        }
        public void testCCGOperationsFieldThis2()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.OperationsFieldThis2.cs" });
        }
        public void testCCGOperationsLocal()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.OperationsLocal.cs" });
        }
        public void testCCGOperationsLocal2()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.OperationsLocal2.cs" });
        }
        public void testCCGOperationsMethods()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.OperationsMethods.cs" });
        }
        public void testCCGOperationsParam()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.OperationsParam.cs" });
        }
        public void testCCGReturns()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.Returns.cs" });
        }
        public void testCCGShowArray()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.ShowArray.cs" });
        }
        public void testCCGTestsStats2()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.TestsStats2.cs" });
        }
        public void testCCGTestsStats2Vers2()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.TestsStats2Vers2.cs" });
        }
        public void testCCGTestStat3()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.TestStat3.cs" });
        }
        public void testCCGTestStats()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.TestStats.cs" });
        }
        public void testCCGTestStats2()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.TestStats2.cs" });
        }
        public void testCCGtestVarParametersAndReturn()
        {            
            runTest(new string[] { "tests/code.generation/Access/CG.testVar.ParametersAndReturn.cs" });
        }
        public void testCCGThisBase()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.ThisBase.cs" });
        }
        public void testCCGTreeModsParser()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.TreeModsParser.cs" });
        }
        public void testCCGUnaryStats()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.UnaryStats.cs" });
        }
        public void testCCGUnaryStats2()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.UnaryStats2.cs" });
        }
        public void testCcgvaluetypes()
        {
            runTest(new string[] { "tests/code.generation/Access/cg.value.types.cs" });
        }
        public void testCCGvarAnd()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.var.And.cs" });
        }
        public void testCCGvarAnd2()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.var.And2.cs" });
        }
        public void testCCGvarBoxUnbox()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.var.BoxUnbox.cs" });
        }
        public void testCCGvarBoxUnbox1()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.var.BoxUnbox1.cs" });
        }
        public void testCCGvarBoxUnbox2()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.var.BoxUnbox2.cs" });
        }
        public void testCCGvarBoxUnbox3()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.var.BoxUnbox3.cs" });
        }
        public void testCcgvarmethodinvocation()
        {
            runTest(new string[] { "tests/code.generation/Access/cg.var.methodinvocation.cs" });
        }
        public void testCcgvarmethodinvocation2()
        {
            runTest(new string[] { "tests/code.generation/Access/cg.var.methodinvocation2.cs" });
        }
        public void testCCGvarParamLocal()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.var.ParamLocal.cs" });
        }
        public void testCCGvarParamLocal2()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.var.ParamLocal2.cs" });
        }

        public void testCCGvarParamLocal3()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.var.ParamLocal3.cs" });
        }
        public void testCCGvarParamLocal32()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.var.ParamLocal32.cs" });
        }
        public void testCCGvarParamLocal33()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.var.ParamLocal33.cs" });
        }
        public void testCCGvarParamLocal34()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.var.ParamLocal34.cs" });
        }
        public void testCCGvarParamLocal4()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.var.ParamLocal4.cs" });
        }
        public void testCCGvarParamLocal5()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.var.ParamLocal5.cs" });
        }
        public void testCCGvarParamLocal52()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.var.ParamLocal52.cs" });
        }
        public void testCCGvarParamLocal53()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.var.ParamLocal53.cs" });
        }
        public void testCCGvarParamLocal54()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.var.ParamLocal54.cs" });
        }
        public void testCCGvarParamLocal6()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.var.ParamLocal6.cs" });
        }
        public void testCCGvarParamLocalDyn()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.var.ParamLocalDyn.cs" });
        }
        public void testCCGvarParamLocalSta()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.var.ParamLocalSta.cs" });
        }
        public void testCCGvarParamLocalStaDyn()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.var.ParamLocalStaDyn.cs" });
        }
        public void testCCGvarReturnUnionType()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.var.ReturnUnionType.cs" });
        }
        public void testCCGvarReturnUnionType2()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.var.ReturnUnionType2.cs" });
        }
        public void testCCGvarTestIf()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.var.TestIf.cs" });
        }
        public void testCCGvarTestValueTypesField()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.var.TestValueTypesField.cs" });
        }
        public void testCCGvarTestValueTypesLocal()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.var.TestValueTypesLocal.cs" });
        }
        public void testCCGvartestVarAttributes()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.var.testVar.Attributes.cs" });
        }
        public void testCCGvartestVarAttributes2()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.var.testVar.Attributes2.cs" });
        }
        public void testSSimpleDynBinding()
        {
            runTest(new string[] { "tests/code.generation/Access/SimpleDynBinding.cs" });
        }
        public void testCGVarSimpleTest()
        {
            runTest(new string[] { "tests/code.generation/Access/Var.Simple.Test.cs" });
        }
        public void testCGExplicitNamespaceAndStatic()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.Explicit.NamespaceAndStatic.cs" });
        }
        public void testCGExplicitOverload()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.Explicit.Overload.cs" });
        }
        public void testCGVarAliasAnalysis()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.Var.AliasAnalysis.cs" });
        }
        public void testCGMemberAccessConstraints()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.Var.MemberAccessConstraints.cs" });
        }

        public void testVarWrap()
        {
            runTest(new string[] { "tests/code.generation/Access/VarWrap.cs" });
        }

        public void testGettersAndSetters()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.TestGettersAndSetters.cs" });
        }

        //public void testFields()
        //{
        //    runTest(new string[] { "tests/code.generation/Access/CG.TestFields.cs" });
        //}

        public void testAbstract()
        {
            runTest(new string[] { "tests/code.generation/Access/CG.TestAbstract.cs" });
        }

	}
}

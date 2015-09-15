//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: CLRCGVarTests .cs                                                             
// Author: Francisco Ortin  -  francisco.ortin@gmail.com                    
// Description:                                                               
//    Testing of the implicit features of the language, compiled
//            into the CLR platform.
// -------------------------------------------------------------------------- 
// Create date: 22-08-2007                                                    
// Modification date: 22-08-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Tests
{
   class CLRCGVarTests : CLRCodeGenerationTest
   {

       public void testCGVarMethodInvocation() {
           runTest(new string[] { "tests/code.generation/cg.var.methodinvocation.cs", });
       }

       public void testCGVarBoxUnbox() {
           runTest(new string[] { "tests/code.generation/CG.var.BoxUnbox.cs" });
       }

       public void testCGVarBoxUnbox1() {
           runTest(new string[] { "tests/code.generation/CG.var.BoxUnbox1.cs" });
       }

       public void testCGVarBoxUnbox2() {
           runTest(new string[] { "tests/code.generation/CG.var.BoxUnbox2.cs" });
       }

       public void testCGVarBoxUnbox3() {
           runTest(new string[] { "tests/code.generation/CG.var.BoxUnbox3.cs" });
       }

       public void testCGVarAnd() {
           runTest(new string[] { "tests/code.generation/CG.var.And.cs" });
       }

       public void testCGVarAnd2() {
           runTest(new string[] { "tests/code.generation/CG.var.And2.cs" });
       }

       public void testCGVarReturnUnionType() {
           runTest(new string[] { "tests/code.generation/CG.var.ReturnUnionType.cs" });
       }

       public void testCGVarReturnUnionType2() {
           runTest(new string[] { "tests/code.generation/CG.var.ReturnUnionType2.cs" });
       }

       public void testCGVarParamLocalStatic() {
           runTest(new string[] { "tests/code.generation/CG.var.ParamLocalSta.cs" });
       }

       public void testCGVarParamLocalStaticDyn() {
           runTest(new string[] { "tests/code.generation/CG.var.ParamLocalStaDyn.cs" });
       }

       public void testCGVarParamLocalDynamic() {
           runTest(new string[] { "tests/code.generation/CG.var.ParamLocalDyn.cs" });
       }

       public void testCGVarParamLocal1() {
           runTest(new string[] { "tests/code.generation/CG.var.ParamLocal.cs" });
       }

       public void testCGVarParamLocal2() {
           runTest(new string[] { "tests/code.generation/CG.var.ParamLocal2.cs" });
       }

       public void testCGVarParamLocal3() {
           runTest(new string[] { "tests/code.generation/CG.var.ParamLocal3.cs" });
       }

       public void testCGVarParamLocal32() {
           runTest(new string[] { "tests/code.generation/CG.var.ParamLocal32.cs" });
       }

       public void testCGVarParamLocal33() {
           runTest(new string[] { "tests/code.generation/CG.var.ParamLocal33.cs" });
       }

       public void testCGVarParamLocal34() {
           runTest(new string[] { "tests/code.generation/CG.var.ParamLocal34.cs" });
       }

       public void testCGVarParamLocal4() {
           runTest(new string[] { "tests/code.generation/CG.var.ParamLocal4.cs" });
       }

       public void testCGVarParamLocal5() {
           runTest(new string[] { "tests/code.generation/CG.var.ParamLocal5.cs" });
       }

       public void testCGVarParamLocal52() {
           runTest(new string[] { "tests/code.generation/CG.var.ParamLocal52.cs" });
       }

       public void testCGVarParamLocal53() {
           runTest(new string[] { "tests/code.generation/CG.var.ParamLocal53.cs" });
       }

       public void testCGVarParamLocal54() {
           runTest(new string[] { "tests/code.generation/CG.var.ParamLocal54.cs" });
       }
        //Este test no funciona
       //public void testCGVarParamLocal6() {
       //    runTest(new string[] { "tests/code.generation/CG.var.ParamLocal6.cs" });
       //}

       public void testCGVarUnifiedReferenceNoUnion() {
           runTest(new string[] { "tests/code.generation/CG.var.UnifiedReferenceNoUnion.cs", "tests/code.generation/Figures.cs" });
       }

       public void testCGVarUnifiedReferenceNoUnion2() {
           runTest(new string[] { "tests/code.generation/CG.var.UnifiedReferenceNoUnion2.cs", "tests/code.generation/Figures.cs" });
       }

       public void testCGVarUnionTypeDyn() {
           runTest(new string[] { "tests/code.generation/CG.var.UnionTypeDyn.cs" });
       }

       public void testCGVarUnionTypeSta() {
           runTest(new string[] { "tests/code.generation/CG.var.UnionTypeSta.cs" });
       }

       public void testCGVarDynamicsAttributes() {
           runTest(new string[] { "tests/code.generation/Semantic/CG.testDynamics.Attributes.cs", "tests/code.generation/Semantic/Figures.cs" });
       }

       public void testCGVarAttributes() {
           runTest(new string[] { "tests/code.generation/CG.var.testVar.Attributes.cs" });
       }

       public void testCGVarAttributes2() {
           runTest(new string[] { "tests/code.generation/CG.var.testVar.Attributes2.cs" });
       }

       public void testCGVarTestValueTypesField() {
           runTest(new string[] { "tests/code.generation/CG.var.TestValueTypesField.cs" });
       }

       public void testCGVarTestValueTypesLocal() {
           runTest(new string[] { "tests/code.generation/CG.var.TestValueTypesLocal.cs" });
       }

       public void testCGVarTestIf() {
           runTest(new string[] { "tests/code.generation/CG.var.TestIf.cs" });
       }

       //public void testCGVar() {
       //    runTest(new string[] { "tests/code.generation/" });
       //}

   }
}

//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                            
// -------------------------------------------------------------------------- 
// File: WrongVarTests.cs                                                             
// Author: Francisco Ortin  -  francisco.ortin@gmail.com                    
// Description:                                                               
//    Testing of wrong programs that use implicit type inference.                                  
// -------------------------------------------------------------------------- 
// Create date: 12-04-2007                                                    
// Modification date: 16-05-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace Tests {
    class WrongVarTests : SemanticTest {
        public void testWrongVarCast()
        {
            runTest(new string[] { "tests/wrong/testWrongVar.Cast.cs" });
        }

        public void testWrongVarAssignmentConstraint()
        {
            runTest(new string[] { "tests/wrong/testWrongVar.AssignmentConstraint.cs" });
        }

        public void testWrongVarParametersAndReturn()
        {
            runTest(new string[] { "tests/wrong/testWrongVar.ParametersAndReturn.cs" });
        }

        public void testWrongVarStaticUnionTypes()
        {
            runTest(new string[] { "tests/wrong/testWrongVar.StaticUnionTypes.cs" });
        }

        public void testWrongVarCommonOperatorConstraints()
        {
            runTest(new string[] { "tests/wrong/testWrongVar.CommonOperatorConstraints.cs" });
        }

        public void testWrongBracketConstraints()
        {
            runTest(new string[] { "tests/wrong/testWrongVar.BracketConstraints.cs" });
        }

        public void testWrongMemberAccessConstraints()
        {
            runTest(new string[] { "tests/wrong/testWrongVar.MemberAccessConstraints.cs" });
        }

        public void testWrongVarAttributes()
        {
            runTest(new string[] { "tests/wrong/testWrongVar.Attributes.cs" });
        }

        public void testWrongVarRecursiveTypes()
        {
            runTest(new string[] { "tests/wrong/testWrongVar.Recursion.cs" });
        }

        public void testWrongVarSSAsequential()
        {
            runTest(new string[] { "tests/wrong/testWrongVar.SSA.sequential.cs" });
        }

        //public void testWrongVarSSAwhile()
        //{
        //    runTest(new string[] { "tests/wrong/Figures.cs", "tests/wrong/testWrongVar.SSA.while.cs" });
        //}

        public void testWrongVarSSAdo()
        {
            runTest(new string[] { "tests/wrong/Figures.cs", "tests/wrong/testWrongVar.SSA.do.cs" });
        }

        public void testWrongVarSSAfor()
        {
            runTest(new string[] { "tests/wrong/Figures.cs", "tests/wrong/testWrongVar.SSA.for.cs" });
        }

        public void testWrongVarSSAif()
        {
            runTest(new string[] { "tests/wrong/Figures.cs", "tests/wrong/VarWrap.cs", "tests/wrong/testWrongVar.SSA.if.cs" });
        }

        public void testWrongVarSSAswitch()
        {
            runTest(new string[] { "tests/wrong/VarWrap.cs", "tests/wrong/testWrongVar.SSA.switch.cs" });
        }

        //public void testWrongVarSSAnested()
        //{
        //    runTest(new string[] { "tests/wrong/Figures.cs", "tests/wrong/VarWrap.cs", "tests/wrong/testWrongVar.SSA.nested.cs" });
        //}
     
    }
}

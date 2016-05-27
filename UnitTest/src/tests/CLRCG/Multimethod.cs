using System;
using System.Diagnostics;
using System.Reflection;
using ErrorManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TargetPlatforms;

namespace UnitTest.Tests.CLRCG
{
    [TestClass]
    public class Multimethod
    {
        private Test GetDefaultTest()
        {
            return new Test(true, false, true);
        }

        [TestMethod]
        [TestCategory("CLRCG"), TestCategory("Multimethod")]
        public void Multimethod1()
        {
            String path = Test.TESTS_PATH + "\\CLRCG\\Multimethod\\";
            GetDefaultTest().runTest(new [] {path+"Value.cs", path + "Operators.cs", path + "EvaluateExpression.cs", path + "Program.cs" }, path + "Multimethod.exe");
        }
    }
}

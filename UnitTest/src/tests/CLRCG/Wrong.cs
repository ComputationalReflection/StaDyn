using System;
using System.Diagnostics;
using System.Reflection;
using ErrorManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Tests.CLRCG
{
    [TestClass]
    public class Wrong
    {
        private Test GetDefaultTest()
        {
            return new Test(true, false, true);
        }

        [TestMethod]
        [TestCategory("CLRCG"),TestCategory("Wrong")]
        public void Wrong1()
        {            
            GetDefaultTest().runTest(new[] { "../../../Wrong Test Cases/Case X/CG.cs" });                       
        }
    }
}

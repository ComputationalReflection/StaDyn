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
        [TestMethod]
        [TestCategory("CLRCG"),TestCategory("Wrong")]
        public void Wrong1()
        {            
            CLRCGTest test = new CLRCGTest {Specialized = true, Dynamic = true};
            test.runTest(new []{ "../../../Wrong Test Cases/Case X/CG.cs" });                       
        }
    }
}

using System;
using System.Diagnostics;
using System.Reflection;
using ErrorManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Tests.CLRCG
{
    [TestClass]
    public class Specialization
    {
        [TestMethod]
        public void Specialization1()
        {            
            CLRCGTest test = new CLRCGTest {Specialized = true, Dynamic = true};
            test.runTest(new string[] { "res/tests/CLRCG/Specialization/Specialization1.cs" });
            Assert.AreEqual(test.ExpectedErrors, test.ToError - test.FromError, test.ExpectedErrors + " errors expected, " + (test.ToError - test.FromError) + " found.");            
            Assert.IsTrue(test.Success);            
        }
    }
}

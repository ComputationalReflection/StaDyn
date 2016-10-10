using System;

using System.Collections.Generic;
using System.Text;
using System.IO;
using DynVarManagement;

namespace Tests
{
    class Benchmarks : CLRCodeGenerationTest
    {
        public void testexplic()
        {
            runTest(new string[] { "tests/benchmarks/synthetic/explic.cs" });
        }

        public void testfive()
        {
            runTest(new string[] { "tests/benchmarks/synthetic/five.cs" });
        }

        /*
         * To try this test it's necessary to add "process.StartInfo.CreateNoWindow = true" to src/compiler/Program ln 387
         */
        //public void testnoinference()
        //{
        //    runTest(new string[] { "tests/benchmarks/synthetic/no.inference.cs" });
        //}

        public void testPystoneStatic()
        {
            runTest(new string[] { "tests/benchmarks/static/pystone/Pystone.Static.cs" });
        }

        public void testPystoneProc1()
        {
            runTest(new string[] { "tests/benchmarks/dynamic/pystone/TestProc1.cs" });
        }

        public void testPystoneProc2()
        {
            runTest(new string[] { "tests/benchmarks/dynamic/pystone/TestProc2.cs" });
        }

        public void testPystoneProc3()
        {
            runTest(new string[] { "tests/benchmarks/dynamic/pystone/TestProc3.cs" });
        }

        public void testPystoneProc4()
        {
            runTest(new string[] { "tests/benchmarks/dynamic/pystone/TestProc4.cs" });
        }

        public void testPystoneProc5()
        {
            runTest(new string[] { "tests/benchmarks/dynamic/pystone/TestProc5.cs" });
        }

        public void testPystoneProc6()
        {
            runTest(new string[] { "tests/benchmarks/dynamic/pystone/TestProc6.cs" });
        }

        public void testPystoneProc7()
        {
            runTest(new string[] { "tests/benchmarks/dynamic/pystone/TestProc7.cs" });
        }

        public void testPystoneProc8()
        {
            runTest(new string[] { "tests/benchmarks/dynamic/pystone/TestProc8.cs" });
        }

        public void testPystoneFunc1()
        {
            runTest(new string[] { "tests/benchmarks/dynamic/pystone/TestFunc1.cs" });
        }

        public void testPystoneFunc2()
        {
            runTest(new string[] { "tests/benchmarks/dynamic/pystone/TestFunc2.cs" });
        }

        public void testPystoneFunc3()
        {
            runTest(new string[] { "tests/benchmarks/dynamic/pystone/TestFunc3.cs" });
        }

        public void testPystoneDynamic()
        {
            DynVarOptions.Instance.EverythingDynamic = true;
            runTest(new string[] { "tests/benchmarks/dynamic/pystone/Pystone.Dynamic.cs" });
            DynVarOptions.Instance.EverythingDynamic = false;
        }

        public void testPybenchArithmetic()
        {
            DynVarOptions.Instance.EverythingDynamic = true;
            runTest(new string[] { "tests/benchmarks/dynamic/pybench/Pybench.Arithmetic.cs" });
            DynVarOptions.Instance.EverythingDynamic = false;
        }

        public void testPybenchCalls()
        {
            DynVarOptions.Instance.EverythingDynamic = true;
            runTest(new string[] { "tests/benchmarks/dynamic/pybench/Pybench.Calls.cs" });
            DynVarOptions.Instance.EverythingDynamic = false;
        }

        public void testPybenchConstructs()
        {
            DynVarOptions.Instance.EverythingDynamic = true;
            runTest(new string[] { "tests/benchmarks/dynamic/pybench/Pybench.Constructs.cs" });
            DynVarOptions.Instance.EverythingDynamic = false;
        }

        public void testPybenchDict()
        {
            DynVarOptions.Instance.EverythingDynamic = true;
            runTest(new string[] { "tests/benchmarks/dynamic/pybench/Pybench.Dict.cs" });
            DynVarOptions.Instance.EverythingDynamic = false;
        }

        public void testPybenchExceptions()
        {
            DynVarOptions.Instance.EverythingDynamic = true;
            runTest(new string[] { "tests/benchmarks/dynamic/pybench/Pybench.Exceptions.cs" });
            DynVarOptions.Instance.EverythingDynamic = false;
        }

        public void testPybenchInstances()
        {
            DynVarOptions.Instance.EverythingDynamic = true;
            runTest(new string[] { "tests/benchmarks/dynamic/pybench/Pybench.Instances.cs" });
            DynVarOptions.Instance.EverythingDynamic = false;
        }

        public void testPybenchLists()
        {
            DynVarOptions.Instance.EverythingDynamic = true;
            runTest(new string[] { "tests/benchmarks/dynamic/pybench/Pybench.Lists.cs" });
            DynVarOptions.Instance.EverythingDynamic = false;
        }

        public void testPybenchNewInstances()
        {
            DynVarOptions.Instance.EverythingDynamic = true;
            runTest(new string[] { "tests/benchmarks/dynamic/pybench/Pybench.NewInstances.cs" });
            DynVarOptions.Instance.EverythingDynamic = false;
        }

        public void testPybenchLookups()
        {
            DynVarOptions.Instance.EverythingDynamic = true;
            runTest(new string[] { "tests/benchmarks/dynamic/pybench/Pybench.Lookups.cs" });
            DynVarOptions.Instance.EverythingDynamic = false;
        }

        public void testPybenchNumbers()
        {
            DynVarOptions.Instance.EverythingDynamic = true;
            runTest(new string[] { "tests/benchmarks/dynamic/pybench/Pybench.Numbers.cs" });
            DynVarOptions.Instance.EverythingDynamic = false;
        }

        public void testPybenchStrings()
        {
            DynVarOptions.Instance.EverythingDynamic = true;
            runTest(new string[] { "tests/benchmarks/dynamic/pybench/Pybench.Strings.cs" });
            DynVarOptions.Instance.EverythingDynamic = false;
        }

        public void testJavaGrandeStaticSparseMatmult()
        {
            runTest(new string[] { "tests/benchmarks/static/javagrande/JGFSparseMatmult.cs" });
        }

        public void testJavaGrandeDynamicSparseMatmult()
        {
            DynVarOptions.Instance.EverythingDynamic = true;
            runTest(new string[] { "tests/benchmarks/dynamic/javagrande/JGFSparseMatmult.cs" });
            DynVarOptions.Instance.EverythingDynamic = false;
        }

        public void testJavaGrandeStaticFFT()
        {
            runTest(new string[] { "tests/benchmarks/static/javagrande/JGFFFT.cs" });
        }

        public void testJavaGrandeDynamicFFT()
        {
            DynVarOptions.Instance.EverythingDynamic = true;
            runTest(new string[] { "tests/benchmarks/dynamic/javagrande/JGFFFT.cs" });
            DynVarOptions.Instance.EverythingDynamic = false;
        }

        public void testJavaGrandeStaticNumericSortTest()
        {
            runTest(new string[] { "tests/benchmarks/static/javagrande/JGFNumericSortTest.cs" });
        }

        public void testJavaGrandeDynamicNumericSortTest()
        {
            DynVarOptions.Instance.EverythingDynamic = true;
            runTest(new string[] { "tests/benchmarks/dynamic/javagrande/JGFNumericSortTest.cs" });
            DynVarOptions.Instance.EverythingDynamic = false;
        }

        public void testJavaGrandeStaticRayTracerTest()
        {
            runTest(new string[] { "tests/benchmarks/static/javagrande/JGFRayTracer.cs" });
        }

        /*
         * To try this test it's necessary to add "process.StartInfo.CreateNoWindow = true" to src/compiler/Program ln 387
         */
        //public void testJavaGrandeDynamicRayTracerTest()
        //{
        //    DynVarOptions.Instance.EverythingDynamic = true;
        //    runTest(new string[] { "tests/benchmarks/dynamic/javagrande/JGFRayTracer.cs" });
        //    DynVarOptions.Instance.EverythingDynamic = false;
        //}
    }
}

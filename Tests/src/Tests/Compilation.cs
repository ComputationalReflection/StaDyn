using System;

using System.Collections.Generic;
using System.Text;
using System.IO;
using DynVarManagement;
using TargetPlatforms;

namespace Tests
{
    class Compilation : Test 
    {
        public Compilation(): base(true, false, false, TargetPlatform.CLR, false){}

        protected void runTest(string[] fileNames)
        {
            this.runTest(fileNames, Path.ChangeExtension(fileNames[0], ".exe"));
        }
        
        public void testPystoneStatic()
        {
            runTest(new string[] { "tests/benchmarks/static/pystone/Pystone.Static.cs" });
        }

        public void testPystoneHybrid()
        {
            runTest(new string[] { "tests/benchmarks/hybrid/pystone/Pystone.Hybrid.cs" });
        }

        public void testPystoneDynamic()
        {
            DynVarOptions.Instance.EverythingDynamic = true;
            runTest(new string[] { "tests/benchmarks/dynamic/pystone/Pystone.Dynamic.cs" });
            DynVarOptions.Instance.EverythingDynamic = false;
        }

        public void testJavaGrandeStaticSparseMatmult()
        {
            runTest(new string[] { "tests/benchmarks/static/javagrande/JGFSparseMatmult.cs" });
        }

        public void testJavaGrandeHybridSparseMatmult()
        {
            runTest(new string[] { "tests/benchmarks/hybrid/javagrande/JGFSparseMatmult.cs" });
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

        public void testJavaGrandeHybridFFT()
        {
            runTest(new string[] { "tests/benchmarks/hybrid/javagrande/JGFFFT.cs" });
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

        public void testJavaGrandeHybridNumericSortTest()
        {
            runTest(new string[] { "tests/benchmarks/hybrid/javagrande/JGFNumericSortTest.cs" });
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
         
         public void testJavaGrandeHybridRayTracerTest()
         {
             runTest(new string[] { "tests/benchmarks/hybrid/javagrande/JGFRayTracer.cs" });
         }

         public void testJavaGrandeDynamicRayTracerTest()
         {
             DynVarOptions.Instance.EverythingDynamic = true;
             runTest(new string[] { "tests/benchmarks/dynamic/javagrande/JGFRayTracer.cs" });
             DynVarOptions.Instance.EverythingDynamic = false;
         }
    }
}

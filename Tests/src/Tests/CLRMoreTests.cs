////////////////////////////////////////
// -------------------------------------------------------------------------- ///
// Project rROTOR                                                             /
// -------------------------------------------------------------------------- /
// File: CLRCGTest .cs                                                        /
// Author: Daniel Zapico Rodríguez daniel.zapico@rodriguez                    /
// Description:                                                               /
//    Testing of code generation in general, with and without implicit        /
// additionalfeatures of the language, compiled into the CLR 2.0 platform.                                            /
// -------------------------------------------------------------------------- /
// Create date: 14-09-2009                                                    /
// Modification date: 14-09-2009                                              /
////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Tests {
    class CLRMoreTests : CLRCodeGenerationTest {

        //public void testGCArray() {
        //    runTest(new string[] { "tests/code.generation/more.tests/CG.Array.cs" });
        //}
        //public void testGCArray2() {
        //    runTest(new string[] { "tests/code.generation/more.tests/CG.Array2.cs" });
        //}
        //public void testGCArray3() {
        //    runTest(new string[] { "tests/code.generation/more.tests/CG.Array3.cs" });
        //}
        //public void testGCArray4() {
        //    runTest(new string[] { "tests/code.generation/more.tests/CG.Array4.cs" });
        //}
        //public void testGCArray5() {
        //    runTest(new string[] { "tests/code.generation/more.tests/CG.Array5.cs" });
        //}
        //public void testGCScope1() {
        //   runTest(new string[] { "tests/code.generation/more.tests/CG.Scope1.cs" });
        //}
        //public void testGCScope2() {
        //    runTest(new string[] { "tests/code.generation/more.tests/CG.Scope2.cs" });
        //}
        //public void testGCScope3() {
        //    runTest(new string[] { "tests/code.generation/more.tests/CG.Scope3.cs" });
        //}
        //public void testGCCasting() {
        //    runTest(new string[] { "tests/code.generation/more.tests/CG.Casting.cs" });
        //}
        //public void testCGUnionTypes1() {
        //    runTest(new string[] { "tests/code.generation/more.tests/CG.UnionTypes1.cs" });
        //}
     //   public void testCGUnionTypes2() {
     //       runTest(new string[] { "tests/code.generation/more.tests/CG.UnionTypes2.cs" });
     //   }
     //   public void testCGUnionTypes3() {
     //       runTest(new string[] { "tests/code.generation/more.tests/CG.UnionTypes3.cs" });
     //   }
     //   public void testCGUnionTypes4() {
     //       runTest(new string[] { "tests/code.generation/more.tests/CG.UnionTypes4.cs" });
     //   }
     //   public void testCGUnionTypes5() {
     //       runTest(new string[] { "tests/code.generation/more.tests/CG.UnionTypes5.cs" });
     //   }
     //   public void testCGUnionTypes6() {
     //       runTest(new string[] { "tests/code.generation/more.tests/CG.UnionTypes6.cs" });
     //   }
     //   public void testCGUnionTypes7() {
     //       runTest(new string[] { "tests/code.generation/more.tests/CG.UnionTypes7.cs" });
     //   }
     //   public void testCGUnionTypes8() {
     //       runTest(new string[] { "tests/code.generation/more.tests/CG.UnionTypes8.cs" });
     //   }

          //public void testCGSuma() {
          //    runTest(new string[] { "tests/code.generation/more.tests/C.cs" });
          //}
     //   public void testCGSimpleAbstractInheritance1() {
     //       runTest(new string[] { "tests/code.generation/more.tests/CG.SimpleAbstractInheritance1.cs" });
     //   }
     //   public void testCGSimpleAbstractInheritance2() {
     //       runTest(new string[] { "tests/code.generation/more.tests/CG.SimpleAbstractInheritance2.cs" });
     //   }
     //   // Este test falla si se usa una property en lugar de un getter
     //   // Si no se le cualifica el String.Format con System. también falla a pesar de usar el using
     //   public void testCGSimpleAbstractInheritance3() {
     //       runTest(new string[] { "tests/code.generation/more.tests/CG.SimpleAbstractInheritance3.cs" });
     //   }

     //   public void testCGVarAbstractInheritance1() {
     //       runTest(new string[] { "tests/code.generation/more.tests/CG.VarAbstractInheritance1.cs" });
     //   }
     // /// si descomentamos la parte comentada de este test casca en tiempo de ejecición (ver mejoras)
     //   public void testCGVarAbstractInheritance2() {
     //       runTest(new string[] { "tests/code.generation/more.tests/CG.VarAbstractInheritance2.cs" });
     //   }
     ////Falla en semántico
     //   public void testCGVarAbstractInheritance3() {
     //       runTest(new string[] { "tests/code.generation/more.tests/CG.VarAbstractInheritance3.cs" });
     //   }
     //   // falla en semántico
     //   public void testCGVarAbstractInheritance4() {
     //       runTest(new string[] { "tests/code.generation/more.tests/CG.VarAbstractInheritance4.cs" });
     //   }
     //   public void testCGInterfaceInheritance1() {
     //       runTest(new string[] { "tests/code.generation/more.tests/CG.InterfaceInheritance1.cs" });
     //   }
     //   public void testCGInterfaceInheritance2() {
     //       runTest(new string[] { "tests/code.generation/more.tests/CG.InterfaceInheritance2.cs" });
     //   }
     //   public void testCGInterfaceInheritance3() {
     //       runTest(new string[] { "tests/code.generation/more.tests/CG.InterfaceInheritance3.cs" });
     //   }
     //   public void testCGInterfaceInheritance4() {
     //       runTest(new string[] { "tests/code.generation/more.tests/CG.InterfaceInheritance4.cs" });
     //   }
     //   public void testCGInterfaceVar1() {
     //       runTest(new string[] { "tests/code.generation/more.tests/CG.InterfaceVar1.cs" });
     //   }
     //   public void testCGProperty() {
     //       runTest(new string[] { "tests/code.generation/more.tests/CG.property.cs" });
     //   }
        //public void testCGException1() {
        //    runTest(new string[] { "tests/code.generation/more.tests/CG.Exception1.cs" });
        //}
        //public void testCGException2() {
        //    runTest(new string[] { "tests/code.generation/more.tests/CG.Exception2.cs" });
        //}
        //public void testCGException3() {
        //    runTest(new string[] { "tests/code.generation/more.tests/CG.Exception3.cs" });
        //}
        //public void testCGException4() {
        //    runTest(new string[] { "tests/code.generation/more.tests/CG.Exception4.cs" });
        //}
        //public void testCGException5() {
        //    runTest(new string[] { "tests/code.generation/more.tests/CG.Exception5.cs" });
        //}
        //public void testCGException6() {
        //    runTest(new string[] { "tests/code.generation/more.tests/CG.Exception6.cs" });
        //}
        //public void testCGException7() {
        //    runTest(new string[] { "tests/code.generation/more.tests/CG.Exception7.cs" });
        //}
        //public void testCGException8() {
        //    runTest(new string[] { "tests/code.generation/more.tests/CG.Exception8.cs" });
        //}
        //public void testCGException9() {
        //    runTest(new string[] { "tests/code.generation/more.tests/CG.Exception9.cs" });
        //}

        //public void testCGSample1() {
        //    runTest(new string[] { "tests/code.generation/more.tests/cg.sample1.cs" });
        //}
        //public void testCGSample2() {
        //    runTest(new string[] { "tests/code.generation/more.tests/cg.sample2.cs" });
        ////}
        //public void testCGSample3() {
        //    runTest(new string[] { "tests/code.generation/more.tests/cg.sample3.cs" });
        //}
        //public void testCGSample4() {
        //    runTest(new string[] { "tests/code.generation/more.tests/cg.sample4.cs" });
        //}
       //public void testCGArithmeticLiterals() {
       //     runTest(new string[] { "tests/code.generation/more.tests/CG.ArithmeticLiterals.cs" });
       // }
       //public void testCGArithmeticIds() {
       //    runTest(new string[] { "tests/code.generation/more.tests/CG.ArithmeticIds.cs" });
       //}
      //  public void testCGArithmeticUnionAndLiteral() {
      //      runTest(new string[] { "tests/code.generation/more.tests/CG.ArithmeticUnionAndLiteral.cs" });
        public void testCGC() {
           runTest(new string[] { "tests/code.generation/more.tests/C.cs" });
      }
    }

}


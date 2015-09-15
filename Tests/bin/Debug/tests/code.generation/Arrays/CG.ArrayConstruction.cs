using System;
using System.IO;

namespace ArrayConstruction
{
   //.class private auto ansi beforefieldinit ArrayConstruction.ArrConst extends [mscorlib]System.Object
   class ArrConst
   {
      //  .field private string[] stringArrayField
      private string[] stringArrayField = new string[] { "H", "i" };

      //  .method public hidebysig instance int32[][] Method(bool[] b) cil managed
      public int[][] Method(bool[] b)
      {
         // .locals init (int32[] V_0, int32[] V_1, int32[][] V_2, int32[][] V_3, char[] V_4)

         char[] pp;

         //    IL_0001:  ldc.i4.4
         //    IL_0002:  newarr     [mscorlib]System.Int32
         //    IL_0007:  dup
         //    IL_0008:  ldtoken    field valuetype '<PrivateImplementationDetails>{7EBADB3D-D434-4B0B-B184-8AAC0D195823}'/'__StaticArrayInitTypeSize=16' '<PrivateImplementationDetails>{7EBADB3D-D434-4B0B-B184-8AAC0D195823}'::'$$method0x6000001-1'
         //    IL_000d:  call       void [mscorlib]System.Runtime.CompilerServices.RuntimeHelpers::InitializeArray(class [mscorlib]System.Array, valuetype [mscorlib]System.RuntimeFieldHandle)
         //    IL_0012:  stloc.0
         int[] a = new int[] { 1, 2, 3, 4 };

         //    IL_0013:  ldc.i4.4
         //    IL_0014:  newarr     [mscorlib]System.Int32
         //    IL_0019:  dup
         //    IL_001a:  ldtoken    field valuetype '<PrivateImplementationDetails>{7EBADB3D-D434-4B0B-B184-8AAC0D195823}'/'__StaticArrayInitTypeSize=16' '<PrivateImplementationDetails>{7EBADB3D-D434-4B0B-B184-8AAC0D195823}'::'$$method0x6000001-2'
         //    IL_001f:  call       void [mscorlib]System.Runtime.CompilerServices.RuntimeHelpers::InitializeArray(class [mscorlib]System.Array, valuetype [mscorlib]System.RuntimeFieldHandle)
         //    IL_0024:  stloc.1
         int[] a2 = new int[] { 9, 8, 7, 6 };

         //IL_0025:  ldc.i4.4
         //IL_0026:  newarr     [mscorlib]System.Char
         //IL_002b:  dup
         //IL_002c:  ldtoken    field int64 '<PrivateImplementationDetails>{1E82A016-0CAF-46BA-8F67-4F1F15596907}'::'$$method0x6000001-3'
         //IL_0031:  call       void [mscorlib]System.Runtime.CompilerServices.RuntimeHelpers::InitializeArray(class [mscorlib]System.Array, valuetype [mscorlib]System.RuntimeFieldHandle)
         //IL_0036:  stloc.3
         pp = new char[] { 'H', 'O', 'L', 'A' };

         //    IL_0025:  ldc.i4.2
         //    IL_0026:  newarr     int32[]
         //    IL_002b:  stloc.3

         //    IL_002c:  ldloc.3
         //    IL_002d:  ldc.i4.0
         //    IL_002e:  ldloc.0
         //    IL_002f:  stelem.ref
         //    IL_0030:  ldloc.3
         //    IL_0031:  ldc.i4.1
         //    IL_0032:  ldloc.1
         //    IL_0033:  stelem.ref
         //    IL_0034:  ldloc.3
         //    IL_0035:  stloc.2
         //    IL_0036:  br.s       IL_0038

         //    IL_0038:  ldloc.2
         //    IL_0039:  ret
         return new int[][] { a, a2 };

         //int[][] aux;

         //IL_0025:  ldc.i4.2
         //IL_0026:  newarr     int32[]
         //IL_002b:  stloc.s    V_4
         //aux = new int[][] { a, a2 };


         //IL_002d:  ldloc.s    V_4
         //IL_002f:  ldc.i4.0
         //IL_0030:  ldloc.0
         //IL_0031:  stelem.ref
         //IL_0032:  ldloc.s    V_4
         //IL_0034:  ldc.i4.1
         //IL_0035:  ldloc.1
         //IL_0036:  stelem.ref
         //IL_0037:  ldloc.s    V_4
         //IL_0039:  stloc.2

         //IL_003a:  ldloc.2
         //IL_003b:  stloc.3

         //IL_003c:  br.s       IL_003e

         //IL_003e:  ldloc.3
         //IL_003f:  ret
         //return aux;

         //aux = new int[2][];

         //    IL_0025:  ldc.i4.2
         //    IL_0026:  newarr     int32[]
         //    IL_002b:  stloc.2
         //    IL_002c:  ldloc.2
         //    IL_002d:  stloc.3
         //    IL_002e:  br.s       IL_0030

         //    IL_0030:  ldloc.3
         //    IL_0031:  ret
         //return aux;

         // IL_002c:  ldc.i4.2
         // IL_002d:  newarr     int32[]
         // IL_0032:  stloc.3
         // IL_0033:  br.s       IL_0035

         // IL_0035:  ldloc.3
         // IL_0036:  ret
         //return new int[2][];
      }

      //public int[][] Method(StreamWriter[] sws)
      //{
      //   int[] a = new int[] { 1, 2, 3, 4 };
      //   int[] a2 = new int[] { 9, 8, 7, 6 };
      //   return new int[][] { a, a2 };
      //}

      // .method private hidebysig static void  Main(string[] args) cil managed
      static void Main(string[] args)
      {
         //    .entrypoint
         //    .locals init (class ArrayConstruction.ArrConst V_0, bool[] V_1)

         // bool[] boolArray = new bool[] { true, false};
         // StreamWriter[] sws = new StreamWriter[] { new StreamWriter("pp.txt"), new StreamWriter("tt.txt") };

         //    IL_0001:  newobj     instance void ArrayConstruction.ArrConst::.ctor()
         //    IL_0006:  stloc.0
         ArrConst ac = new ArrConst();

         //    IL_0007:  ldloc.0
         //    IL_0008:  ldc.i4.2
         //    IL_0009:  newarr     [mscorlib]System.Boolean
         //    IL_000e:  stloc.1
         //    IL_000f:  ldloc.1
         //    IL_0010:  ldc.i4.0
         //    IL_0011:  ldc.i4.1
         //    IL_0012:  stelem.i1
         //    IL_0013:  ldloc.1
         //    IL_0014:  callvirt   instance int32[][] ArrayConstruction.ArrConst::Method(bool[])
         //    IL_0019:  call       void [mscorlib]System.Console::WriteLine(object)
         Console.WriteLine(ac.Method(new bool[] { true, false }));

         //    IL_001f:  ret
      }

      //  .method public hidebysig specialname rtspecialname instance void  .ctor() cil managed
      //  {
      //    .locals init (string[] V_0)
      //    IL_0000:  ldarg.0
      //    IL_0001:  ldc.i4.2
      //    IL_0002:  newarr     [mscorlib]System.String
      //    IL_0007:  stloc.0
      //    IL_0008:  ldloc.0
      //    IL_0009:  ldc.i4.0
      //    IL_000a:  ldstr      "H"
      //    IL_000f:  stelem.ref
      //    IL_0010:  ldloc.0
      //    IL_0011:  ldc.i4.1
      //    IL_0012:  ldstr      "i"
      //    IL_0017:  stelem.ref
      //    IL_0018:  ldloc.0
      //    IL_0019:  stfld      string[] ArrayConstruction.ArrConst::stringArrayField
      //    IL_001e:  ldarg.0
      //    IL_001f:  call       instance void [mscorlib]System.Object::.ctor()
      //    IL_0025:  ret
      //  } // end of method ArrConst::.ctor
   }

   //.class private auto ansi '<PrivateImplementationDetails>{7EBADB3D-D434-4B0B-B184-8AAC0D195823}' extends [mscorlib]System.Object
   //{
   //  .custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
   //
   //  .class explicit ansi sealed nested private '__StaticArrayInitTypeSize=16' extends [mscorlib]System.ValueType
   //  {
   //    .pack 1
   //    .size 16
   //  } // end of class '__StaticArrayInitTypeSize=16'
   //
   //  .field static assembly valuetype '<PrivateImplementationDetails>{7EBADB3D-D434-4B0B-B184-8AAC0D195823}'/'__StaticArrayInitTypeSize=16' '$$method0x6000001-1' at I_00002050
   //  .field static assembly valuetype '<PrivateImplementationDetails>{7EBADB3D-D434-4B0B-B184-8AAC0D195823}'/'__StaticArrayInitTypeSize=16' '$$method0x6000001-2' at I_00002060
   //} // end of class '<PrivateImplementationDetails>{7EBADB3D-D434-4B0B-B184-8AAC0D195823}'

   //// =============================================================

   //.data cil I_00002050 = bytearray ( 01 00 00 00 02 00 00 00 03 00 00 00 04 00 00 00) 
   //.data cil I_00002060 = bytearray ( 09 00 00 00 08 00 00 00 07 00 00 00 06 00 00 00) 

}




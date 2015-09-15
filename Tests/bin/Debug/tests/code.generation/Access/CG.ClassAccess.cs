using System.IO;
using System;

namespace ClassAccess
{
   //.class private auto ansi beforefieldinit ClassAccess.MyClass extends [mscorlib]System.Object
   class MyClass
   {
      public static char ce = 'c';

      // .method public hidebysig instance char Method(int32 i, float64 d) cil managed
      public char Method(int i, double d)
      {
         // .locals init (int32 V_0, char V_1)
         int a;

         //    IL_0001:  ldarg.1
         //    IL_0002:  stloc.0
         a = i;

         //    IL_0003:  ldc.i4.s   99
         //    IL_0005:  stloc.1
         //    IL_0006:  br.s       IL_0008
         //    IL_0008:  ldloc.1
         //    IL_0009:  ret
         return ce;
      }

      //  .method public hidebysig specialname rtspecialname instance void  .ctor() cil managed
      //  {
      //    IL_0000:  ldarg.0
      //    IL_0001:  call       instance void [mscorlib]System.Object::.ctor()
      //    IL_0006:  ret
      //  } // end of method MyClass::.ctor
   }

   //.class private auto ansi beforefieldinit ClassAccess.ClassAccess extends [mscorlib]System.Object
   class ClassAccess
   {
      // .method private hidebysig static void  Main(string[] args) cil managed
      static void Main(string[] args)
      {
         // .entrypoint
         // .locals init (class [mscorlib]System.IO.StreamWriter V_0, string V_1)

         //    IL_0001:  ldstr      "pepe.txt"
         //    IL_0006:  newobj     instance void [mscorlib]System.IO.StreamWriter::.ctor(string)
         //    IL_000b:  stloc.0
         StreamWriter w = new StreamWriter("ClassAccess.txt");

         //    IL_000c:  ldloc.0
         //    IL_000d:  newobj     instance void ClassAccess.MyClass::.ctor()
         //    IL_0012:  ldc.i4.3
         //    IL_0013:  ldc.r8     3.3999999999999999
         //    IL_001c:  call       instance char ClassAccess.MyClass::Method(int32, float64)
         //    IL_0021:  callvirt   instance void [mscorlib]System.IO.TextWriter::Write(char)
         w.Write(new MyClass().Method(3, 3.4));

         //    IL_0027:  call       string [mscorlib]System.Console::ReadLine()
         //    IL_002c:  stloc.1
         //string s = Console.ReadLine();
 
         w.Close();

         StreamReader r = new StreamReader("ClassAccess.txt");

         if (!(r.ReadLine().Equals(MyClass.ce.ToString())))
            Environment.Exit(-1);

         r.Close();
         //    IL_002d:  ret
      }

      //  .method public hidebysig specialname rtspecialname instance void  .ctor() cil managed
      //  {
      //    IL_0000:  ldarg.0
      //    IL_0001:  call       instance void [mscorlib]System.Object::.ctor()
      //    IL_0006:  ret
      //  } // end of method ClassAccess::.ctor
   }
}
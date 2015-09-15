using System.IO;
using System;

namespace Constructors
{
   class Parent
   {
      private int aux = 35;

      //.method family hidebysig specialname rtspecialname instance void  .ctor(string str, class [mscorlib]System.IO.StreamReader sr) cil managed
      protected Parent(string str, StreamReader sr)
      {
         //IL_0000:  ldarg.0
         //IL_0001:  ldc.i4.s   35
         //IL_0003:  stfld      int32 Constructors.Parent::aux

         //IL_0008:  ldarg.0
         //IL_0009:  call       instance void [mscorlib]System.Object::.ctor()

         //IL_0011:  ret
      }

      public int MethodP()
      {
         //IL_0001:  ldstr      "Parent method"
         //IL_0006:  call       void [mscorlib]System.Console::WriteLine(string)
         Console.WriteLine("Parent method");

         //IL_000c:  ldc.i4.1
         //IL_000d:  stloc.0
         //IL_000e:  br.s       IL_0010
         //IL_0010:  ldloc.0
         //IL_0011:  ret
         return 1;
      }
   }

   class Child : Parent
   {
      private int aux = 45;
      public int aux2;

      //.method public hidebysig specialname rtspecialname instance void  .ctor(string str, int32 i, class [mscorlib]System.IO.StreamReader sr) cil managed
      public Child(string str, int i, StreamReader sr) : base(str, sr)
      {
         //IL_0000:  ldarg.0
         //IL_0001:  ldc.i4.s   45
         //IL_0003:  stfld      int32 Constructors.Child::aux

         //IL_0008:  ldarg.0
         //IL_0009:  ldarg.1
         //IL_000a:  ldarg.3
         //IL_000b:  call       instance void Constructors.Parent::.ctor(string, class [mscorlib]System.IO.StreamReader)

         //IL_0012:  ldarg.0
         //IL_0013:  ldarg.2
         //IL_0014:  stfld      int32 Constructors.Child::aux2
         aux2 = i;

         //IL_001a:  ret
      }

      public void MethodC()
      {
         //.locals init (int32 V_0, int32 V_1)
         int i;

         //IL_0001:  ldstr      "Child method"
         //IL_0006:  call       void [mscorlib]System.Console::WriteLine(string)
         Console.WriteLine("Child method");

         //IL_000c:  ldarg.0
         //IL_000d:  call       instance int32 Constructors.Parent::MethodP()
         //IL_0012:  stloc.0
         i = base.MethodP();

         //IL_0013:  ldarg.0
         //IL_0014:  call       instance int32 Constructors.Parent::MethodP()
         //IL_0019:  stloc.1
         int j = base.MethodP();

         if ((i != j) || (i != 1))
            Environment.Exit(-1);

         //IL_001a:  ldarg.0
         //IL_001b:  call       instance int32 Constructors.Parent::MethodP()
         //IL_0020:  call       void [mscorlib]System.Console::WriteLine(int32)
         //Console.WriteLine(base.MethodP());

         //IL_0026:  ret
      }

      public static void Method()
      {
         //IL_0001:  ret
      }
   }

   class Constr
   {
      static void Main(string[] args)
      {
         //.entrypoint
         //.locals init (class Constructors.Child V_0)

         //IL_0001:  ldstr      "H"
         //IL_0006:  ldarg.0
         //IL_0007:  ldlen
         //IL_0008:  conv.i4
         //IL_0009:  ldstr      "pp.txt"
         //IL_000e:  newobj     instance void [mscorlib]System.IO.StreamReader::.ctor(string)
         //IL_0013:  newobj     instance void Constructors.Child::.ctor(string, int32, class [mscorlib]System.IO.StreamReader)
         //IL_0018:  stloc.0
         Constructors.Child c = new Child("H", args.Length, new StreamReader("pp.txt"));

         if (c.aux2 != args.Length)
            Environment.Exit(-1);

         //IL_0019:  ldloc.0
         //IL_001a:  callvirt   instance void Constructors.Child::MethodC()
         c.MethodC();

         //IL_0020:  call       void Constructors.Child::Method()
         Constructors.Child.Method();

         //IL_0026:  ret
      }

      //.method public hidebysig specialname rtspecialname instance void  .ctor() cil managed
      //{
      //  IL_0000:  ldarg.0
      //  IL_0001:  call       instance void [mscorlib]System.Object::.ctor()
      //  IL_0006:  ret
      //} // end of method Constr::.ctor
   }
}
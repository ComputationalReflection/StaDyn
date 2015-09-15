using System;

namespace ThisBase
{
   class Parent
   {
      public int number;

      public Parent(int n)
      {
         //IL_0000:  ldarg.0
         //IL_0001:  call       instance void [mscorlib]System.Object::.ctor()

         //IL_0008:  ldarg.0
         //IL_0009:  ldarg.1
         //IL_000a:  stfld      int32 ThisBase.Parent::number
         this.number = n;

         //IL_000f:  ldarg.0
         //IL_0010:  ldarg.1
         //IL_0011:  stfld      int32 ThisBase.Parent::number
         number = n;

         //IL_0016:  ldstr      "Parent "
         //IL_001b:  call       void [mscorlib]System.Console::Write(string)
         //Console.Write("Parent");

         //IL_0021:  ldarg.0
         //IL_0022:  ldfld      int32 ThisBase.Parent::number
         //IL_0027:  call       void [mscorlib]System.Console::WriteLine(int32)
         //Console.WriteLine(number);

         if (this.number != n)
            Environment.Exit(-1);

         if (number != n)
            Environment.Exit(-1);

         //IL_0010:  ret
      }

      public void ParentMethod()
      {
         //IL_0001:  ldc.i4.1
         //IL_0002:  stloc.0
         bool a = true;

         //IL_0003:  ldstr      "ParentMethod "
         //IL_0008:  call       void [mscorlib]System.Console::Write(string)
         //Console.Write("ParentMethod ");

         //IL_000e:  ldloc.0
         //IL_000f:  call       void [mscorlib]System.Console::WriteLine(bool)
         //Console.WriteLine(a);

         if (!a)
            Environment.Exit(-1);

         //IL_0015:  ret
      }

      protected string PCMethod(int a, string c)
      {
         //.locals init (bool V_0)

         bool b;

         //IL_0001:  ldc.i4.0
         //IL_0002:  stloc.0
         b = false;

         //IL_0003:  ldstr      "PCMethod "
         //IL_0008:  call       void [mscorlib]System.Console::Write(string)
         //Console.Write("PCMethod ");

         //IL_000e:  ldarg.1
         //IL_000f:  call       void [mscorlib]System.Console::Write(int32)
         //Console.Write(a);
         if (a != 467)
            Environment.Exit(-1);

         //IL_0015:  ldloc.0
         //IL_0016:  call       void [mscorlib]System.Console::Write(bool)
         //Console.Write(b);

         if (b)
            Environment.Exit(-1);

         //IL_001c:  ldarg.2
         //IL_001d:  call       void [mscorlib]System.Console::WriteLine(string)
         //Console.WriteLine(c);

         //IL_0023:  ret
         return c;
      }
   }

   class Child : Parent
   {
      public Child(int n)
         : base(n)
      {
         //IL_0000:  ldarg.0
         //IL_0001:  ldarg.1
         //IL_0002:  call       instance void ThisBase.Parent::.ctor(int32)

         //IL_0009:  ldstr      "Child "
         //IL_000e:  call       void [mscorlib]System.Console::Write(string)
         //Console.Write("Child ");

         //IL_0014:  ldarg.1
         //IL_0015:  call       void [mscorlib]System.Console::WriteLine(int32)
         //Console.WriteLine(n);

         //IL_001c:  ret

         if (this.number != n)
            Environment.Exit(-1);

         if (number != n)
            Environment.Exit(-1);
      }

      public void ChildMethod()
      {
         //IL_0001:  ldarg.0
         //IL_0002:  call       instance void ThisBase.Parent::ParentMethod()
         base.ParentMethod();

         //IL_0008:  ldarg.0
         //IL_0009:  call       instance void ThisBase.Parent::ParentMethod()
         ParentMethod();

         //IL_0008:  ldarg.0
         //IL_0009:  ldc.i4     0x1d3
         //IL_000e:  ldstr      "Hello"
         //IL_0013:  call       instance void ThisBase.Parent::PCMethod(int32, string)
         if (!(this.PCMethod(467, "Hello").Equals("Hello")))
            Environment.Exit(-1);

         //IL_0019:  ldarg.0
         //IL_001a:  ldc.i4     0x1d3
         //IL_001f:  ldstr      "Bye"
         //IL_0024:  call       instance void ThisBase.Parent::PCMethod(int32, string)
         if (!(PCMethod(467, "Bye").Equals("Bye")))
            Environment.Exit(-1);

         //IL_002a:  ldstr      "ChildMethod"
         //IL_002f:  call       void [mscorlib]System.Console::WriteLine(string)
         //Console.WriteLine("ChildMethod");

         //IL_0035:  ret
      }
   }

   class ThisBase
   {
      static void Main(string[] args)
      {
         //.entrypoint

         //.locals init (class ThisBase.Parent V_0)

         //IL_0001:  ldc.i4.s   24
         //IL_0003:  newobj     instance void ThisBase.Child::.ctor(int32)
         //IL_0008:  stloc.0
         Parent c = new Child(24);

         //IL_0009:  ldloc.0
         //IL_000a:  callvirt   instance void ThisBase.Parent::ParentMethod()
         c.ParentMethod();

         //IL_0010:  ldloc.0
         //IL_0011:  castclass  ThisBase.Child
         //IL_0016:  callvirt   instance void ThisBase.Child::ChildMethod()
         ((Child)c).ChildMethod();

         //IL_001c:  ret
      }

      //.method public hidebysig specialname rtspecialname instance void  .ctor() cil managed
      //{
      //  IL_0000:  ldarg.0
      //  IL_0001:  call       instance void [mscorlib]System.Object::.ctor()
      //  IL_0006:  ret
      //} // end of method ThisBase::.ctor
   }
}

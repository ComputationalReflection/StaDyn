using System;

namespace OperationsMethods
{
   //.class private auto ansi beforefieldinit OperationsMethods.Operations extends [mscorlib]System.Object
   class Operations
   {
      // .field private string field1
      string field1 = "Hello World";

      // .field private static string sfield
      static string sfield = "Hello";


      // .method public hidebysig instance void Method() cil managed
      public void Method()
      {
         //    IL_0001:  ldstr      "Hello world"
         //    IL_0006:  call       void [mscorlib]System.Console::WriteLine(string)
         Console.WriteLine(field1);
         //    IL_000c:  ret
      }

      // .method public hidebysig instance void Method2() cil managed
      public void Method2()
      {
         //    IL_0001:  ldarg.0
         //    IL_0002:  call       instance void OperationsMethods.Operations::Method()
         this.Method();

         //IL_0008:  ldarg.0
         //IL_0009:  call       instance void OperationsMethods.Operations::Method()
         Method();

         //    IL_0008:  ret
      }

      // .method public hidebysig instance void Method3(class OperationsMethods.Operations param) cil managed
      public void Method3(Operations param)
      {
         //    IL_0001:  ldarg.1
         //    IL_0002:  callvirt   instance void OperationsMethods.Operations::Method()
         param.Method();
         //    IL_0008:  ret
      }

      // .method public hidebysig static void  Method4() cil managed
      public static void Method4()
      {
         ////    IL_0001:  ldsfld     string OperationsMethods.Operations::sfield
         ////    IL_0006:  callvirt   instance string [mscorlib]System.String::ToUpper()
         //sfield.ToUpper();
         ////    IL_000b:  pop
         ////    IL_000c:  ret

         //    IL_0001:  ldsfld     string OperationsMethods.Operations::sfield
         //    IL_0006:  callvirt   instance string [mscorlib]System.String::ToUpper()
         string s = sfield.ToUpper();
         //    IL_000b:  pop
         //    IL_000c:  ret
      }

      // .method public hidebysig instance void Method5() cil managed
      public void Method5()
      {
         ////    IL_0001:  ldarg.0
         ////    IL_0002:  ldfld      string OperationsMethods.Operations::'field'
         ////    IL_0007:  callvirt   instance string [mscorlib]System.String::ToLower()
         ////    IL_000c:  pop
         //field1.ToLower();
         ////    IL_000d:  ret

         //    IL_0001:  ldarg.0
         //    IL_0002:  ldfld      string OperationsMethods.Operations::'field'
         //    IL_0007:  callvirt   instance string [mscorlib]System.String::ToLower()
         //    IL_000c:  pop
         string s = field1.ToLower();
         //    IL_000d:  ret
      }

      //  .method private hidebysig static void  Main(string[] args) cil managed
      static void Main(string[] args)
      {
         //    .locals init (class OperationsMethods.Operations V_0)

         //    IL_0001:  newobj     instance void OperationsMethods.Operations::.ctor()
         //    IL_0006:  stloc.0
         Operations local = new Operations();

         //    IL_0007:  ldloc.0
         //    IL_0008:  callvirt   instance void OperationsMethods.Operations::Method()
         local.Method();

         //    IL_000e:  ldloc.0
         //    IL_000f:  callvirt   instance void OperationsMethods.Operations::Method2()
         local.Method2();

         //    IL_0015:  ldloc.0
         //    IL_0016:  ldloc.0
         //    IL_0017:  callvirt   instance void OperationsMethods.Operations::Method3(class OperationsMethods.Operations)
         local.Method3(local);

         //    IL_001d:  call       void OperationsMethods.Operations::Method4()
         Method4();

         ////    IL_0023:  call       string [mscorlib]System.Console::ReadLine()
         ////    IL_0028:  pop
         //Console.ReadLine();
         ////    IL_0029:  ret
      }
   
      //  .method public hidebysig specialname rtspecialname 
      //          instance void  .ctor() cil managed
      //  {
      //    // Code size       19 (0x13)
      //    .maxstack  8
      //    IL_0000:  ldarg.0
      //    IL_0001:  ldstr      "HelloWorld"
      //    IL_0006:  stfld      string OperationsMethods.Operations::'field'
      //    IL_000b:  ldarg.0
      //    IL_000c:  call       instance void [mscorlib]System.Object::.ctor()
      //    IL_0011:  nop
      //    IL_0012:  ret
      //  } // end of method Operations::.ctor

      //  .method private hidebysig specialname rtspecialname static 
      //          void  .cctor() cil managed
      //  {
      //    // Code size       11 (0xb)
      //    .maxstack  8
      //    IL_0000:  ldstr      "Hello"
      //    IL_0005:  stsfld     string OperationsMethods.Operations::sfield
      //    IL_000a:  ret
      //  } // end of method Operations::.cctor

      //} // end of class OperationsMethods.Operations
   }
}
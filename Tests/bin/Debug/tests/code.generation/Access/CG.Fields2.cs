using System;

namespace Fields
{
   //.class public auto ansi beforefieldinit Fields.Field extends [mscorlib]System.Object
   public class Field
   {
      //.field public int32 atrib
      public int atrib = 24;

      //  .method public hidebysig specialname rtspecialname instance void  .ctor() cil managed
      //  {
      //    IL_0000:  ldarg.0
      //    IL_0001:  ldc.i4.s   24
      //    IL_0003:  stfld      int32 Fields.Field::atrib
      //    IL_0008:  ldarg.0
      //    IL_0009:  call       instance void [mscorlib]System.Object::.ctor()
      //    IL_000f:  ret
      //  } // end of method Field::.ctor
   }

   //.class public auto ansi beforefieldinit Fields.MyClass extends [mscorlib]System.Object
   public class MyClass
   {

      //.field private class Fields.Field f
      private Field f = new Field();

      //.method public hidebysig instance class Fields.Field getField() cil managed
      public Field getField()
      {
         //.locals init (class Fields.Field V_0)
         //    IL_0001:  ldarg.0
         //    IL_0002:  ldfld      class Fields.Field Fields.MyClass::f
         //    IL_0007:  stloc.0
         //    IL_0008:  br.s       IL_000a
         //    IL_000a:  ldloc.0
         //    IL_000b:  ret
         return f;
      }

      //  .method public hidebysig specialname rtspecialname instance void  .ctor() cil managed
      //  {
      //    IL_0000:  ldarg.0
      //    IL_0001:  newobj     instance void Fields.Field::.ctor()
      //    IL_0006:  stfld      class Fields.Field Fields.MyClass::f
      //    IL_000b:  ldarg.0
      //    IL_000c:  call       instance void [mscorlib]System.Object::.ctor()
      //    IL_0012:  ret
      //  } // end of method MyClass::.ctor
   }

   // .class private auto ansi beforefieldinit Fields.Program extends [mscorlib]System.Object
   class Program
   {
      // .field private static class Fields.MyClass mc
      private static MyClass mc = new MyClass();
      // .field private class Fields.MyClass mc2
      private MyClass mc2 = new MyClass();

      //  .method private hidebysig static void  Main(string[] args) cil managed
      static void Main(string[] args)
      {
         // .entrypoint

         // .locals init (class Fields.Program V_0)

         //IL_0001:  ldsfld     class Fields.MyClass Fields.Program::mc
         //IL_0006:  callvirt   instance class Fields.Field Fields.MyClass::getField()
         //IL_000b:  ldfld      int32 Fields.Field::atrib
         //IL_0010:  call       void [mscorlib]System.Console::WriteLine(int32)
         //Console.WriteLine(mc.getField().atrib);
         if (mc.getField().atrib != 24)
            Environment.Exit(-1);

         //IL_0016:  newobj     instance void Fields.MyClass::.ctor()
         //IL_001b:  call       instance class Fields.Field Fields.MyClass::getField()
         //IL_0020:  ldfld      int32 Fields.Field::atrib
         //IL_0025:  call       void [mscorlib]System.Console::WriteLine(int32)
         //Console.WriteLine(new MyClass().getField().atrib);

         if (new MyClass().getField().atrib != 24)
            Environment.Exit(-1);

         //IL_002b:  newobj     instance void Fields.Program::.ctor()
         //IL_0030:  ldfld      class Fields.MyClass Fields.Program::mc2
         //IL_0035:  callvirt   instance class Fields.Field Fields.MyClass::getField()
         //IL_003a:  ldfld      int32 Fields.Field::atrib
         //IL_003f:  call       void [mscorlib]System.Console::WriteLine(int32)
         //Console.WriteLine(new Program().mc2.getField().atrib);

         if (new Program().mc2.getField().atrib != 24)
            Environment.Exit(-1);

         //IL_0045:  newobj     instance void Fields.Program::.ctor()
         //IL_004a:  stloc.0         
         Program p = new Program();

         //IL_004b:  ldloc.0
         //IL_004c:  ldfld      class Fields.MyClass Fields.Program::mc2
         //IL_0051:  callvirt   instance class Fields.Field Fields.MyClass::getField()
         //IL_0056:  ldc.i4.s   45
         //IL_0058:  stfld      int32 Fields.Field::atrib
         p.mc2.getField().atrib = 45;

         if (p.mc2.getField().atrib != 45)
            Environment.Exit(-1);

         //IL_005d:  ldloc.0
         //IL_005e:  ldfld      class Fields.MyClass Fields.Program::mc2
         //IL_0063:  callvirt   instance class Fields.Field Fields.MyClass::getField()

         //IL_0068:  newobj     instance void Fields.Program::.ctor()
         //IL_006d:  ldfld      class Fields.MyClass Fields.Program::mc2
         //IL_0072:  callvirt   instance class Fields.Field Fields.MyClass::getField()
         //IL_0077:  ldfld      int32 Fields.Field::atrib
         //IL_007c:  ldc.i4.1
         //IL_007d:  add

         //IL_007e:  stfld      int32 Fields.Field::atrib
         p.mc2.getField().atrib = new Program().mc2.getField().atrib + 1;

         //IL_005d:  ldloc.0
         //IL_005e:  ldfld      class Fields.MyClass Fields.Program::mc2
         //IL_0063:  callvirt   instance class Fields.Field Fields.MyClass::getField()
         //IL_0068:  ldfld      int32 Fields.Field::atrib
         //IL_006d:  call       void [mscorlib]System.Console::WriteLine(int32)
         //Console.WriteLine(p.mc2.getField().atrib);

         if (p.mc2.getField().atrib != 25)
            Environment.Exit(-1);

         // Console.WriteLine((new Program().mc2.getField().atrib = 45));
         // Console.ReadLine();

         //IL_0073:  ret
      }

      //  .method public hidebysig specialname rtspecialname instance void  .ctor() cil managed
      //  {
      //    IL_0000:  ldarg.0
      //    IL_0001:  newobj     instance void Fields.MyClass::.ctor()
      //    IL_0006:  stfld      class Fields.MyClass Fields.Program::mc2
      //    IL_000b:  ldarg.0
      //    IL_000c:  call       instance void [mscorlib]System.Object::.ctor()
      //    IL_0012:  ret
      //  } // end of method Program::.ctor

      //  .method private hidebysig specialname rtspecialname static void  .cctor() cil managed
      //  {
      //    IL_0000:  newobj     instance void Fields.MyClass::.ctor()
      //    IL_0005:  stsfld     class Fields.MyClass Fields.Program::mc
      //    IL_000a:  ret
      //  } // end of method Program::.cctor

   }
}
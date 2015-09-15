using System.IO;
using System;
namespace C {
 
   class Constants
   {
      //.field static family literal char character = char(0x0043)
      protected const char character = 'C';

      //.field private static literal int32 number = int32(0x00000004)
      private const int number = 4;

      //.field public static literal class [mscorlib]System.IO.StreamReader sr = nullref
      public const StreamReader sr = null;

      // los únicos valores posibles para constantes de tipos de referencia son string y null.
      // public const TextWriter sw = Console.Out; // Error

      public void Method()
      {
         //.locals init (string V_0)

         const bool boolean = true;

         const string str = "Hello";
         const string str2 = "World";

         //IL_0000:  nop
         //IL_0001:  ldstr      "Hello World"
         //IL_0006:  stloc.0
         string aux = str + " " + str2;

         //IL_0007:  ldc.i4.1
         //IL_0008:  call       void [mscorlib]System.Console::WriteLine(bool)
         //Console.WriteLine(boolean);

         if (boolean != true)
            Environment.Exit(-1);

         //IL_000e:  ldloc.0
         //IL_000f:  call       void [mscorlib]System.Console::WriteLine(string)
         //Console.WriteLine(aux);
         if (!(aux.Equals("Hello World")))
            Environment.Exit(-1);

         //IL_0015:  ldc.i4.s   30
         //IL_0017:  call       void [mscorlib]System.Console::WriteLine(int32)
         //Console.WriteLine(number + 26);
         if ((number + 26) != 30)
            Environment.Exit(-1);

         //IL_001d:  ret
      }

      //.method public hidebysig specialname rtspecialname instance void  .ctor() cil managed
      //{
      //  IL_0000:  ldarg.0
      //  IL_0001:  call       instance void [mscorlib]System.Object::.ctor()
      //  IL_0006:  ret
      //} // end of method Constants::.ctor

      static void Main(string[] args)
      {
         //.entrypoint

         //.locals init (class Constants.Constants V_0)

         //IL_0001:  newobj     instance void Constants.Constants::.ctor()
         //IL_0006:  stloc.0
         Constants c = new Constants();

         //IL_0007:  ldloc.0
         //IL_0008:  callvirt   instance void Constants.Constants::Method()
         c.Method();

         //IL_000e:  ret
      }
   }
}
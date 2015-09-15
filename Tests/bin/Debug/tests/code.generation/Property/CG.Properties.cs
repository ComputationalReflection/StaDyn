using System;
using System.Text;

namespace Properties
{
   //.class private auto ansi beforefieldinit Properties.Properties extends [mscorlib]System.Object
   class Properties
   {
      //.method private hidebysig static void  Main(string[] args) cil managed
      static void Main(string[] args)
      {
         //  .entrypoint
         //  .locals init (string V_0, class [mscorlib]System.IO.StreamWriter V_1)

         // Not available in framework 1.1

         //  IL_0001:  call       int32 [mscorlib]System.Console::get_WindowHeight()
         //  IL_0006:  ldc.i4.2
         //  IL_0007:  div
         //  IL_0008:  call       void [mscorlib]System.Console::set_WindowHeight(int32)
         //Console.WindowHeight = Console.WindowHeight / 2;

         //  IL_000e:  call       int32 [mscorlib]System.Console::get_WindowWidth()
         //  IL_0013:  ldc.i4.2
         //  IL_0014:  div
         //  IL_0015:  call       void [mscorlib]System.Console::set_WindowWidth(int32)
         //Console.WindowWidth = Console.WindowWidth / 2;

         //  IL_001b:  call       string [mscorlib]System.Console::ReadLine()
         //  IL_0020:  stloc.0
         //string p = Console.ReadLine();

         //  IL_0021:  call       int32 [mscorlib]System.Console::get_WindowHeight()
         //  IL_0026:  ldc.i4.2
         //  IL_0027:  mul
         //  IL_0028:  call       void [mscorlib]System.Console::set_WindowHeight(int32)
         //Console.WindowHeight = Console.WindowHeight * 2;

         //  IL_002e:  call       int32 [mscorlib]System.Console::get_WindowWidth()
         //  IL_0033:  ldc.i4.2
         //  IL_0034:  mul
         //  IL_0035:  call       void [mscorlib]System.Console::set_WindowWidth(int32)
         //Console.WindowWidth = Console.WindowWidth * 2;

         //IL_003b:  ldstr      "pp.txt"
         //IL_0040:  newobj     instance void [mscorlib]System.IO.StreamWriter::.ctor(string)
         //IL_0045:  stloc.1
         System.IO.StreamWriter sw = new System.IO.StreamWriter("pp.txt");

         //IL_0046:  ldloc.1
         //IL_0047:  ldc.i4.1
         //IL_0048:  callvirt   instance void [mscorlib]System.IO.StreamWriter::set_AutoFlush(bool)
         sw.AutoFlush = true;

         if (!(sw.AutoFlush))
            Environment.Exit(-1);

         //Console.WriteLine(sw.AutoFlush);

         if (!(Encoding.UTF8.EncodingName.Equals("Unicode (UTF-8)")))
            Environment.Exit(-1);
		Console.WriteLine("Successfull!!");

         //Console.WriteLine(sw.Encoding.EncodingName); //Unicode (UTF-8)
  
         //IL_004e:  ret
      }

      //.method public hidebysig specialname rtspecialname instance void  .ctor() cil managed
      //{
      //  IL_0000:  ldarg.0
      //  IL_0001:  call       instance void [mscorlib]System.Object::.ctor()
      //  IL_0006:  ret
      //} // end of method Properties::.ctor
   }
}

using System;
using System.IO;

namespace Calls
{
   class Calls
   {
      public TextReader r;
      private static TextWriter w = new StreamWriter("pp2.txt");
      private string s = "Equals";

      public void Method()
      {
         //IL_0000:  nop
         //IL_0001:  ldarg.0
         //IL_0002:  ldstr      "tt.txt"
         //IL_0007:  newobj     instance void [mscorlib]System.IO.StreamReader::.ctor(string)
         //IL_000c:  stfld      class [mscorlib]System.IO.TextReader Calls.Calls::r
         this.r = new StreamReader("pp2.txt");

         //IL_0011:  ldarg.0
         //IL_0012:  ldfld      class [mscorlib]System.IO.TextReader Calls.Calls::r
         //IL_0017:  callvirt   instance string [mscorlib]System.Object::ToString()
         //IL_001c:  callvirt   instance string [mscorlib]System.String::ToLower()
         //IL_0021:  callvirt   instance object [mscorlib]System.String::Clone()
         //IL_0026:  ldarg.0
         //IL_0027:  ldfld      string Calls.Calls::s
         //IL_002c:  callvirt   instance bool [mscorlib]System.Object::Equals(object)
         bool b = r.ToString().ToLower().Clone().Equals(this.s);

         if (b)
            Environment.Exit(-1);

         //IL_0031:  stloc.0
         //IL_0032:  ret

         if (!(this.r.ReadLine().Equals("Bye")))
            Environment.Exit(-1);
      }

      static void Main(string[] args)
      {
         //IL_0000:  nop
         //IL_0001:  ldstr      "Hello world"
         //IL_0006:  call       void [mscorlib]System.Console::WriteLine(string)
         Console.WriteLine("Hello world");

         //IL_000b:  nop
         //IL_000c:  ldstr      "pp.txt"
         //IL_0011:  newobj     instance void [mscorlib]System.IO.StreamWriter::.ctor(string)
         //IL_0016:  ldstr      "Hello world"
         //IL_001b:  callvirt   instance void [mscorlib]System.IO.TextWriter::WriteLine(string)
         new StreamWriter("pp.txt").WriteLine("Hello world");

         //IL_0020:  nop
         //IL_0021:  ldsfld     class [mscorlib]System.IO.TextWriter Calls.Calls::w
         //IL_0026:  call       class [mscorlib]System.IO.TextWriter [mscorlib]System.IO.TextWriter::Synchronized(class [mscorlib]System.IO.TextWriter)
         w = TextWriter.Synchronized(w);

         w.WriteLine("Bye");
         w.Close();

         //IL_002b:  stsfld     class [mscorlib]System.IO.TextWriter Calls.Calls::w

         //IL_002c:  newobj     instance void Calls.Calls::.ctor()
         //IL_0031:  call       instance void Calls.Calls::Method()
         //IL_0036:  nop
         new Calls().Method();
         //IL_0037:  ret
      }

      //.method public hidebysig specialname rtspecialname 
      //       instance void  .ctor() cil managed
      //{
      // // Code size       19 (0x13)
      // .maxstack  8
      // IL_0000:  ldarg.0
      // IL_0001:  ldstr      "Equals"
      // IL_0006:  stfld      string Calls.Calls::s
      // IL_000b:  ldarg.0
      // IL_000c:  call       instance void [mscorlib]System.Object::.ctor()
      // IL_0011:  nop
      // IL_0012:  ret
      //} // end of method Calls::.ctor

      //.method private hidebysig specialname rtspecialname static 
      //       void  .cctor() cil managed
      //{
      // // Code size       16 (0x10)
      // .maxstack  8
      // IL_0000:  ldstr      "pp2.txt"
      // IL_0005:  newobj     instance void [mscorlib]System.IO.StreamWriter::.ctor(string)
      // IL_000a:  stsfld     class [mscorlib]System.IO.TextWriter Calls.Calls::w
      // IL_000f:  ret
      //} // end of method Calls::.cctor

   }
}

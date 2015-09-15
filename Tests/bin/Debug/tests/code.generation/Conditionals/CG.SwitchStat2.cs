using System;
using System.Collections.Generic;
using System.Text;

namespace SwitchStat
{
   class SwitchStat
   {
      public string MethodSwitch(int a, int b)
      {
         StringBuilder aux = new StringBuilder();

         //IL_0006:  ldloc.0
         //IL_0007:  ldc.i4.1
         //IL_0008:  sub
         //IL_0009:  switch     (IL_001c, IL_0025, IL_0025)
         //IL_001a:  br.s       IL_002f // DEFAULT
         switch (a = b)
         {
            case 1:
               //Console.WriteLine(1);
               aux.Append(1);
               break;
            case 2:
            case 3:
               //Console.WriteLine(23);
               aux.Append(23);
               break;
            default:
               //Console.WriteLine("default");
               aux.Append("default");
               break;
         }

         return aux.ToString();
      }

      public string MethodSwitch2(int a)
      {
         StringBuilder aux = new StringBuilder();

         //IL_0006:  ldloc.0
         //IL_0007:  switch     (IL_001a, IL_0023, IL_0023)
         //IL_0018:  br.s       IL_002d
         switch (a)
         {
            case 0:
               //Console.WriteLine(0);
               aux.Append(0);
               break;
            case 1:
            case 2:
               //Console.WriteLine(12);
               aux.Append(12);
               break;
            default:
               //Console.WriteLine("default");
               aux.Append("default");
               break;
         }

         return aux.ToString();
      }

      public string MethodSwitch3(int a)
      {
         StringBuilder aux = new StringBuilder();

         //IL_0006:  ldloc.0
         //IL_0007:  ldc.i4.s   21
         //IL_0009:  beq.s      IL_0020

         //IL_000b:  ldloc.0
         //IL_000c:  ldc.i4.s   57
         //IL_000e:  beq.s      IL_0020

         //IL_0010:  ldloc.0
         //IL_0011:  ldc.i4.s   80
         //IL_0013:  beq.s      IL_0017

         //IL_0015:  br.s       IL_002a // DEFAULT
         switch (a)
         {
            case 80:
               //Console.WriteLine(80);
               aux.Append(80);
               break;
            case 21:
            case 57:
               //Console.WriteLine(2157);
               aux.Append(2157);
               break;
            default:
               //Console.WriteLine("default");
               aux.Append("default");
               break;
         }

         return aux.ToString();
      }

      public string MethodSwitch4(int a)
      {
         StringBuilder aux = new StringBuilder();

         //IL_0003:  ldloc.0
         //IL_0004:  ldc.i4.2
         //IL_0005:  beq.s      IL_001a

         //IL_0007:  ldloc.0
         //IL_0008:  ldc.i4.5
         //IL_0009:  beq.s      IL_001a

         //IL_000b:  ldloc.0
         //IL_000c:  ldc.i4.8
         //IL_000d:  beq.s      IL_0011

         //IL_000f:  br.s       IL_0024
         switch (a)
         {
            case 8:
               //Console.WriteLine(8);
               aux.Append(8);
               break;
            case 2:
            case 5:
               //Console.WriteLine(25);
               aux.Append(25);
               break;
         }

         return aux.ToString();
      }

      public string MethodSwitch5(int a)
      {
         StringBuilder aux = new StringBuilder();

         //  IL_0001:  ldarg.1
         //  IL_0002:  stloc.0

         //  IL_0003:  ldloc.0
         //  IL_0004:  ldc.i4.5
         //  IL_0005:  beq.s      IL_0016

         //  IL_0007:  ldloc.0
         //  IL_0008:  ldc.i4.8
         //  IL_0009:  beq.s      IL_000d

         //  IL_000b:  br.s       IL_0016
         switch (a)
         {
            case 8:
               //  IL_000d:  ldc.i4.8
               //  IL_000e:  call       void [mscorlib]System.Console::WriteLine(int32)
               //  IL_0013:  nop
               //  IL_0014:  br.s       IL_0023
               //Console.WriteLine(8);
               aux.Append(8);
               break;
            case 5:
            default:
               //  IL_0016:  ldstr      "default5"
               //  IL_001b:  call       void [mscorlib]System.Console::WriteLine(string)
               //  IL_0020:  nop
               //  IL_0021:  br.s       IL_0023
               //Console.WriteLine("default5");
               aux.Append("default5");
               break;
         }
         //  IL_0023:  ret

         return aux.ToString();
      }

      static void Main(string[] args)
      {
         SwitchStat ss = new SwitchStat();
         if (!(ss.MethodSwitch(8, 1).Equals("1")))
            Environment.Exit(-1);
         if (!(ss.MethodSwitch(8, 2).Equals("23")))
            Environment.Exit(-1);
         if (!(ss.MethodSwitch(8, 3).Equals("23")))
            Environment.Exit(-1);
         if (!(ss.MethodSwitch(8, 4).Equals("default")))
            Environment.Exit(-1);
         if (!(ss.MethodSwitch2(0).Equals("0")))
            Environment.Exit(-1);
         if (!(ss.MethodSwitch2(1).Equals("12")))
            Environment.Exit(-1);
         if (!(ss.MethodSwitch2(2).Equals("12")))
            Environment.Exit(-1);
         if (!(ss.MethodSwitch2(3).Equals("default")))
            Environment.Exit(-1);
         if (!(ss.MethodSwitch3(80).Equals("80")))
            Environment.Exit(-1);
         if (!(ss.MethodSwitch3(21).Equals("2157")))
            Environment.Exit(-1);
         if (!(ss.MethodSwitch3(57).Equals("2157")))
            Environment.Exit(-1);
         if (!(ss.MethodSwitch3(11).Equals("default")))
            Environment.Exit(-1);
         if (!(ss.MethodSwitch4(8).Equals("8")))
            Environment.Exit(-1);
         if (!(ss.MethodSwitch4(2).Equals("25")))
            Environment.Exit(-1);
         if (!(ss.MethodSwitch4(5).Equals("25")))
            Environment.Exit(-1);
         if (!(ss.MethodSwitch4(21).Equals("")))
            Environment.Exit(-1);
         if (!(ss.MethodSwitch5(8).Equals("8")))
            Environment.Exit(-1);
         if (!(ss.MethodSwitch5(5).Equals("default5")))
            Environment.Exit(-1);
         if (!(ss.MethodSwitch5(42).Equals("default5")))
            Environment.Exit(-1);
      }
   }
}

using System.IO;
using System.Collections;
using System;

namespace ImplicitConversion
{
   class ImplicitConversion
   {
      private int atrib = 34;
      private static int staticAtrib = 81;

      public void Method1()
      {
         string n = "H";

         //IL_000e:  ldstr      "file.txt"
         //IL_0013:  newobj     instance void [mscorlib]System.IO.StreamWriter::.ctor(string)
         //IL_0018:  stloc.3
         StreamWriter sw = new StreamWriter("file.txt");

         //IL_0019:  ldloc.3
         //IL_001a:  stloc.s    V_4
         TextWriter tw = sw; // Implicit Promotion (StreamWriter to TextWriter)

         Object o;

         //IL_0046:  ldloc.2
         //IL_0047:  stloc.s    V_5
         o = n; // Implicit Conversion (string to object)
         //IL_0049:  ldloc.s    V_5
         //IL_004b:  callvirt   instance string [mscorlib]System.Object::ToString()
         //IL_0050:  call       void [mscorlib]System.Console::WriteLine(string)
         //Console.WriteLine(o.ToString());

         if (!(o.ToString().Equals(n)))
            Environment.Exit(-1);

         //IL_0056:  ldloc.3
         //IL_0057:  stloc.s    V_5
         o = sw; // Implicit Conversion (StreamWriter to object)
         //IL_0059:  ldloc.s    V_5
         //IL_005b:  callvirt   instance string [mscorlib]System.Object::ToString()
         //IL_0060:  call       void [mscorlib]System.Console::WriteLine(string)
         //Console.WriteLine(o.ToString());

         if (!(o.ToString().Equals(sw.ToString())))
            Environment.Exit(-1);

         //IL_0066:  ldloc.s    V_4
         //IL_0068:  stloc.s    V_5
         o = tw; // Implicit Conversion (TextWriter to object)
         //IL_006a:  ldloc.s    V_5
         //IL_006c:  callvirt   instance string [mscorlib]System.Object::ToString()
         //IL_0071:  call       void [mscorlib]System.Console::WriteLine(string)
         //Console.WriteLine(o.ToString());

         if (!(o.ToString().Equals(tw.ToString())))
            Environment.Exit(-1);
      }

      public void Method2(int argument)
      {
         //.locals init (int32 V_0, float64 V_1, string V_2, object V_3)

         //IL_0001:  ldc.i4.6
         //IL_0002:  stloc.0
         int a = 6;

         //IL_0003:  ldloc.0
         //IL_0004:  conv.r8
         //IL_0005:  stloc.1
         double b = a; // Implicit Promotion (int to double)

         //IL_0006:  ldloca.s   V_0
         //IL_0008:  call       instance string [mscorlib]System.Int32::ToString()
         //IL_000d:  stloc.2
         string n = a.ToString();

         //Console.WriteLine(atrib.ToString());
         //Console.WriteLine(staticAtrib.ToString());
         //Console.WriteLine(argument.ToString());

         if (!(atrib.ToString().Equals("34")))
            Environment.Exit(-1);

         if (!(staticAtrib.ToString().Equals("81")))
            Environment.Exit(-1);

         if (!(argument.ToString().Equals("345")))
            Environment.Exit(-1);

         //IL_000e:  ldloc.1
         //IL_000f:  ldloc.0
         //IL_0010:  conv.r8
         //IL_0011:  add
         //IL_0012:  stloc.1
         b += a;

         Object o;

         //IL_0013:  ldloc.0
         //IL_0014:  box        [mscorlib]System.Int32
         //IL_0019:  stloc.3
         o = a; // Implicit Conversion (int to object)
         //IL_001a:  ldloc.3
         //IL_001b:  callvirt   instance string [mscorlib]System.Object::ToString()
         //IL_0020:  call       void [mscorlib]System.Console::WriteLine(string)
         //Console.WriteLine(o.ToString());

         if (!(o.ToString().Equals(a.ToString())))
            Environment.Exit(-1);

         //IL_0026:  ldloc.1
         //IL_0027:  box        [mscorlib]System.Double
         //IL_002c:  stloc.3
         o = b; // Implicit Conversion (double to object)
         //IL_002d:  ldloc.3
         //IL_002e:  callvirt   instance string [mscorlib]System.Object::ToString()
         //IL_0033:  call       void [mscorlib]System.Console::WriteLine(string)
         //Console.WriteLine(o.ToString());

         if (!(o.ToString().Equals(b.ToString())))
            Environment.Exit(-1);

         //IL_0039:  ldloc.2
         //IL_003a:  stloc.3
         o = n; // Implicit Conversion (string to object)
         //IL_003b:  ldloc.3
         //IL_003c:  callvirt   instance string [mscorlib]System.Object::ToString()
         //IL_0041:  call       void [mscorlib]System.Console::WriteLine(string)
         //Console.WriteLine(o.ToString());

         if (!(o.ToString().Equals(n)))
            Environment.Exit(-1);

         //IL_0047:  ret
      }

      public void Method3()
      {
         //.locals init (int32 V_0, float64 V_1, char V_2, string V_3, bool V_4,
         //              int32 V_5, int32 V_6, object[] V_7)

         //IL_0001:  ldc.i4.s   24
         //IL_0003:  stloc.0
         int i = 24;

         //IL_0004:  ldc.r8     19.800000000000001
         //IL_000d:  stloc.1
         double d = 19.80;

         //IL_000e:  ldc.i4.s   67
         //IL_0010:  stloc.2
         char c = 'C';

         //IL_0011:  ldstr      "Madrid"
         //IL_0016:  stloc.3
         string s = "Madrid";

         //IL_0017:  ldc.i4.1
         //IL_0018:  stloc.s    V_4
         bool b = true;

         //IL_001a:  ldstr      "{0}\t{1}\t{2}\t{3}\t{4}"
         //IL_001f:  ldc.i4.5
         //IL_0020:  newarr     [mscorlib]System.Object
         //IL_0025:  stloc.s    V_7

         //IL_0027:  ldloc.s    V_7
         //IL_0029:  ldc.i4.0
         //IL_002a:  ldloc.0
         //IL_002b:  box        [mscorlib]System.Int32
         //IL_0030:  stelem.ref

         //IL_0031:  ldloc.s    V_7
         //IL_0033:  ldc.i4.1
         //IL_0034:  ldloc.1
         //IL_0035:  box        [mscorlib]System.Double
         //IL_003a:  stelem.ref

         //IL_003b:  ldloc.s    V_7
         //IL_003d:  ldc.i4.2
         //IL_003e:  ldloc.2
         //IL_003f:  box        [mscorlib]System.Char
         //IL_0044:  stelem.ref

         //IL_0045:  ldloc.s    V_7
         //IL_0047:  ldc.i4.3
         //IL_0048:  ldloc.3
         //IL_0049:  stelem.ref

         //IL_004a:  ldloc.s    V_7
         //IL_004c:  ldc.i4.4
         //IL_004d:  ldloc.s    V_4
         //IL_004f:  box        [mscorlib]System.Boolean
         //IL_0054:  stelem.ref

         //IL_0055:  ldloc.s    V_7
         //IL_0057:  call       void [mscorlib]System.Console::WriteLine(string, object[])
//         Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", i, d, c, s, b);

         //IL_005d:  ldc.i4.s   45
         //IL_005f:  stloc.s    V_5
         System.Int32 integer = 45;

         //IL_0061:  ldloc.s    V_5
         //IL_0063:  stloc.s    V_6
         int integer2 = integer;

         //IL_0065:  ldstr      "{0}\t{1}"
         //IL_006a:  ldloc.s    V_5
         //IL_006c:  box        [mscorlib]System.Int32
         //IL_0071:  ldloc.s    V_6
         //IL_0073:  box        [mscorlib]System.Int32

         //IL_0078:  call       void [mscorlib]System.Console::WriteLine(string, object, object)
         Console.WriteLine("{0}\t{1}", integer, integer2);

         object o1 = integer;
         Object o2 = integer2;
         //Console.WriteLine("{0}\t{1}", o1, o2);

         if (!(o1.ToString().Equals(integer2.ToString())))
            Environment.Exit(-1);

         //IL_007e:  ret
      }

      public void Method4()
      {
         ArrayList intList = new ArrayList();
         intList.Add(256);

         int number = (int)intList[0];

         //Console.WriteLine(number);
         //Console.WriteLine(intList[0]);

         if (!(intList[0].ToString().Equals(number.ToString())))
            Environment.Exit(-1);

         intList[0] = 1024;

         //Console.WriteLine(intList[0]);
         if (!(intList[0].ToString().Equals("1024")))
            Environment.Exit(-1);

         Object[] objArray = new object[] { 1, 2, 3 };

         //Console.WriteLine(objArray[2]);
         if (!(objArray[2].ToString().Equals("3")))
            Environment.Exit(-1);
      }

      public int Method5()
      {
         int number;
         object thing;

         number = 42;

         // Boxing
         thing = number;

         // Unboxing
         number = (int)thing;

         //Console.WriteLine((int)thing);
         if ((int)thing != number)
            Environment.Exit(-1);

         return (int)thing;
      }

      static void Main(string[] args)
      {
         ImplicitConversion ic = new ImplicitConversion();
         ic.Method1();
         ic.Method2(345);
         ic.Method3();
         ic.Method4();
         ic.Method5();
      }
   }
}

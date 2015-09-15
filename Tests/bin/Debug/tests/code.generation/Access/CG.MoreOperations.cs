using System;
using System.IO;

namespace MoreOperations
{
   class Auxiliar
   {
      public int atribInt = 22;
      public int[] atribArrInt = new int[] { 12, 45, 68, 36, 27 };
      public static int atribStaticInt = 3;
      public static int[] atribArrayStaticInt = new int[] { 3, 5, 7, 11, 17, 21 };
   }

   class Operations
   {
      public void MethodProperties(StreamWriter sw, string[] s)
      {
         sw.NewLine = s[0];
         if (!(sw.NewLine.Equals(s[0])))
            Environment.Exit(-1);

         sw.NewLine += s[1];
         if (!(sw.NewLine.Equals(s[0] + s[1])))
            Environment.Exit(-1);

         sw.NewLine = sw.NewLine + sw.NewLine;
         if (!(sw.NewLine.Equals("HiHi")))
            Environment.Exit(-1);

         // Not available in framework 1.1
         //int aux = Console.WindowHeight;
         //Console.WriteLine(Console.WindowHeight -= Console.WindowHeight / 2);
         //Console.WindowHeight = aux;
      }

      public void PublicFields()
      {
         Auxiliar a = new Auxiliar();

         // Binary Expression
         // -----------------

         // Arithmethic
         if ((((double)(a.atribInt * Auxiliar.atribStaticInt)) / ((double)a.atribArrInt[0] * Auxiliar.atribArrayStaticInt[0])) != (66.0 / 36.0))
            Environment.Exit(-1);

         if ((((double)(a.atribInt * Auxiliar.atribStaticInt)) / ((double)(a.atribArrInt[0] * Auxiliar.atribArrayStaticInt[0]))) != (66.0 / 36.0))
            Environment.Exit(-1);

         if ((((a.atribInt * a.atribArrInt[3] / 2) % 2) - a.atribInt * (a.atribArrInt[2] / (a.atribArrInt[3] - Auxiliar.atribStaticInt))) != -44)
            Environment.Exit(-1);

         // Bitwise

         if ((a.atribInt | a.atribArrInt[2]) != 86)
            Environment.Exit(-1);

         if ((Auxiliar.atribStaticInt & Auxiliar.atribArrayStaticInt[3]) != 3)
            Environment.Exit(-1);

         // Assignment

         if ((a.atribInt = Auxiliar.atribStaticInt) != 3)
            Environment.Exit(-1);

         if ((a.atribArrInt[2] += Auxiliar.atribArrayStaticInt[2]) != 75)
            Environment.Exit(-1);

         if ((Auxiliar.atribArrayStaticInt[3] *= a.atribArrInt[3]) != 396)
            Environment.Exit(-1);

         if ((Auxiliar.atribStaticInt -= a.atribInt) != 0)
            Environment.Exit(-1);

         // Ternary Expression
         // ------------------

         if (((a.atribInt > Auxiliar.atribStaticInt) && (a.atribArrInt[1] != Auxiliar.atribArrayStaticInt[2]) ? (a.atribInt += (Auxiliar.atribStaticInt * a.atribInt)) : (a.atribInt -= (Auxiliar.atribStaticInt / a.atribInt))) != 3)
            Environment.Exit(-1);

         // Unary Expression
         // ----------------

         // No funcionan porque estan haciendo un dup y falta indicar el acceso anterior, o eso creo.
         // Revisar lo que compila

         if ((a.atribInt++) != 3)
            Environment.Exit(-1);

         if ((++Auxiliar.atribStaticInt) != 1)
            Environment.Exit(-1);

         if ((Auxiliar.atribArrayStaticInt[3]--) != 396)
            Environment.Exit(-1);

         if ((--a.atribArrInt[2]) != 74)
            Environment.Exit(-1);
      }

      public void Method(string [] arrStr)
      {
         int[] local = new int[] { 2, 4, 6, 8, 10};
         string s;

         // Concat --> atributos estaticos y no estaticos, vars locales, parametros, arrays, propiedades, ...

         // Binary Expression
         // -----------------

         // Arithmethic
         s = arrStr[0] + arrStr[1];
         if (!(s.Equals("Hello World")))
            Environment.Exit(-1);

         if ((((double)(local[2] * local[3])) / (double)local[0]) != 24)
            Environment.Exit(-1);

         // Bitwise

         if ((local[3] | local[0]) != 10)
            Environment.Exit(-1);

         // Relational

         if ((local[3] != local[0]) != true)
            Environment.Exit(-1);

         // Assignment

         if ((local[4] = local[0]) != 2)
            Environment.Exit(-1);

         arrStr[0] += arrStr[1];
         if (!(arrStr[0].Equals("Hello World")))
            Environment.Exit(-1);

         if ((local[1] *= local[0]) != 8)
            Environment.Exit(-1);

         // Unary Expression
         // ----------------

         if ((local[0]++) != 2)
            Environment.Exit(-1);

         if ((--local[0]) != 2)
            Environment.Exit(-1);

         // Ternary Expression
         // ------------------

         if (((local[2] < local[3]) && (local[0] != local[1]) ? true : false) != true)
            Environment.Exit(-1);
      }

      static void Main(string[] args)
      {
         Operations op = new Operations();
         op.PublicFields();
         op.MethodProperties(new StreamWriter("pp.txt"), new string[] { "H", "i"});
         op.Method(new string[] {"Hello", " World"});
      }
   }
}

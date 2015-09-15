using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NSTestsStats2
{
   class Auxiliar
   {
      public int atribInt = 22;
      public int[] atribArrInt = new int[] { 12, 45, 68, 36, 27 };
      public static int atribStaticInt = 3;
      public static int[] atribArrayStaticInt = new int[] { 3, 5, 7, 11, 17, 21 };

      public FileInfo[] fi = new FileInfo[4];

      public void createFileInfoArray()
      {
         for (int i = 0; i < fi.Length; i++)
         {
            string name = i.ToString() + ".txt";
            fi[i] = new FileInfo(name);
         }
      }
   }

   class TestsStats2
   {
      private static int a = 24;
      private int b = 24 + 6;
      private static bool c = false;
      private bool d = true;

      public string ForStat()
      {
         StringBuilder aux = new StringBuilder();

         Auxiliar a = new Auxiliar();
         a.createFileInfoArray();
         for (int i = 0; i < a.fi.Length; i++)
         {
            aux.AppendLine(a.fi[i].Name);
         }

         return aux.ToString();
      }

      public string WhileStat()
      {
         StringBuilder aux = new StringBuilder();

         while ((d && (b > a)) || (c))
         {
            aux.AppendLine("(" + this.d + " AND (" + this.b + " > " + a + ")) OR " + c);
            a = a + 2;
         }

         aux.AppendLine("(" + this.d + " AND (" + this.b + " > " + a + ")) OR " + c);

         return aux.ToString();
      }

      public string DoStat()
      {
         StringBuilder aux = new StringBuilder();

         Auxiliar a = new Auxiliar();

         do
         {
            aux.AppendLine("(" + Auxiliar.atribStaticInt + " < " + a.atribInt + ") AND (" + a.atribArrInt[0] + " <= " + Auxiliar.atribArrayStaticInt[4] + ")");
            a.atribArrInt[0] = a.atribArrInt[0] + Auxiliar.atribArrayStaticInt[0];
         } while ((Auxiliar.atribStaticInt < a.atribInt) && (a.atribArrInt[0] <= Auxiliar.atribArrayStaticInt[4]));

         aux.AppendLine("(" + Auxiliar.atribStaticInt + " < " + a.atribInt + ") AND (" + a.atribArrInt[0] + " <= " + Auxiliar.atribArrayStaticInt[4] + ")");

         return aux.ToString();
      }

      static void Main(string[] args)
      {
         string aux = "";

         TestsStats2 ts2 = new TestsStats2();

         aux = "0.txt\r\n1.txt\r\n2.txt\r\n3.txt\r\n";
         if (!(ts2.ForStat().Equals(aux)))
            Environment.Exit(-1);

         aux = "(True AND (30 > 24)) OR False\r\n(True AND (30 > 26)) OR False\r\n(True AND (30 > 28)) OR False\r\n(True AND (30 > 30)) OR False\r\n";
         if (!(ts2.WhileStat().Equals(aux)))
            Environment.Exit(-1);

         aux = "(3 < 22) AND (12 <= 17)\r\n(3 < 22) AND (15 <= 17)\r\n(3 < 22) AND (18 <= 17)\r\n";
         if (!(ts2.DoStat().Equals(aux)))
            Environment.Exit(-1);
      }
   }
}

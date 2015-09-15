using System;
using System.IO;

namespace MoreOperations3
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
      public void MethodProperties()
      {
         // Not available in framework 1.1
         //int aux = Console.WindowHeight;
         //Console.Write(aux - 1);
         //Console.Write("\t");
         //Console.WriteLine(--Console.WindowHeight);
         //Console.WindowHeight = aux;
         
      }

      public void PublicFields()
      {
         Auxiliar a = new Auxiliar();
         int i;

         // Unary Expression
         // ----------------

         i = (++a.atribInt);
         if (i != 23)
            Environment.Exit(-1);

         i = (++Auxiliar.atribStaticInt);
         if (i != 4)
            Environment.Exit(-1);
      }

      public void Method(int[] arrInt)
      {
         int i;

         // Unary Expression
         // ----------------

         i = (++arrInt[0]);
         if (i != 8)
            Environment.Exit(-1);
      }

      static void Main(string[] args)
      {
         Operations op = new Operations();
         op.PublicFields();
         op.MethodProperties();
         op.Method(new int[] {7});
      }
   }
}

using System;
using System.IO;

namespace MoreOperations
{
   class Auxiliar
   {
      public int atribInt = 22;
      public static int atribStaticInt = 3;
   }

   class Operations
   {
      public void PublicFields()
      {
         Auxiliar a = new Auxiliar();

         if ((a.atribInt * Auxiliar.atribStaticInt) != 66)
            Environment.Exit(-1);

         if (((double)(a.atribInt * Auxiliar.atribStaticInt)) != 66)
            Environment.Exit(-1);

         if (((double)a.atribInt * Auxiliar.atribStaticInt) != 66)
            Environment.Exit(-1);
      }

      static void Main(string[] args)
      {
         Operations op = new Operations();
         op.PublicFields();
      }
   }
}

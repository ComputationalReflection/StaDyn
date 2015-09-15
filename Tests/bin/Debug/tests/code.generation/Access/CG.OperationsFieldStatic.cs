using System;

namespace OperationsThis
{
   class Operations
   {
      private static int a = 0;
      private static int b = 3;
      private static bool c;

      static void Main(string[] args)
      {
         a = 4;
         Console.WriteLine(a);    //4
         if (a != 4)
            Environment.Exit(-1);

         a = a + b;
         Console.WriteLine(a);    //7
         if (a != 7)
            Environment.Exit(-1);

         c = a < b;
         Console.WriteLine(c);    //false
         if (c != false)
            Environment.Exit(-1);

         c = a != b;
         Console.WriteLine(c);    //true
         if (c != true)
            Environment.Exit(-1);

         c = (a > b) && (a != b); //true
         Console.WriteLine(c);
         if (c != true)
            Environment.Exit(-1);
      }
   }
}
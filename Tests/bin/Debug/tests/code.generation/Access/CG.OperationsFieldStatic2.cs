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
         Console.WriteLine(a = 4);    //4
         if (a != 4)
            Environment.Exit(-1);

         Console.WriteLine(a = a + b);    //7
         if (a != 7)
            Environment.Exit(-1);

         Console.WriteLine(c = a < b);    //false
         if (c != false)
            Environment.Exit(-1);

         Console.WriteLine(c = a != b);    //true
         if (c != true)
            Environment.Exit(-1);

         Console.WriteLine(c = (a > b) && (a != b)); //true
         if (c != true)
            Environment.Exit(-1);
      }
   }
}
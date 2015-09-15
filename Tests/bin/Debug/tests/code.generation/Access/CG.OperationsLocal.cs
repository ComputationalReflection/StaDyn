using System;

namespace Operations
{
   class Operations
   {
      static void Main(string[] args)
      {
         int a = 0;
         int b = 3;
         bool c;

         a = 4;
         Console.WriteLine(a); //4
         if (a != 4)
            Environment.Exit(-1);

         a = a + b;
         Console.WriteLine(a); //7
         if (a != 7)
            Environment.Exit(-1);

         c = a < b;
         Console.WriteLine(c); //false
         if (c != false)
            Environment.Exit(-1);

         c = a != b;
         Console.WriteLine(c); //true
         if (c != true)
            Environment.Exit(-1);

         c = (a > b) && (a != b); //true
         Console.WriteLine(c);
         if (c != true)
            Environment.Exit(-1);
      }
   }
}

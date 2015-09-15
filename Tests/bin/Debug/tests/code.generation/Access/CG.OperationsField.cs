using System;

namespace OperationsThis
{
   class Operations
   {
      private int a = 0;
      private int b = 3;
      private bool c;

      public void Method()
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

      static void Main(string[] args)
      {
         Operations op = new Operations();
         op.Method();
      }
   }
}
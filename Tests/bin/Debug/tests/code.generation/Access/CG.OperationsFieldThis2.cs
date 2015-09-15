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
         Console.WriteLine(this.a = 4);    //4
         if (this.a != 4)
            Environment.Exit(-1);

         Console.WriteLine(this.a = this.a + this.b);    //7
         if (this.a != 7)
            Environment.Exit(-1);

         Console.WriteLine(this.c = this.a < this.b);    //false
         if (this.c != false)
            Environment.Exit(-1);

         Console.WriteLine(this.c = this.a != this.b);    //true
         if (this.c != true)
            Environment.Exit(-1);

         Console.WriteLine(this.c = (this.a > this.b) && (this.a != this.b)); //true
         if (this.c != true)
            Environment.Exit(-1);
      }

      static void Main(string[] args)
      {
         Operations op = new Operations();
         op.Method();
      }
   }
}
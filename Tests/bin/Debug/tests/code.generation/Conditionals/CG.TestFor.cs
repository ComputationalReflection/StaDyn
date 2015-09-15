using System;
using System.Collections.Generic;
using System.Text;

namespace TestFor
{
   class TestFor
   {
      private int atrib1;
      private int atrib2;

      public string MethodField()
      {
         StringBuilder aux = new StringBuilder();

         for (atrib1 = atrib2, atrib2 = atrib1; (atrib1 = atrib2) < 70; atrib1 = atrib1 + 1, atrib2 = atrib1 + 2)
         {
            int temp;
            temp = atrib1;
            atrib1 = atrib2;
            atrib2 = temp;
         }

         aux.Append(atrib1);
         aux.Append(atrib2);

         return aux.ToString();
      }

      public string MethodFieldThis()
      {
         StringBuilder aux = new StringBuilder();

         for (this.atrib1 = this.atrib2, this.atrib2 = this.atrib1; (atrib1 = this.atrib2) <= 50; this.atrib1 = this.atrib1 + 1, this.atrib2 = this.atrib1 + 2)
         {
            int temp;
            temp = this.atrib1;
            this.atrib1 = this.atrib2;
            this.atrib2 = temp;
         }

         aux.Append(this.atrib1);
         aux.Append(this.atrib2);

         return aux.ToString();
      }

      public string MethodParam(int a, int b)
      {
         StringBuilder aux = new StringBuilder();

         for (a = b, b = a; (a = b) < 50; a = a + 1, b = a + 2)
         {
            int temp;
            temp = a;
            a = b;
            b = temp;
         }

         aux.Append(a);
         aux.Append(b);

         return aux.ToString();
      }

      public string MethodLocals()
      {
         StringBuilder aux = new StringBuilder();

         int a;
         int b = -2;
         int temp = 0;

         for (a = b, b = a; (a = b) != 0; a = a + 1, b = a + 2)
         {
            temp = a;
            a = b;
            b = temp;
         }

         aux.Append(a);
         aux.Append(b);
         aux.Append(temp);

         return aux.ToString();
      }

      public string MethodLocals2()
      {
         StringBuilder aux = new StringBuilder();
         int a2 = 0;
         int b2 = 0;

         for (int a = 0, b = a; (a = b) != 0; a = a + 1, b = a + 2)
         {
            int temp;
            temp = a;
            a = b;
            b = temp;
            a2 = a;
            b2 = b;
         }

         aux.Append(a2);
         aux.Append(b2);
         return aux.ToString();
      }

      static void Main(string[] args)
      {
         TestFor f = new TestFor();

         if (!(f.MethodLocals().Equals("00-3")))
            Environment.Exit(-1);

         if (!(f.MethodLocals2().Equals("00")))
            Environment.Exit(-1);

         if (!(f.MethodParam(18, 10).Equals("5252")))
            Environment.Exit(-1);

         if (!(f.MethodField().Equals("7272")))
            Environment.Exit(-1);

         if (!(f.MethodFieldThis().Equals("7272")))
            Environment.Exit(-1);

         f.atrib1 = 18;
         f.atrib2 = 28;

         if (!(f.MethodFieldThis().Equals("5252")))
            Environment.Exit(-1);

         if (!(f.MethodField().Equals("7070")))
            Environment.Exit(-1);
      }
   }
}

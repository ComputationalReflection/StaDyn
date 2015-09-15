using System;
using System.Collections.Generic;
using System.Text;

namespace TestWhile
{
   class While
   {
      private int atrib1 = 19;
      private int atrib2 = 08;

      public string MethodField()
      {
         StringBuilder aux = new StringBuilder();

         while ((atrib1 = atrib2 + atrib1) <= 100)
         {
            atrib2 = atrib2 + atrib1;
            atrib2 = atrib2 + 1;
            atrib1 = atrib1 + 1;
         }

         aux.Append(atrib1);
         aux.Append(atrib2);

         return aux.ToString();
      }

      public string MethodFieldThis()
      {
         StringBuilder aux = new StringBuilder();

         while ((this.atrib1 = this.atrib2 + this.atrib1) <= 100)
         {
            this.atrib2 = this.atrib2 + this.atrib1;
            this.atrib2 = this.atrib2 + 1;
            this.atrib1 = this.atrib1 + 1;
         }

         aux.Append(this.atrib1);
         aux.Append(this.atrib2);

         return aux.ToString();
      }

      public string MethodParam(int a, int b)
      {
         StringBuilder aux = new StringBuilder();

         while ((a = b - a) != 0)
         {
            b = b + a;
            b = b + 1;
            a = a + 1;
         }

         aux.Append(a);
         aux.Append(b);

         return aux.ToString();
      }

      public string MethodLocals()
      {
         StringBuilder aux = new StringBuilder();

         int a = 15;
         int b = 30;

         while ((a = b + a) <= 100)
         {
            b = b + a;
            b = b + 1;
            a = a + 1;
         }

         aux.Append(a);
         aux.Append(b);

         return aux.ToString();
      }


      static void Main(string[] args)
      {
         While w = new While();
         w.MethodLocals();

         if (!(w.MethodParam(5, 5).Equals("05")))
            Environment.Exit(-1);

         if (!(w.MethodParam(5, 0).Equals("0-4")))
            Environment.Exit(-1);

         if (!(w.MethodField().Equals("166101")))
            Environment.Exit(-1);

         if (!(w.MethodFieldThis().Equals("267101")))
            Environment.Exit(-1);

         w.atrib1 = 46;
         w.atrib2 = 36;

         if (!(w.MethodFieldThis().Equals("202119")))
            Environment.Exit(-1);

         if (!(w.MethodField().Equals("321119")))
            Environment.Exit(-1);
      }
   }
}

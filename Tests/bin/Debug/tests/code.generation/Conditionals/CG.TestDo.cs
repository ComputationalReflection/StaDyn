using System;
using System.Collections.Generic;
using System.Text;

namespace TestDo
{
   class TestDo
   {
      public string MethodLocals()
      {
         StringBuilder aux = new StringBuilder();

         int a = 4;
         int b = -5;

         do
         {
            a = a + 1;
            b = b + a;
         } while ((a = b + a) != 12);

         aux.Append(a);
         aux.Append(b);

         return aux.ToString();
      }

      public string MethodParam(int a, int b)
      {
         StringBuilder aux = new StringBuilder();

         do
         {
            a = a + 1;
            b = b + a;
         } while ((a = b + a) <= 20);

         aux.Append(a);
         aux.Append(b);

         return aux.ToString();
      }

      private int atrib1;
      private int atrib2;

      public string MethodFieldThis()
      {
         StringBuilder aux = new StringBuilder();

         do
         {
            this.atrib1 = this.atrib1 + 1;
            this.atrib2 = this.atrib2 + this.atrib1;
         } while ((this.atrib1 = this.atrib2 + this.atrib1) <= 10);

         aux.Append(this.atrib1);
         aux.Append(this.atrib2);

         return aux.ToString();
      }

      public string MethodField()
      {
         StringBuilder aux = new StringBuilder();

         do
         {
            atrib1 = atrib1 + 1;
            atrib2 = atrib2 + atrib1;
         } while ((atrib1 = atrib2 + atrib1) <= 10);

         aux.Append(atrib1);
         aux.Append(atrib2);

         return aux.ToString();
      }

      static void Main(string[] args)
      {
         TestDo d = new TestDo();
         if (!(d.MethodLocals().Equals("126")))
            Environment.Exit(-1);

         if (!(d.MethodParam(4, -5).Equals("3219")))
            Environment.Exit(-1);

         if (!(d.MethodParam(1, 1).Equals("4125")))
            Environment.Exit(-1);

         if (!(d.MethodField().Equals("2012")))
            Environment.Exit(-1);

         d.atrib1 = 10;
         d.atrib2 = -8;
         if (!(d.MethodFieldThis().Equals("143")))
            Environment.Exit(-1);
      }
   }
}

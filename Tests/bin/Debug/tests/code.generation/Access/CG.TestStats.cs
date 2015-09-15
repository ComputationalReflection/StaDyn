using System;
using System.Text;

namespace TestStats
{
   class TestStats
   {
      private int atrib1 = 8;
      private int atrib2 = 4;

      public void MethodIfWhile()
      {
         if ((atrib1 = this.atrib2) != 0)
         {
            atrib1 = atrib1 + this.atrib2;
            atrib2 = this.atrib1;
            this.atrib1 = 3;

            while ((atrib1 = this.atrib2 + atrib1) < 50)
            {
               this.atrib2 = atrib2 + this.atrib1;
               atrib2 = this.atrib2 + 1;
               this.atrib1 = atrib1 + 1;
            }
         }

         if (atrib1 != 86)
            Environment.Exit(-1);

         if (atrib2 != 53)
            Environment.Exit(-1);
      }

      public string MethodIfWhileElseDo(int a, int b)
      {
         StringBuilder aux = new StringBuilder();

         if ((a = b) != 0)
         {
            a = a + b;
            b = a;
            a = 3;

            while ((a = b + a) <= 40)
            {
               b = b + a;
               b = b + 1;
               a = a + 1;
            }

         }
         else
         {
            a = b + a;

            int c = 3;

            do
            {
               c = c + 1;
               b = b + c;
            } while ((c = b + c) <30);

            b = 0;
         }

         aux.Append(a);
         aux.Append(b);

         return aux.ToString();
      }

      public void MethodWhileDoIf()
      {
         int a = 6;
         int b = 2;

         while ((a = b + a) < 50)
         {
            a = a + b;
            do
            {
               if ((a = b) != 0)
               {
                  b = b + a;
                  b = b + 1;
                  a = a + 1;
               }
            } while ((b = b + a) < 20);
            b = a;
            a = 3;
         }

         if (a != 50)
            Environment.Exit(-1);
         if (b != 47)
            Environment.Exit(-1);
      }
      
      static void Main(string[] args)
      {
         int a = 6;
         int b = a;

         TestStats ts = new TestStats();
         ts.MethodIfWhile();

         if (!(ts.MethodIfWhileElseDo(a, b).Equals("4428")))
            Environment.Exit(-1);
         if ((a != 6) || (b != 6))
            Environment.Exit(-1);

         a = 2;
         b = 5;
         if (!(ts.MethodIfWhileElseDo(a, b).Equals("10263")))
            Environment.Exit(-1);
         if ((a != 2) || (b != 5))
            Environment.Exit(-1);

         ts.MethodWhileDoIf();
      }
   }
}

using System;
using System.Text;

namespace TestIf1
{
   class TestIf1
   {
      public string If()
      {
         StringBuilder aux = new StringBuilder();

         int a = 12;
         int b = a * 2;

         if ((a = b) != 0)
         {
            a = a + b;
            b = a;
            a = 3;
         }

         aux.Append(a);
         aux.Append(b);

         return aux.ToString(); 
      }

      public string IfElse()
      {
         StringBuilder aux = new StringBuilder();

         int a = 12;
         int b = a * 2;

         if ((a = b) != 0)
         {
            a = a + b;
            b = a;
            a = 3;
         }
         else
         {
            a = b + a;
            b = 0;
         }

         aux.Append(a);
         aux.Append(b);

         return aux.ToString();
      }

      private int atrib;

      public int IfElseThis()
      {
         if (this.atrib != 0)
         {
            this.atrib = this.atrib + 1;
            return 0;
         }
         else
         {
            this.atrib = 3;
            this.atrib = this.atrib - 1;
            return 1;
         }
      }

      public string IfElseField()
      {
         StringBuilder aux = new StringBuilder();

         if ((atrib = bField) != 0)
         {
            atrib = atrib + bField;
            bField = atrib;
            atrib = 3;
         }
         else
         {
            atrib = bField + atrib;
            bField = 6;
         }

         aux.Append(atrib);
         aux.Append(bField);

         return aux.ToString();
      }

      private int bField;

      public string IfElseParam(int a, int b)
      {
         StringBuilder aux = new StringBuilder();

         if ((a = b) == 0)
         {
            a = 3;
         }
         else
         {
            a = 7;
         }

         aux.Append(a);
         aux.Append(b);

         return aux.ToString();
      }

      public string IfParam(bool a, int b)
      {
         StringBuilder aux = new StringBuilder();

         if (a)
         {
            b = 3;
         }

         aux.Append(a);
         aux.Append(b);

         return aux.ToString();
      }

      static void Main(string[] args)
      {
         TestIf1 testif = new TestIf1();

         if (!(testif.If().Equals("348")))
            Environment.Exit(-1);

         if (!(testif.IfElse().Equals("348")))
            Environment.Exit(-1);

         if (testif.IfElseThis() != 1)
            Environment.Exit(-1);

         testif.atrib = 8;
         if (testif.IfElseThis() != 0)
            Environment.Exit(-1);

         if (!(testif.IfElseField().Equals("06")))
            Environment.Exit(-1);

         if (!(testif.IfParam(true, 34).Equals("True3")))
            Environment.Exit(-1);

         if (!(testif.IfParam(false, 34).Equals("False34")))
            Environment.Exit(-1);

         if (!(testif.IfElseParam(42, 86).Equals("786")))
            Environment.Exit(-1);

         if (!(testif.IfElseParam(42, 0).Equals("30")))
            Environment.Exit(-1);
      }
   }
}

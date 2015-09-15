using System;
using System.Text;

namespace UnaryStats
{
   class UnaryStats
   {
      private int SwitchStat(int x)
      {
         int a;

         switch (a = x)
         {
            case 1:
               a = a + 1;
               break;

            default:
               a = 48;
               a = a + 1;
               break;
         }

         return a;
      }

      private int DoStat(int x)
      {
         int a = x;

         do
         {
            a = 32;
            a = a + 1;
         } while (a < 10);

         return a;
      }

      private int WhileStat(int x)
      {
         int a = x;

         while (a < 10)
         {
            a = a + 1;
         }

         return a;
      }

      private int ForStat(int x)
      {
         //throw new Exception("The method or operation is not implemented.");
         int i;

         for (i = x; i < 10; i++)
            ;

         return i;
      }

      private string IfStat(int x)
      {
         StringBuilder aux = new StringBuilder();

         int a;

         if ((a = x) == 0)
         {
            a = a + 1;
         }
         else
         {
            a = a + 1;
         }

         aux.Append(a);

         if (a == 1)
            a = a + 1;

         aux.Append(a);

         return aux.ToString();
      }

      static void Main(string[] args)
      {
         UnaryStats us = new UnaryStats();

         if (!(us.IfStat(0).Equals("12")))
            Environment.Exit(-1);

         if (!(us.IfStat(4).Equals("55")))
            Environment.Exit(-1);

         if (us.ForStat(7) != 10)
            Environment.Exit(-1);

         if (us.WhileStat(5) != 10)
            Environment.Exit(-1);

         if (us.DoStat(11) != 33)
            Environment.Exit(-1);

         if (us.SwitchStat(1) != 2)
            Environment.Exit(-1);

         if (us.SwitchStat(24) != 49)
            Environment.Exit(-1);
      }
   }
}

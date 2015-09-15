using System;

namespace UnaryStats
{
   class UnaryStats
   {
      private string SwitchStat(int x)
      {
         int a;

         switch (a = x)
         {
            case 1:
               a++;
               break;

            default:
               a = 48;
               ++a;
               break;
         }
         return a.ToString();
      }

      private string DoStat(int x)
      {
         int a = x;

         do
         {
            a = 32;
            a++;
         } while (a < 10);

         return a.ToString();
      }

      private string WhileStat(int x)
      {
         int a = x;

         while (a < 10)
         {
            ++a;
         }

         return a.ToString();
      }

      private string ForStat(int x)
      {
         int i;

         for (i = x; i < 10; i++)
            ;
         return i.ToString();
      }

      private string IfStat(int x)
      {
         int a;
         string s = "";

         if ((a = x) == 0)
         {
            a++;
         }
         else
         {
            ++a;
         }

         s = s + a.ToString();
         s = s + " ";

         if (a == 1)
            ++a;

         s = s + a.ToString();
         return s;
      }

      static void Main(string[] args)
      {
         UnaryStats us = new UnaryStats();
         if (!(us.IfStat(0).Equals("1 2")))
            Environment.Exit(-1);

         if (!(us.IfStat(4).Equals("5 5")))
            Environment.Exit(-1);

         if (!(us.ForStat(7).Equals("10")))
            Environment.Exit(-1);

         if (!(us.WhileStat(5).Equals("10")))
            Environment.Exit(-1);

         if (!(us.DoStat(11).Equals("33")))
            Environment.Exit(-1);

         if (!(us.SwitchStat(1).Equals("2")))
            Environment.Exit(-1);

         if (!(us.SwitchStat(24).Equals("49")))
            Environment.Exit(-1);
      }
   }
}

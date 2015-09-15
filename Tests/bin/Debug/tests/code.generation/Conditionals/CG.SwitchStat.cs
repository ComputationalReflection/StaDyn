using System;
using System.Collections.Generic;
using System.Text;

namespace SwitchStat
{
   class SwitchStat
   {
      public string MethodSwitch(int a, int b)
      {
         StringBuilder aux = new StringBuilder();

         switch (a = b)
         {
            case 1:
               //Console.WriteLine(1);
               aux.Append(1);
               break;
            case 2:
            case 3:
               //Console.WriteLine(23);
               aux.Append(23);
               break;
            default:
               //Console.WriteLine("default");
               aux.Append("default");
               break;
         }

         return aux.ToString();
      }

      static void Main(string[] args)
      {
         SwitchStat ss = new SwitchStat();
         if (!(ss.MethodSwitch(8, 1).Equals("1")))
            Environment.Exit(-1);
         if (!(ss.MethodSwitch(8, 2).Equals("23")))
            Environment.Exit(-1);
         if (!(ss.MethodSwitch(8, 3).Equals("23")))
            Environment.Exit(-1);
         if (!(ss.MethodSwitch(8, 4).Equals("default")))
            Environment.Exit(-1);
      }
   }
}

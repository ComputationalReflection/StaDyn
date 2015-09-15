using System.Collections;
using System;

class ParamLocal
{
   public static string Write(string s)
   {
      return s;
   }

   public static double Write(double d)
   {
      return d;
   }

   public static var varios(var param, bool cond1, bool cond2)
   {
      var local = param;

      if (cond1)
         local = param = 2.2;
      else
      {
         if (cond2)
            local = param = "1.5";
         else
            local = param = 5;
      }

      //ParamLocal.Write(param);
      return ParamLocal.Write(local);
   }

   static void Main()
   {
      Console.WriteLine(ParamLocal.varios(0, true, true));
      Console.WriteLine(ParamLocal.varios(0, true, false));
      Console.WriteLine(ParamLocal.varios(0, false, true));
      Console.WriteLine(ParamLocal.varios(0, false, false));

      //if (ParamLocal.varios(0, true, true) != 2.2)
      //   Environment.Exit(-1);   // 2,2

      //if (ParamLocal.varios(0, true, false) != 2.2)
      //   Environment.Exit(-1);  // 2,2

      //if (!(ParamLocal.varios(0, false, true).Equals("1.5")))
      //   Environment.Exit(-1);  // 1.5

      //if (ParamLocal.varios(0, false, false) != 5)
      //   Environment.Exit(-1); // 5
   }
}
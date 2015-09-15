using System.Collections;
using System;

class ParamLocal
{
   public static void Write(string s1, string s2)
   {
      Console.WriteLine("Local: {0}, Param: {0}", s1, s2);
   }

   public static void Write(double d1, double d2)
   {
      Console.WriteLine("Local: {0}, Param: {0}", d1, d2);
   }

   public static void varios(var param, bool cond1, bool cond2)
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
      ParamLocal.Write(local, param);
   }

   static void Main()
   {      
      ParamLocal.varios(0, true, true);   // 2,2
      ParamLocal.varios(0, true, false);  // 2,2
      ParamLocal.varios(0, false, true);  // 1.5
      ParamLocal.varios(0, false, false); // 5
   }
}
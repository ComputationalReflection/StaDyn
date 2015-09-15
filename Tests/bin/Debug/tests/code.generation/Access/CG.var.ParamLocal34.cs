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

      ParamLocal.Write(param);
      return ParamLocal.Write(local);
   }

   static void Main()
   {
      //Console.WriteLine(ParamLocal.varios(0, true, true));
      //Console.WriteLine(ParamLocal.varios(0, true, false));
      //Console.WriteLine(ParamLocal.varios(0, false, true));
      //Console.WriteLine(ParamLocal.varios(0, false, false));

      // We have to use dynamic auxiliar variable to compile.
      var v1 = ParamLocal.varios(0, true, true);
      if (v1 != 2.2)
         Environment.Exit(-1);

      var v2 = ParamLocal.varios(0, true, false);
      if (v2 != 2.2)
         Environment.Exit(-1);

      var v3 = ParamLocal.varios(0, false, true);
      if (!(v3.Equals("1.5")))
         Environment.Exit(-1);

      var v4 = ParamLocal.varios(0, false, false);
      if (v4 != 5.0) // With '5' the comparation is incorrect
         Environment.Exit(-1);
   }
}
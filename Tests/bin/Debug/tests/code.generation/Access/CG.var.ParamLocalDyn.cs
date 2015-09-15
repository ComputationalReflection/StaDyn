using System.Collections;
using System;

// File executed successfully if it has dynamic references

class ParamLocal
{
   public static var varios(var param, bool cond)
   {
      var local = param;

      if (cond)
         local = param = 2.2;
      else
         local = param = "1.5";

      return param;
   }

   static void Main()
   {
      if (ParamLocal.varios(5, true) != 2.2)
         Environment.Exit(-1);

      if (!(ParamLocal.varios(5, false).Equals("1.5")))
         Environment.Exit(-1);

      if (ParamLocal.varios(new ArrayList(), true) != 2.2)
         Environment.Exit(-1);

      if (!(ParamLocal.varios(new ArrayList(), false).Equals("1.5")))
         Environment.Exit(-1);
   }
}
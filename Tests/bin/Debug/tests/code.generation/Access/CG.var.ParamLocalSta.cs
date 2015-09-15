using System.Collections;
using System;

// File executed successfully if it hasn't dynamic references

class ParamLocal
{
   public static void varios(var param, bool condicion)
   {
      var local = param;
      if (condicion)
         local = param = 2.2;
      else
         local = param = "1.5";
      Console.WriteLine(local);
      Console.WriteLine(param);
   }

   static void Main()
   {
      ParamLocal.varios(5, true);
      ParamLocal.varios(5, false);
      ParamLocal.varios(new ArrayList(), true);
      ParamLocal.varios(new ArrayList(), false);
   }
}
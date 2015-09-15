using System.Collections;
using System;

class ParamLocal
{
   public static void varios(var param, bool condicion)
   {
      var local = param;
      if (condicion)
         local = param = 1;
      else
         local = param = 2.2;
      Console.WriteLine(local);
      Console.WriteLine(param);
   }

   static void Main()
   {
      ParamLocal.varios(5, true);  // 1
      ParamLocal.varios(5, false); // 2,2
   }
}
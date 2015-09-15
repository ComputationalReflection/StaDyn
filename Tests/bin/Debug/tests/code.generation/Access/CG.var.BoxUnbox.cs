using System;
using System.Collections;

namespace IfStat
{
   class IfStat
   {
      var attribute;

      void setAttribute(var p)
      {
         this.attribute = p;
      }

      var getAttribute()
      {
         return this.attribute;
      }

      public static void Main()
      {
         IfStat obj = new IfStat();
         obj.setAttribute(3);
         int n = obj.getAttribute();
         Console.WriteLine(""+ obj.getAttribute());
         if (n != 3)
            Environment.Exit(-1);

         obj.setAttribute(true);
         bool b = obj.getAttribute();
         Console.WriteLine(""+obj.getAttribute());
         if (b != true)
            Environment.Exit(-1);
      }
   }
}

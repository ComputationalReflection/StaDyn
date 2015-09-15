using System;

namespace IfStat
{
   class IfStat
   {
      public string If(bool a)
      {
         if (a)
            Console.WriteLine("TRUE");
         else
            Console.WriteLine("FALSE");

         return a.ToString();
      }

      static void Main(string[] args)
      {
         bool b = false;
         IfStat ifstat = new IfStat();

         if (!(ifstat.If(true).Equals("True")))
            Environment.Exit(-1);

         if (!(ifstat.If(false).Equals("False")))
            Environment.Exit(-1);

         if (!(ifstat.If(b).Equals("False")))
            Environment.Exit(-1);

         if (!(ifstat.If(!b).Equals("True")))
            Environment.Exit(-1);
      }
   }
}

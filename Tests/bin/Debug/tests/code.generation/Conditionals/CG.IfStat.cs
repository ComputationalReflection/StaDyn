using System;

namespace IfStat
{
   class IfStat
   {
      public string If(bool a)
      {
         string str = "";

         if (a)
            str = "true";
         else
            str = "false";

         return str;
      }

      public string If2(bool a)
      {
         string str;

         if (!a)
         {
            str = "true";
         }
         else
         {
            str = "false";
         }

         return str;
      }

      static void Main(string[] args)
      {
         bool b = false;
         IfStat ifstat = new IfStat();

         if (!(ifstat.If(true).Equals("true")))
            Environment.Exit(-1);

         if (!(ifstat.If(false).Equals("false")))
            Environment.Exit(-1);

         if (!(ifstat.If(b).Equals("false")))
            Environment.Exit(-1);

         if (!(ifstat.If(!b).Equals("true")))
            Environment.Exit(-1);

         if (!(ifstat.If2(b).Equals("true")))
            Environment.Exit(-1);

         if (!(ifstat.If2(!b).Equals("false")))
            Environment.Exit(-1);
      }
   }
}

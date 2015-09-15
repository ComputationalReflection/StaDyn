using System;
using System.IO;

namespace Returns
{
   class Returns
   {
      public int GetsInt()
      {
         return 45;
      }

      static void Main(string[] args)
      {
         StreamWriter sw = new StreamWriter("pp.txt");
         sw.ToString();
         Returns r = new Returns();
         if (r.GetsInt() != 45)
            Environment.Exit(-1);         
      }
   }
}

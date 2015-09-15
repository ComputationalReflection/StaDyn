using System.Collections;
using System;
using System.IO;

class ParamLocal
{
   public static void Write(TextWriter t1, TextWriter t2)
   {
      if (t1 is TextWriter)
         Console.WriteLine("[TextWriter] Local: {0}, Param: {0}", t1, t2);
      else
         Environment.Exit(-1);
   }

   public static void Write(ArrayList s1, ArrayList s2)
   {
      if (s1 is ArrayList)
         Console.WriteLine("[ArrayList] Local: {0}, Param: {0}", s1, s2);
      else
         Environment.Exit(-1);
   }

   public static void varios(var param, bool cond1, bool cond2)
   {
      var local = param;

      if (cond1)
      {
         local = param = new StreamWriter("pp.txt");
      }
      else
      {
         if (cond2)
            local = param = new ArrayList();
         else
            local = param = new StringWriter();
      }

      ParamLocal.Write(local, param);
   }

   static void Main()
   {
      ParamLocal.varios(0, false, true);  // ArrayList
      ParamLocal.varios(0, true, true);   // StreamWriter
      ParamLocal.varios(0, false, false); // StringWriter
   }
}
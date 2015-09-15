using System;
using System.Text;

namespace TestStat3
{
   class TestStats3
   {
      public string MethodDoIf()
      {
         StringBuilder aux = new StringBuilder();

         int a = 30;
         int b = 19;

         a = a;
         a = a + b;
         do
         {
            if ((a = b) != 0)
            {
               b = b + a;
               b = b + 1;
               a = a + 1;
            }
         } while ((b = b + a) < 20);
         b = a;
         a = 27;

         aux.Append(a);
         aux.Append(b);

         return aux.ToString();
      }

      static void Main(string[] args)
      {
         TestStats3 ts = new TestStats3();
         if (!(ts.MethodDoIf().Equals("2720")))
            Environment.Exit(-1);
      }
   }
}

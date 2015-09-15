using System;
using System.Text;

namespace TestStat2
{
   class TestStats2
   {
      public string MethodWhileDoIf()
      {
         StringBuilder aux = new StringBuilder();

         int a = 30;
         int b = 19;

         int i = 0;
         int j = 0;
         
         while ((a = b + a) < 50)
         {
            i++;
            a = a + b;
            do
            {
               j++;
               if ((a = b) != 0)
               {
                  b = b + a;
                  b = b + 1;
                  a = a + 1;
               }
            } while ((b = b + a) < 20);
            b = a;
            a = 27;
         }

         aux.Append(i);
         aux.Append(j);
         aux.Append(a);
         aux.Append(b);

         return aux.ToString();
      }
      
      static void Main(string[] args)
      {
         TestStats2 ts = new TestStats2();
         if (!(ts.MethodWhileDoIf().Equals("445023")))
            Environment.Exit(-1);
      }
   }
}

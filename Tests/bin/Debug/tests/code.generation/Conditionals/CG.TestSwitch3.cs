using System;
using System.Collections.Generic;
using System.Text;

namespace TestSwitch
{
   class TestSwitch
   {
      public string Method1Locals(int x, int y)
      {
         StringBuilder aux = new StringBuilder();

         int a = x;
         int b = y;

         switch (a = b)
         {
            case 3:
            default:
               switch (a)
               {
                  case 3:
                  default:
                     a = a + 83;
                     b = b + 78;
                     break;

                  case 1:
                     a = b + 1;
                     b = a;
                     a = a + 1;
                     break;

                  case 2:
                     b = b + a;
                     break;
               }
               break;

            case 1:
               switch (a)
               {
                  case 3:
                  default:
                     a = a + 83;
                     b = b + 78;
                     break;

                  case 1:
                     a = b + 1;
                     b = a;
                     a = a + 1;
                     break;

                  case 2:
                     b = b + a;
                     break;
               }
               break;

            case 2:
               switch (a)
               {
                  case 3:
                  default:
                     a = a + 83;
                     b = b + 78;
                     break;

                  case 1:
                     a = b + 1;
                     b = a;
                     a = a + 1;
                     break;

                  case 2:
                     b = b + a;
                     break;
               }
               break;
         }

         aux.Append(a);
         aux.Append(b);

         return aux.ToString();
      }

      public string Method1Param(int a, int b)
      {
         StringBuilder aux = new StringBuilder();

         switch (a = b)
         {
            case 3:
            default:
               switch (a)
               {
                  case 3:
                  default:
                     a = a + 83;
                     b = b + 78;
                     break;

                  case 1:
                     a = b + 1;
                     b = a;
                     a = a + 1;
                     break;

                  case 2:
                     b = b + a;
                     break;
               }
               break;

            case 1:
               switch (a)
               {
                  case 3:
                  default:
                     a = a + 83;
                     b = b + 78;
                     break;

                  case 1:
                     a = b + 1;
                     b = a;
                     a = a + 1;
                     break;

                  case 2:
                     b = b + a;
                     break;
               }
               break;

            case 2:
               switch (a)
               {
                  case 3:
                  default:
                     a = a + 83;
                     b = b + 78;
                     break;

                  case 1:
                     a = b + 1;
                     b = a;
                     a = a + 1;
                     break;

                  case 2:
                     b = b + a;
                     break;
               }
               break;
         }

         aux.Append(a);
         aux.Append(b);

         return aux.ToString();
      }

      public void Method1()
      {
         if (!(Method1Locals(9, 1).Equals("32")))
            Environment.Exit(-1);
         if (!(Method1Locals(9, 2).Equals("24")))
            Environment.Exit(-1);
         if (!(Method1Locals(9, 3).Equals("8681")))
            Environment.Exit(-1);
         if (!(Method1Locals(9, 4).Equals("8782")))
            Environment.Exit(-1);
         if (!(Method1Param(8, 1).Equals("32")))
            Environment.Exit(-1);
         if (!(Method1Param(8, 2).Equals("24")))
            Environment.Exit(-1);
         if (!(Method1Param(8, 3).Equals("8681")))
            Environment.Exit(-1);
         if (!(Method1Param(8, 4).Equals("8782")))
            Environment.Exit(-1);
      }

      static void Main(string[] args)
      {
         TestSwitch ts = new TestSwitch();
         ts.Method1();
      }
   }
}

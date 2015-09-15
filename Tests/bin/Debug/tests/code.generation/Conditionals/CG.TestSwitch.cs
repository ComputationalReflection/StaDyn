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
            case 1:
               a = b + 1;
               b = a;
               a = a + 1;
               break;

            case 2:
               b = b + a;
               break;

            case 3:
            default:
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
            case 1:
               a = b + 1;
               b = a;
               a = a + 1;
               break;

            case 2:
               b = b + a;
               break;

            case 3:
            default:
               break;
         }
         aux.Append(a);
         aux.Append(b);

         return aux.ToString();
      }

      public string Method2Param(int a, int b)
      {
         StringBuilder aux = new StringBuilder();

         switch (a = b)             // switch (a1 = b0) 
         {                          // {                    a5 <-- a1           b3 <-- b0
            case 1:                 //    case 1:
               a = b + 1;           //       a2 = b0 + 1;
               b = a;               //       b1 = a2;       b3 <-- b1
               a = a + 1;           //       a3 = a2 + 1;   a5 <-- a3
               break;               //       break;

            case 2:                 //    case 2:
               b = b + a;           //       b2 = b0 + a1;  b3 <-- b2
               break;               //       break;

            case 3:                 //    case 3:
            default:                //    default:
               a = a + b;           //       a4 = a1 + b0;  a5 <-- a4
               break;               //       break;
         }                          // }                    a5 = 0(a1, a3, a4)  b3 = 0(b0, b1, b2)
         aux.Append(a);
         aux.Append(b);

         return aux.ToString();
      }

      public string Method2Locals(int x, int y)
      {
         StringBuilder aux = new StringBuilder();

         int a = x;
         int b = y;

         switch (a = b)
         {
            case 1:
               a = b + 1;
               b = a;
               a = a + 1;
               break;

            case 2:
               b = b + a;
               break;

            case 3:
            default:
               a = a + b;
               break;
         }
         aux.Append(a);
         aux.Append(b);

         return aux.ToString();
      }

      public string Method3Param(int a, int b)
      {
         StringBuilder aux = new StringBuilder();

         switch (a = b)             // switch (a1 = b0) 
         {                          // {                    a4 <-- a1
            case 1:                 //    case 1:
               a = b + 1;           //       a2 = b0 + 1;
               b = a;               //       b1 = a2;       b4 <-- b1
               a = a + 1;           //       a3 = a2 + 1;   a4 <-- a3
               break;               //       break;

            case 2:                 //    case 2:
               b = b + a;           //       b2 = b0 + a1;  b4 <-- b2
               break;               //       break;

            case 3:                 //    case 3:
            default:                //    default:
               b = a + b;           //       b3 = a1 + b0;  b4 <-- b3
               break;               //       break;
         }                          // }                    a4 = 0(a1, a3)      b4 = 0(b1, b2, b3)
         aux.Append(a);
         aux.Append(b);

         return aux.ToString();
      }

      public string Method3Locals(int x, int y)
      {
         StringBuilder aux = new StringBuilder();

         int a = x;
         int b = y;

         switch (a = b)
         {
            case 1:
               a = b + 1;
               b = a;
               a = a + 1;
               break;

            case 2:
               b = b + a;
               break;

            case 3:
            default:
               b = a + b;
               break;
         }
         aux.Append(a);
         aux.Append(b);

         return aux.ToString();
      }

      public string Method4Param(int a, int b)
      {
         StringBuilder aux = new StringBuilder();

         switch (a = b)             // switch (a1 = b0) 
         {                          // {                    a5 <-- a1
            case 1:                 //    case 1:
               a = b + 1;           //       a2 = b0 + 1;
               b = a;               //       b1 = a2;       b4 <-- b1
               a = a + 1;           //       a3 = a2 + 1;   a5 <-- a3
               break;               //       break;

            case 2:                 //    case 2:
               b = b + a;           //       b2 = b0 + a1;  b4 <-- b2
               break;               //       break;

            case 3:                 //    case 3:
            default:                //    default:
               b = a + b;           //       b3 = a1 + b0;  b4 <-- b3
               a = b;               //       a4 = b3;       a5 <-- a4
               break;               //       break;
         }                          // }                    a5 = 0(a1, a3, a4)      b4 = 0(b1, b2, b3)
         aux.Append(a);
         aux.Append(b);

         return aux.ToString();
      }

      public string Method4Locals(int x, int y)
      {
         StringBuilder aux = new StringBuilder();

         int a = x;
         int b = y;

         switch (a = b)
         {
            case 1:
               a = b + 1;
               b = a;
               a = a + 1;
               break;

            case 2:
               b = b + a;
               break;

            case 3:
            default:
               b = a + b;
               a = b;
               break;
         }
         aux.Append(a);
         aux.Append(b);

         return aux.ToString();
      }

      public string Method5Param(int a, int b)
      {
         StringBuilder aux = new StringBuilder();

         switch (a = b)             // switch (a1 = b0) 
         {                          // {                    a4 <-- a1           b3 <-- b0
            case 1:                 //    case 1:
               a = b + 1;           //       a2 = b0 + 1;   a4 <-- a2
               break;               //       break;

            case 2:                 //    case 2:
               b = b + a;           //       b1 = b0 + a1;  b3 <-- b1
               break;               //       break;

            case 3:                 //    case 3:
            default:                //    default:
               b = a + b;           //       b2 = a1 + b0;  b3 <-- b2
               a = b;               //       a3 = b2;       a4 <-- a3
               break;               //       break;
         }                          // }                    a4 = 0(a1, a2, a3)  b3 = 0(b0, b1, b2)
         aux.Append(a);
         aux.Append(b);

         return aux.ToString();
      }

      public string Method5Locals(int x, int y)
      {
         StringBuilder aux = new StringBuilder();

         int a = x;
         int b = y;

         switch (a = b)
         {
            case 1:
               a = b + 1;
               break;

            case 2:
               b = b + a;
               break;

            case 3:
            default:
               b = a + b;
               a = b;
               break;
         }
         aux.Append(a);
         aux.Append(b);

         return aux.ToString();
      }

      public string Method6Param(int a, int b)
      {
         StringBuilder aux = new StringBuilder();

         switch (a = b)
         {
            case 1:
               a = a + 1;
               break;

            case 2:
               break;

            case 3:
            default:
               b = a + b;
               a = b;
               break;
         }
         aux.Append(a);
         aux.Append(b);

         return aux.ToString();
      }

      public string Method6Locals(int x, int y)
      {
         StringBuilder aux = new StringBuilder();

         int a = x;
         int b = y;

         switch (a = b)
         {
            case 1:
               a = a + 1;
               break;

            case 2:
               break;

            case 3:
            default:
               b = a + b;
               a = b;
               break;
         }
         aux.Append(a);
         aux.Append(b);

         return aux.ToString();
      }

      public string Method7Param(int a, int b)
      {
         StringBuilder aux = new StringBuilder();

         switch (a = b)             // switch (a1 = b0) 
         {                          // {                    a4 <-- a1          b3 <-- b0
            case 1:                 //    case 1:
               a = a + 1;           //       a2 = a1 + 1;   a4 <-- a2
               b = b + a;           //       b1 = b0 + a2;  b3 <-- b1
               break;               //       break;

            case 2:                 //    case 2:
               a = b + a;           //       a3 = b0 + a1;  a4 <-- a3
               b = a;               //       b2 = a3;       b3 <-- b2
               break;               //       break;
         }                          // }                    a4 = 0(a1, a2, a3)     b3 = 0(b0, b1, b2)
         aux.Append(a);
         aux.Append(b);

         return aux.ToString();
      }

      public string Method7Locals(int x, int y)
      {
         StringBuilder aux = new StringBuilder();

         int a = x;
         int b = y;

         switch (a = b)
         {
            case 1:
               a = a + 1;
               b = b + a;
               break;

            case 2:
               a = b + a;
               b = a;
               break;
         }
         aux.Append(a);
         aux.Append(b);

         return aux.ToString();
      }

      public string Method8Param(int a, int b)
      {
         StringBuilder aux = new StringBuilder();

         switch (a = b)             // switch (a1 = b0) 
         {                          // {
            case 1:                 //    case 1:
               a = a + 1;           //       a2 = a1 + 1;   a5 <-- a2
               b = b + a;           //       b1 = b0 + a2;  b4 <-- b1
               break;               //       break;

            case 2:                 //    case 2:
               a = b + a;           //       a3 = b0 + a1;  a5 <-- a3
               b = a;               //       b2 = a3;       b4 <-- b2
               break;               //       break;

            case 3:                 //    case 3:
            default:                //    default:
               b = a + b;           //       b3 = a1 + b0;  b4 <-- b3
               a = b;               //       a4 = b3;       a5 <-- a4
               break;               //       break;
         }                          // }                    a5 = 0(a2, a3, a4)     b4 = 0(b1, b2, b3)
         aux.Append(a);
         aux.Append(b);

         return aux.ToString();
      }

      public string Method8Locals(int x, int y)
      {
         StringBuilder aux = new StringBuilder();

         int a = x;
         int b = y;

         switch (a = b)
         {
            case 1:
               a = a + 1;
               b = b + a;
               break;

            case 2:
               a = b + a;
               b = a;
               break;

            case 3:
            default:
               b = a + b;
               a = b;
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
         if (!(Method1Locals(9, 3).Equals("33")))
            Environment.Exit(-1);
         if (!(Method1Locals(9, 4).Equals("44")))
            Environment.Exit(-1);
         if (!(Method1Param(8, 1).Equals("32")))
            Environment.Exit(-1);
         if (!(Method1Param(8, 2).Equals("24")))
            Environment.Exit(-1);
         if (!(Method1Param(8, 3).Equals("33")))
            Environment.Exit(-1);
         if (!(Method1Param(8, 4).Equals("44")))
            Environment.Exit(-1);
      }

      public void Method2()
      {
         if (!(Method2Locals(12, 1).Equals("32")))
            Environment.Exit(-1);
         if (!(Method2Locals(12, 2).Equals("24")))
            Environment.Exit(-1);
         if (!(Method2Locals(12, 3).Equals("63")))
            Environment.Exit(-1);
         if (!(Method2Locals(12, 4).Equals("84")))
            Environment.Exit(-1);
         if (!(Method2Param(13, 1).Equals("32")))
            Environment.Exit(-1);
         if (!(Method2Param(13, 2).Equals("24")))
            Environment.Exit(-1);
         if (!(Method2Param(13, 3).Equals("63")))
            Environment.Exit(-1);
         if (!(Method2Param(13, 4).Equals("84")))
            Environment.Exit(-1);
      }

      public void Method3()
      {
         if (!(Method3Locals(65, 1).Equals("32")))
            Environment.Exit(-1);
         if (!(Method3Locals(65, 2).Equals("24")))
            Environment.Exit(-1);
         if (!(Method3Locals(65, 3).Equals("36")))
            Environment.Exit(-1);
         if (!(Method3Locals(65, 4).Equals("48")))
            Environment.Exit(-1);
         if (!(Method3Param(22, 1).Equals("32")))
            Environment.Exit(-1);
         if (!(Method3Param(22, 2).Equals("24")))
            Environment.Exit(-1);
         if (!(Method3Param(22, 3).Equals("36")))
            Environment.Exit(-1);
         if (!(Method3Param(22, 4).Equals("48")))
            Environment.Exit(-1);
      }

      public void Method4()
      {
         if (!(Method4Locals(65, 1).Equals("32")))
            Environment.Exit(-1);
         if (!(Method4Locals(65, 2).Equals("24")))
            Environment.Exit(-1);
         if (!(Method4Locals(65, 3).Equals("66")))
            Environment.Exit(-1);
         if (!(Method4Locals(65, 4).Equals("88")))
            Environment.Exit(-1);
         if (!(Method4Param(22, 1).Equals("32")))
            Environment.Exit(-1);
         if (!(Method4Param(22, 2).Equals("24")))
            Environment.Exit(-1);
         if (!(Method4Param(22, 3).Equals("66")))
            Environment.Exit(-1);
         if (!(Method4Param(22, 4).Equals("88")))
            Environment.Exit(-1);
      }

      public void Method5()
      {
         if (!(Method5Locals(12, 1).Equals("21")))
            Environment.Exit(-1);
         if (!(Method5Locals(12, 2).Equals("24")))
            Environment.Exit(-1);
         if (!(Method5Locals(12, 3).Equals("66")))
            Environment.Exit(-1);
         if (!(Method5Locals(12, 4).Equals("88")))
            Environment.Exit(-1);
         if (!(Method5Param(13, 1).Equals("21")))
            Environment.Exit(-1);
         if (!(Method5Param(13, 2).Equals("24")))
            Environment.Exit(-1);
         if (!(Method5Param(13, 3).Equals("66")))
            Environment.Exit(-1);
         if (!(Method5Param(13, 4).Equals("88")))
            Environment.Exit(-1);
      }

      public void Method6()
      {
         if (!(Method6Locals(9, 1).Equals("21")))
            Environment.Exit(-1);
         if (!(Method6Locals(9, 2).Equals("22")))
            Environment.Exit(-1);
         if (!(Method6Locals(9, 3).Equals("66")))
            Environment.Exit(-1);
         if (!(Method6Locals(9, 4).Equals("88")))
            Environment.Exit(-1);
         if (!(Method6Param(8, 1).Equals("21")))
            Environment.Exit(-1);
         if (!(Method6Param(8, 2).Equals("22")))
            Environment.Exit(-1);
         if (!(Method6Param(8, 3).Equals("66")))
            Environment.Exit(-1);
         if (!(Method6Param(8, 4).Equals("88")))
            Environment.Exit(-1);
      }

      public void Method7()
      {
         if (!(Method7Locals(65, 1).Equals("23")))
            Environment.Exit(-1);
         if (!(Method7Locals(65, 2).Equals("44")))
            Environment.Exit(-1);
         if (!(Method7Locals(65, 3).Equals("33")))
            Environment.Exit(-1);
         if (!(Method7Param(22, 1).Equals("23")))
            Environment.Exit(-1);
         if (!(Method7Param(22, 2).Equals("44")))
            Environment.Exit(-1);
         if (!(Method7Param(22, 3).Equals("33")))
            Environment.Exit(-1);
      }

      public void Method8()
      {
         if (!(Method8Locals(9, 1).Equals("23")))
            Environment.Exit(-1);
         if (!(Method8Locals(9, 2).Equals("44")))
            Environment.Exit(-1);
         if (!(Method8Locals(9, 3).Equals("66")))
            Environment.Exit(-1);
         if (!(Method8Locals(9, 4).Equals("88")))
            Environment.Exit(-1);
         if (!(Method8Param(8, 1).Equals("23")))
            Environment.Exit(-1);
         if (!(Method8Param(8, 2).Equals("44")))
            Environment.Exit(-1);
         if (!(Method8Param(8, 3).Equals("66")))
            Environment.Exit(-1);
         if (!(Method8Param(8, 4).Equals("88")))
            Environment.Exit(-1);
      }

      static void Main(string[] args)
      {
         TestSwitch ts = new TestSwitch();
         ts.Method1();
         ts.Method2();
         ts.Method3();
         ts.Method4();
         ts.Method5();
         ts.Method6();
         ts.Method7();
         ts.Method8();
      }
   }
}

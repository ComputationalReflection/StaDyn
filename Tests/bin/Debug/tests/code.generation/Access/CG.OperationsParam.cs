using System;

namespace OperationsFields
{
   class Program
   {
      private static int atrib = 24;

      public void Method(int a, int b, bool c, bool d)
      {
         // Binary Expression
         // -----------------

         // Arithmethic
         if ((a - b) != -6)
            Environment.Exit(-1);

         if ((a + b) != 54)
            Environment.Exit(-1);

         if ((a * b) != 720)
            Environment.Exit(-1);

         if ((a / b) != 0)
            Environment.Exit(-1);

         if (((double)a / (double)b) != 0.8)
            Environment.Exit(-1);

         if ((a % b) != 24)
            Environment.Exit(-1);

         if ((((a * b / 2) % 2) - a * (b / (b - a))) != -120)
            Environment.Exit(-1);

         // Bitwise
         if ((a | b) != 30)
            Environment.Exit(-1);

         if ((a & b) != 24)
            Environment.Exit(-1);

         if ((a ^ b) != 6)
            Environment.Exit(-1);

         //Console.Write("true\t");
         //Console.WriteLine(c | d);      // Not available
         //Console.Write("false\t");
         //Console.WriteLine(c & d);      // Not available
         //Console.Write("true\t");
         //Console.WriteLine(c ^ d);      // Not available

         if ((a << b) != 0)
            Environment.Exit(-1);

         if ((a >> b) != 0)
            Environment.Exit(-1);

         // Logical
         if ((c && d) != false)
            Environment.Exit(-1);

         if ((c || d) != true)
            Environment.Exit(-1);

         // Relational
         if ((a != b) != true)
            Environment.Exit(-1);

         if ((a == b) != false)
            Environment.Exit(-1);

         if ((c != d) != true)
            Environment.Exit(-1);

         if ((c == d) != false)
            Environment.Exit(-1);

         if ((a < b) != true)
            Environment.Exit(-1);

         if ((a > b) != false)
            Environment.Exit(-1);

         if ((a <= b) != true)
            Environment.Exit(-1);

         if ((a >= b) != false)
            Environment.Exit(-1);

         // Assignment

         if ((a = b) != 30)
            Environment.Exit(-1);

         if ((b += b) != 60)
            Environment.Exit(-1);

         if ((b /= 4) != 15)
            Environment.Exit(-1);

         if ((a -= b) != 15)
            Environment.Exit(-1);

         if ((a *= b) != 225)
            Environment.Exit(-1);

         if ((a /= b) != 15)
            Environment.Exit(-1);

         if ((a %= b) != 0)
            Environment.Exit(-1);

         if ((a >>= b) != 0)
            Environment.Exit(-1);

         if ((a <<= b) != 0)
            Environment.Exit(-1);

         if ((a &= b) != 0)          // Not available for boolean expression
            Environment.Exit(-1);

         if ((a ^= b) != 15)         // Not available for boolean expression
            Environment.Exit(-1);

         if ((a |= b) != 15)         // Not available for boolean expression
            Environment.Exit(-1);

         // Unary Expression
         // ----------------

         if ((a++) != 15)
            Environment.Exit(-1);

         if ((++a) != 17)
            Environment.Exit(-1);

         if ((b--) != 15)
            Environment.Exit(-1);

         if ((--b) != 13)
            Environment.Exit(-1);

         if ((!c) != true)
            Environment.Exit(-1);

         if ((~b) != -14)
            Environment.Exit(-1);

         if ((-a) != -17)
            Environment.Exit(-1);

         // Ternary Expression
         // ------------------

         if (((a > b) && (a != b) ? (a += (b * a)) : 7) != 238)
            Environment.Exit(-1);
      }

      static void Main(string[] args)
      {
         int b = 24 + 6;
         const bool c = false;
         bool d = true;

         Program p = new Program();
         p.Method(atrib, b, c, d);
      }
   }
}

using System;

namespace Operations2
{
   class Program
   {
      static void Main(string[] args)
      {
         int a = 24;
         int b = 24 + 6;
         bool c = false;
         bool d = true;

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

         if ((a += b) != 60)
            Environment.Exit(-1);

         if ((a -= b) != 30)
            Environment.Exit(-1);

         if ((a *= b) != 900)
            Environment.Exit(-1);

         if ((a /= b) != 30)
            Environment.Exit(-1);

         if ((a %= b) != 0)
            Environment.Exit(-1);

         if ((a >>= b) != 0)
            Environment.Exit(-1);

         if ((a <<= b) != 0)
            Environment.Exit(-1);

         if ((a &= b) != 0)    // Not available for boolean expression
            Environment.Exit(-1);

         if ((a ^= b) != 30)    // Not available for boolean expression
            Environment.Exit(-1);

         if ((a |= b) != 30)    // Not available for boolean expression
            Environment.Exit(-1);

         // Unary Expression
         // ----------------

         if ((a++) != 30)
            Environment.Exit(-1);

         if ((++a) != 32)
            Environment.Exit(-1);

         if ((b--) != 30)
            Environment.Exit(-1);

         if ((--b) != 28)
            Environment.Exit(-1);

         if ((!c) != true)
            Environment.Exit(-1);

         if ((~b) != -29)
            Environment.Exit(-1);

         if ((-a) != -32)
            Environment.Exit(-1);

         // Ternary Expression
         // ------------------

         if (((a > b) && (a != b) ? (a += (b * a)) : 7) != 928)
            Environment.Exit(-1);
      }
   }
}

using System;

namespace OperationsExps
{
   class OpExpressions
   {
      private int[] a = new int[] { 1, 2, 3, 4, 5 };
      private static int[] b = new int[1] {7};

      public void Method(bool[] c)
      {
         // Binary Expression
         // -----------------

         // Arithmethic
         if ((a[0] + a[1]) != 3)
            Environment.Exit(-1);

         if ((((double)(a[4] * a[4])) / (double)b[0]) != (25.0 / 7.0))
            Environment.Exit(-1);

         if ((((a[3] * b[0] / a[1]) % a[2]) - a[4] * (b[0] / (b[0] - a[0]))) != -3)
            Environment.Exit(-1);

         // Bitwise

         if ((a[3] | b[0]) != 7)
            Environment.Exit(-1);

         if ((b[0] & a[3]) != 4)
            Environment.Exit(-1);

         if ((a[4] ^ a[2]) != 6)
            Environment.Exit(-1);

         if ((a[0] << a[1]) != 4)
            Environment.Exit(-1);

         if ((a[1] >> a[0]) != 1)
            Environment.Exit(-1);

         // Logical

         if ((c[0] && c[1]) != false)
            Environment.Exit(-1);

         if ((c[0] || c[1]) != true)
            Environment.Exit(-1);

         // Relational

         if ((a[3] != a[0]) != true)
            Environment.Exit(-1);

         if ((a[3] == a[2]) != false)
            Environment.Exit(-1);

         if ((c[0] != c[1]) != true)
            Environment.Exit(-1);

         if ((c[1] == c[0]) != false)
            Environment.Exit(-1);

         if ((a[2] < b[0]) != true)
            Environment.Exit(-1);

         if ((a[1] > a[4]) != false)
            Environment.Exit(-1);

         if ((b[0] <= a[3]) != false)
            Environment.Exit(-1);

         if ((a[2] >= a[1]) != true)
            Environment.Exit(-1);

         // Assignment

         if ((a[4] = b[0]) != 7)
            Environment.Exit(-1);

         if ((a[2] += a[2]) != 6)
            Environment.Exit(-1);

         if ((a[3] -= a[3]) != 0)
            Environment.Exit(-1);

         if ((a[1] *= b[0]) != 14)
            Environment.Exit(-1);

         if ((b[0] /= a[2]) != 1)
            Environment.Exit(-1);

         if ((a[2] %= (a[3] + a[2])) != 0)
            Environment.Exit(-1);

         if ((a[4] >>= a[1]) != 0)
            Environment.Exit(-1);

         if ((a[4] <<= a[2]) != 0)
            Environment.Exit(-1);

         if ((a[1] &= a[1]) != 14) // Not available for boolean expression
            Environment.Exit(-1);

         if ((a[3] ^= b[0]) != 1)  // Not available for boolean expression
            Environment.Exit(-1);

         if ((a[0] |= a[0]) != 1)  // Not available for boolean expression
            Environment.Exit(-1);

         // Unary Expression
         // ----------------

         if ((b[0]++) != 1)
            Environment.Exit(-1);

         if ((++b[0]) != 3)
            Environment.Exit(-1);

         if ((b[0]--) != 3)
            Environment.Exit(-1);

         if ((--b[0]) != 1)
            Environment.Exit(-1);

         if ((a[3]++) != 1)
            Environment.Exit(-1);

         if ((++a[3]) != 3)
            Environment.Exit(-1);

         if ((a[3]--) != 3)
            Environment.Exit(-1);

         if ((--a[3]) != 1)
            Environment.Exit(-1);

         if ((!c[0]) != true)
            Environment.Exit(-1);

         if ((~a[4]) != -1)
            Environment.Exit(-1);

         if ((-a[1]) != -14)
            Environment.Exit(-1);

         // Ternary Expression
         // ------------------

         if (((a[2] > a[2]) && (a[4] != a[0]) ? 7 : (a[2] += (a[1] * a[3]))) != 14)
            Environment.Exit(-1);
      }

      static void Main(string[] args)
      {
         bool[] c = new bool[] { false, true };
         OpExpressions op = new OpExpressions();
         op.Method(c);
      }
   }
}

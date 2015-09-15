using System;

namespace Testing.Dynamics
{
   class Arrays
   {
      public static void Main()
      {
         // * Local is a dynamic reference
         var[] vectorList = new var[10];
         for (int i = 0; i < vectorList.Length; i++)
            if (i % 2 == 0)
               vectorList[i] = i < 5;
            else
               vectorList[i] = i;

         // vectorList: Array(int\/bool)
         
         int n = vectorList.Length/2; // System.Int32.MAXVALUE
         if (n % 2 == 0)
             n++;
         bool b = vectorList[n] > n || vectorList[n + 1];
         Console.WriteLine("{0} {1}", n, b);
      }
   }
}
using System;

namespace OpArrays
{
   class OpArraysParam
   {
      public void Method(int[] intArray, string[][] strArray, int index)
      {
         if (!(strArray is System.Array))
            Environment.Exit(-1);

         intArray[0] = 3;

         if (intArray[0] != 3)
            Environment.Exit(-1);

         strArray[3] = new string[2];

         if (!(strArray[3] is System.Array))
            Environment.Exit(-1);

         strArray[3][index] = "Hello";
         index = intArray[index - 1];
         if (!(strArray[index][1].Equals("Hello")))
            Environment.Exit(-1);
      }

      static void Main(string[] args)
      {
         OpArraysParam aF = new OpArraysParam();
         int[] intArray = new int[2];
         string[][] strArray;
         strArray = new string[4][];
         int index = 1;
         aF.Method(intArray, strArray, index);
      }
   }
}

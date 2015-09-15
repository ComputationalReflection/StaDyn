using System;
using System.Text;

namespace TestType
{
   public class ShowArray
   {
      private string show(int[] arr)
      {
         StringBuilder aux = new StringBuilder();

         if (arr != null)
         {
            for (int i = 0; i < arr.Length; i++)
            {
               aux.Append(arr[i]);
               aux.Append(" ");
            }
            aux.AppendLine();
         }

         return aux.ToString();
      }

      public void Arrays()
      {
         StringBuilder aux = new StringBuilder();

         int[][] a2 = new int[2][];
         a2[0] = new int[] { 1, 2 };
         a2[0][0] = 24;

         for (int i = 0; i < a2.Length; i++)
         {
            aux.Append(show(a2[i]));
         }

         if (!(aux.ToString().Equals("24 2 \r\n")))
            Environment.Exit(-1);

         aux.Remove(0, aux.Length);

         int[][][] a3 = new int[2][][];
         a3[1] = new int[3][];
         a3[1][2] = new int[] { 9, 8, 7 };

         for (int i = 0; i < a3.Length; i++)
         {
            if (a3[i] != null)
            {
               for (int j = 0; j < a3[i].Length; j++)
               {
                  aux.Append(show(a3[i][j]));
               }
            }
         }

         if (!(aux.ToString().Equals("9 8 7 \r\n")))
            Environment.Exit(-1);
      }

      static void Main(string[] args)
      {
         ShowArray a = new ShowArray();
         a.Arrays();
      }
   }
}
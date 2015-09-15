using System;
using System.Text;

namespace TestFor2
{
   public class TestFor2
   {
      private string show(int[] arr)
      {
         StringBuilder aux = new StringBuilder();

         for (int i = 0; i < arr.Length; i++)
         {
            aux.Append(arr[i]);
            aux.Append(" ");
         }
         aux.AppendLine();
         return aux.ToString();
      }

      public int[][] createArray()
      {
         int[][] a2 = new int[2][];
         a2[0] = new int[] { 1, 2, 3 };
         a2[1] = new int[] { 9, 8, 7, 6 };
         return a2;
      }

      public int[][] modifyArray(int[][] a2)
      {
         int a = 7;

         for (int i = 0; i < a2[0].Length; i++)
         {
            a2[0][i] = a + i;
         }

         for (int i = 0; i < a2[1].Length; i++)
         {
            a2[1][i] = (a + i) * 2;
         }
         return a2;
      }

      public string showArray(int[][] arr)
      {
         StringBuilder aux = new StringBuilder();

         for (int i = 0; i < arr.Length; i++)
         {
            aux.Append(show(arr[i]));
         }

         return aux.ToString();
      }

      static void Main(string[] args)
      {
         TestFor2 tf = new TestFor2();
         int[][] a = tf.createArray();

         string aux = "1 2 3 \r\n9 8 7 6 \r\n";
         if (!(tf.showArray(a).Equals(aux)))
            Environment.Exit(-1);

         aux = "7 8 9 \r\n14 16 18 20 \r\n";
         if (!(tf.showArray(tf.modifyArray(a)).Equals(aux)))
            Environment.Exit(-1);
      }
   }
}
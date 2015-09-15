using System.IO;
using System.Collections;
using System;

namespace NSArrayTest
{
   class ArrayTest
   {
      public void Method1()
      {
         double[] doubleList = new double[2];
         doubleList[0] = 10.24;

         if (doubleList[0] != 10.24)
            Environment.Exit(-1);
      }

      public void Method2()
      {
         StreamWriter[] swList = new StreamWriter[2];
         swList[0] = new StreamWriter("cc.txt");
         if (!(swList[0] is System.IO.StreamWriter))
            Environment.Exit(-1);
      }

      public void Method4()
      {
         ArrayList intList = new ArrayList();

         intList.Add(256);

         if (!(intList[0].ToString().Equals("256")))
            Environment.Exit(-1);

         intList[0] = 1024;

         if (!(intList[0].ToString().Equals("1024")))
            Environment.Exit(-1);
      }

      static void Main(string[] args)
      {
         ArrayTest a = new ArrayTest();
         a.Method1();
         a.Method2();
         a.Method4();
      }
   }
}

using System;
using System.IO;

namespace ArraySize
{
   class ArraySize
   {
      private const int size = 24;
      
      public  static void Main()
      {
         int size = 3;
         int [] sr = new int [size]; // size can be a constant or variable, but has to be an integer type
         //System.Console.WriteLine(sr.Length);
         if (sr.Length != 3)
            Environment.Exit(-1);
      }
}
}
      //public void NewArray2()
      
       // const int[] sizes = new int[2] {2, 3};
        //StreamWriter[] sw = new StreamWriter[sizes[0]];
      

      // static void Main(string[] args)
      // {
         // const int size = 45;
         // ArraySize[] aS = new ArraySize[size];

         // if (aS.Length != 45)
            // Environment.Exit(-1);

         // aS[0] = new ArraySize();
         // aS[0].NewArray();

         // if (aS[0].arrayStr.Length != 24)
            // Environment.Exit(-1);
      // }
   // }
// }

// Solo acepto enteros y constantes enteras para definir el tamaño de los arrays

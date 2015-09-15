using System;

namespace Operations3
{
   class Op3
   {
      static void Main(string[] args)
      {
         // Not available in framework 1.1
         //Console.WriteLine(Console.WindowHeight = Console.WindowHeight / 2);
         //Console.Error.WriteLine("Press any key");
         //Console.ReadLine();
         //Console.WriteLine(Console.WindowHeight = Console.WindowHeight * 2);

         System.IO.StreamWriter sw = new System.IO.StreamWriter("pp.txt");
         bool b = (new System.IO.StreamWriter("pp2.txt").AutoFlush = false);
         if (b != false)
            Environment.Exit(-1);

         if ((sw.AutoFlush = true) != true)
            Environment.Exit(-1);
      }
   }
}

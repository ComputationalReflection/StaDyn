using System;

namespace TreeModsParser
{
   class TreeMods
   {
      public void Arithmetic()
      {
         int a = 4;
         int b = 2;

         a += b;

         if (a != 6)
            Environment.Exit(-1); 

         a += b;

         if (a != 8)
            Environment.Exit(-1); 
      }

      public void Bitwise()
      {
         int a = 29;
         int b = 12;

         a |= b;

         if (a != 29)
            Environment.Exit(-1); 

         a &= b;

         if (a != 12)
            Environment.Exit(-1); 
      }

      public void IncrementDecrement()
      {
         int a = 30;

         if (a++ != 30)
            Environment.Exit(-1); 

         if (++a != 32)
            Environment.Exit(-1); 

         if (a-- != 32)
            Environment.Exit(-1); 

         if (--a != 30)
            Environment.Exit(-1); 
      }


      static void Main()
      {
         TreeMods tm = new TreeMods();
         tm.Arithmetic();
         tm.Bitwise();
         tm.IncrementDecrement();
      }
   }
}
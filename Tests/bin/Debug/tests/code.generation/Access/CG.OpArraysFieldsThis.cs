using System;

namespace OpArrays
{
   class OpArraysFieldsThis
   {
      private int[] intArray = new int[2];
      private string[][] strArray;
      private int index = 1;

      public void Method()
      {
         //Console.WriteLine(this.strArray = new string[4][]);
         //Console.WriteLine(this.intArray[0] = 3);
         //Console.WriteLine(this.strArray[3] = new string[2]);
         //Console.WriteLine(this.strArray[3][this.index] = "Hola");
         //Console.WriteLine(this.index = this.intArray[this.index - 1]);
         //string str;
         //Console.WriteLine(str = this.strArray[this.index][1]);

         this.strArray = new string[4][];

         if (!(this.strArray is System.Array))
            Environment.Exit(-1);

         this.intArray[0] = 3;

         if (this.intArray[0] != 3)
            Environment.Exit(-1);

         this.strArray[3] = new string[2];

         if (!(this.strArray[3] is System.Array))
            Environment.Exit(-1);

         this.strArray[3][this.index] = "Hello";
         this.index = this.intArray[this.index - 1];
         if (!(this.strArray[this.index][1].Equals("Hello")))
            Environment.Exit(-1);
      }

      static void Main(string[] args)
      {
         OpArraysFieldsThis aF = new OpArraysFieldsThis();
         aF.Method();
      }
   }
}

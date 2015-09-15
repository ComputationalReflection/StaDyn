using System;

namespace ValueTypes
{
   class TestValueTypes
   {
      private int n1;
      private var n2;

      private string TestValueTypeInt(bool cond)
      {
         if (cond)
         {
            this.n1 = 3;
         }
         else
         {
            n1 = 7;
         }

         return n1.ToString();
      }

      private string TestValueTypeVarInt(bool cond)
      {
         if (cond)
         {
            this.n2 = 3;
         }
         else
         {
            n2 = 7;
         }

         return n2.ToString();
      }

      private var n3;

      private string TestValueTypeVarIntDouble(bool cond)
      {
         if (cond)
         {
            n3 = 3;
         }
         else
         {
            this.n3 = 7.9;
         }

         return n3.ToString();
      }

      static void Main(string[] args)
      {
         TestValueTypes tvt = new TestValueTypes();

         if (!(tvt.TestValueTypeInt(true).Equals("3")))
            Environment.Exit(-1);

         if (!(tvt.TestValueTypeInt(false).Equals("7")))
            Environment.Exit(-1);

         if (!(tvt.TestValueTypeVarInt(true).Equals("3")))
            Environment.Exit(-1);

         if (!(tvt.TestValueTypeVarInt(false).Equals("7")))
            Environment.Exit(-1);

         if (!(tvt.TestValueTypeVarIntDouble(true).Equals("3")))
            Environment.Exit(-1);

         if (!(tvt.TestValueTypeVarIntDouble(false).Equals("7,9")))
            Environment.Exit(-1);
      }
   }
}

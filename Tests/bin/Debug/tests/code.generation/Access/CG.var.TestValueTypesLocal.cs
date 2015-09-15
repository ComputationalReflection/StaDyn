using System;

namespace ValueTypes
{
   class TestValueTypes
   {
      private string TestValueTypeInt(bool cond)
      {
         int x;

         if (cond)
         {
            x = 3;
         }
         else
         {
            x = 7;
         }

         return x.ToString();
      }

      private string TestValueTypeVarInt(bool cond)
      {
         var x;

         if (cond)
         {
            x = 3;
         }
         else
         {
            x = 7;
         }

         return x.ToString();
      }

      private string TestValueTypeVarIntDouble(bool cond)
      {
         var x;

         if (cond)
         {
            x = 3;
         }
         else
         {
            x = 7.9;
         }

         return x.ToString();
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

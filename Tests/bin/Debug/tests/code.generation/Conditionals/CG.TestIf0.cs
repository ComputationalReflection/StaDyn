using System;

namespace TestIf0
{
   class TestIf0
   {
      public string If(bool a)
      {
         string str = "";

         if (a == true)
            str = "True";
         else
            str = "False";

         return str;
      }

      public string If2(bool a)
      {
         string str = "";

         if (a != true)
            str = "True";
         else
            str = "False";

         return str;
      }


      static void Main(string[] args)
      {
         TestIf0 testif = new TestIf0();
         bool b = true;

         if (!(testif.If(b).Equals("True")))
            Environment.Exit(-1);

         if (!(testif.If2(b).Equals("False")))
            Environment.Exit(-1);

         if (!(testif.If(!b).Equals("False")))
            Environment.Exit(-1);

         if (!(testif.If2(!b).Equals("True")))
            Environment.Exit(-1);
      }
   }
}

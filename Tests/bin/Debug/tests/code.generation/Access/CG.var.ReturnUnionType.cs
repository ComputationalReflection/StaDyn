using System;

class VarReturnUnionType
{
   public static var union1(bool condicion)
   {
      if (condicion)
         return 2;
      else
         return 1.5;
   }
   public static var union2(bool condicion)
   {
      if (condicion)
         return "2";
      else
         return 1.5;
   }

   static void Main()
   {
      var a = VarReturnUnionType.union1(true);
      if (a != 2)
         Environment.Exit(-1);

      var b = VarReturnUnionType.union1(false);
      if (b != 1.5)
         Environment.Exit(-1);

      var c = VarReturnUnionType.union2(true);
      if (!(c.Equals("2")))
         Environment.Exit(-1);

      var d = VarReturnUnionType.union2(false);
      if (d != 1.5)
         Environment.Exit(-1);

   }
}
using System;

public class Ands
{
   public var And(var op1, var op2)
   {
      return op1 && op2;
   }

   public static var StaticAnd(var op1, var op2)
   {
      return op1 && op2;
   }
}

public class VarsUnified
{
   public void CallAnd()
   {
      Ands a = new Ands();
      //Console.WriteLine(a.And(true, false));
      if (a.And(true, false) != false)
         Environment.Exit(-1);

      int n1 = 23;
      int n2 = 43;
      //Console.WriteLine(a.And(n1 < n2, n2 > n1));
      if (!(a.And(n1 < n2, n2 > n1)))
         Environment.Exit(-1);

      bool c = n1 > n2;
      bool d = true;
      //Console.WriteLine(a.And(c, d));
      if (a.And(c, d))
         Environment.Exit(-1);
   }

   public void CallStaticAnd()
   {
      //Console.WriteLine(Ands.StaticAnd(false, false));
      if (Ands.StaticAnd(false, false) == true)
         Environment.Exit(-1);
      int a = 23;
      int b = 43;
      //Console.WriteLine(Ands.StaticAnd(a < b, b > a));
      if (Ands.StaticAnd(a < b, b > a) == false)
         Environment.Exit(-1);

      bool c = a > b;
      bool d = true;
      //Console.WriteLine(Ands.StaticAnd(c, d));
      if (Ands.StaticAnd(c, d))
         Environment.Exit(-1);
   }

   static void Main()
   {
      VarsUnified vu = new VarsUnified();
      vu.CallAnd();
      vu.CallStaticAnd();
   }
}

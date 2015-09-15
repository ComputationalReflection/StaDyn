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
      Console.WriteLine(a.And(true, false));
      int n1 = 23;
      int n2 = 43;
      Console.WriteLine(a.And(n1 < n2, n2 > n1));
      bool c = n1 > n2;
      bool d = true;
      Console.WriteLine(a.And(c, d));
   }

   public void CallStaticAnd()
   {
      Console.WriteLine(Ands.StaticAnd(false, false));
      int a = 23;
      int b = 43;
      Console.WriteLine(Ands.StaticAnd(a < b, b > a));
      bool c = a > b;
      bool d = true;
      Console.WriteLine(Ands.StaticAnd(c, d));
   }

   static void Main()
   {
      VarsUnified vu = new VarsUnified();
      vu.CallAnd();
      vu.CallStaticAnd();
   }
}

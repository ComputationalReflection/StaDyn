using System;
using Test;

namespace Test
{
   public class ClassA
   {
      protected string id;

      public ClassA()
      {
         id = "A";
      }

      public string GetId()
      {
         return id;
      }
   }

   public class ClassB : ClassA
   {
      public ClassB()
      {
         id = "B";
      }
   }
}

class ParamLocal
{
   public static void Write(ClassA c1, ClassA c2)
   {
      Console.WriteLine("[ClassA] Local: {0}, Param: {0}", c1.GetId(), c2.GetId());
   }

   public static void Write(bool b1, bool b2)
   {
      Console.WriteLine("[Boolean] Local: {0}, Param: {0}", b1, b2);
   }

   public static void varios(var param, bool cond1, bool cond2)
   {
      var local = param;

      if (cond1)
      {
         local = param = new ClassA();
      }
      else
      {
         if (cond2)
            local = param = true;
         else
            local = param = new ClassB();
      }

      ParamLocal.Write(local, param);
   }

   static void Main()
   {
      ParamLocal.varios(0, true, true);   // A
      ParamLocal.varios(0, false, true);  // true
      ParamLocal.varios(0, false, false); // B
   }
}
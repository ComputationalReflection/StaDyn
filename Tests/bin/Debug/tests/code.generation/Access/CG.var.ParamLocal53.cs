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

   public class ClassC
   {
      private string id;

      public ClassC()
      {
         id = "C";
      }

      public string GetId()
      {
         return id;
      }
   }
}

class ParamLocal
{
   public static ClassA Write(ClassA c1, ClassA c2)
   {
      return c1;
   }

   //public static ClassC Write(ClassC c1, ClassC c2)
   //{
   //   return c1;
   //}

   public static bool Write(bool b1, bool b2)
   {
      return b1;
   }

   public static var varios(var param, bool cond1, bool cond2)
   {
      var local = param;
      var toRet;

      if (cond1)
      {
         local = param = new ClassA();
      }
      else
      {
         if (cond2)
            local = param = true;
            //local = param = new ClassC();
         else
            local = param = new ClassB();
      }

      toRet = ParamLocal.Write(local, param);

      return toRet;
   }

   static void Main()
   {
      Console.WriteLine(ParamLocal.varios(0, true, true));   // A
      Console.WriteLine(ParamLocal.varios(0, false, true));  // true // C
      Console.WriteLine(ParamLocal.varios(0, false, false)); // B
   }
}
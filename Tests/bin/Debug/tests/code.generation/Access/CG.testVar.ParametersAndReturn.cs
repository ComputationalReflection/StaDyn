using System;

namespace TestVar.ParametersAndReturn
{
   class Class
   {
      Class() { }

      var m(var param)
      {
         return param;
      }

      static var staticMethod(var param)
      {
         return param;
      }

      public static void testOK()
      {
         // * New class reference with fresh variables
         Class klass = new Class();
         // * Method invocation by means of unification
         string myString = klass.m("hello");

         if (!(myString.Equals("hello")))
            Environment.Exit(-1);

         // * Another class: fresh variables
         Class other;
         // * Assignment means unification
         other = klass;

         char c = other.m('a');
         if (c != 'a')
            Environment.Exit(-1);

         // * Static method invocation
         int n = staticMethod(3);
         if (n != 3)
            Environment.Exit(-1);

         // * Unification of local references 
         var local = klass;

         if (!(local is Class))
            Environment.Exit(-1);
      }

      public static void Main()
      {
         Class.testOK();
      }
   }
}


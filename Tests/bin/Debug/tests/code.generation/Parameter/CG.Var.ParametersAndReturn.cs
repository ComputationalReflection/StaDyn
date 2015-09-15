using System;

namespace CG.Var.ParametersAndReturn {
    public class Klass {
        public Klass() { }
        public var m(var param) {
            return param;
        }
		
        public static var staticMethod(var param) {
            return param;
        }
		
        public static void testOK() {
            // * New class reference with fresh variables
            Klass klass = new Klass();
            // * Method invocation by means of unification
            string myString = klass.m("hello");
            // * Another class: fresh variables
            Klass other;
            // * Assignment means unification
            other = klass;
            char c = other.m('a');
            // * Static method invocation
            int n = staticMethod(3);
            // * Unification of local references 
            var local = klass;
        }

        public static void Main() {
			Klass.testOK();
			Console.WriteLine("Successfull!!");
        }
    }
}


using System;

namespace Testing.CG.Var.MethodInvocation {

    class Test {

        public void greet() {
            Console.WriteLine("Hello, world!");
        }

        public string greet(string name) {
            string s = "Hello, " + name + "!";
            Console.WriteLine(s);
            return s;
        }

        public char greet(char c, int i, double d, bool b) {
            string s = "Hello, " + c + ", " + i + ", " + d + ", " + b + "!";
            Console.WriteLine(s);
            return c;
        }
        public static void Main() {
            Test obj = new Test();
            // * Explicit call
            obj.greet();
            // * Implicit call with substitution
            var subst = obj;
            subst.greet();
            // * Implicit call without substitution
            varGreet(obj);
            // * Implicit call without substitution, 
            //   passing parameters,
            //   overload resolution,
            //   return value
            Console.WriteLine(varGreet(obj, "Mary"));
            // * Implicit call without substitution, 
            //   passing parameters,
            //   overload resolution,
            //   boxing,
            //   promotion,
            //   boxed and promoted return value
            char c = '1';
            double d = varGreet(obj, c, 2, '0', true);

            if (d != c)
                Environment.Exit(-1);
        }

        public static void varGreet(var obj) {
            // * The type of obj is unknown
            obj.greet();
        }

        public static string varGreet(var obj, string name) {
            // * The type of obj is unknown
            return obj.greet(name);
        }

        public static double varGreet(var obj, char c, int i, char d, bool b) {
            // * The type of obj is unknown
            // * All the paramenters are boxed
            // * The parameter d is promoted
            return obj.greet(c, i, d, b);
            // * The return value is unboxed
            // * The return value is promoted
        }

    }
}

using System;

namespace Testing.TestNameSpace {

    class Base {
        public void m() {
            // * Some qualified names resolution
            System.Diagnostics.CodeAnalysis.SuppressMessageAttribute;
            Testing.TestNameSpace.Base;
            System;
            System.Console;
        }

        public void staticMembers() {
            System.Console.WriteLine();
            Console.WriteLine();
            Testing.TestNameSpace.Objeto.StaticMethod();
            Objeto.staticAttribute;
        }

    }

    class Objeto : Base {
        public static void StaticMethod() { }

        private static int staticAttribute;

        public static void Main() { }
    }
}
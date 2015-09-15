using System;

namespace CG.Explicit.NamespaceAndStatic {

     class Base
    {
        private static int c = 3;
        public void Namespaces()
        {
            // * Some qualified names resolution
            object o = new System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("","");
            int c = CG.Explicit.NamespaceAndStatic.Base.c;
        }

        public void StaticMembers()
        {
            System.Console.WriteLine();
            Console.WriteLine();
            CG.Explicit.NamespaceAndStatic.Objeto.StaticMethod();
            int sa = Objeto.staticAttribute;
        }

    }

    class Objeto : Base
    {
        public static void StaticMethod() { }

        public static int staticAttribute;

        public static void Main()
        {
            Objeto o = new Objeto();
            o.Namespaces();
            o.StaticMembers();
        }
    }
}
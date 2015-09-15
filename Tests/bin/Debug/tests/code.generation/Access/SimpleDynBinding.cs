
using System;

namespace Tests {
    internal class Parent {
        public virtual void message() {
            Console.WriteLine("PADRE");
        }
    }
    internal class Child : Parent{
        public override void message() {
            Console.WriteLine("Child");
        }
    }
    class Principal {
        public static void Main(String[] args) {
            Parent P;
            Child  C;
            P = C = new Child();
            P.message();
            C.message();

        }
    }
}
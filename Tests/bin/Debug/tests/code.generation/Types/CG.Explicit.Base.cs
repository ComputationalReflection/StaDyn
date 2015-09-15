using System;

namespace CG.Explicit.Base {

    class Base {
        public void m() { }
    }

    class Objeto : Base {
        public Objeto(var p)
            : base() {
            base.m();
        }

        public static void Main() { }

    }
}
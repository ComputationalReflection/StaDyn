using System;

namespace Testing.Wrong.TestBase {

    class Base {
        public void m() { }
    }

    class Objeto : Base {
        public Objeto(int p)
            : base(p) { // * Error
            base.m(p);  // * Error
        }

    }

    public class Run {
        public static void Main() {
        }
    }

}
using System;

namespace Testing.Wrong.Overload {


    class WrongTests {

        public void m1() { }
        public void m1(int n) { }
        // * Already exists
        public void m1() { } // * Error


        public void m2() { }
        public void m2(int n) { }
        public void m2(var n) { }
        // * Already exists
        public void m2(var m) { }  // * Error


    }

    public class Run {
        public static void Main() {
        }
    }

}
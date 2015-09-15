using System;

namespace Testing.Wrong.TestCast {

    class Test {

        static void test() {
            Test test = new Test();

            bool b = (bool)3.45; // * Error
            int m = (int)test; // * Error
        }

    }

    public class Run {
        public static void Main() {
        }
    }

}
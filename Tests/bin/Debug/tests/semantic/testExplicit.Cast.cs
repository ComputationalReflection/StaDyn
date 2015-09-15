using System;

namespace Testing.TestCast {

    class Test {

        static void test() {
            // * Not necessary casts
            double d = (double)3;
            object obj = (Test)new Test();

            // * Necessary casts
            int n = (int)d;
            Test test = (Test)obj;
        }

        public static void Main() { }
    }


}
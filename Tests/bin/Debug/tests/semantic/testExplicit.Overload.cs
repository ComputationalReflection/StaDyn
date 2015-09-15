using System;

namespace Testing.Overload {

    class Class {
        public void m() {}
        public void m(int n) {}

        public void m2() { }
        public void m2(int n) { }
        public void m2(var m) { }

        public void m3(int n) { }
        public void m3(var m) { }

        public static void test() {
            Class obj = new Class();
            obj.m2('a');
            obj.m3(3);
        }

        public static void Main() { }

    }


}
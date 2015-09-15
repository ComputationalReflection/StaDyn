using System;
using TestVar;

namespace TestVar.SSA {

    class Test {
        public static void testSwitchLocal() {
            int n; char c; double d; string s; bool b;
            var r1, r2, r3;
            r1 = "hi";
            r2 = "hi";
            r3 = "hi";
            switch (n) {
                case 0:
                    s = r1;
                    r1 = 3;
                    n = r1;
                    break;
                case 1:
                    s = r1;
                    r1 = '3';
                    c = r1;
                    break;
                case 2:
                    s = r1;
                    r1 = 33.3;
                    d = r1;

                    s = r2;
                    r2 = 33.3;
                    d = r2;
                    break;
                default:
                    s = r1;
                    r1 = true;
                    b = r1;

                    s = r3;
                    r3 = 33.3;
                    d = r3;
                    break;
            }
            r1 + "bye";
            r2 + "bye";
        }

        public static void testSwitchObj() {
            int n; char c; double d; string s; bool b;
            var r1, r2, r3;
            r1 = new VarWrap("hi");
            r2 = new VarWrap("hi");
            r3 = new VarWrap("hi");
            switch (n) {
                case 0:
                    s = r1.get();
                    r1.set(3);
                    n = r1.get();
                    break;
                case 1:
                    s = r1.get();
                    r1.set('3');
                    c = r1.get();
                    break;
                case 2:
                    s = r1.get();
                    r1.set(33.3);
                    d = r1.get();

                    s = r2.get();
                    r2.set(33.3);
                    d = r2.get();
                    break;
                default:
                    s = r1.get();
                    r1.set(true);
                    b = r1.get();

                    s = r3.get();
                    r3.set(33.3);
                    d = r3.get();
                    break;
            }
            r1.get() + "bye";
            r2.get() + "bye";
        }
   
        public static void testSwitchAlias() {
            double d; string s; int n; bool b; char c;
            var obj = new VarWrap(2.2);
            var reference = new VarWrap(obj);

            var obj2 = new VarWrap(2);
            var ref2 = new VarWrap(obj2);

            switch (n) {
                case 1:
                    d = reference.get().get();
                    reference.get().set(3);
                    n = reference.get().get();
                    n = obj.get();
                case 2:
                    d = reference.get().get();
                    reference.get().set('3');
                    c = reference.get().get();
                    c = obj.get();

                    n = ref2.get().get();
                    ref2.get().set('3');
                    c = ref2.get().get();
                    c = obj2.get();
            }
            d = reference.get().get();
            d = obj.get();

            n = ref2.get().get();
            n = obj2.get();
        }

        var attribute1, attribute2, attribute3;

        public void testSwitchThis() {
            int n; char c; double d; string s; bool b;
            attribute1 = new VarWrap("hi");
            attribute2 = new VarWrap(3);
            attribute3 = new VarWrap(3);
            switch (n) {
                case 0:
                    s = attribute1.get();
                    attribute1.set(3);
                    n = attribute1.get();
                    break;
                case 1:
                    s = attribute1.get();
                    attribute1.set('3');
                    c = attribute1.get();
                    break;
                case 2:
                    s = attribute1.get();
                    attribute1.set(33.3);
                    d = attribute1.get();

                    d = attribute2.get();
                    attribute2.set(33.3);
                    d = attribute2.get();
                    break;
                case 3:
                    s = attribute1.get();
                    attribute1.set(true);
                    b = attribute1.get();

                    d = attribute3.get();
                    attribute3.set(33.3);
                    d = attribute3.get();
                    break;
            }
            attribute1.get() + "bye";
            d = attribute2.get();
            d = attribute3.get();
        }

        public static void runTestSwitchThis() {
            double d;
            Test test = new Test();
            test.testSwitchThis();
            test.attribute1.get() + "bye";
            d = test.attribute2.get();
            d = test.attribute3.get();
        }

        public static void Main() {
        }
    }

}


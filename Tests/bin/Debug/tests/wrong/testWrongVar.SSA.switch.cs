using System;
using TestVar;

namespace TestVar.SSA {

    class Test {

        public static void wrongTestSwitchLocal() {
            int n; char c; double d; string s; bool b;
            var r1, r2, r3;
            r1 = "hi";
            r2 = "hi";
            r3 = "hi";
            switch (n) {
                case 0:
                    n = r1; // * Error
                    r1 = 3;
                    s = r1; // * Error
                    break;
                case 1:
                    c = r1; // * Error
                    r1 = '3';
                    s = r1; // * Error
                    break;
                case 2:
                    d = r1; // * Error
                    r1 = 33.3;
                    s = r1; // * Error

                    d = r2; // * Error
                    r2 = 33.3;
                    s = r2; // * Error
                    break;
                default:
                    b = r1; // * Error
                    r1 = true;
                    s = r1; // * Error

                    d = r3; // * Error
                    r3 = 33.3;
                    s = r3; // * Error
                    break;
            }
            d = r1; // * Error
            d = r2; // * Error
            d = r3; // * Error
        }

        public static void wrongTestSwitchObj() {
            int n; char c; double d; string s; bool b;
            var r1, r2, r3;
            r1 = new VarWrap("hi");
            r2 = new VarWrap("hi");
            r3 = new VarWrap("hi");
            switch (n) {
                case 0:
                    n = r1.get(); // * Error
                    r1.set(3);
                    s = r1.get(); // * Error
                    break;
                case 1:
                    c = r1.get(); // * Error
                    r1.set('3');
                    s = r1.get(); // * Error
                    break;
                case 2:
                    d = r1.get(); // * Error
                    r1.set(33.3);
                    s = r1.get(); // * Error

                    d = r2.get(); // * Error
                    r2.set(33.3);
                    s = r2.get(); // * Error
                    break;
                default:
                    b = r1.get(); // * Error
                    r1.set(true);
                    s = r1.get(); // * Error

                    d = r3.get(); // * Error
                    r3.set(33.3);
                    s = r3.get(); // * Error
                    break;
            }
            d = r1.get(); // * Error
            d = r2.get(); // * Error
            d = r3.get(); // * Error
        }

        public static void wrongTestSwitchAlias() {
            double d; string s; int n; bool b; char c;
            var obj = new VarWrap('2');
            var reference = new VarWrap(obj);

            var obj2 = new VarWrap(2);
            var ref2 = new VarWrap(obj2);

            switch (n) {
                case 1:
                    reference.get().set(3);
                    c = reference.get().get(); // * Error
                    c = obj.get(); // * Error
                case 2:
                    reference.get().set(3.3);
                    c = reference.get().get(); // * Error
                    c = obj.get(); // * Error

                    c = ref2.get().get(); // * Error
                    ref2.get().set(3.3);
                    n = ref2.get().get(); // * Error
                    n = obj2.get(); // * Error
            }
            n = reference.get().get();  // * Error
            n = obj.get(); // * Error

            c = ref2.get().get(); // * Error
            c = obj2.get(); // * Error
        }
        
        var attribute1, attribute2, attribute3;

        public void wrongTestSwitchThis() {
            int n; char c; double d; string s; bool b;
            attribute1 = new VarWrap("hi");
            attribute2 = new VarWrap(3);
            attribute3 = new VarWrap(3);
            switch (n) {
                case 0:
                    attribute1.set(3);
                    s = attribute1.get(); // * Error
                    break;
                case 1:
                    n = attribute1.get(); // * Error
                    attribute1.set('3');
                    s = attribute1.get(); // * Error
                    break;
                case 2:
                    c = attribute1.get(); // * Error
                    attribute1.set(33.3);
                    s = attribute1.get(); // * Error

                    attribute2.set(33.3);
                    n = attribute2.get();  // * Error
                    break;
                case 3:
                    d = attribute1.get(); // * Error
                    attribute1.set(3);
                    s = attribute1.get(); // * Error

                    attribute3.set(33.3);
                    n = attribute3.get(); // * Error

                    break;
            }
            s = attribute1.get(); // * Error
            n = attribute2.get(); // * Error
            n = attribute3.get();  // * Error
        }

        public static void runWrongTestSwitchThis() {
            double d; int n; string s;
            Test test = new Test();
            test.wrongTestSwitchThis();
            s = test.attribute1.get(); // * Error
            n = test.attribute2.get(); // * Error
            n = test.attribute3.get();  // * Error

        }
    }

    public class Run {
        public static void Main() {
        }
    }

}


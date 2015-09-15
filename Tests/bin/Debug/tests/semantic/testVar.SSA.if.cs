using System;
using Figures;
using TestVar;

namespace TestVar.SSA {


    class Test {

        var attribute;

        public void setAttribute(var p) {
            this.attribute = p;
        }

        public var getAttribute() {
            return this.attribute;
        }

        /**************** If ***************/

        public void testIfLocal() {
            var reference;
            int i, j;

            reference = "hello";
            reference.Length;
            if (i > j) {
                reference.Length;
                reference = 3;
                reference % 3;
            }
            else {
                reference.Length;
                reference = 3.4;
                reference * 2;
            }
            double d = reference;

            reference = new Circle(0, 0, 10);
            reference.getRadius();
            if (i > j) {
                reference.getRadius();
                reference = new Rectangle(0, 0, 20, 20);
                reference.getWidth();
            }
            reference.getX();
        }

        public static void testIf() {
            double d; string s; int n; bool b;
            var r1 = new VarWrap(2);
            var r2 = new VarWrap(2);

            if (true) {
                n = r1.get();
                r1.set(3.3);
                d = r1.get();
            }
            n = r2.get();
            d = r1.get();
        }

        public static void testIfElse() {
            double d; string s; int n; bool b;
            var r1 = new VarWrap(2.2);
            var r2 = new VarWrap(2.2),
                r4 = new VarWrap(2.2),
                r5 = new VarWrap(2.2);

            if (true) {
                d = r1.get();
                r1.set("hi");
                s = r1.get();

                d = r2.get();
                r2.set("hi");
                s = r2.get();
            }
            else {
                d = r1.get();
                r1.set(0);
                n = r1.get();

                d = r4.get();
                r4.set(0);
                n = r4.get();
            }
            s = r1.get() + "3";
            d = r4.get();
            d = r5.get();
        }

        public static void testIfAlias() {
            double d; string s; int n; bool b; char c;
            var obj = new VarWrap(2.2);
            var reference = new VarWrap(obj);

            if (true) {
                d = reference.get().get();
                reference.get().set(3);
                n = reference.get().get();
                n = obj.get();
            }
            else {
                d = reference.get().get();
                reference.get().set('3');
                c = reference.get().get();
                c = obj.get();
            }
            n = reference.get().get();
            n = obj.get();
        }


        public static void testIfConstraint(var obj) {
            double d; string s; int n; bool b;
            if (true) {
                d = obj.get();
                obj.set("hi");
                s = obj.get();
            }
            else {
                d = obj.get();
                obj.set(0);
                n = obj.get();
            }
            s = obj.get() + "3";
        }

        public static void testIfConstraint() {
            var obj = new VarWrap(2.2);
            testIfConstraint(obj);
            string s = obj.get() + "3";
        }

        var attribute1, attribute2, attribute3, attribute4;


        public void testThisIfElse() {
            double d; string s; int n; bool b;
            attribute1 = new VarWrap(2.2);
            attribute2 = new VarWrap(2.2);
            attribute3 = new VarWrap(2.2);
            attribute4 = new VarWrap(2.2);

            if (true) {
                d = attribute1.get();
                attribute1.set("hi");
                s = attribute1.get();

                d = attribute2.get();
                attribute2.set("hi");
                s = attribute2.get();
            }
            else {
                d = attribute1.get();
                attribute1.set(0);
                n = attribute1.get();

                d = attribute3.get();
                attribute3.set(0);
                n = attribute3.get();
            }
            s = attribute1.get() + "3";
            d = attribute3.get();
            d = attribute4.get();
        }


        public static void runTestThisIfElse() {
            Test test = new Test();
            test.testThisIfElse();
        }

        public static void Main() {
        }

    }

}


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

        public void wrongTestIfLocal() {
            var reference;
            int i, j;

            reference = "hello";
            reference * 2; // * Error
            if (i > j) {
                reference * 2; // * Error
                reference = 3;
                reference.Length; // * Error
            }
            else {
                reference % 2; // * Error
                reference = 3.4;
                reference.Length; // * Error
            }
            int d = reference; // * Error

            reference = new Circle(0, 0, 10);
            double dd = reference; // * Error 
            if (i > j) {
                reference.getWidth(); // * Error
                reference = new Rectangle(0, 0, 20, 20);
                reference.getRadius(); // * Error
            }
            reference.getRadius(); // * Error
        }
        public static void wrongTestIf() {
            double d; string s; int n; bool b; char c;
            var r1 = new VarWrap(2);
            var r2 = new VarWrap(2);

            if (true) {
                c = r1.get(); // * Error
                r1.set(3.3);
                n = r1.get(); // * Error
            }
            c = r2.get(); // * Error
            n = r1.get(); // * Error
        }

        public static void wrongTestIfElse() {
            double d; string s; int n; bool b; char c;
            var r1 = new VarWrap(2.2);
            var r2 = new VarWrap(2.2),
                r4 = new VarWrap(2.2),
                r5 = new VarWrap(2.2);

            if (true) {
                n = r1.get(); // * Error
                r1.set("hi");
                d = r1.get(); // * Error

                n = r2.get(); // * Error
                r2.set("hi");
                d = r2.get(); // * Error
            }
            else {
                s = r1.get(); // * Error
                r1.set(0);
                c = r1.get(); // * Error

                s = r4.get(); // * Error
                r4.set(0);
                c = r4.get(); // * Error
            }
            d = r2.get(); // * Error
            s = r2.get(); // * Error
            n = r4.get(); // * Error
        }

        public static void wrongTestIfAlias() {
            double d; string s; int n; bool b; char c;
            var obj = new VarWrap('2');
            var reference = new VarWrap(obj);

            if (true) {
                c = reference.get().get();
                reference.get().set(3);
                c = reference.get().get(); // * Error
                c = obj.get(); // * Error
            }
            else {
                c = reference.get().get();
                reference.get().set(3.3);
                c = reference.get().get(); // * Error
                c = obj.get(); // * Error
            }
            n = reference.get().get(); // * Error
            n = obj.get();  // * Error
        }

        public static void wrongTestIfConstraint(var obj) {
            double d; string s; int n; bool b; char c;
            if (true) {
                s = obj.get(); // * Error
                obj.set("hi");
                d = obj.get(); // * Error
            }
            else {
                n = obj.get(); // * Error
                obj.set(0);
                c = obj.get(); // * Error
            }
            s = obj.get(); // * Error
            n = obj.get(); // * Error
        }

        public static void wrongTestIfConstraint() {
            var obj = new VarWrap(2.2);
            wrongTestIfConstraint(obj);
            string s = obj.get(); // * Error
            int n = obj.get(); // * Error
        }

        var attribute1, attribute2, attribute3, attribute4;


        public void wrongTestThisIfElse() {
            double d; string s; int n; bool b; char c;
            attribute1 = new VarWrap('0');
            attribute2 = new VarWrap('0');
            attribute3 = new VarWrap('0');
            attribute4 = new VarWrap('0');

            if (true) {
                s = attribute1.get(); // * Error
                attribute1.set("hi");
                c = attribute1.get(); // * Error

                s = attribute2.get(); // * Error
                attribute2.set("hi");
                c = attribute2.get(); // * Error
            }
            else {
                s = attribute1.get(); // * Error
                attribute1.set(0);
                c = attribute1.get(); // * Error

                s = attribute3.get(); // * Error
                attribute3.set(0);
                c = attribute3.get(); // * Error
            }
            c = attribute1.get(); // * Error 
            s = attribute2.get(); // * Error 
            c = attribute3.get(); // * Error 
        }


        public static void runWrongTestThisIfElse() {
            Test test = new Test();
            test.wrongTestThisIfElse();
            char c = test.attribute1.get(); // * Error
            string s = test.attribute2.get(); // * Error
            c = test.attribute3.get(); // * Error
        }


    }

    public class Run {
        public static void Main() {
        }
    }

}


using System;
using Figures;
using TestVar;

namespace TestVar.SSA {

    class Test {


        public static void wrongReferenceTestWhileIf() {
            int n; char c; double d; string s; bool b;
            var r;
            r = new Circle(0, 0, 10);
            while (true) {
                r.getRadius(); // * Error
                r.getWidth(); // * Error
                if (true) {
                    r = new Rectangle(0, 0, 10, 20);
                    r.getRadius(); // * Error
                }
                else {
                    r = new Circle(0, 0, 10);
                    r.Width(); // * Error
                }
                r = new Rectangle(0, 0, 10, 20);
                r.getY();
            }
            r.getWidth(); // * Error
            r.getRadius(); // * Error
        }

        public static void wrongReferenceTestWhileIf() {
            int n; char c; double d; string s; bool b;
            var r;
            r = new Circle(0, 0, 10);
            if (true) {
                r.getWidth(); // * Error
                while (true) {
                    r.getRadius(); // * Error
                    r = new Rectangle(0, 0, 10, 20);
                }
                r.getWidth(); // * Error
            }
            else {
                r.getWidth(); // * Error
                while (true) {
                    r.getX();
                    r.getWidth(); // * Error
                    r = new Rectangle(0, 0, 10, 20);
                }
                r.getWidth(); // * Error
            }
            r.getWidth(); // * Error
            r.getRadius(); // * Error
        }

        public static void wrongObjTestWhileIf() {
            int n; char c; double d; string s; bool b;
            var r;
            r = new VarWrap('a');
            while (true) {
                c = r.get(); // * Error
                if (true) {
                    c = r.get(); // * Error
                    r.set(3);
                    c = r.get(); // * Error
                }
                else {
                    c = r.get(); // * Error
                    r.set(3.3);
                    c = r.get(); // * Error
                }
                c = r.get(); // * Error
                r.set(3.3);
                c = r.get(); // * Error
            }
            n = r.get(); // * Error
        }

        public static void wrongObjTestIfWhile() {
            int n; char c; double d; string s; bool b;
            var r = new VarWrap('a');
            if (true) {
                while (true) {
                    c = r.get(); // * Error
                    r.set(20);
                }
                c = r.get(); // * Error
            }
            else {
                while (true) {
                    c = r.get(); // * Error
                    r.set(2.2);
                }
                c = r.get(); // * Error
            }
            c = r.get(); // * Error
        }

        var attribute;


        public void wrongThisTestWhileIf() {
                int n; char c; double d; string s; bool b;
                attribute = new VarWrap('a');
                while (true) {
                    c = attribute.get(); // * Error
                    if (true) {
                        c = attribute.get(); // * Error
                        attribute.set(3);
                        c = attribute.get(); // * Error
                    }
                    else {
                        c = attribute.get(); // * Error
                        attribute.set(3.3);
                        c = attribute.get(); // * Error
                    }
                    c = attribute.get(); // * Error
                    attribute.set(3.3);
                    c = attribute.get(); // * Error
                }
                n = attribute.get(); // * Error
            }

        public void wrongThisTestIfWhile() {
            int n; char c; double d; string s; bool b;
            attribute = new VarWrap('a');
            if (true) {
                while (true) {
                    c = attribute.get(); // * Error
                    attribute.set(20);
                    c = attribute.get(); // * Error
                }
                c = attribute.get(); // * Error
            }
            else {
                while (true) {
                    c = attribute.get(); // * Error
                    attribute.set(2.2);
                    c = attribute.get(); // * Error
                }
                c = attribute.get(); // * Error
            }
            c = attribute.get(); // * Error
        }

        public static void runWrongTestThis() {
                char c;
                Test test = new Test();
                test.wrongThisTestWhileIf();
                test.wrongThisTestIfWhile();
            }

    }

    public class Run {
        public static void Main() {
        }
    }

}


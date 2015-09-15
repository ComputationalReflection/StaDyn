using System;
using Figures;
using TestVar;

namespace TestVar.SSA {

    class Test {

        public static void referenceTestWhileIf() {
            int n; char c; double d; string s; bool b;
            var r;
            r = new Circle(0, 0, 10);
            while (true) {
                r.getX();
                if (true) {
                    r = new Rectangle(0, 0, 10, 20);
                    r.getWidth();
                }
                else {
                    r = new Circle(0, 0, 10);
                    r.getRadius();
                }
                r = new Rectangle(0, 0, 10, 20);
                r.getY();
            }
            r.getX() + r.getY();
        }

        public static void referenceTestIfWhile() {
            int n; char c; double d; string s; bool b;
            var r;
            r = new Circle(0, 0, 10);
            if (true) {
                r.getRadius();
                while (true) {
                    r.getX();
                    r = new Rectangle(0, 0, 10, 20);
                }
                r.getX() + r.getY();
            }
            else {
                r.getRadius();
                while (true) {
                    r.getX();
                    r = new Rectangle(0, 0, 10, 20);
                }
                r.getX() + r.getY();
            }
            r.getX() + r.getY();
        }


        public static void objTestWhileIf() {
            int n; char c; double d; string s; bool b;
            var r;
            r = new VarWrap('a');
            while (true) {
                d = r.get();
                if (true) {
                    d = r.get();
                    r.set(3);
                    n = r.get();
                }
                else {
                    d = r.get();
                    r.set(3.3);
                    d = r.get();
                }
                d = r.get();
                r.set(3.3);
                d = r.get();
            }
            d = r.get();
        }

        public static void objTestIfWhile() {
            int n; char c; double d; string s; bool b;
            var r = new VarWrap('a');
            if (true) {
                c = r.get();
                while (true) {
                    n = r.get();
                    r.set(20);
                    n = r.get();
                }
                n = r.get();
            }
            else {
                c = r.get();
                while (true) {
                    d = r.get();
                    r.set(2.2);
                    d = r.get();
                }
                d = r.get();
            }
            d = r.get();
        }


        var attribute;

        public void thisTestWhileIf() {
            int n; char c; double d; string s; bool b;
            attribute = new VarWrap('a');
            while (true) {
                d = attribute.get();
                if (true) {
                    d = attribute.get();
                    attribute.set(3);
                    n = attribute.get();
                }
                else {
                    d = attribute.get();
                    attribute.set(3.3);
                    d = attribute.get();
                }
                d = attribute.get();
                attribute.set(3.3);
                d = attribute.get();
            }
            d = attribute.get();
        }
   
        public void thisTestIfWhile() {
            int n; char c; double d; string s; bool b;
            attribute = new VarWrap('a');
            if (true) {
                c = attribute.get();
                while (true) {
                    n = attribute.get();
                    attribute.set(20);
                    n = attribute.get();
                }
                n = attribute.get();
            }
            else {
                c = attribute.get();
                while (true) {
                    d = attribute.get();
                    attribute.set(2.2);
                    d = attribute.get();
                }
                d = attribute.get();
            }
            d = attribute.get();
        }

        public static void runTestThis() {
            double d;
            Test test = new Test();
            test.thisTestWhileIf();
            d = test.attribute.get();
            test.thisTestIfWhile();
            d = test.attribute.get();
        }

        public static void Main() {
        }

    }

}


using System;
using Figures;

namespace TestVar.SSA {


    class Test {

        var attribute;

        public void setAttribute(var p) {
            this.attribute = p;
        }

        public var getAttribute() {
            return this.attribute;
        }

        /**************** DoWhile ***************/


        public void testDo() {
            var reference;
            int i;
            reference = new Circle(0, 0, 10);
            do {
                i = i + reference.getX();
                reference = new Rectangle(reference.getX(), reference.getY(), i * 2, i * 4); // * Recursive type inference
                reference.getWidth();
            } while (i < 10);
            int n = reference.getY() + reference.getX();
        }

        public static void testDoAttributes() {
            Test obj = new Test();
            obj.setAttribute(new Test());
            obj.getAttribute().setAttribute("hello");
            obj.getAttribute().getAttribute().Length;
            do {
                obj.getAttribute().getAttribute() + 3;
                obj.getAttribute().setAttribute(3);
                obj.attribute.attribute + '3';
            } while (true);
            string s = obj.getAttribute().getAttribute() + "3";
        }

        public void thisDoAttributes() {
            this.setAttribute(new Test());
            this.getAttribute().setAttribute("hello");
            this.getAttribute().getAttribute().Length;
            do {
                this.getAttribute().getAttribute() + 3;
                this.getAttribute().setAttribute(3);
                this.attribute.attribute + '3';
            } while (true);
            string s = this.getAttribute().getAttribute() + "3";
        }

        public static void testThisDoAttributes() {
            Test obj = new Test();
            obj.thisDoAttributes();
            string s = obj.getAttribute().getAttribute() + "3";
        }

        public static void Main() {
        }

    }

}


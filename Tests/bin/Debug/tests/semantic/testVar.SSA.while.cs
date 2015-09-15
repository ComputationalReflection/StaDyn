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


        /**************** While ***************/


        public void testWhile() {
            var reference;
            int i;
            reference = new Circle(0, 0, 10);
            while (i < 10) {
                i = i + reference.getX();
                reference = new Rectangle(reference.getX(), reference.getY(), i * 2, i * 4); // * Recursive type inference
                reference.getWidth();
            }
            int n = reference.getY() + reference.getX();
        }

        public static void testWhileAttributes() {
            Test obj = new Test();
            obj.setAttribute(new Test());
            obj.getAttribute().setAttribute("hello");
            obj.getAttribute().getAttribute().Length;
            while (true) {
                obj.getAttribute().getAttribute() + 3;
                obj.getAttribute().setAttribute(3);
                obj.attribute.attribute + '3';
            }
            string s = obj.getAttribute().getAttribute() + "3";
        }

        public void thisWhileAttributes() {
            this.setAttribute(new Test());
            this.getAttribute().setAttribute("hello");
            this.getAttribute().getAttribute().Length;
            while (true) {
                this.getAttribute().getAttribute() + 3;
                this.getAttribute().setAttribute(3);
                this.attribute.attribute + '3';
            }
            string s = this.getAttribute().getAttribute() + "3";
        }

        public static void testThisWhileAttributes() {
            Test obj = new Test();
            obj.thisWhileAttributes();
            string s = obj.getAttribute().getAttribute() + "3";
        }

        public void nestedWhile() {
            var reference;
            reference = '3';
            attribute = "3";
            attribute.Length;
            while (true) {
                reference + 3;
                reference = 3;
                reference % 2;
                attribute + 3;
                attribute = 3;
                attribute + 3;
                while (true) {
                    reference + 3;
                    reference = 3.3;
                    reference * 2;
                    attribute = 3.3;
                    reference * 2;
                }
                string s = attribute + "end";
            }
        }

        public static void testNestedWhile() {
            Test obj = new Test();
            obj.nestedWhile();
            string s = ""+obj.getAttribute(); 
        }

        public static void Main() {
        }
    }

}


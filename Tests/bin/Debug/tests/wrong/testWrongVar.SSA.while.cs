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


        public void wrongTestWhile() {
            var reference;
            int i;
            reference = new Circle(0, 0, 10);
            reference.getWidth(); // * Error
            while (i < 10) {
                i = i + reference.getRadius(); // * Error
                reference = new Rectangle(reference.getX(), reference.getY(), i * 2, i * 4); // * Recursive type inference
                reference.getRadius(); // * Error
            }
            int n = reference.getWidth(); // * Error
        }

        public static void wrongTestWhileAttributes() {
            Test obj = new Test();
            obj.setAttribute(new Test());
            obj.getAttribute().setAttribute("hello");
            obj.getAttribute().getAttribute().Length;
            while (true) {
                obj.getAttribute().getAttribute().Length; // * Error
                obj.getAttribute().getAttribute()*3; // * Error
                obj.getAttribute().getAttribute()[3]; // * Error
                obj.getAttribute().setAttribute(3);
                obj.attribute.attribute * 3.3; // * Error :-(
                obj.attribute.attribute.IndexOf('h'); // * Error
                obj.attribute.attribute['8']; // * Error
            }
            int n = obj.getAttribute().getAttribute(); // * Error
        }

        public void wrongThisWhileAttributes() {
            this.setAttribute(new Test());
            this.getAttribute().setAttribute("hello");
            while (true) {
                this.getAttribute().getAttribute().Length; // * Error
                this.getAttribute().getAttribute()*3; // * Error
                this.getAttribute().getAttribute()[3]; // * Error
                this.getAttribute().setAttribute(3);
                this.attribute.attribute * 3.3; // * Error :-(
                this.attribute.attribute.IndexOf('h'); // * Error
                this.attribute.attribute['8']; // * Error
            }
            int n = this.getAttribute().getAttribute(); // * Error
        }

        public static void wrongTestThisWhileAttributes() {
            Test obj = new Test();
            obj.wrongThisWhileAttributes();
            int n = obj.getAttribute().getAttribute(); // * Error
        }

        public void wrongNestedWhile() {
            var reference;
            reference = '3';
            attribute = "3";
            attribute.Length;
            while (true) {
                reference + 3; // * Error
                reference = 3; 
                reference % 2;
                attribute + 3; // * Error
                attribute = 3;
                attribute + 3; // * Error :-(
                while (true) {
                    reference + 3; // * Error
                    reference = new Circle(0,0,10);
                    reference * 2; // * Error
                    attribute = new Circle(0,0,10);
                    reference * 2; // * Error
                }
                double d = attribute + 3.2; // * Error
            }
        }

        public static void wrongTestNestedWhile() {
            Test obj = new Test();
            obj.wrongNestedWhile();
            int n = obj.getAttribute(); // * Error
        }
    }

    public class Run {
        public static void Main() {
        }
    }

}


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


           public void wrongTestDo() {
               var reference;
               int i;
               reference = new Circle(0, 0, 10);
               reference.getWidth(); // * Error
               do {
                   i = i + reference.getRadius(); // * Error
                   reference = new Rectangle(reference.getX(), reference.getY(), i * 2, i * 4); // * Recursive type inference
                   reference.getRadius(); // * Error
               }while (i < 10);
               int n = reference.getWidth(); // * Error
           }
           

        public static void wrongTestDoAttributes() {
            Test obj = new Test();
            obj.setAttribute(new Test());
            obj.getAttribute().setAttribute("hello");
            obj.getAttribute().getAttribute().Length;
            do {
                obj.getAttribute().getAttribute().Length; // * Error
                obj.getAttribute().getAttribute()*3; // * Error
                obj.getAttribute().getAttribute()[3]; // * Error
                obj.getAttribute().setAttribute(3);
                obj.attribute.attribute * 3.3; // * Error :-(
                obj.attribute.attribute.IndexOf('h'); // * Error
                obj.attribute.attribute['8']; // * Error
            } while(true);
            int n = obj.getAttribute().getAttribute(); // * Error
        }

        public void wrongThisDoAttributes() {
            this.setAttribute(new Test());
            this.getAttribute().setAttribute("hello");
            do {
                this.getAttribute().getAttribute().Length; // * Error
                this.getAttribute().getAttribute()*3; // * Error
                this.getAttribute().getAttribute()[3]; // * Error
                this.getAttribute().setAttribute(3);
                this.attribute.attribute * 3.3; // * Error :-(
                this.attribute.attribute.IndexOf('h'); // * Error
                this.attribute.attribute['8']; // * Error
            } while (true);
            int n = this.getAttribute().getAttribute(); // * Error
        }

        public static void wrongTestThisDoAttributes() {
            Test obj = new Test();
            obj.wrongThisDoAttributes();
            int n = obj.getAttribute().getAttribute(); // * Error
        }

    }

    public class Run {
        public static void Main() {
        }
    }

}


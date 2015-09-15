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


        /**************** For ***************/

        public void wrongTestFor() {
            var reference;
            int i, n;

            for (reference = new Circle(0, 0, 10);
                    reference.getRadius() < 10; // * Error
                    reference = new Rectangle(0, 0, 20, 10))
                reference.getWidth(); // * Error

            reference.getWidth(); // * Error

            reference = new Circle(0, 0, 10);
            for (int i = 0; i < 10; i++) {
                reference.getRadius(); // * Error
                reference = new Rectangle(0, 0, 10, 20);
                reference.getRadius(); // * Error
            }
            reference.getWidth(); // * Error
        }

        public void wrongTestForAttributes() {
            Test obj = new Test();
            int i, n;

            for (obj.attribute = new Circle(0, 0, 10);
                        obj.getAttribute().getRadius() < 10; // * Error
                        obj.setAttribute(new Rectangle(0, 0, 20, 10)))
                obj.getAttribute().getWidth(); // * Error

            n = obj.getAttribute().getWidth(); // * Error

            obj.setAttribute(new Circle(0, 0, 10));
            for (int i = 0; i < 10; i++) {
                obj.attribute.getRadius(); // * Error
                obj.attribute = new Rectangle(0, 0, 10, 20);
                obj.getAttribute().getWidth(); // * Error :-(
            }
            n = obj.getWidth(); // * Error
        }

        public void wrongThisForAttributesSimple() {
            int i, n;

            for (attribute = new Circle(0, 0, 10);
                        getAttribute().getRadius() < 10; // * Error
                        setAttribute(new Rectangle(0, 0, 20, 10)))
                getAttribute().getWidth(); // * Error

            n = getAttribute().getWidth(); // * Error

            setAttribute(new Circle(0, 0, 10));
            for (int i = 0; i < 10; i++) {
                attribute.getRadius(); // * Error
                setAttribute(new Rectangle(0, 0, 10, 20));
                getAttribute().getWidth(); // * Error :-(
            }
            n = getAttribute().getWidth(); // * Error
        }


        public static void wrongTestThisForAttributesSimple() {
            Test obj = new Test();
            obj.wrongThisForAttributesSimple();
            int n = obj.getAttribute(); // * Error
        }

        public void wrongThisForAttributesMultiple() {
            attribute = new Test();
            int i, n;

            for (attribute.attribute = new Circle(0, 0, 10);
                        getAttribute().attribute.getRadius() < 10; // * Error
                        getAttribute().setAttribute(new Rectangle(0, 0, 20, 10)))
                attribute.getAttribute().getWidth(); // * Error

            n = getAttribute().getAttribute().getWidth(); // * Error


            getAttribute().setAttribute(new Circle(0, 0, 10));
            for (int i = 0; i < 10; i++) {
                attribute.attribute.getRadius(); // * Error
                getAttribute().setAttribute(new Rectangle(0, 0, 10, 20));
                getAttribute().getAttribute().getWidth(); // * Error :-(
            }
            n = getAttribute().getAttribute().getWidth(); // * Error
        }

        public static void wrongTestThisForAttributesMultiple() {
            Test obj = new Test();
            obj.wrongThisForAttributesMultiple();
            obj.getAttribute().getAttribute().getWidth(); // * Error
        }
    }

    public class Run {
        public static void Main() {
        }
    }

}


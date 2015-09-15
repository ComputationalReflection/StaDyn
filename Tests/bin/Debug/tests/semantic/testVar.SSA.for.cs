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

        public void testFor() {
            var reference;
            int i, n;

            for (reference = new Circle(0, 0, 10); reference.getX() < 10; reference = new Rectangle(0, 0, 20, 10))
                reference.getX() * reference.getY();

            n = reference.getY() + reference.getX();

            reference = new Circle(0, 0, 10);
            for (int i = 0; i < 10; i++) {
                reference.getX();
                reference = new Rectangle(0, 0, 10, 20);
                reference.getWidth();
            }
            n = reference.getY() + reference.getX();
        }

        public void testForAttributes() {
            Test obj = new Test();
            int i, n;

            for (obj.attribute = new Circle(0, 0, 10);
                        obj.getAttribute().getX() < 10;
                        obj.setAttribute(new Rectangle(0, 0, 20, 10)))
                obj.getAttribute().getX() * obj.attribute.getY();

            n = obj.getAttribute().getY() + obj.attribute.getX();

            obj.setAttribute(new Circle(0, 0, 10));
            for (int i = 0; i < 10; i++) {
                obj.attribute.getX();
                obj.attribute = new Rectangle(0, 0, 10, 20);
                obj.getAttribute().getX();
            }
            n = obj.getAttribute().getY() + obj.attribute.getX();
        }

        public void thisForAttributesSimple() {
            int i, n;

            for (attribute = new Circle(0, 0, 10);
                        getAttribute().getX() < 10;
                        setAttribute(new Rectangle(0, 0, 20, 10)))
                getAttribute().getX() * attribute.getY();

            n = getAttribute().getY() + attribute.getX();


            setAttribute(new Circle(0, 0, 10));
            for (int i = 0; i < 10; i++) {
                attribute.getX();
                setAttribute(new Rectangle(0, 0, 10, 20));
                getAttribute().getY();
            }
            n = getAttribute().getY() + attribute.getX();
        }


        public static void testThisForAttributesSimple() {
            Test obj = new Test();
            obj.thisForAttributesSimple();
            int n = obj.getAttribute().getX() + obj.attribute.getY();
        }
                
        public void thisForAttributesMultiple() {
            attribute = new Test();
            int i, n;

            for (attribute.attribute = new Circle(0, 0, 10);
                        getAttribute().attribute.getX() < 10;
                        getAttribute().setAttribute(new Rectangle(0, 0, 20, 10)))
                attribute.getAttribute().getX() * getAttribute().attribute.getY();

            n = getAttribute().getAttribute().getY() + attribute.attribute.getX();


            getAttribute().setAttribute(new Circle(0, 0, 10));
            for (int i = 0; i < 10; i++) {
                attribute.attribute.getX();
                getAttribute().setAttribute(new Rectangle(0, 0, 10, 20));
                getAttribute().getAttribute().getY();
            }
            n = getAttribute().getAttribute().getY() + attribute.attribute.getX();
        }

        public static void testThisForAttributesMultiple() {
            Test obj = new Test();
            obj.thisForAttributesMultiple();
            int n = obj.getAttribute().getAttribute().getX() + obj.attribute.attribute.getY();
        }

        public static void Main() {
        }

    }

}


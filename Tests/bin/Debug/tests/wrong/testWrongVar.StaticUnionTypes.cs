using System;

namespace TestVar.Wrong.StaticUnionTypes {

    class Rectangle {
        private int x, y, width, height;
        public Rectangle(int x, int y, int width, int height) {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
        public int getX() { return x; }
        public int getY() { return y; }
        public int getWidth() { return width; }
        public int getHeight() { return height; }
    }

    class Circle {
        private int x, y, radius;
        public Circle(int x, int y, int radius) {
            this.x = x;
            this.y = y;
            this.radius = radius;
        }
        public int getX() { return x; }
        public int getY() { return y; }
        public int getRadius() { return radius; }
    }

    class A { void ma() { } }
    class B : A { void mb() { } }
    class C : A { void mc() { } }


    class Class {
        var attribute;

        Class() { }

        Class(var parameter) {
            this.attribute = parameter;
        }

        void setAttribute(var a) { attribute = a; }
        var getAttribute() { return attribute; }

        static var m(var param) {
            if (3 > 10) return 10;
            return param;
        }

        static var m(var param1, var param2) {
            if (3 > 10) return param1;
            return param2;
        }


        public static void wrongTestStaticUnionCalls() {
            // * Attribute is int
            Class klass = new Class(1);
            klass.setAttribute(3.3);
            // * Attribute is int \/ double
            string s = klass.getAttribute(); // * Error
            // * int \/ int
            char n = klass.m(3);          // * Error
            // * int \/ string
            string obj = klass.m("hello");   // * Error
            // Rectangle \/ Circle
            var rectangleOrCircle = m(new Rectangle(0, 0, 10, 20), new Circle(0, 0, 30));
            int radius = rectangleOrCircle.getRadius(); // * Error
        }

        public static void wrongTestStaticUnionArrays() {
            var[] genericArray = m(new int[10], new double[10]);
            int d = genericArray[0]; // * Error
            var[] badArray = m(new int[10], 3); // * Error
            var[] anotherArray = new var[10];
            anotherArray[0] = new Rectangle(0, 0, 10, 20);
            anotherArray[1] = new Circle(0, 0, 30);
            int i;
            char c = anotherArray[i].getX(); // * Error
            anotherArray[i++].getWidth();  // * Error
        }

        public static void wrongTestStaticUnionInheritance() {
            B b = m(new B(), new C()); // * Error
            Class klass = new Class(new A());
            klass.setAttribute(new B());
            klass.setAttribute(new C());
            b = klass.getAttribute(); // * Error
        }

        public static void wrongTestStaticUnionClasses() {
            // Class(int) \/ Class(double)  unifies to Class( int\/double )
            Class klass = m(new Class(3), new Class(3.3));
            int n = klass.getAttribute(); // * Error
            Class otherClass = m(new Class(3), new Rectangle(0,0,10,20)); // * Error
        }

        public static void wrongTestArithmetic() {
            // * int \/ char + int \/ double promotes to double
            int i = m(3, '3') + m(1, 3.3); // * Error
            // * int \/ char >= int \/ double is a boolean
            int b = m(3, '3') >= m(1, 3.3); // * Error
            // * bool \/ bool &&  bool \/ bool is a boolean
            int n = m(true, false) && m(false, true); // * Error
        }

    }

    public class Run {
        public static void Main() {
        }
    }

}


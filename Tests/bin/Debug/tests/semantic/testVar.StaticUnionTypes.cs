using System;
using Figures;

namespace TestVar.StaticUnionTypes {

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

        public static void testStaticUnionCalls() {
            // * Attribute is int
            Class klass = new Class(1);
            klass.setAttribute(3.3);
            // * Attribute is double
            double d = klass.getAttribute();
            // * int \/ int
            int n = Class.m(3);
            // * int \/ string
            object obj = klass.m("hello");
            int hashCode = obj.GetHashCode();
            // Rectangle \/ Circle
            var rectangleOrCircle = m(new Rectangle(0, 0, 10, 20), new Circle(0, 0, 30));
            int x = rectangleOrCircle.getX();
            Console.WriteLine("Position: ({0},{1}).", rectangleOrCircle.getX(), rectangleOrCircle.getY());
        }

        public static void testStaticUnionArrays() {
            // * Array(int) \/ Array(double)  unifies to  Array(int \/ double) 
            var[] genericArray = m(new int[10], new double[10]);
            // * int \/ double
            double d = genericArray[0];
            // * genericArray: Array(Rectangle \/ Circle)
            var[] anotherArray = new var[10];
            anotherArray[0] = new Rectangle(0, 0, 10, 20);
            anotherArray[1] = new Circle(0, 0, 30);
            int i;
            int n = anotherArray[i].getX();
            anotherArray[i++].GetHashCode();
        }

        public static void testStaticUnionInheritance() {
            // * B \/ C promotes to A
            A a = m(new B(), new C());
            a.ma();
            // * A \/ B \/ C promotes to A
            Class klass = new Class(new A());
            klass.setAttribute(new B());
            klass.setAttribute(new C());
            a = klass.getAttribute();
            a.ma();
        }

        public static void testStaticUnionClasses() {
            // Class(int) \/ Class(double)  unifies to Class( int\/double )
            Class klass = m(new Class(3), new Class(3.3));
            double d = klass.getAttribute();
        }

        public static void testArithmetic() {
            // * int \/ char + int \/ double promotes to double
            double d = m(3, '3') + m(1, 3.3);
            // * string + int \/ string + string \/ char + bool \/ string + string\/ double promotes to string
            string s = " " + m(3, "3") + m("3", '3') + m(true, "3.3") + m("true", 3.3);
            // * int \/ char >= int \/ double is a boolean
            bool b = m(3, '3') >= m(1, 3.3);
            // * bool \/ bool &&  bool \/ bool is a boolean
            b = m(true, false) && m(false, true);
        }


        public static void Main() {
        }

    }

}


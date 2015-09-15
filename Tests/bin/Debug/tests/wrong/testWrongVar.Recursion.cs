using System;

namespace TestVar.Wrong.TestRecursion {

    class DirectTypeRecursion {

        // * To make the type unifiable
        var genericAttribute;

        // * Each time a new reference of DirectTypeRecursion is created, the
        //   free variables of all its attributes are cloned => infinite loop
        // * It should be detected, leaving the reference to the existing type
        //   and doing type inference in a lazy way
        DirectTypeRecursion attribute;

        DirectTypeRecursion() { }

        void set(DirectTypeRecursion p) {
            this.attribute = p;
        }
        
        void setRecursiveAttribute(DirectTypeRecursion attribute) {
            this.attribute = attribute;
        }

        public static void test() {
            DirectTypeRecursion klass = new DirectTypeRecursion();
            klass.attribute = new DirectTypeRecursion();
            klass.attribute.genericAttribute = 3;
            klass.setRecursiveAttribute(klass);
            bool b = klass.attribute.genericAttribute; // * Error
        }
        
    }


    class IndirectTypeRecursionA {
        // * To make the type unifiable
        var genericAttribute;

        // * Indirect recursion
        IndirectTypeRecursionB b;

        IndirectTypeRecursionA(var a) {
            this.genericAttribute = a;
        }

        void setAttribute(IndirectTypeRecursionB b) {
            this.b = b;
        }

        IndirectTypeRecursionB getAttribute() {
            return b;
        }
    }


    class IndirectTypeRecursionB {
        // * To make the type unifiable
        var genericAttribute;


        // * Indirect recursion
        IndirectTypeRecursionA a;

        IndirectTypeRecursionB(var a) {
            this.genericAttribute = a;
        }

        void setAttribute(IndirectTypeRecursionA a) {
            this.a = a;
        }

        IndirectTypeRecursionA getAttribute() {
            return a;
        }
    }


    class TestIndirectTypeRecursion {
        public static void test() {
            IndirectTypeRecursionA a = new IndirectTypeRecursionA(3);
            IndirectTypeRecursionB b = new IndirectTypeRecursionB(3.3);
            a.setAttribute(b);
            b.setAttribute(a);

            int d = a.getAttribute().genericAttribute; // * Error
            char n = b.getAttribute().genericAttribute; // * Error
        }
    }

    class RoutineRecursion {
        static var direct(bool condition) {
            if (condition)
                return "hi!";
            else 
                return direct(!condition);
        }

        static var indirectA(bool condition) {
            if (condition)
                return 45;
            return indirectB(condition);
        }

        static var indirectB(bool condition) {
            if (condition)
                return indirectA(condition)+3;
            return 1.1;
        }

        public static void testRoutineRecursion() {
            char n = direct(true); // * Error
            int b1 = indirectA(true); // * Error
            int b2 = indirectB(true); // * Error
        }
    }

    public class Run {
        public static void Main() {
        }
    }

}
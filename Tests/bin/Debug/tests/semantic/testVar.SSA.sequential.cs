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

        /*********** Sequential ***************/

        public static void testSequential() {
            var reference;

            reference = 3;
            double d = reference;

            reference = 3.3;
            d = reference;

            reference = "3";
            string s = reference;

            reference = new int[10];
            d = reference[3];
        }

        public void sequentialAttributes() {
            attribute = 3;
            this.attribute % 3;
            setAttribute(3.3);            
            getAttribute() * 3;
            setAttribute("3");
            this.attribute.Length;
            attribute = new int[10];
            this.attribute[3];
        }

        public static void testSequentialAttributes() {
            Test test = new Test();
            test.sequentialAttributes();
            int n = test.attribute[0];
        }

        public static void Main() {
        }

    }

}


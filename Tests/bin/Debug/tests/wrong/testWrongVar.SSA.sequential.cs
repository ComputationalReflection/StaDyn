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
            int n = reference; // * Error

            reference = "3";
            double d = reference; // * Error

            reference = new int[10];
            char c = reference[3]; // * Error
        }

        
         public void wrongSequentialAttributes() {
             attribute = 3;
             this.attribute(3); // * Error
             setAttribute(3.3);
             getAttribute().field; // * Error
             setAttribute("3");
             this.attribute[3]; // * Error
             attribute = new int[10];
             this.attribute.Index(3); // * Error
         }

         public static void testWrongSequentialAttributes() {
             Test test = new Test();
             test.wrongSequentialAttributes();
             int n = test.attribute; // * Error
         }
         

    }

    public class Run {
        public static void Main() {
        }
    }

}


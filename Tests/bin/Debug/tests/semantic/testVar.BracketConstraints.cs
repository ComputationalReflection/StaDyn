using System;

namespace TestVar.BracketConstraints {


    class Class {

        // * Generates a constraint:
        //   var1 x var2 -> var15  |   var15:=var1[var2], var2<=int
        static var access(var op1, var op2) { 
            return op1[op2]; 
        }

        // * The inherited constraints of "add" are included into the "inc" method
        //   var4 -> var17    |   var17:=var4[int]
        static var first(var param) {
            return param[0];
        }

        // * The constraint is a composition of three constraints
        //   var6 -> var31  | var24:=var6[int], var31:=var24[int]
        static var incrementalConstraint(var param) {
            return first(first(param));
        }

        public static void testArithmeticConstraint() {
            var[] array = new var[10];
            array[0] = 'c';
            // * Constraint satisfaction
            char c = access(array,1);
            // * Constraint inheritance
            char s = first(array);
            // * Constraint composition
            var[] bidimensionalArray = new var[10];
            bidimensionalArray[0] = array;
            char r = incrementalConstraint(bidimensionalArray);
        }

        public static void Main() {
        }
    
    }

}


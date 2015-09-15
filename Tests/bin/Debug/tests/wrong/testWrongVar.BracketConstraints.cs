using System;

namespace TestVar.Wrong.BracketConstraints {


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

        public static void wrongTestArithmeticConstraint() {
            var[] array = new var[10];
            array[0] = 'c';
            // * Constraint satisfaction
            access(3, 1); // * Error
            access(array, 1.2); // * Error
            bool c = access(array, 1); // * Error
            // * Constraint inheritance
            bool s = first(array); // * Error
            // * Constraint composition
            var[] bidimensionalArray = new var[10];
            bidimensionalArray[0] = array;
            incrementalConstraint(array); // * Error
            bool r = incrementalConstraint(bidimensionalArray); // * Error
        }

    
    }

    public class Run {
        public static void Main() {
        }
    }

}


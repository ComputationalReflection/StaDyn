using System;

namespace TestVar.Wrong.CommonOperatorConstraints {


    class Class {
        // * Generates a constraint:
        //   var0 x var1 -> var3 | var3 := var0 + var1
        static var add(var op1, var op2) {
            return op1 + op2;
        }

        // * The inherited constraints of "add" are included into the "inc" method
        //   var4 -> var5 | var5 := var4 + int
        static var inc(var param) {
            return add(param, 1);
        }

        // * The constraint is a composition of three constraints
        //   var6 x var7 -> var27 | var22 := var6 + int, var23 := var22 * var7, var27 := var22 + var23
        static var incrementalConstraint(var param1, var param2) {
            var temp1 = inc(param1);
            var temp2 = temp1 * param2;
            return add(temp1, temp2);
        }

        // * Unary arithmetic constraints
        //   var10 x var11 x var(12) x var(13) -> void  |   var55:=va10++, var45:=-var11, var57:=+var12,  var58:=--var13
        static void unaryOperations(var p1, var p2, var p3, var p4) {
            p1++;
            -p2;
            +p3;
            --p4;
        }

        // * Logic constraints
        //   var23 x var24 -> bool   |  var23 <= bool,  var24 <= bool
        static var logicOperations(var p1, var p2) {
            return p1 && p2 || !p2;
        }

        // * Relational constraints
        //   var26 x var27 -> bool  | var137:=Var26==var27, var138:=var26>=var27, var137<=bool,  var138<=bool, 
        //                            var141:=Var26<var27, var142:=var26!=var27, var142<=bool, var141<=bool
        static var relationalOperations(var p1, var p2) {
            return p1 == p2 && p1 >= p2 || p1 < p2 && !(p1 != p2);
        }

        // * Bitwise constraints
        //   var29 x var30 -> var171   |   var167:=var29&var30, var29<=int, var169:=var30&int, var170:=var29^var169, var171:=var167|var170
        static var bitwiseOperations(var p1, var p2) {
            return p1 & p2 | p1 ^ p2 & ~p1;
        }

        // * Shift constraints
        //   var32 x var33 -> int   |   var32 <= int, var33 <= int
        static var shiftOperations(var p1, var p2) {
            return p1 << p2 | p1 >> p2;
        }


        public static void wrongTestArithmeticConstraint() {
            // * Type inference by means of constraint satisfaction
            char c = add(1, 1); // * Error
            // * No constraint satisfaction
            int n = add(1, new Class()); // * Error
            // * Constraint inheritance
            char s = inc('a'); // * Error
            // * Constraint composition
            int d = incrementalConstraint('a', 3.3); // * Error
            incrementalConstraint('a', "hello"); // * Error
        }

        public static void wrongTestUnaryConstraints() {
            unaryOperations(3, '4', 5.6, "wrong"); // * Error
        }

        public static void wrongTestLogicConstraints() {
            logicOperations(3, '4'); // * Error
            int n = logicOperations(false, true); // * Error
        }

        public static void wrongTestRelationalConstraints() {
            relationalOperations(4, "6.7"); // * Error
            int b = relationalOperations('3', 3); // * Error
        }

        public static void wrongTestBitwiseConstraints() {
            bitwiseOperations(4, true); // * Error
            char c = bitwiseOperations('a', 'b'); // * Error
        }

        public static void wrongTestShiftConstraints() {
            shiftOperations('4', 3.3); // * Error
            shiftOperations(true, 1); // * Error
            char c = shiftOperations('4', '3'); // * Error
        }

    }

    public class Run {
        public static void Main() {
        }
    }

}


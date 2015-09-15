using System;

namespace CG.Var.CommonOperatorConstraints {

    public class Test {
        // * Generates a constraint:
        //   var0 x var1 -> var3 | var3 := var0 + var1
        public static var Add(var op1, var op2) { 
            return op1 + op2; 
        }

        // * The inherited constraints of "Add" are included into the "inc" method
        //   var4 -> var5 | var5 := var4 + int
        public static var inc(var param) {
            return Add(param, 1);
        }

        // * The constraint is a composition of three constraints
        //   var6 x var7 -> var27 | var22 := var6 + int, var23 := var22 * var7, var27 := var22 + var23
        public static var incrementalConstraint(var param1, var param2) {
            var temp1 = inc(param1);
            var temp2 = temp1 * param2;
            return Add(temp1, temp2);
        }

        public static void testArithmeticConstraint() {
            // * Constraint satisfaction
            int n = Add(1,1);
            // * Constraint inheritance
            int s = inc('a');
            // * Constraint composition
            Double d = incrementalConstraint('a', 3.3);
        }

        // * Unary arithmetic constraints
        //   var10 x var11 x var(12) x var(13) -> void  |   var55:=va10++, var45:=-var11, var57:=+var12,  var58:=--var13
        public static void unaryOperations(var p1, var p2, var p3, var p4) {
            p1++; 
            p2 = (+p2);
            p3 = (-p3);
            --p4; 
			String result =  p1.ToString() + " " + p2.ToString()  + " " +  p3.ToString() + " " + p4.ToString();;
			Console.WriteLine(result);
        }

        public static void testUnaryConstraints() {
            unaryOperations(3, '4', -5.6, 0);
        }

        // * Logic constraints
        //   var23 x var24 -> bool   |  var23 <= bool,  var24 <= bool
        public static var logicOperations(var p1, var p2) {
            return p1 && p2 || !p2;
        }

        public static void testLogicConstraints() {
            logicOperations(true, false);
            bool b = logicOperations(true, false);
        }

        // * Relational constraints
        //   var26 x var27 -> bool  | var137:=Var26==var27, var138:=var26>=var27, var137<=bool,  var138<=bool, 
        //                            var141:=Var26<var27, var142:=var26!=var27, var142<=bool, var141<=bool
        public static var relationalOperations(var p1, var p2) {
            return p1 == p2 && p1 >= p2 || p1 < p2 && !(p1 != p2);
        }

        public static void testRelationalConstraints() {
            relationalOperations(4, 6.7);
            bool b = relationalOperations('3', 3);
        }

        // * Bitwise constraints
        //   var29 x var30 -> var171   |   var167:=var29&var30, var29<=int, var169:=var30&int, var170:=var29^var169, var171:=var167|var170
        public static var bitwiseOperations(var p1, var p2) {
            return p1 & p2 | p1 ^ p2 & ~p1;
        }

        public static void testBitwiseConstraints() {            
            int n = bitwiseOperations(4, 6);
        }

        // * Shift constraints
        //   var32 x var33 -> int   |   var32 <= int, var33 <= int
        public static var shiftOperations(var p1, var p2) {
            return p1<<p2 | p1>>p2;
        }

        public static void testShiftConstraints() {
            int n= shiftOperations(4, 3);            
        }

        public static void Main() {
			Test.testArithmeticConstraint();
			Test.testUnaryConstraints();
			Test.testLogicConstraints();
			Test.testRelationalConstraints();
			Test.testBitwiseConstraints();
			Test.testShiftConstraints();
			Console.WriteLine("Sucessful!!");
        }    
    }
}
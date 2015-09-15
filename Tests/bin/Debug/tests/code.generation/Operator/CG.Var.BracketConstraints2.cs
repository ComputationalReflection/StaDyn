using System;

namespace CG.Var.BracketConstraints 
{
    public class Test 
	{
		// * Generates a constraint:
        //   var1 x var2 -> var15  |   var15:=var1[var2], var2<=int
        public static var Access(var op1, var op2) { 
            return op1[op2]; 
        }

        // * The inherited constraints of "add" are included into the "inc" method
        //   var4 -> var17    |   var17:=var4[int]
        public static var First(var param) {
            return param[0];
        }

        // * The constraint is a composition of three constraints
        //   var6 -> var31  | var24:=var6[int], var31:=var24[int]
        public static var IncrementalConstraint(var param) {
            return Test.First(Test.First(param));
        }
		
		public static void TestArithmeticConstraint() {
			var varArray = new var[10];									
			varArray[0] = "string";					
			varArray[1] = 'c';				
			// * Constraint satisfaction
			var c = Test.Access(varArray,0);			
			Console.WriteLine("The first element is: " + c.ToString());						
			// * Constraint inheritance
			var s = Test.First(varArray);
			Console.WriteLine("The second element is: " + s.ToString());
			// * Constraint composition
			var bidimensionalArray = new var[10];
			bidimensionalArray[0] = varArray;
			var r = Test.IncrementalConstraint(bidimensionalArray);
			Console.WriteLine("The first element of the first element is: " + r.ToString());			
        }
		
        public static void Main() {		
			Test.TestArithmeticConstraint();
        }    
    }
}
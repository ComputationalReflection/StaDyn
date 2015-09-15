using System;

namespace Pybench.Aritmethic
{

	public class Paybench
	{		
		public static void Main()
        {				
			Test test = new SimpleIntegerArithmetic();
			test.test();
			test = new SimpleFloatArithmetic();
			test.test();
			test = new SimpleIntFloatArithmetic();
			test.test();			
			Console.WriteLine("Pybench completed!!");		
        }	
	}
	
	public abstract class Test {
        public abstract void test();
    }

    public class SimpleIntegerArithmetic : Test 
	{				
        public override void test() 
		{
			var a = 2;
			var b = 3;
			var c = 3;
            for (var i = 0; i < 120000; i++)
            {				
                c = a + b;
                c = b + c;
                c = c + a;
                c = a + b;
                c = b + c;

                c = c - a;
                c = a - b;
                c = b - c;
                c = c - a;
                c = b - c;

                c = a / b;
                c = b / a;
                c = c / b;

                c = a * b;
                c = b * a;
                c = c * b;

                c = a / b;
                c = b / a;
                c = c / b;

                a = 2;
                b = 3;
                c = 3;

                c = a + b;
                c = b + c;
                c = c + a;
                c = a + b;
                c = b + c;

                c = c - a;
                c = a - b;
                c = b - c;
                c = c - a;
                c = b - c;

                c = a / b;
                c = b / a;
                c = c / b;

                c = a * b;
                c = b * a;
                c = c * b;

                c = a / b;
                c = b / a;
                c = c / b;

                a = 2;
                b = 3;
                c = 3;

                c = a + b;
                c = b + c;
                c = c + a;
                c = a + b;
                c = b + c;

                c = c - a;
                c = a - b;
                c = b - c;
                c = c - a;
                c = b - c;

                c = a / b;
                c = b / a;
                c = c / b;

                c = a * b;
                c = b * a;
                c = c * b;

                c = a / b;
                c = b / a;
                c = c / b;

                a = 2;
                b = 3;
                c = 3;

                c = a + b;
                c = b + c;
                c = c + a;
                c = a + b;
                c = b + c;

                c = c - a;
                c = a - b;
                c = b - c;
                c = c - a;
                c = b - c;

                c = a / b;
                c = b / a;
                c = c / b;

                c = a * b;
                c = b * a;
                c = c * b;

                c = a / b;
                c = b / a;
                c = c / b;

                a = 2;
                b = 3;
                c = 3;

                c = a + b;
                c = b + c;
                c = c + a;
                c = a + b;
                c = b + c;

                c = c - a;
                c = a - b;
                c = b - c;
                c = c - a;
                c = b - c;

                c = a / b;
                c = b / a;
                c = c / b;

                c = a * b;
                c = b * a;
                c = c * b;

                c = a / b;
                c = b / a;
                c = c / b;
            }
        }				
    }
	
	public class SimpleFloatArithmetic : Test 
	{		
        public override void test() 
		{
			var a = 2.1;
			var b = 3.3332;
			var c = 3.14159;

            for (var i = 0; i < 120000; i++)
            {
                c = a + b;
                c = b + c;
                c = c + a;
                c = a + b;
                c = b + c;

                c = c - a;
                c = a - b;
                c = b - c;
                c = c - a;
                c = b - c;

                c = a / b;
                c = b / a;
                c = c / b;

                c = a * b;
                c = b * a;
                c = c * b;

                c = a / b;
                c = b / a;
                c = c / b;

                a = 2.1;
                b = 3.3332;
                c = 3.14159;

                c = a + b;
                c = b + c;
                c = c + a;
                c = a + b;
                c = b + c;

                c = c - a;
                c = a - b;
                c = b - c;
                c = c - a;
                c = b - c;

                c = a / b;
                c = b / a;
                c = c / b;

                c = a * b;
                c = b * a;
                c = c * b;

                c = a / b;
                c = b / a;
                c = c / b;

                a = 2.1;
                b = 3.3332;
                c = 3.14159;

                c = a + b;
                c = b + c;
                c = c + a;
                c = a + b;
                c = b + c;

                c = c - a;
                c = a - b;
                c = b - c;
                c = c - a;
                c = b - c;

                c = a / b;
                c = b / a;
                c = c / b;

                c = a * b;
                c = b * a;
                c = c * b;

                c = a / b;
                c = b / a;
                c = c / b;

                a = 2.1;
                b = 3.3332;
                c = 3.14159;

                c = a + b;
                c = b + c;
                c = c + a;
                c = a + b;
                c = b + c;

                c = c - a;
                c = a - b;
                c = b - c;
                c = c - a;
                c = b - c;

                c = a / b;
                c = b / a;
                c = c / b;

                c = a * b;
                c = b * a;
                c = c * b;

                c = a / b;
                c = b / a;
                c = c / b;

                a = 2.1;
                b = 3.3332;
                c = 3.14159;

                c = a + b;
                c = b + c;
                c = c + a;
                c = a + b;
                c = b + c;

                c = c - a;
                c = a - b;
                c = b - c;
                c = c - a;
                c = b - c;

                c = a / b;
                c = b / a;
                c = c / b;

                c = a * b;
                c = b * a;
                c = c * b;

                c = a / b;
                c = b / a;
                c = c / b;
            }
        }
    }

	public class SimpleIntFloatArithmetic : Test 
	{        
        public override void test() 
		{
			var a = 2;
			var b = 3;
			var c = 3.14159;
            for (var i = 0; i < 120000; i++)
            {
                c = a + b;
                c = b + c;
                c = c + a;
                c = a + b;
                c = b + c;

                c = c - a;
                c = a - b;
                c = b - c;
                c = c - a;
                c = b - c;

                c = a / b;
                c = b / a;
                c = c / b;

                c = a * b;
                c = b * a;
                c = c * b;

                c = a / b;
                c = b / a;
                c = c / b;

                a = 2;
                b = 3;
                c = 3.14159;

                c = a + b;
                c = b + c;
                c = c + a;
                c = a + b;
                c = b + c;

                c = c - a;
                c = a - b;
                c = b - c;
                c = c - a;
                c = b - c;

                c = a / b;
                c = b / a;
                c = c / b;

                c = a * b;
                c = b * a;
                c = c * b;

                c = a / b;
                c = b / a;
                c = c / b;

                a = 2;
                b = 3;
                c = 3.14159;

                c = a + b;
                c = b + c;
                c = c + a;
                c = a + b;
                c = b + c;

                c = c - a;
                c = a - b;
                c = b - c;
                c = c - a;
                c = b - c;

                c = a / b;
                c = b / a;
                c = c / b;

                c = a * b;
                c = b * a;
                c = c * b;

                c = a / b;
                c = b / a;
                c = c / b;

                a = 2;
                b = 3;
                c = 3.14159;

                c = a + b;
                c = b + c;
                c = c + a;
                c = a + b;
                c = b + c;

                c = c - a;
                c = a - b;
                c = b - c;
                c = c - a;
                c = b - c;

                c = a / b;
                c = b / a;
                c = c / b;

                c = a * b;
                c = b * a;
                c = c * b;

                c = a / b;
                c = b / a;
                c = c / b;

                a = 2;
                b = 3;
                c = 3.14159;

                c = a + b;
                c = b + c;
                c = c + a;
                c = a + b;
                c = b + c;

                c = c - a;
                c = a - b;
                c = b - c;
                c = c - a;
                c = b - c;

                c = a / b;
                c = b / a;
                c = c / b;

                c = a * b;
                c = b * a;
                c = c * b;

                c = a / b;
                c = b / a;
                c = c / b;
            }
        }
    }
}
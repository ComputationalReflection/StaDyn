using System;

namespace Pybench.Numbers
{
	public class Pybench
	{		
		public static void Main()
        {				
			Test test = new CompareIntegers();
			test.test();			
			test = new CompareFloats();
			test.test();		
			test = new CompareFloatsIntegers();
			test.test();
			Console.WriteLine("Pybench completed!!");		
        }	
	}
	
	public abstract class Test 
	{
        public abstract void test();
    }
	
	public class CompareIntegers : Test 
	{
        public override void test() 
		{
            var b;
            var two = 2;
            var three = 3;            
            for (var i = 0; i < 120000; i++)
            {
                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;
            }
        }
    }
	
	public class CompareFloats : Test 
	{
        public override void test() 
		{
            var b;
            var two = 2.1;
            var three = 3.31;
            for (var i = 0; i < 80000; i++)
            {
                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;

                b = two < three;
                b = two > three;
                b = two == three;
                b = two > three;
                b = two < three;
            }
        }
    }
	
	public class CompareFloatsIntegers : Test 
	{
        public override void test() 
		{
            var b;
            var two = 2.1;
            var four = 4;
            for (var i = 0; i < 60000; i++)
            {
                b = two < four;
                b = two > four;
                b = two == four;
                b = two > four;
                b = two < four;

                b = two < four;
                b = two > four;
                b = two == four;
                b = two > four;
                b = two < four;

                b = two < four;
                b = two > four;
                b = two == four;
                b = two > four;
                b = two < four;

                b = two < four;
                b = two > four;
                b = two == four;
                b = two > four;
                b = two < four;

                b = two < four;
                b = two > four;
                b = two == four;
                b = two > four;
                b = two < four;

                b = two < four;
                b = two > four;
                b = two == four;
                b = two > four;
                b = two < four;

                b = two < four;
                b = two > four;
                b = two == four;
                b = two > four;
                b = two < four;

                b = two < four;
                b = two > four;
                b = two == four;
                b = two > four;
                b = two < four;

                b = two < four;
                b = two > four;
                b = two == four;
                b = two > four;
                b = two < four;

                b = two < four;
                b = two > four;
                b = two == four;
                b = two > four;
                b = two < four;

                b = two < four;
                b = two > four;
                b = two == four;
                b = two > four;
                b = two < four;

                b = two < four;
                b = two > four;
                b = two == four;
                b = two > four;
                b = two < four;

                b = two < four;
                b = two > four;
                b = two == four;
                b = two > four;
                b = two < four;

                b = two < four;
                b = two > four;
                b = two == four;
                b = two > four;
                b = two < four;

                b = two < four;
                b = two > four;
                b = two == four;
                b = two > four;
                b = two < four;

                b = two < four;
                b = two > four;
                b = two == four;
                b = two > four;
                b = two < four;

                b = two < four;
                b = two > four;
                b = two == four;
                b = two > four;
                b = two < four;

                b = two < four;
                b = two > four;
                b = two == four;
                b = two > four;
                b = two < four;

                b = two < four;
                b = two > four;
                b = two == four;
                b = two > four;
                b = two < four;

                b = two < four;
                b = two > four;
                b = two == four;
                b = two > four;
                b = two < four;

                b = two < four;
                b = two > four;
                b = two == four;
                b = two > four;
                b = two < four;

                b = two < four;
                b = two > four;
                b = two == four;
                b = two > four;
                b = two < four;

                b = two < four;
                b = two > four;
                b = two == four;
                b = two > four;
                b = two < four;

                b = two < four;
                b = two > four;
                b = two == four;
                b = two > four;
                b = two < four;

                b = two < four;
                b = two > four;
                b = two == four;
                b = two > four;
                b = two < four;

                b = two < four;
                b = two > four;
                b = two == four;
                b = two > four;
                b = two < four;

                b = two < four;
                b = two > four;
                b = two == four;
                b = two > four;
                b = two < four;

                b = two < four;
                b = two > four;
                b = two == four;
                b = two > four;
                b = two < four;

                b = two < four;
                b = two > four;
                b = two == four;
                b = two > four;
                b = two < four;

                b = two < four;
                b = two > four;
                b = two == four;
                b = two > four;
                b = two < four;
            }
        }
    }
}
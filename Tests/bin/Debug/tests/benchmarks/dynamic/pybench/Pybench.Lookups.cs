using System;

namespace Pybench.Lookups
{
	public class Pybench
	{		
		public static void Main()
        {				
			Test test = new NormalClassAttribute();
			test.test();			
			test = new NormalInstanceAttribute();
			test.test();						
			Console.WriteLine("Pybench completed!!");		
        }	
	}
	
	public abstract class Test 
	{
        public abstract void test();
    }
	
	public class C 
	{
        public static var a;
        public static var b;
        public static var c;
    }
	
	public class NormalClassAttribute : Test 
	{
        public override void test() 
		{
            var x;
            var two = 2;
			var three = 3;
			var four = 4;
			
            for (var i = 0; i < 100000; i++)
            {
                C.a = two;
                C.b = three;
                C.c = four;

                C.a = two;
                C.b = three;
                C.c = four;

                C.a = two;
                C.b = three;
                C.c = four;

                C.a = two;
                C.b = three;
                C.c = four;

                x = C.a;
                x = C.b;
                x = C.c;

                x = C.a;
                x = C.b;
                x = C.c;

                x = C.a;
                x = C.b;
                x = C.c;

                x = C.a;
                x = C.b;
                x = C.c;

                C.a = two;
                C.b = three;
                C.c = four;

                C.a = two;
                C.b = three;
                C.c = four;

                C.a = two;
                C.b = three;
                C.c = four;

                C.a = two;
                C.b = three;
                C.c = four;

                x = C.a;
                x = C.b;
                x = C.c;

                x = C.a;
                x = C.b;
                x = C.c;

                x = C.a;
                x = C.b;
                x = C.c;

                x = C.a;
                x = C.b;
                x = C.c;

                C.a = two;
                C.b = three;
                C.c = four;

                C.a = two;
                C.b = three;
                C.c = four;

                C.a = two;
                C.b = three;
                C.c = four;

                C.a = two;
                C.b = three;
                C.c = four;

                x = C.a;
                x = C.b;
                x = C.c;

                x = C.a;
                x = C.b;
                x = C.c;

                x = C.a;
                x = C.b;
                x = C.c;

                x = C.a;
                x = C.b;
                x = C.c;

                C.a = two;
                C.b = three;
                C.c = four;

                C.a = two;
                C.b = three;
                C.c = four;

                C.a = two;
                C.b = three;
                C.c = four;

                C.a = two;
                C.b = three;
                C.c = four;

                x = C.a;
                x = C.b;
                x = C.c;

                x = C.a;
                x = C.b;
                x = C.c;

                x = C.a;
                x = C.b;
                x = C.c;

                x = C.a;
                x = C.b;
                x = C.c;

                C.a = two;
                C.b = three;
                C.c = four;

                C.a = two;
                C.b = three;
                C.c = four;

                C.a = two;
                C.b = three;
                C.c = four;

                C.a = two;
                C.b = three;
                C.c = four;

                x = C.a;
                x = C.b;
                x = C.c;

                x = C.a;
                x = C.b;
                x = C.c;

                x = C.a;
                x = C.b;
                x = C.c;

                x = C.a;
                x = C.b;
                x = C.c;
            }
        }
    }
	
	public class D 
	{
        public var a;
        public var b;
        public var c;

        public D(var a, var b, var c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }
    }
	
	public class NormalInstanceAttribute : Test 
	{
        public override void test() 
		{
            var o = new D(0, 0, 0);
            var x;
			
			var two = 2;
			var three = 3;
			var four = 4;

            for (var i = 0; i < 100000; i++)
            {

                o.a = two;
                o.b = three;
                o.c = four;

                o.a = two;
                o.b = three;
                o.c = four;

                o.a = two;
                o.b = three;
                o.c = four;

                o.a = two;
                o.b = three;
                o.c = four;

                x = o.a;
                x = o.b;
                x = o.c;

                x = o.a;
                x = o.b;
                x = o.c;

                x = o.a;
                x = o.b;
                x = o.c;

                x = o.a;
                x = o.b;
                x = o.c;

                o.a = two;
                o.b = three;
                o.c = four;

                o.a = two;
                o.b = three;
                o.c = four;

                o.a = two;
                o.b = three;
                o.c = four;

                o.a = two;
                o.b = three;
                o.c = four;

                x = o.a;
                x = o.b;
                x = o.c;

                x = o.a;
                x = o.b;
                x = o.c;

                x = o.a;
                x = o.b;
                x = o.c;

                x = o.a;
                x = o.b;
                x = o.c;

                o.a = two;
                o.b = three;
                o.c = four;

                o.a = two;
                o.b = three;
                o.c = four;

                o.a = two;
                o.b = three;
                o.c = four;

                o.a = two;
                o.b = three;
                o.c = four;

                x = o.a;
                x = o.b;
                x = o.c;

                x = o.a;
                x = o.b;
                x = o.c;

                x = o.a;
                x = o.b;
                x = o.c;

                x = o.a;
                x = o.b;
                x = o.c;

                o.a = two;
                o.b = three;
                o.c = four;

                o.a = two;
                o.b = three;
                o.c = four;

                o.a = two;
                o.b = three;
                o.c = four;

                o.a = two;
                o.b = three;
                o.c = four;

                x = o.a;
                x = o.b;
                x = o.c;

                x = o.a;
                x = o.b;
                x = o.c;

                x = o.a;
                x = o.b;
                x = o.c;

                x = o.a;
                x = o.b;
                x = o.c;

                o.a = two;
                o.b = three;
                o.c = four;

                o.a = two;
                o.b = three;
                o.c = four;

                o.a = two;
                o.b = three;
                o.c = four;

                o.a = two;
                o.b = three;
                o.c = four;

                x = o.a;
                x = o.b;
                x = o.c;

                x = o.a;
                x = o.b;
                x = o.c;

                x = o.a;
                x = o.b;
                x = o.c;

                x = o.a;
                x = o.b;
                x = o.c;
            }
        }
    }
}
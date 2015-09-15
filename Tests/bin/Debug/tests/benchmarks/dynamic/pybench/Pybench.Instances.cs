using System;

namespace Pybench.Instances
{
	public class Pybench
	{		
		public static void Main()
        {				
			Test test = new CreateInstances();
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

    public class E 
	{
        public var a;
        public var b;
        public var c;
        public var d;
        public var e;
        public var f;

        public E(var a, var b, var c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = a;
            this.e = b;
            this.f = c;
        }
    }
	
	public class CreateInstances : Test 
	{
        public override void test() 
		{
            var i = 0;
						
			var three = 3;
            while (i < 80000)
			{
                var n;
                var o = new C();
                var o1 = new C();
                var o2 = new C();
                n = three;
                var p = new D(i, i, n);
                var p1 = new D(i, i, n);
                var p2 = new D(i, n, n);
                var p3 = new D(n, i, n);
                var p4 = new D(i, i, i);
                var p5 = new D(n, i, n);
                var p6 = new D(i, i, i);
                var q = new E(i, i, n);
                var q1 = new E(i, i, n);
                var q2 = new E(i, i, n);
                var zero = 0;
                var q3 = new E(i, i, zero);
				
				i++;
            }
        }
    }	
}
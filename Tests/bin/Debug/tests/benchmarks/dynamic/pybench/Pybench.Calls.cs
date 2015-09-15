using System;

namespace Pybench.Calls
{
	public class Pybench
	{		
		public static void Main()
        {				
			Test test = new PythonFunctionCalls();
			test.test();				
			test = new PythonMethodCalls();			
			test.test();
			test = new Recursion();			
			test.test();			
			Console.WriteLine("Pybench completed!!");		
        }	
	}
	
	public abstract class Test 
	{
        public abstract void test();
    }

    
    public class PythonFunctionCalls : Test 
	{		
        void f() { }

        void f1(var x) { }

        var g(var a, var b, var c)
        {
            return new var[] { a, b, c };
        }

        var h(var a, var b, var c, var d, var e, var f)
        {
            return new var[] { d, e, f };
        }
		
        public override void test() 
		{    
			var zero = 0;			
			var two = 2;
			var three = 3;				
            for (var i = 0; i < 60000; i++) 
			{				
                f();
                f1(i);
                f1(i);
                f1(i);
                f1(i);
                g(i, i, i);
                g(i, i, i);
                g(i, i, i);
                g(i, i, i);
                h(i, i, three, i, i, zero);
                h(i, i, i, two, i, three);

                f();
                f1(i);
                f1(i);
                f1(i);
                f1(i);
                g(i, i, i);
                g(i, i, i);
                g(i, i, i);
                g(i, i, i);
                h(i, i, three, i, i, zero);
                h(i, i, i, two, i, three);

                f();
                f1(i);
                f1(i);
                f1(i);
                f1(i);
                g(i, i, i);
                g(i, i, i);
                g(i, i, i);
                g(i, i, i);
                h(i, i, three, i, i, zero);
                h(i, i, i, two, i, three);

                f();
                f1(i);
                f1(i);
                f1(i);
                f1(i);
                g(i, i, i);
                g(i, i, i);
                g(i, i, i);
                g(i, i, i);
                h(i, i, three, i, i, zero);
                h(i, i, i, two, i, three);

                f();
                f1(i);
                f1(i);
                f1(i);
                f1(i);
                g(i, i, i);
                g(i, i, i);
                g(i, i, i);
                g(i, i, i);
                h(i, i, three, i, i, zero);
                h(i, i, i, two, i, three);
            }
        }
    }
	
	public class C 
	{
        var x = 2;
        var y;
        var t;
        var s = "string";

        public var f()
        {
            return this.x;
        }

        public var j(var a, var b)
        {
            this.y = a;
            this.t = b;
            return this.y;
        }

        public void k(var a, var b, var c)
        {
            this.y = a;
            this.s = b.ToString();
            this.t = c;
        }
    }
	
	public class PythonMethodCalls : Test 
	{		
        public override void test() 
		{
            var o = new C();
            var two = 2;
            var three = 3;
            var four = 4;
            for (var i = 0; i < 30000; i++)
            {
                o.f();
                o.f();
                o.f();
                o.f();
                o.f();
                o.f();
                o.j(i, i);
                o.j(i, i);
                o.j(i, two);
                o.j(i, two);
                o.j(two, two);
                o.k(i, i, three);
                o.k(i, two, three);
                o.k(i, two, three);
                o.k(i, i, four);

                o.f();
                o.f();
                o.f();
                o.f();
                o.f();
                o.f();
                o.j(i, i);
                o.j(i, i);
                o.j(i, two);
                o.j(i, two);
                o.j(two, two);
                o.k(i, i, three);
                o.k(i, two, three);
                o.k(i, two, three);
                o.k(i, i, four);

                o.f();
                o.f();
                o.f();
                o.f();
                o.f();
                o.f();
                o.j(i, i);
                o.j(i, i);
                o.j(i, two);
                o.j(i, two);
                o.j(two, two);
                o.k(i, i, three);
                o.k(i, two, three);
                o.k(i, two, three);
                o.k(i, i, four);

                o.f();
                o.f();
                o.f();
                o.f();
                o.f();
                o.f();
                o.j(i, i);
                o.j(i, i);
                o.j(i, two);
                o.j(i, two);
                o.j(two, two);
                o.k(i, i, three);
                o.k(i, two, three);
                o.k(i, two, three);
                o.k(i, i, four);

                o.f();
                o.f();
                o.f();
                o.f();
                o.f();
                o.f();
                o.j(i, i);
                o.j(i, i);
                o.j(i, two);
                o.j(i, two);
                o.j(two, two);
                o.k(i, i, three);
                o.k(i, two, three);
                o.k(i, two, three);
                o.k(i, i, four);
            }
        }
    }
	
	public class Recursion : Test 
	{		
        public var f(var x)
        {
            if (x > 1)
                return f(x - 1);
            return 1;
        }
		
        public override void test() 
		{
            for (var i = 0; i < 100000; i++)
            {
                f(10);
                f(10);
                f(10);
                f(10);
                f(10);
            }
        }
    }
}
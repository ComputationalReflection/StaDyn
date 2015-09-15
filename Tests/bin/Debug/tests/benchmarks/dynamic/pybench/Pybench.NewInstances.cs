using System;

namespace Pybench.NewInstances
{	
	public class Pybench
	{		
		public static void Main()
        {		
			Test test = new CreateNewInstances();
			test.test();		
			Console.WriteLine("Pybench.NewInstances completed!!");
        }	
	}
	
	public abstract class Test 
	{
        public abstract void test();
    }
	
	public class Root
	{
	    public var a;
		
		public Root(){}
		
		public Root(var a)
		{
			this.a = a;
		}
	}

	public class C:Root
	{        		
        public static var b;
        public static var c;				
    }

    public class D:Root
	{        
        public var b;
        public var c;

        public D(var a, var b, var c):base(a)
        {            
            this.b = b;
            this.c = c;
        }
    }

    public class E:Root
	{        
        public var b;
        public var c;
        public var d;
        public var e;
        public var f;

        public E(var a, var b, var c):base(a)
        {            
            this.b = b;
            this.c = c;
            this.d = a;
            this.e = b;
            this.f = c;
        }
    }
	
	public class CreateNewInstances : Test 
	{
        public override void test() 
		{					            
			var three = 3;
			var four = 4;
            for (var i = 0 ; i < 800000; i = i + 1)
			{                				
                var o = new C();
                var o1 = new C();
                var o2 = new C();
                var p = new D(i, i, three);
                var p1 = new D(i, i, three);
                var p2 = new D(i, three, three);
                var p3 = new D(three, i, three);
                var p4 = new D(i, i, i);
                var p5 = new D(three, i, three);
                var p6 = new D(i, i, i);
                var q = new E(i, i, three);
                var q1 = new E(i, i, three);
                var q2 = new E(i, i, three);                
                var q3 = new E(i, i, four);				
            }        
        }
    }	
}
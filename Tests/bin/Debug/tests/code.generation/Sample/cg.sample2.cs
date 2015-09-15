using System;

namespace CG.Sample 
{
	public class A 
	{
		public void m() { 
			Console.WriteLine("A:m()");
		}
	}
    public class B 
	{
	    public void m1() { 
			Console.WriteLine("B:m1()");
		}   
	}
	public class C 
	{
        public void m() {
			Console.WriteLine("C:m()");		
		}
    }

    public class Run 
	{
		public static void Main() 
		{
			var obj;
			int n = 2;
			switch (n) {
				case 1: obj = new A(); break;
				case 2: obj = new B(); break;
				case 3: obj = new C(); break;
			}
			obj.m1();			 
        }
     }
}

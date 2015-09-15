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

    public class Test
	{
		public static void Main() 
		{
			var reference;
			if (true)
			{				
				reference = new A();
			}
			else
			{				
				reference = new B();             			
			}
			reference.m();			
        }
     }
}

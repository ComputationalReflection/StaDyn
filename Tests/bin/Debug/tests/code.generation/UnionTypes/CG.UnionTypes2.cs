using System;

namespace CG.UnionTypes
{
	public class A  {
		public void m() { }
	}

	public class B {
		public void m() { }
	}

	public class C {
    }

	public class Test 
	{
		public static void Main() { 			 
			var referencia;
			if (true)
				referencia = new A();
			else if (true)
				referencia = new B();
			else
				referencia=new C();
			referencia.m();   
			Console.WriteLine("Successfull!!");
		}
	}
}

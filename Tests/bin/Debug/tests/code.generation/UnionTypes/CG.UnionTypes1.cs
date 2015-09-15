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

	public  class Test {
		public static void m1() {
			var obj;
			if (true) {
				obj = new A();
			}
			else {
				obj = new B();
			}
			obj.m();
		}


		public static void m2() {
			var obj;			
			int n=1;
			switch(n) {
				case 1: obj = new A(); break;
				case 2: obj= new B(); break;
				default: obj= new C(); ;
			}
			obj.m();
		}

		public static void Main() { 
			Test.m1();
			Test.m2();
			Console.WriteLine("Successfull!!");
		}
	}
}

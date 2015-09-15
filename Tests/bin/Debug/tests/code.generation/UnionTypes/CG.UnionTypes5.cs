using System;

namespace UnionTypes
{
    public class A {
        public void m() { }
    }

    public class B {
        public void m() { }
    }
    
	public class Test
	{     
		public static  void Main()
		{
           var reference;
           int n = 1;
           switch (n) {
               case 1: reference = new A(); break;
               case 2: reference = new B(); break;
               default: reference = "hello"; break;
           }
           reference.m();
		   System.Console.WriteLine("Sucessfull!!");
		}
	}
}
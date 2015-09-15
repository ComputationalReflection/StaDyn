using System;
using System.IO;

namespace CG.UnionTypes
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
           var obj;
           int n = 2;
		   System.Console.WriteLine(n);
           switch (n) {
				case 1: obj = new A(); break;
				case 2: obj = new B(); break;
				default: obj = "hello"; break;
           }
           obj.m();
		}
	}
}
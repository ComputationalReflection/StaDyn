using System;

namespace CG.Sample 
{
	public class A {
		public void m() { 
			Console.WriteLine("A::m");  
		}
		public override String ToString()
		{
			return "Class A";
		}
	}

    public class B {
		public override String ToString()
		{
			return "Class B";
		}
    }

    public class Run {
        public static void f(var obj) {
			String s = obj.ToString();
			Console.WriteLine(s);
            obj.m();
        }

        public static void Main() {
            var obj = new B();
            if (true)
                obj = new A();
            Run.f(obj);
			Console.WriteLine("Successfull!!");
        }
    }
}


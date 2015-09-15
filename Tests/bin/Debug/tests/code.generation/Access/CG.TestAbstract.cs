using System;
namespace CG.TestAbstract
{
	public class MyClass{
		public int attr;
		public MyClass(){
			this.attr = 5;
		}
	}
	
	public class Test{
		public static void Main() {
			MyClass myClass = new MyClass();
			object attr = myClass.GetType().GetField("attr").GetValue(myClass);
			Console.WriteLine(attr);
		}
	}
}
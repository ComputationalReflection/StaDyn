using System;

namespace Testing
{
	public class ClassA{
	}
	
	public class ClassB{
	}

	public class VarsUnified
	{
		public var LocalVariable(bool cond)
		{
			var generic;
			if (cond)
				generic = 3.3;
			else
				generic = 9;
			return generic;
		}

		public var LocalVariable2(bool cond)
		{
			var generic;
			if (cond)
				generic = 9;
			else
				generic = "Hello";
			return generic;
		}

		public var LocalVariable3(bool cond)
		{
			var generic;
			if (cond)
				generic = new ClassA();
			else
				generic = new ClassB();
			return generic;
		}

	   public static void Main()
	   {
			VarsUnified vu = new VarsUnified();
			if (vu.LocalVariable(true) != 3.3)
				Environment.Exit(-1);
			if (vu.LocalVariable(false) != 9)
				Environment.Exit(-1);
			if (vu.LocalVariable2(true) != 9)
				Environment.Exit(-1);
			if (!(vu.LocalVariable2(false).Equals("Hello")))
				Environment.Exit(-1);
			if (!(vu.LocalVariable3(true) is ClassA))
				Environment.Exit(-1);
			if (!(vu.LocalVariable3(false) is ClassB))
				Environment.Exit(-1);
			Console.WriteLine("Successfull!!");
	   }
	}
}
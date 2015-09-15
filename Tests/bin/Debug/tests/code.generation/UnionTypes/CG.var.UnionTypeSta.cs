using System;

namespace Testing
{
	public class ClassA{
	}
	
	public class ClassB{
	}

	public class VarsUnified
	{
		public void LocalVariable(bool cond)
		{
			var generic;
			if (cond)
				generic = 3.3;
			else
				generic = 9;
			Console.WriteLine(generic);
		}

		public void LocalVariable2(bool cond)
		{
			var generic;
			if (cond)
				generic = 9;
			else
				generic = "Hello";
			Console.WriteLine(generic);
		}

		public void LocalVariable3(bool cond)
		{
			var generic;
			if (cond)
				generic = new ClassA();
			else
				generic = new ClassB();
			Console.WriteLine(generic);
		}

		public static void Main()
		{
			VarsUnified vu = new VarsUnified();
			vu.LocalVariable(true);
			vu.LocalVariable(false);
			vu.LocalVariable2(true);
			vu.LocalVariable2(false);
			vu.LocalVariable3(true);
			vu.LocalVariable3(false);
			Console.WriteLine("Successfull!!");
		}
	}
}
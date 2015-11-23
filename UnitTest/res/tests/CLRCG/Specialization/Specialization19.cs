using System;

namespace ProgramSpecialization
{	
	public class SuperClass
	{				
		public var SuperClassMethod()
		{
			return "SuperClass";
		}
	}
	
	public class DerivedClass:SuperClass
	{		
		public var DerivedClassMethod(String param)
		{
			return base.SuperClassMethod();
		}		
	}
	
    public class Program
    {		
		public static void Main()
        {		
			DerivedClass dc = new DerivedClass();
			var result = dc.DerivedClassMethod("Hello");
			System.Console.WriteLine("Result {0}", result.ToString());
        }
	}
}
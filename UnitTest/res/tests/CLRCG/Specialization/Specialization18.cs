using System;

namespace ProgramSpecialization
{	
	public class A
	{				
		public var Method(var param)
		{
			return param;
		}
	}
		
    public class Program
    {		
		public static void Main()
        {		
			A a = new A();
			var result = a.Method("String param");
			System.Console.WriteLine("Result {0}", result.ToString());
        }
	}
}
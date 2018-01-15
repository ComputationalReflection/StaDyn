using System;

namespace ProgramSpecialization
{	
	public class Program 
	{
		public static var inc(var x){ return x + 1; }
		public static void Main() 
		{			
			var paramA;
			if(true)
				paramA = 1;
			else
				paramA = 1.1;							
			var resultA = inc(paramA);
			System.Console.WriteLine("Result {0}", resultA.ToString());
			
			var paramB;
			if(true)
				paramB = 1.1;							
			else				
				paramB = 1;
			var resultB = inc(paramB);
			System.Console.WriteLine("Result {0}", resultB.ToString());
		}
    }
}
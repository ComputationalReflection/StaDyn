using System;

namespace ProgramSpecialization
{	
	public class Program 
	{
		public static var inc(var x){ return x + 1; }
		public static void Main(string[] args) 
		{			
			var arg;
			if(true)
				arg = 1;
			else
				arg = 1.1;							
			var result = inc(arg);
			System.Console.WriteLine("Result {0}", result.ToString());
		}
    }
}
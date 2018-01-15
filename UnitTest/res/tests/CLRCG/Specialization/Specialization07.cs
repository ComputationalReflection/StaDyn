using System;

namespace ProgramSpecialization
{	
	public class Program 
	{
		public static var inc(var x,var y){ return x + y; }
		public static void Main(string[] args) 
		{			
			var arg;
			if(false)
				arg = 1;
			else
				arg = 1.1;										
			var result = inc(arg,arg);
			System.Console.WriteLine("Result {0}", result.ToString());
		}
    }
}
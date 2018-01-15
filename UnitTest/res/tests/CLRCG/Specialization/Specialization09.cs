using System;

namespace ProgramSpecialization
{	
	public class Program 
	{
		public static var inc(var x,var y,var z){ return x + y + z; }
		public static void Main(string[] args) 
		{			
			var argX;
			if(false)
				argX = 1;
			else
				argX = 1.1;	
			var argY;
			if(false)
				argY = 1;
			else
				argY = 1.1;
			var argZ = 1; 						
			var result = inc(argX,argY,argZ);
			System.Console.WriteLine("Result {0}", result.ToString());
		}
    }
}
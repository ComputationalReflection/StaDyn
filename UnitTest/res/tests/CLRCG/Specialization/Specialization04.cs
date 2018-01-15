using System;

namespace ProgramSpecialization
{	
	public class Program 
	{
		public static var addition(var x, var y){ return x + y; }
		public static void Main(string[] args) 
		{
			int i = addition(3,4);		
			double d = addition(3.5,4.5);	
			double d2 = addition(3,4.5);	
			String s = addition("1","2");
			System.Console.WriteLine("Result {0} ",i);					
			System.Console.WriteLine("Result {0} ",d);					
			System.Console.WriteLine("Result {0} ",d2);					
			System.Console.WriteLine("Result {0} ",s);					
		}
    }
}
using System;

namespace ProgramSpecialization
{	
	public class Node
	{		
		public var data;		
		public Node()
		{
			this.data = "Node";
		}
		public var GetData()
		{
			return this.data;
		}
	}
	
    public class Program
    {		
		public static var GetData(var param) 
		{ 	
			var result = new Node();				
			return result.GetData() + param.GetData();
		}
				
        public static void Main()
        {		
			var result = GetData(new Node());
			System.Console.WriteLine("Result {0} ",result.ToString());					
        }
	}
}
using System;

namespace ProgramSpecialization
{	
	public class Node
	{		
		public var data;		
		public Node()
		{
			this.data = 1;
		}
	}
	
    public class Program
    {		
		public static var GetData(var x) 
		{ 	
			if(x is Node)
				return ((Node)x).data;			
			return x.data;				
		}
				
        public static void Main()
        {		
			Program program = new Program();
			var arg = new Node();			
            var result = GetData(arg);
			System.Console.WriteLine("Result {0} ",result.ToString());					
        }
	}
}
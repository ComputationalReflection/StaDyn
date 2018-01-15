using System;

namespace ProgramSpecialization
{	
	public class NodeA
	{		
		public var data;		
		public NodeA()
		{
			this.data = "NodeA";
		}
	}
	
	public class NodeB
	{
		public var data;		
		public NodeB()
		{
			this.data = "NodeB";
		}
	}
	
    public class Program
    {		
		public static var GetData(var x) 
		{ 						
			return x.data;
		}
				
        public static void Main()
        {					
			var args;
			if(true)
				args = new NodeA();
			else
				args = new NodeB();			
            var result = GetData(args);
			System.Console.WriteLine("Result {0} ",result.ToString());					
        }
	}
}
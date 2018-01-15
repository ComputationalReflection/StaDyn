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
		public var GetData()
		{
			return this.data;
		}
	}
	
	public class NodeB
	{
		public var data;		
		public NodeB()
		{
			this.data = "NodeB";
		}
		public var GetData()
		{
			return this.data;
		}
	}
	
    public class Program
    {		
		public static var GetParamData(var a, var b) 
		{ 	
			return a.GetData() + b.GetData();				
		}
				
        public static void Main()
        {		
			Program program = new Program();
			var paramA;
			if(true)
				paramA = new NodeA();
			else
				paramA = new NodeB();			
			
			var paramB;
			if(true)
				paramB = new NodeB();			
			else				
				paramB = new NodeA();
            object result = GetParamData(paramA,paramB);
			System.Console.WriteLine("Result {0} ",result.ToString());					
        }
	}
}
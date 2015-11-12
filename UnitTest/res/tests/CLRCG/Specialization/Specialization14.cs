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
		public static var GetParamData(var param) 
		{ 	
			return param.GetData();				
		}
				
        public static void Main()
        {		
			Program program = new Program();
			var param;
			if(true)
				param = new NodeA();
			else
				param = new NodeB();			
            object result = GetParamData(param);
			System.Console.WriteLine("Result {0} ",result.ToString());					
        }
	}
}
using System;

namespace UnionTypes
{
	public class Test
	{     	
		public static void Arithmetic(bool cond, int factor)
		{
			var local;
			if (cond)
				local = 2.2;
			else 
				local  = 5;      
			local *= 5;
			local *= factor;
			Console.WriteLine("Local: {0}", local);
		}
		
		public static  void Main()
		{
			Test.Arithmetic(true, 3);   // 33
			Test.Arithmetic(false, 3);  // 75
		}
	}
}

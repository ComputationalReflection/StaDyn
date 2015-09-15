using System;

namespace UnionTypes
{
	public class Test
	{     	
		public static void Arithmetic(bool cond)
		{
			var x;
			if (cond) {
				x = 2.2;
			} else {
				x = 3;
			}
			x *= 3.2;
			Console.WriteLine("Local: {0}", x);
		}
		
		public static  void Main()
		{
			Test.Arithmetic(true);   // 7,04 
			Test.Arithmetic(false);   // 9,6
		}
	}
}
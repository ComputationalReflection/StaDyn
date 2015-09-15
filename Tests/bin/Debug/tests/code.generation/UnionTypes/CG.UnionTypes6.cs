using System;

namespace UnionTypes
{
	public class Test
	{     
		public static  void Main()
		{
			var referencia;
			if (true)
				referencia = "dfa";
			else
				referencia = 3;
			Console.WriteLine(referencia.ToString());
		}
	}
}

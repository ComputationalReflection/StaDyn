/*A port to StaDyn (dynamic) of the pystone benchmark using duck typing.*/

using System;

namespace StaDyn.Dynamic
{
	public class Idents
    {
        public static var Ident3 = 3;
    }

	public class Pystone
    {      		
		public static var Func3(var EnumParIn)
        {
            var EnumLoc;
            EnumLoc = EnumParIn;
            if (EnumLoc == Idents.Ident3) return true;
            return false;
        }
		
		public static void TestFunc3()
		{			
			var EnumParIn = 3;			
			var result = Pystone.Func3(EnumParIn);			
			if(result != true)
				Environment.Exit(-1);					
		}
		
        public static void Main()
        {	
			Pystone.Test();
			Console.WriteLine("Pystone completed!!");		
        }	
		
		public static void Test()
		{			
			Pystone.TestFunc3(); 
			Console.WriteLine("TestFunc3 completed!!");	
		}		
    }
}
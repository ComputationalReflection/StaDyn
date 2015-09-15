/*A port to StaDyn (dynamic) of the pystone benchmark using duck typing.*/

using System;

namespace StaDyn.Dynamic
{
	public class Pystone
    {
		public static var Proc7(var IntParI1, var IntParI2)
        {
            var IntParOut;
            var IntLoc;

            IntLoc = IntParI1 + 2;
            IntParOut = IntParI2 + IntLoc;
            return IntParOut;
        }
		
		public static void TestProc7()
		{			
			var IntParI1 = 2;
			var IntParI2 = 3;
			var result = Pystone.Proc7(IntParI1,IntParI2);			
			if(result != 7)
				Environment.Exit(-1);					
		}	
		
        public static void Main()
        {	
			Pystone.Test();
			Console.WriteLine("Pystone completed!!");		
        }	
		
		public static void Test()
		{			
			Pystone.TestProc7();
			Console.WriteLine("TestProc7 completed!!");			
		}		
    }
}
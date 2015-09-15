/*A port to StaDyn (dynamic) of the pystone benchmark using duck typing.*/

using System;

namespace StaDyn.Dynamic
{
	public class Pystone
    {
        public static var BoolGlob = false;
        public static var Char1Glob = '\0';
       
		public static void Proc5()
        {
            Char1Glob = 'A';
            BoolGlob = false;
        }
		
		public static void TestProc5()
		{			
			Pystone.Proc5();			
			if(Pystone.Char1Glob != 'A')
				Environment.Exit(-1);	
			if(Pystone.BoolGlob != false)
				Environment.Exit(-1);				
		}		
		
        public static void Main()
        {		
			Pystone.Test();
			Console.WriteLine("Pystone completed!!");		
        }	

		public static void Test()
		{
			Pystone.TestProc5();
			Console.WriteLine("TestProc5 completed!!");		
		}		
    }
}
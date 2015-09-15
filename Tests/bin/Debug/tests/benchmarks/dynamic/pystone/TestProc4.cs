/*A port to StaDyn (dynamic) of the pystone benchmark using duck typing.*/

using System;

namespace StaDyn.Dynamic
{
	public class Pystone
    {
        public static var BoolGlob = false;
        public static var Char1Glob = '\0';
        public static var Char2Glob = '\0';
      
		public static void Proc4()
        {
            var BoolLoc = (Char1Glob == 'A');
            BoolLoc = (BoolLoc || BoolGlob);
            Char2Glob = 'B';
        }
		
		public static void TestProc4()
		{
			Pystone.BoolGlob = false;
			Pystone.Char1Glob = 'A';
			Pystone.Proc4();
			if(Pystone.Char2Glob != 'B')
				Environment.Exit(-1);			
		}		
		
        public static void Main()
        {	
			Pystone.Test();
			Console.WriteLine("Pystone completed!!");		
        }	
			
		public static void Test()
		{			
			Pystone.TestProc4();
			Console.WriteLine("TestProc4 completed!!");		
		}		
    }
}
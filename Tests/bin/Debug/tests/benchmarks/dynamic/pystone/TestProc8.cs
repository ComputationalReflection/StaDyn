/*A port to StaDyn (dynamic) of the pystone benchmark using duck typing.*/

using System;

namespace StaDyn.Dynamic
{
	public class Pystone
    {
        public static var IntGlob = 0;
              
		public static void Proc8(var Array1Par, var Array2Par, var IntParI1, var IntParI2)
        {
            var IntLoc;

            IntLoc = IntParI1 + 5;
            Array1Par[IntLoc] = IntParI2;
            Array1Par[IntLoc + 1] = Array1Par[IntLoc];
            Array1Par[IntLoc + 30] = IntLoc;
            for (int IntIndex = IntLoc; IntIndex < IntLoc + 2; IntIndex++)
                Array2Par[IntLoc][IntIndex] = IntLoc;
            Array2Par[IntLoc][IntLoc - 1] = Array2Par[IntLoc][IntLoc - 1] + 1;
            Array2Par[IntLoc + 20][IntLoc] = Array1Par[IntLoc];
            Pystone.IntGlob = 5;
        }
		
		public static void TestProc8()
		{	
			var Array1Par = new int[51];
			var Array2Par = new int[51][];			
			for (int i = 0; i < 51; i++)
				Array2Par[i] = new int[51];
			Array2Par[8][7] = 10;
			var IntParI1 = 3;
			var IntParI2 = 7;
			Pystone.Proc8(Array1Par,Array2Par,IntParI1,IntParI2);			
			if(Pystone.IntGlob != 5)
				Environment.Exit(-1);					
		}
		
        public static void Main()
        {				
			Pystone.Test();
			Console.WriteLine("Pystone completed!!");		
        }	

		public static void Test()
		{			
			Pystone.TestProc8();
			Console.WriteLine("TestProc8 completed!!");			
		}		
    }
}
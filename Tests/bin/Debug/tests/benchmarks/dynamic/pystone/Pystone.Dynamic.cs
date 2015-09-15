/*A port to StaDyn (dynamic) of the pystone benchmark using duck typing.*/

using System;

namespace StaDyn.Dynamic
{
	public class Chronometer
    {
        private DateTime ticks1, ticks2;
        private bool stopped;

        public void Start()
        {
            ticks1 = DateTime.Now;
            stopped = false;
        }
        public void Stop()
        {
            ticks2 = DateTime.Now;
            stopped = true;
        }

        private static int TicksToMicroSeconds(DateTime t1, DateTime t2)
        {
            return TicksToMiliSeconds(t1, t2) * 1000;
        }

        private static int TicksToMiliSeconds(DateTime t1, DateTime t2)
        {
            TimeSpan difference = t2.Subtract(t1);
            return (difference.Milliseconds + difference.Seconds * 1000 + difference.Minutes * 60000);
        }

        private static int TicksToSeconds(DateTime t1, DateTime t2)
        {
            TimeSpan difference = t2.Subtract(t1);
            return (difference.Seconds + difference.Minutes * 60);
        }

        public int GetMicroSeconds()
        {
            if (stopped)
                return TicksToMicroSeconds(ticks1, ticks2);
            return TicksToMicroSeconds(ticks1, DateTime.Now);
        }

        public int GetMiliSeconds()
        {
            if (stopped)
                return TicksToMiliSeconds(ticks1, ticks2);
            return TicksToMiliSeconds(ticks1, DateTime.Now);
        }

        public int GetSeconds()
        {
            if (stopped)
                return TicksToSeconds(ticks1, ticks2);
            return TicksToSeconds(ticks1, DateTime.Now);
        }
    }
	
    public class Idents
    {
        public static var Ident1 = 1;
        public static var Ident2 = 2;
        public static var Ident3 = 3;
        public static var Ident4 = 4;
        public static var Ident5 = 5;
    }

    public class Record
    {
        public var PtrComp;        
        public var Discr;
		public var EnumComp;
		public var IntComp;
		public var StringComp;

        public Record(var Discr, var EnumComp, var IntComp, var StringComp)
        {            
            this.Discr = Discr;  
			this.EnumComp = EnumComp;
			this.IntComp = IntComp;
			this.StringComp = StringComp;
        }        
		
		public var copy()
        {
            var record = new Record(this.Discr, this.EnumComp, this.IntComp, this.StringComp);
			record.PtrComp = this.PtrComp;
			return record;
        }
    }
	
	public class Pystone
    {
        public static string __version__ = "1.1";

        public static var IntGlob = 0;
        public static var BoolGlob = false;
        public static var Char1Glob = '\0';
        public static var Char2Glob = '\0';
        public static var Array1Glob = new int[51];
        public static var Array2Glob = new int[51][];
        public static var PtrGlb;
        public static var PtrGlbNext;

        public Pystone()
        {
            for (int i = 0; i < 51; i++)
                Pystone.Array2Glob[i] = new int[51];
        }
			        
		public static var pystones(var loops)
        {            
            return Pystone.Proc0(loops);
        }
        
		public static var Proc0(var loops)
        {			
            var benchtime = 0;
            var nulltime = 0;
			var String1Loc = "";
         
            Chronometer chronometer = new Chronometer();
            chronometer.Start();
            for (int i = 0; i < loops; i++) ;
            chronometer.Stop();
            benchtime = chronometer.GetMiliSeconds();
			
			PtrGlbNext = new Record(0, Idents.Ident1, 0, "");
            PtrGlb = new Record(0, Idents.Ident1, 0, "");
            PtrGlb.PtrComp = PtrGlbNext;
			PtrGlb.Discr = Idents.Ident1;
            PtrGlb.EnumComp = Idents.Ident3;
            PtrGlb.IntComp = 40;
            PtrGlb.StringComp = "DHRYSTONE PROGRAM, SOME STRING";
            String1Loc = "DHRYSTONE PROGRAM, 1'ST STRING";
            Array2Glob[8][7] = 10;
           	
			chronometer.Start();			
            for (int i = 0; i < loops; i++)            
				Pystone.DoWork();			
			chronometer.Stop();
			benchtime = benchtime + chronometer.GetMiliSeconds();
			
            var result = new double[2];
            result[0] = benchtime;
            result[1] = (benchtime != 0)?(loops / benchtime):0;			
			return result;
		}
		
		private static void DoWork()
		{
			var EnumLoc = 0;            
            var String1Loc = "DHRYSTONE PROGRAM, 1'ST STRING";
            var String2Loc = "";
            var IntLoc1 = 0;
            var IntLoc2 = 0;
            var IntLoc3 = 0;
			var CharIndex = 'A';			
			
			Proc5();
			Proc4();
			IntLoc1 = 2;
			IntLoc2 = 3;
			String2Loc = "DHRYSTONE PROGRAM, 2'ND STRING";
			EnumLoc = Idents.Ident2;
			BoolGlob = !Func2(String1Loc, String2Loc);
			while (IntLoc1 < IntLoc2)
			{
				IntLoc3 = 5 * IntLoc1 - IntLoc2;
				IntLoc3 = Proc7(IntLoc1, IntLoc2);
				IntLoc1 = IntLoc1 + 1;
			}
			Proc8(Array1Glob, Array2Glob, IntLoc1, IntLoc3);                
			PtrGlb = Proc1(PtrGlb);         		
			CharIndex = 'A';							
			while (CharIndex <= Char2Glob) {																							
				if (EnumLoc == Func1(CharIndex, 'C')){						
					EnumLoc = Proc6(Idents.Ident1);
				}				
				CharIndex = (char)(((int)CharIndex) + 1);
			}			
			IntLoc3 = IntLoc2 * IntLoc1;
			IntLoc2 = IntLoc3 / IntLoc1;
			IntLoc2 = 7 * (IntLoc3 - IntLoc2) - IntLoc1;				
			IntLoc1 = Proc2(IntLoc1);
						
			//Show Results
			if(true){
				Console.WriteLine("Global Results: ");
				Console.WriteLine("IntGlob: {0}", IntGlob);
				Console.WriteLine("BoolGlob: {0}", BoolGlob);
				Console.WriteLine("Char1Glob: {0}", Char1Glob);
				Console.WriteLine("Char2Glob: {0}", Char2Glob);
				Console.WriteLine("Array1Glob[8]: {0}", Array1Glob[8]);
				Console.WriteLine("Array2Glob[8][8]: {0}", Array2Glob[8][8]);
				Console.WriteLine("PtrGlb.Discr: {0}", PtrGlb.Discr);
				Console.WriteLine("PtrGlb.EnumComp: {0}", PtrGlb.EnumComp);
				Console.WriteLine("PtrGlb.IntComp: {0}", PtrGlb.IntComp);
				Console.WriteLine("PtrGlb.PtrComp.Discr: {0}", PtrGlb.PtrComp.Discr);
				Console.WriteLine("PtrGlb.PtrComp.EnumComp: {0}", PtrGlb.PtrComp.EnumComp);
				Console.WriteLine("PtrGlb.PtrComp.IntComp: {0}", PtrGlb.PtrComp.IntComp);
				Console.WriteLine("PtrGlb.PtrComp.StringComp: {0}", PtrGlb.PtrComp.StringComp);			
				Console.WriteLine("PtrGlb.StringComp: {0}", PtrGlb.StringComp);			
				Console.WriteLine("PtrGlbNext.Discr: {0}", PtrGlbNext.Discr);
				Console.WriteLine("PtrGlbNext.EnumComp: {0}", PtrGlbNext.EnumComp);
				Console.WriteLine("PtrGlbNext.IntComp: {0}", PtrGlbNext.IntComp);
				Console.WriteLine("PtrGlbNext.StringComp: {0}", PtrGlbNext.StringComp);						
				
				Console.WriteLine("Local Results: ");
				Console.WriteLine("EnumLoc: {0}", EnumLoc);
				Console.WriteLine("IntLoc1: {0}", IntLoc1);
				Console.WriteLine("IntLoc2: {0}", IntLoc2);
				Console.WriteLine("IntLoc3: {0}", IntLoc3);
				Console.WriteLine("String1Loc: {0}", String1Loc);
				Console.WriteLine("String2Loc: {0}", String2Loc);
				Console.WriteLine("CharIndex: {0}", CharIndex);			
			}		
		}
		
		public static var Proc1(var PtrParIn)
        {			
            var NextRecord = PtrGlb.copy();		
            PtrParIn.PtrComp = NextRecord;			
            PtrParIn.IntComp = 5;			
            NextRecord.IntComp = PtrParIn.IntComp;			
            NextRecord.PtrComp = PtrParIn.PtrComp;			
            NextRecord.PtrComp = Proc3(NextRecord.PtrComp);			
            if (NextRecord.Discr == Idents.Ident1)
            {				
                NextRecord.IntComp = 6;				
                NextRecord.EnumComp = Proc6(PtrParIn.EnumComp);				
                NextRecord.PtrComp = PtrGlb.PtrComp;
                NextRecord.IntComp = Proc7(NextRecord.IntComp, 10);
            }
            else PtrParIn = NextRecord.copy();
            NextRecord.PtrComp = null;
            return PtrParIn;
        }
		
		public static var Proc2(var IntParIO) {
            var IntLoc;
            var EnumLoc = Idents.Ident5;

            IntLoc = IntParIO + 10;            
            while (true) {
                if (Pystone.Char1Glob == 'A') {
                    IntLoc = IntLoc - 1;
                    IntParIO = IntLoc - Pystone.IntGlob;
                    EnumLoc = Idents.Ident1;
                }
                if (EnumLoc == Idents.Ident1)
					return IntParIO;
            }
            return IntParIO;
        }
	
		public static var Proc3(var PtrParOut)
        {
            if (PtrGlb != null)
            {
                PtrParOut = PtrGlb.PtrComp;
            }
            else IntGlob = 100;
            PtrGlb.IntComp = Proc7(10, IntGlob);
            return PtrParOut;
        }
	
		public static void Proc4()
        {
            var BoolLoc = (Char1Glob == 'A');
            BoolLoc = (BoolLoc || BoolGlob);
            Char2Glob = 'B';
        }
	
		public static void Proc5()
        {
            Char1Glob = 'A';
            BoolGlob = false;
        }
		
		public static var Proc6(var EnumParIn)
        {
            var EnumParOut;

            EnumParOut = EnumParIn;
            if (!Func3(EnumParIn))
                EnumParOut = Idents.Ident4;
            if (EnumParIn == Idents.Ident1)
                EnumParOut = Idents.Ident1;
            else
            {
                if (EnumParIn == Idents.Ident2)
                {
                    if (IntGlob > 100) EnumParOut = Idents.Ident1;
                    else EnumParOut = Idents.Ident4;
                }
                else
                {
                    if (EnumParIn == Idents.Ident3)
                        EnumParOut = Idents.Ident2;
                    else
                    {
                        if (EnumParIn == Idents.Ident4)
                        {
                        }
                        else
                        {
                            if (EnumParIn == Idents.Ident5)
                                EnumParOut = Idents.Ident3;
                        }
                    }
                }
            }
            return EnumParOut;
        }
		
		public static var Proc7(var IntParI1, var IntParI2)
        {
            var IntParOut;
            var IntLoc;

            IntLoc = IntParI1 + 2;
            IntParOut = IntParI2 + IntLoc;
            return IntParOut;
        }
	
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
	
		public static var Func1(var CharPar1, var CharPar2)
        {									
            var CharLoc1;
            var CharLoc2;
            CharLoc1 = CharPar1;
            CharLoc2 = CharLoc1;			
			if (CharLoc2 != CharPar2)
                return Idents.Ident1;
            else 
				return Idents.Ident2;
        }
		
		public static var Func2(var StrParI1, var StrParI2)
        {
			var IntLoc;
            var CharLoc = ' ';

            IntLoc = 1;
            while(IntLoc <= 1)
            {
                if (Func1(StrParI1.ToCharArray()[IntLoc], StrParI2.ToCharArray()[IntLoc + 1]) == Idents.Ident1)
                {
                    CharLoc = 'A';
                    IntLoc = IntLoc + 1;
                }
            }
            if ((CharLoc >= 'W') && (CharLoc <= 'Z'))
                IntLoc = 7;
            if (CharLoc == 'X')
                return true;
            else
            {
                if (StrParI1.CompareTo(StrParI2) > 0)
                {
                    IntLoc = IntLoc + 7;
                    return true;
                }
                else return false;
            }         
        }
		
		public static var Func3(var EnumParIn)
        {
            var EnumLoc;
            EnumLoc = EnumParIn;
            if (EnumLoc == Idents.Ident3) return true;
            return false;
        }
		
		public static void Main()
        {	
			Pystone.Run();
			Console.WriteLine("Pystone completed!!");		
        }	
		
		public static void Run()
		{
			Pystone pystone = new Pystone();
            var result = Pystone.pystones(1);
			Console.WriteLine("benchtime: " + result[0]);
			Console.WriteLine("loops / benchtime: " + result[1]);			
		}	
    }
}
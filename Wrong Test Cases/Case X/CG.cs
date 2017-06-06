/*A port to StaDyn (dynamic) of the pystone benchmark using duck typing.*/

using System;
using System.Collections;

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
        public static dynamic Ident1 = 1;
        public static dynamic Ident2 = 2;
        public static dynamic Ident3 = 3;
        public static dynamic Ident4 = 4;
        public static dynamic Ident5 = 5;
    }

	public class BenchMark 
	{
		private int iterations;
		protected int microSeconds;

		public BenchMark(int iterations) 
		{
			this.iterations = iterations;
		}

		public int run() 
		{
			BenchMark self = this;
			for (int i = 0; i < iterations; i = i + 1)
				self.runOneIteration();
			return this.microSeconds;
		}

		virtual public object runOneIteration() { return null; }
	}
	
	public class ArithmethicBenchmark : BenchMark 
	{
		public ArithmethicBenchmark(int iterations) : base(iterations) { }	
		public override object runOneIteration() 
		{				
			Chronometer chronometer = new Chronometer();
			Pystone pystone = new Pystone();	
			
			chronometer.Start();				
			object value = Pystone.pystones(5000);
			chronometer.Stop();
			
			this.microSeconds = this.microSeconds + chronometer.GetMicroSeconds();
			return value;
		}
	} 
	
    public class Record
    {
        public dynamic PtrComp;        
        public dynamic Discr;
		public dynamic EnumComp;
		public dynamic IntComp;
		public dynamic StringComp;

        public Record(dynamic Discr, dynamic EnumComp, dynamic IntComp, dynamic StringComp)
        {            
            this.Discr = Discr;  
			this.EnumComp = EnumComp;
			this.IntComp = IntComp;
			this.StringComp = StringComp;
        }        
		
		public dynamic copy()
        {
            dynamic record = new Record(this.Discr, this.EnumComp, this.IntComp, this.StringComp);
			record.PtrComp = this.PtrComp;
			return record;
        }
    }
	
	public class Pystone
    {
        public static string __version__ = "1.1";

        public static dynamic IntGlob = 0;
        public static dynamic BoolGlob = false;
        public static dynamic Char1Glob = '\0';
        public static dynamic Char2Glob = '\0';
        public static dynamic Array1Glob = new int[51];
        public static dynamic Array2Glob = new int[51][];
        public static dynamic PtrGlb;
        public static dynamic PtrGlbNext;

        public Pystone()
        {			
            for (int i = 0; i < 51; i = i + 1)
                Pystone.Array2Glob[i] = new int[51];
        }
			        
		public static dynamic pystones(dynamic loops)
        {            
            return Pystone.Proc0(loops);
        }
        
		public static dynamic Proc0(dynamic loops)
        {			
            dynamic benchtime = 0;
            dynamic nulltime = 0;
			dynamic String1Loc = "";
         
            Chronometer chronometer = new Chronometer();
            chronometer.Start();
            for (int i = 0; i < loops; i = i + 1) ;
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
            for (int i = 0; i < loops; i = i + 1)            
				Pystone.DoWork();			
			chronometer.Stop();
			benchtime = benchtime + chronometer.GetMiliSeconds();
			
            dynamic result = new double[2];
            result[0] = benchtime;
            result[1] = (benchtime != 0)?(loops / benchtime):0;			
			return result;
		}
		
		private static void DoWork()
		{
			dynamic EnumLoc = 0;            
            dynamic String1Loc = "DHRYSTONE PROGRAM, 1'ST STRING";
            dynamic String2Loc = "";
            dynamic IntLoc1 = 0;
            dynamic IntLoc2 = 0;
            dynamic IntLoc3 = 0;
			dynamic CharIndex = 'A';

			Proc5();
			Proc4();	
			IntLoc1 = 2;
			IntLoc2 = 3;
			String2Loc = "DHRYSTONE PROGRAM, 2'ND STRING";
			EnumLoc = Idents.Ident2;
			BoolGlob = !Func2(String1Loc, String2Loc);	

			//while (IntLoc1 < IntLoc2){
				IntLoc3 = 5 * IntLoc1 - IntLoc2;
				IntLoc3 = Proc7(IntLoc1, IntLoc2);
				IntLoc1 = IntLoc1 + 1;
			//}
			Proc8(Array1Glob, Array2Glob, IntLoc1, IntLoc3);							
			PtrGlb = Proc1(new Record(0, Idents.Ident1, 0, ""));         		
			CharIndex = 'A';							
			//while (CharIndex <= Char2Glob) {																							
					Func1(CharIndex, 'C');						
				if (EnumLoc == Func1(CharIndex, 'C')){										
					EnumLoc = Proc6(Idents.Ident1);
				}				
				CharIndex = (char)(((int)CharIndex) + 1);
			//}	
			
			IntLoc3 = IntLoc2 * IntLoc1;
			IntLoc2 = IntLoc3 / IntLoc1;
			IntLoc2 = 7 * (IntLoc3 - IntLoc2) - IntLoc1;				
			IntLoc1 = Proc2(IntLoc1);
		}
		
		public static dynamic Proc1(dynamic PtrParIn)
        {			
            dynamic NextRecord = PtrGlb.copy();		
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
				NextRecord.PtrComp = null;
				return PtrParIn;
            }
            PtrParIn = NextRecord.copy();
            NextRecord.PtrComp = null;
            return PtrParIn;
        }
		
		public static dynamic Proc2(dynamic IntParIO) {
            dynamic IntLoc;
            dynamic EnumLoc = Idents.Ident5;

            IntLoc = IntParIO + 10;            
           // while (true) {
                if (Pystone.Char1Glob == 'A') {
                    IntLoc = IntLoc - 1;
                    IntParIO = IntLoc - Pystone.IntGlob;
                    EnumLoc = Idents.Ident1;
                }
                if (EnumLoc == Idents.Ident1)
					return IntParIO;
            //}
            return IntParIO;
        }
		
		public static dynamic Proc3(dynamic PtrParOut)
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
            dynamic BoolLoc = (Char1Glob == 'A');
            BoolLoc = (BoolLoc || BoolGlob);
            Char2Glob = 'B';
        }
	
		public static void Proc5()
        {
            Char1Glob = 'A';
            BoolGlob = false;
        }
		
		public static dynamic Proc6(dynamic EnumParIn)
        {
            dynamic EnumParOut;

            EnumParOut = EnumParIn;
            if (!Func3(EnumParIn))                
			{
                EnumParOut = Idents.Ident4;
			}
            if (EnumParIn == Idents.Ident1)
			{
                EnumParOut = Idents.Ident1;
				return EnumParOut;
			}
			if (EnumParIn == Idents.Ident2)
			{
				if (IntGlob > 100) 
				{
					EnumParOut = Idents.Ident1;
				}
				else
				{
					EnumParOut = Idents.Ident4;
				}
				return EnumParOut;
			}				
			if (EnumParIn == Idents.Ident3)
			{
				EnumParOut = Idents.Ident2;
				return EnumParOut;
			}
			
			if (EnumParIn == Idents.Ident5)
			{
				EnumParOut = Idents.Ident3;
				return EnumParOut;    
            }
			return EnumParOut;            
        }
		
		public static dynamic Proc7(dynamic IntParI1, dynamic IntParI2)
        {
            dynamic IntParOut;
            dynamic IntLoc;

            IntLoc = IntParI1 + 2;
            IntParOut = IntParI2 + IntLoc;
            return IntParOut;
        }
		
		public static void Proc8(dynamic Array1Par, dynamic Array2Par, dynamic IntParI1, dynamic IntParI2)
        {
            dynamic IntLoc;
            IntLoc = IntParI1 + 5;
            Array1Par[IntLoc] = IntParI2;
            Array1Par[IntLoc + 1] = Array1Par[IntLoc];
            Array1Par[IntLoc + 30] = IntLoc;
            for (int IntIndex = IntLoc; IntIndex < IntLoc + 2; IntIndex = IntIndex + 1)
                Array2Par[IntLoc][IntIndex] = IntLoc;
            Array2Par[IntLoc][IntLoc - 1] = Array2Par[IntLoc][IntLoc - 1] + 1;
            Array2Par[IntLoc + 20][IntLoc] = Array1Par[IntLoc];
            Pystone.IntGlob = 5;
        }
		
		public static dynamic Func1(dynamic CharPar1, dynamic CharPar2)
        {									
            dynamic CharLoc1;
            dynamic CharLoc2;
            CharLoc1 = CharPar1;
            CharLoc2 = CharLoc1;			
			if (CharLoc2 != CharPar2)
                return Idents.Ident1;
            else 
				return Idents.Ident2;
        }
		
		public static dynamic Func2(dynamic StrParI1, dynamic StrParI2)
        {
			dynamic IntLoc;
            dynamic CharLoc = ' ';

            IntLoc = 1;
            //while(IntLoc <= 1)
            //{
                if (Func1(StrParI1.ToCharArray()[IntLoc], StrParI2.ToCharArray()[IntLoc + 1]) == Idents.Ident1)
                {
                    CharLoc = 'A';
                    IntLoc = IntLoc + 1;
                }
            //}
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
		
		public static dynamic Func3(dynamic EnumParIn)
        {
            dynamic EnumLoc;
            EnumLoc = EnumParIn;
            if (EnumLoc == Idents.Ident3) return true;
            return false;
        }
					
   }
   
	public class Program 
	{
		public static void Main(string[] args) 
		{			
			ArithmethicBenchmark arith = new ArithmethicBenchmark(1);
			Console.WriteLine(arith.run());
		}
    }
}
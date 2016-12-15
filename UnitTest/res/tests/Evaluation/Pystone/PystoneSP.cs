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
            for (int i = 0; i < iterations; i++)
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

    public class Idents
    {
        public static int Ident1 = 1;
        public static int Ident2 = 2;
        public static int Ident3 = 3;
        public static int Ident4 = 4;
        public static int Ident5 = 5;
    }

    public class Record
    {
        public Record PtrComp;
        public int Discr;
        public int EnumComp;
        public int IntComp;
        public string StringComp;

        public Record(int Discr, int EnumComp, int IntComp, string StringComp)
        {
            this.Discr = Discr;
            this.EnumComp = EnumComp;
            this.IntComp = IntComp;
            this.StringComp = StringComp;
        }

        public Record copy()
        {
            Record record = new Record(this.Discr, this.EnumComp, this.IntComp, this.StringComp);
            record.PtrComp = this.PtrComp;
            return record;
        }
    }

    public class Pystone
    {
        public static string __version__ = "1.1";

        public static int IntGlob = 0;
        public static bool BoolGlob = false;
        public static char Char1Glob = '\0';
        public static char Char2Glob = '\0';
        public static int[] Array1Glob = new int[51];
        public static int[][] Array2Glob = new int[51][];
        public static Record PtrGlb;
        public static Record PtrGlbNext;

        public Pystone()
        {
            for (int i = 0; i < 51; i++)
                Pystone.Array2Glob[i] = new int[51];
        }

        public static double[] pystones(int loops)
        {
            return Pystone.Proc0(loops);
        }

        public static double[] Proc0(int loops)
        {
            int benchtime = 0;
            int nulltime = 0;
            string String1Loc = "";

            Chronometer chronometer = new Chronometer();
            chronometer.Start();
            for (int i = 0; i < loops; i++) ;
            chronometer.Stop();
            nulltime = chronometer.GetMiliSeconds();

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
            benchtime = chronometer.GetMicroSeconds();

            double[] result = new double[2];
            result[0] = benchtime;
            result[1] = (benchtime != 0) ? (loops / benchtime) : 0;
            return result;
        }

        private static void DoWork()
        {
            int EnumLoc = 0;
            string String1Loc = "DHRYSTONE PROGRAM, 1'ST STRING";
            string String2Loc = "";
            int IntLoc1 = 0;
            int IntLoc2 = 0;
            int IntLoc3 = 0;
            char CharIndex = 'A';

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
            while (CharIndex <= Char2Glob)
            {
                if (EnumLoc == Func1(CharIndex, 'C'))
                {
                    EnumLoc = Proc6(Idents.Ident1);
                }
                CharIndex = (char)(CharIndex + 1);
            }
            IntLoc3 = IntLoc2 * IntLoc1;
            IntLoc2 = IntLoc3 / IntLoc1;
            IntLoc2 = 7 * (IntLoc3 - IntLoc2) - IntLoc1;
            IntLoc1 = Proc2(IntLoc1);
        }

        public static Record Proc1(Record PtrParIn)
        {
            Record NextRecord = PtrGlb.copy();
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

        public static int Proc2(int IntParIO)
        {
            int IntLoc;
            int EnumLoc = Idents.Ident5;

            IntLoc = IntParIO + 10;
            while (true)
            {
                if (Pystone.Char1Glob == 'A')
                {
                    IntLoc = IntLoc - 1;
                    IntParIO = IntLoc - Pystone.IntGlob;
                    EnumLoc = Idents.Ident1;
                }
                if (EnumLoc == Idents.Ident1)
                    return IntParIO;
            }
            return IntParIO;
        }

        public static Record Proc3(Record PtrParOut)
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

        public static int Proc6(int EnumParIn)
        {
            int EnumParOut;

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

        public static int Proc7(int IntParI1, int IntParI2)
        {
            int IntParOut;
            int IntLoc;

            IntLoc = IntParI1 + 2;
            IntParOut = IntParI2 + IntLoc;
            return IntParOut;
        }

        public static void Proc8(int[] Array1Par, int[][] Array2Par, int IntParI1, int IntParI2)
        {
            int IntLoc;

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

        public static int Func1(char CharPar1, char CharPar2)
        {
            char CharLoc1;
            char CharLoc2;
            CharLoc1 = CharPar1;
            CharLoc2 = CharLoc1;
            if (CharLoc2 != CharPar2)
                return Idents.Ident1;
            else
                return Idents.Ident2;
        }

        public static bool Func2(string StrParI1, string StrParI2)
        {
            int IntLoc;
            char CharLoc = ' ';

            IntLoc = 1;
            while (IntLoc <= 1)
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

        public static bool Func3(int EnumParIn)
        {
            int EnumLoc;
            EnumLoc = EnumParIn;
            if (EnumLoc == Idents.Ident3) return true;
            return false;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.Error.WriteLine("You must pass the number of thousands iterations.");
                System.Environment.Exit(-1);
            }
            int iterations = Convert.ToInt32(args[0]);
            ArithmethicBenchmark arith = new ArithmethicBenchmark(iterations);
            Console.WriteLine(arith.run());
        }
    }
}
using System;

namespace Pybench.Aritmethic
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
			Test test = new MethodCalls();						
			chronometer.Start();				
			test.test();
			chronometer.Stop();			
			this.microSeconds = this.microSeconds + chronometer.GetMicroSeconds();
			return null;
		}
	}
	
	public abstract class Test {
        public abstract void test();
    }

	public class C 
	{
        var x = 2;
        var y;
        var t;
        var s = "string";

        public var f()
        {
            return this.x;
        }

        public var j(var a, var b)
        {
            this.y = a;
            this.t = b;
            return this.y;
        }

        public void k(var a, var b, var c)
        {
            this.y = a;
            this.s = "" + b;
            this.t = c;
        }
    }
	
	public class MethodCalls : Test 
	{		
        public override void test() 
		{
            var o = new C();
            var two = 2;
            var three = 3;
            var four = 4;
            for (int i = 0; i < 30000; i = i + 1) 
            {
                o.f();
                o.f();
                o.f();
                o.f();
                o.f();
                o.f();
                o.j(i, i);
                o.j(i, i);
                o.j(i, two);
                o.j(i, two);
                o.j(two, two);
                o.k(i, i, three);
                o.k(i, two, three);
                o.k(i, two, three);
                o.k(i, i, four);

                o.f();
                o.f();
                o.f();
                o.f();
                o.f();
                o.f();
                o.j(i, i);
                o.j(i, i);
                o.j(i, two);
                o.j(i, two);
                o.j(two, two);
                o.k(i, i, three);
                o.k(i, two, three);
                o.k(i, two, three);
                o.k(i, i, four);

                o.f();
                o.f();
                o.f();
                o.f();
                o.f();
                o.f();
                o.j(i, i);
                o.j(i, i);
                o.j(i, two);
                o.j(i, two);
                o.j(two, two);
                o.k(i, i, three);
                o.k(i, two, three);
                o.k(i, two, three);
                o.k(i, i, four);

                o.f();
                o.f();
                o.f();
                o.f();
                o.f();
                o.f();
                o.j(i, i);
                o.j(i, i);
                o.j(i, two);
                o.j(i, two);
                o.j(two, two);
                o.k(i, i, three);
                o.k(i, two, three);
                o.k(i, two, three);
                o.k(i, i, four);

                o.f();
                o.f();
                o.f();
                o.f();
                o.f();
                o.f();
                o.j(i, i);
                o.j(i, i);
                o.j(i, two);
                o.j(i, two);
                o.j(two, two);
                o.k(i, i, three);
                o.k(i, two, three);
                o.k(i, two, three);
                o.k(i, i, four);
            }
        }
    }
	
	public class Program 
	{
		public static void Main(string[] args) 
		{
			//if (args.Length<1) {
			//	Console.Error.WriteLine("You must pass the number of thousands iterations.");
			//		System.Environment.Exit(-1);
			//}
			//int iterations = Convert.ToInt32(args[0]);
			ArithmethicBenchmark arith = new ArithmethicBenchmark(1);
			Console.WriteLine(arith.run());
		}
    }
}
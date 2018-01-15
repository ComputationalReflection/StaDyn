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
            Test test = new CreateNewInstances();
            chronometer.Start();
            test.test();
            chronometer.Stop();
            this.microSeconds = this.microSeconds + chronometer.GetMicroSeconds();
            return null;
        }
    }

    public abstract class Test
    {
        public abstract void test();
    }

    public class Root
    {
        public int a;

        public Root() { }

        public Root(int a)
        {
            this.a = a;
        }
    }

    public class C : Root
    {
        public static int b;
        public static int c;
    }

    public class D : Root
    {
        public int b;
        public int c;

        public D(int a, int b, int c) : base(a)
        {
            this.b = b;
            this.c = c;
        }
    }

    public class E : Root
    {
        public int b;
        public int c;
        public int d;
        public int e;
        public int f;

        public E(int a, int b, int c) : base(a)
        {
            this.b = b;
            this.c = c;
            this.d = a;
            this.e = b;
            this.f = c;
        }
    }

    public class CreateNewInstances : Test
    {
        public override void test()
        {
            int three = 3;
            int four = 4;
            for (int i = 0; i < 800000; i = i + 1)
            {
                C o = new C();
                C o1 = new C();
                C o2 = new C();
                D p = new D(i, i, three);
                D p1 = new D(i, i, three);
                D p2 = new D(i, three, three);
                D p3 = new D(three, i, three);
                D p4 = new D(i, i, i);
                D p5 = new D(three, i, three);
                D p6 = new D(i, i, i);
                E q = new E(i, i, three);
                E q1 = new E(i, i, three);
                E q2 = new E(i, i, three);
                E q3 = new E(i, i, four);
            }
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
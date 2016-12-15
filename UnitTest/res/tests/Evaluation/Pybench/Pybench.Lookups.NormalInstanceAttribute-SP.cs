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
            Test test = new NormalInstanceAttribute();
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

    public class D
    {
        public int a;
        public int b;
        public int c;

        public D(int a, int b, int c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }
    }

    public class NormalInstanceAttribute : Test
    {
        public override void test()
        {
            D o = new D(0, 0, 0);
            int x;

            int two = 2;
            int three = 3;
            int four = 4;

            for (int i = 0; i < 100000; i = i + 1)
            {

                o.a = two;
                o.b = three;
                o.c = four;

                o.a = two;
                o.b = three;
                o.c = four;

                o.a = two;
                o.b = three;
                o.c = four;

                o.a = two;
                o.b = three;
                o.c = four;

                x = o.a;
                x = o.b;
                x = o.c;

                x = o.a;
                x = o.b;
                x = o.c;

                x = o.a;
                x = o.b;
                x = o.c;

                x = o.a;
                x = o.b;
                x = o.c;

                o.a = two;
                o.b = three;
                o.c = four;

                o.a = two;
                o.b = three;
                o.c = four;

                o.a = two;
                o.b = three;
                o.c = four;

                o.a = two;
                o.b = three;
                o.c = four;

                x = o.a;
                x = o.b;
                x = o.c;

                x = o.a;
                x = o.b;
                x = o.c;

                x = o.a;
                x = o.b;
                x = o.c;

                x = o.a;
                x = o.b;
                x = o.c;

                o.a = two;
                o.b = three;
                o.c = four;

                o.a = two;
                o.b = three;
                o.c = four;

                o.a = two;
                o.b = three;
                o.c = four;

                o.a = two;
                o.b = three;
                o.c = four;

                x = o.a;
                x = o.b;
                x = o.c;

                x = o.a;
                x = o.b;
                x = o.c;

                x = o.a;
                x = o.b;
                x = o.c;

                x = o.a;
                x = o.b;
                x = o.c;

                o.a = two;
                o.b = three;
                o.c = four;

                o.a = two;
                o.b = three;
                o.c = four;

                o.a = two;
                o.b = three;
                o.c = four;

                o.a = two;
                o.b = three;
                o.c = four;

                x = o.a;
                x = o.b;
                x = o.c;

                x = o.a;
                x = o.b;
                x = o.c;

                x = o.a;
                x = o.b;
                x = o.c;

                x = o.a;
                x = o.b;
                x = o.c;

                o.a = two;
                o.b = three;
                o.c = four;

                o.a = two;
                o.b = three;
                o.c = four;

                o.a = two;
                o.b = three;
                o.c = four;

                o.a = two;
                o.b = three;
                o.c = four;

                x = o.a;
                x = o.b;
                x = o.c;

                x = o.a;
                x = o.b;
                x = o.c;

                x = o.a;
                x = o.b;
                x = o.c;

                x = o.a;
                x = o.b;
                x = o.c;
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
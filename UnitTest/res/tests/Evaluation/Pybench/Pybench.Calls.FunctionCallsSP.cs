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
            Test test = new FunctionCalls();
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

    public class FunctionCalls : Test
    {
        void f() { }

        void f1(int x) { }

        int[] g(int a, int b, int c)
        {
            int[] result = new int[3];
            result[0] = a;
            result[1] = b;
            result[2] = c;
            return result;
        }

        int[] h(int a, int b, int c, int d, int e, int f)
        {
            int[] result = new int[3];
            result[0] = d;
            result[1] = e;
            result[2] = f;
            return result;
        }

        public override void test()
        {
            int zero = 0;
            int two = 2;
            int three = 3;
            for (int i = 0; i < 60000; i = i + 1)
            {
                f();
                f1(i);
                f1(i);
                f1(i);
                f1(i);
                g(i, i, i);
                g(i, i, i);
                g(i, i, i);
                g(i, i, i);
                h(i, i, three, i, i, zero);
                h(i, i, i, two, i, three);

                f();
                f1(i);
                f1(i);
                f1(i);
                f1(i);
                g(i, i, i);
                g(i, i, i);
                g(i, i, i);
                g(i, i, i);
                h(i, i, three, i, i, zero);
                h(i, i, i, two, i, three);

                f();
                f1(i);
                f1(i);
                f1(i);
                f1(i);
                g(i, i, i);
                g(i, i, i);
                g(i, i, i);
                g(i, i, i);
                h(i, i, three, i, i, zero);
                h(i, i, i, two, i, three);

                f();
                f1(i);
                f1(i);
                f1(i);
                f1(i);
                g(i, i, i);
                g(i, i, i);
                g(i, i, i);
                g(i, i, i);
                h(i, i, three, i, i, zero);
                h(i, i, i, two, i, three);

                f();
                f1(i);
                f1(i);
                f1(i);
                f1(i);
                g(i, i, i);
                g(i, i, i);
                g(i, i, i);
                g(i, i, i);
                h(i, i, three, i, i, zero);
                h(i, i, i, two, i, three);
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
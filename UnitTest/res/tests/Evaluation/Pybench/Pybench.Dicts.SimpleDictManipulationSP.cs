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
            Test test = new SimpleDictManipulation();
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

    public class SimpleDictManipulation : Test
    {
        private bool HasKey(int[] d, int key)
        {
            if (key >= d.Length)
                return false;
            return d[key] != 0;
        }

        public override void test()
        {
            int x;

            int zero = 0;
            int one = 1;
            int two = 2;
            int three = 3;
            int four = 4;
            int five = 5;
            int six = 6;
            int eight = 8;
            int ten = 10;

            int[] d = new int[6];

            for (var i = 0; i < 100000; i++)
            {
                d[0] = three;
                d[1] = four;
                d[2] = five;
                d[3] = three;
                d[4] = four;
                d[5] = five;

                x = d[0];
                x = d[1];
                x = d[2];
                x = d[3];
                x = d[4];
                x = d[5];

                HasKey(d, zero);
                HasKey(d, two);
                HasKey(d, four);
                HasKey(d, six);
                HasKey(d, eight);
                HasKey(d, ten);

                d[0] = zero;
                d[1] = zero;
                d[2] = zero;
                d[3] = zero;
                d[4] = zero;
                d[5] = zero;

                d[0] = three;
                d[1] = four;
                d[2] = five;
                d[3] = three;
                d[4] = four;
                d[5] = five;

                x = d[0];
                x = d[1];
                x = d[2];
                x = d[3];
                x = d[4];
                x = d[5];

                HasKey(d, zero);
                HasKey(d, two);
                HasKey(d, four);
                HasKey(d, six);
                HasKey(d, eight);
                HasKey(d, ten);

                d[0] = zero;
                d[1] = zero;
                d[2] = zero;
                d[3] = zero;
                d[4] = zero;
                d[5] = zero;

                d[0] = three;
                d[1] = four;
                d[2] = five;
                d[3] = three;
                d[4] = four;
                d[5] = five;

                x = d[0];
                x = d[1];
                x = d[2];
                x = d[3];
                x = d[4];
                x = d[5];

                HasKey(d, zero);
                HasKey(d, two);
                HasKey(d, four);
                HasKey(d, six);
                HasKey(d, eight);
                HasKey(d, ten);

                d[0] = zero;
                d[1] = zero;
                d[2] = zero;
                d[3] = zero;
                d[4] = zero;
                d[5] = zero;

                d[0] = three;
                d[1] = four;
                d[2] = five;
                d[3] = three;
                d[4] = four;
                d[5] = five;

                x = d[0];
                x = d[1];
                x = d[2];
                x = d[3];
                x = d[4];
                x = d[5];

                HasKey(d, zero);
                HasKey(d, two);
                HasKey(d, four);
                HasKey(d, six);
                HasKey(d, eight);
                HasKey(d, ten);

                d[0] = zero;
                d[1] = zero;
                d[2] = zero;
                d[3] = zero;
                d[4] = zero;
                d[5] = zero;

                d[0] = three;
                d[1] = four;
                d[2] = five;
                d[3] = three;
                d[4] = four;
                d[5] = five;

                x = d[0];
                x = d[1];
                x = d[2];
                x = d[3];
                x = d[4];
                x = d[5];

                HasKey(d, zero);
                HasKey(d, two);
                HasKey(d, four);
                HasKey(d, six);
                HasKey(d, eight);
                HasKey(d, ten);

                d[0] = zero;
                d[1] = zero;
                d[2] = zero;
                d[3] = zero;
                d[4] = zero;
                d[5] = zero;
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
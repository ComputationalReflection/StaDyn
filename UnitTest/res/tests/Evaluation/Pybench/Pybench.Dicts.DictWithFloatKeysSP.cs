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
            Test test = new DictWithFloatKeys();
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

    public class DictWithFloatKeys : Test
    {
        public static int SIZE;

        public static int Hash(double value)
        {
            int val = value.GetHashCode();
            if (val < 0)
                return (0 - val) % SIZE;
            return val % SIZE;
        }

        public override void test()
        {
            DictWithFloatKeys.SIZE = 50;
            int temp;

            int hash1234 = Hash(1.234);
            int hash2345 = Hash(2.345);
            int hash3456 = Hash(3.456);
            int hash4567 = Hash(4.567);
            int hash5678 = Hash(5.678);
            int hash6789 = Hash(6.789);

            int one = 1;
            int two = 2;
            int three = 3;
            int four = 4;
            int five = 5;
            int six = 6;


            int[] d = new int[DictWithFloatKeys.SIZE];

            for (int i = 0; i < 150000; i = i + 1)
            {
                d[hash1234] = one;
                d[hash2345] = two;
                d[hash3456] = three;
                d[hash4567] = four;
                d[hash5678] = five;
                d[hash6789] = six;

                temp = d[hash1234];
                temp = d[hash2345];
                temp = d[hash3456];
                temp = d[hash4567];
                temp = d[hash5678];
                temp = d[hash6789];

                d[hash1234] = one;
                d[hash2345] = two;
                d[hash3456] = three;
                d[hash4567] = four;
                d[hash5678] = five;
                d[hash6789] = six;

                temp = d[hash1234];
                temp = d[hash2345];
                temp = d[hash3456];
                temp = d[hash4567];
                temp = d[hash5678];
                temp = d[hash6789];

                d[hash1234] = one;
                d[hash2345] = two;
                d[hash3456] = three;
                d[hash4567] = four;
                d[hash5678] = five;
                d[hash6789] = six;

                temp = d[hash1234];
                temp = d[hash2345];
                temp = d[hash3456];
                temp = d[hash4567];
                temp = d[hash5678];
                temp = d[hash6789];

                d[hash1234] = one;
                d[hash2345] = two;
                d[hash3456] = three;
                d[hash4567] = four;
                d[hash5678] = five;
                d[hash6789] = six;

                temp = d[hash1234];
                temp = d[hash2345];
                temp = d[hash3456];
                temp = d[hash4567];
                temp = d[hash5678];
                temp = d[hash6789];

                d[hash1234] = one;
                d[hash2345] = two;
                d[hash3456] = three;
                d[hash4567] = four;
                d[hash5678] = five;
                d[hash6789] = six;

                temp = d[hash1234];
                temp = d[hash2345];
                temp = d[hash3456];
                temp = d[hash4567];
                temp = d[hash5678];
                temp = d[hash6789];
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
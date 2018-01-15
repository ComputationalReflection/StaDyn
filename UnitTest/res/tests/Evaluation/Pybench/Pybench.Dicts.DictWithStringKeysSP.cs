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
            Test test = new DictWithStringKeys();
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

    public class DictWithStringKeys : Test
    {
        public static int SIZE;

        private static int Hash(string value)
        {
            int hashVal = 0;
            int c;
            string key = value;
            for (int i = 0; i < key.Length; i++)
            {
                c = key.ToCharArray()[i];
                hashVal = hashVal << 5 ^ c ^ hashVal;
            }
            return hashVal % SIZE;
        }

        public override void test()
        {
            DictWithStringKeys.SIZE = 200;
            int temp;

            int hashABC = Hash("abc");
            int hashDEF = Hash("def");
            int hashGHI = Hash("ghi");
            int hashJKL = Hash("jkl");
            int hashMNO = Hash("mno");
            int hashPQR = Hash("pqr");

            int one = 1;
            int two = 2;
            int three = 3;
            int four = 4;
            int five = 5;
            int six = 6;

            int[] d = new int[SIZE];

            for (int i = 0; i < 200000; i++)
            {
                d[hashABC] = one;
                d[hashDEF] = two;
                d[hashGHI] = three;
                d[hashJKL] = four;
                d[hashMNO] = five;
                d[hashPQR] = six;

                temp = d[hashABC];
                temp = d[hashDEF];
                temp = d[hashGHI];
                temp = d[hashJKL];
                temp = d[hashMNO];
                temp = d[hashPQR];

                d[hashABC] = one;
                d[hashDEF] = two;
                d[hashGHI] = three;
                d[hashJKL] = four;
                d[hashMNO] = five;
                d[hashPQR] = six;

                temp = d[hashABC];
                temp = d[hashDEF];
                temp = d[hashGHI];
                temp = d[hashJKL];
                temp = d[hashMNO];
                temp = d[hashPQR];

                d[hashABC] = one;
                d[hashDEF] = two;
                d[hashGHI] = three;
                d[hashJKL] = four;
                d[hashMNO] = five;
                d[hashPQR] = six;

                temp = d[hashABC];
                temp = d[hashDEF];
                temp = d[hashGHI];
                temp = d[hashJKL];
                temp = d[hashMNO];
                temp = d[hashPQR];

                d[hashABC] = one;
                d[hashDEF] = two;
                d[hashGHI] = three;
                d[hashJKL] = four;
                d[hashMNO] = five;
                d[hashPQR] = six;

                temp = d[hashABC];
                temp = d[hashDEF];
                temp = d[hashGHI];
                temp = d[hashJKL];
                temp = d[hashMNO];
                temp = d[hashPQR];

                d[hashABC] = one;
                d[hashDEF] = two;
                d[hashGHI] = three;
                d[hashJKL] = four;
                d[hashMNO] = five;
                d[hashPQR] = six;

                temp = d[hashABC];
                temp = d[hashDEF];
                temp = d[hashGHI];
                temp = d[hashJKL];
                temp = d[hashMNO];
                temp = d[hashPQR];
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
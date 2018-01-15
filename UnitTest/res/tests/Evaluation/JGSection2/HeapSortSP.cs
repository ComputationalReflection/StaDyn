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

        public object runOneIteration()
        {
            Chronometer chronometer = new Chronometer();
            JGFHeapSortBench test = new JGFHeapSortBench();
            chronometer.Start();
            test.Test();
            chronometer.Stop();
            this.microSeconds = this.microSeconds + chronometer.GetMicroSeconds();
            return null;
        }
    }

    public class JGFHeapSortBench
    {
        private int size;
        private int[] datasizes;

        public int array_rows;
        public int[] TestArray;
        public void buildTestData()
        {
            TestArray = new int[array_rows];
            Random rndnum = new Random(1729);
            int i = 0;
            while (i < array_rows)
            {
                TestArray[i] = rndnum.Next();
                i = i + 1;
            }
        }

        public void Do()
        {
            NumHeapSort();
        }

        private void NumSift(int min, int max)
        {
            while ((min + min) <= max)
            {
                bool inc = false;
                if ((min + min) < max && TestArray[min + min] < TestArray[min + min + 1])
                    inc = true;
                int k = inc ? min + min + 1 : min + min;
                if (TestArray[min] < TestArray[k])
                {
                    int temp = TestArray[k];
                    TestArray[k] = TestArray[min];
                    TestArray[min] = temp;
                    min = k;
                }
                else
                    min = max + 1;
            }
        }

        private void NumHeapSort()
        {
            int temp;

            int top = array_rows - 1;
            int i = top / 2;
            while (i > 0)
            {
                NumSift(i, top);
                i = i - 1;
            }
            i = top;
            while (i > 0)
            {
                NumSift(0, i);
                temp = TestArray[0];
                TestArray[0] = TestArray[i];
                TestArray[i] = temp;
                i = i - 1;
            }
        }

        public JGFHeapSortBench()
        {
            datasizes = new int[3];
            datasizes[0] = 1000000;
            datasizes[1] = 5000000;
            datasizes[2] = 25000000;
        }

        public void JGFsetsize(int size)
        {
            this.size = size;
        }

        public void JGFinitialise()
        {
            array_rows = datasizes[size];
            buildTestData();
        }

        public void JGFkernel()
        {
            Do();
        }

        public void JGFvalidate()
        {
            bool error = false;
            int i = 1;
            while (i < array_rows)
            {
                error = TestArray[i] < TestArray[i - 1];
                if (error)
                {
                    //Console.WriteLine("Validation failed");
                    //Console.WriteLine("Item " + i + " = " + TestArray[i]);
                    //Console.WriteLine("Item " + (i - 1) + " = " + TestArray[i - 1]);					
                    //break;
                }
                i = i + 1;
            }
        }
        public void Test()
        {
            JGFsetsize(0);
            JGFinitialise();
            JGFkernel();
            JGFvalidate();
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
            BenchMark arith = new BenchMark(iterations);
            Console.WriteLine(arith.run());
        }
    }
}
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
            Test test = new JGFHeapSortBench();
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

    public class JGFHeapSortBench : Test
    {
        private void NumSift(dynamic TestArray, dynamic min, dynamic max)
        {					
            while ((min + min) <= max)
            {
                dynamic inc = false;
                if ((min + min) < max && TestArray[min + min] < TestArray[min + min + 1])
                    inc = true;
                dynamic k = inc ? min + min + 1 : min + min;
                if (TestArray[min] < TestArray[k])
                {
                    dynamic temp = TestArray[k];
                    TestArray[k] = TestArray[min];
                    TestArray[min] = temp;
                    min = k;
                }
                else
                    min = max + 1;
            }
        }

        private void JGFkernel(dynamic TestArray, dynamic array_rows)
        {
            dynamic temp;

            dynamic top = array_rows - 1;
            int i = top / 2;
            //while (i > 0)
            //{
                NumSift(TestArray,i, top);
                i = i - 1;
            //}
            i = top;
            //while (i > 0)
            //{
                NumSift(TestArray,0, i);
                temp = TestArray[0];
                TestArray[0] = TestArray[i];
                TestArray[i] = temp;
                i = i - 1;
            //}
        }

        public void JGFvalidate(dynamic TestArray, dynamic array_rows)
        {
            dynamic error = false;
            dynamic i = 1;
            while (i < array_rows)
            {
                error = (TestArray[i] < TestArray[i - 1]);
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
        public override void test()
        {
            dynamic size = 0;
           
            dynamic datasizes = new int[3];
            datasizes[0] = 1000000;
            datasizes[1] = 5000000;
            datasizes[2] = 25000000;
            
            dynamic array_rows = datasizes[size];
            dynamic TestArray = new int[array_rows];
            dynamic rndnum = new Random(1729);
            dynamic i = 0;
            while (i < array_rows)
            {
                TestArray[i] = rndnum.Next();
                i = i + 1;
            }
        
            JGFkernel(TestArray,array_rows);
            JGFvalidate(TestArray,array_rows);
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
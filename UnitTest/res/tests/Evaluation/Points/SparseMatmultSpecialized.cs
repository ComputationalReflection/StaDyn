using System;

namespace JG.SparseMatmult
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
            JGFSparseMatmultBench test = new JGFSparseMatmultBench();
            chronometer.Start();
            test.test();
            chronometer.Stop();
            this.microSeconds = this.microSeconds + chronometer.GetMicroSeconds();
            return null;
        }
    }


    public class JGFSparseMatmultBench
    {
        private object size;

        private static object datasizes_M;
        private static object datasizes_N;
        private static object datasizes_nz;
        private static object SPARSE_NUM_ITER = 10;
        public static object ytotal = 0.0;

        private object R;

        private object x;
        private object y;
        private object val;
        private object col;
        private object row;

        public JGFSparseMatmultBench()
        {
            JGFSparseMatmultBench.datasizes_M = new int[3];
            ((int[])JGFSparseMatmultBench.datasizes_M)[0] = 50000;
            ((int[])JGFSparseMatmultBench.datasizes_M)[1] = 100000;
            ((int[])JGFSparseMatmultBench.datasizes_M)[2] = 500000;

            JGFSparseMatmultBench.datasizes_N = new int[3];
            ((int[])JGFSparseMatmultBench.datasizes_N)[0] = 50000;
            ((int[])JGFSparseMatmultBench.datasizes_N)[1] = 100000;
            ((int[])JGFSparseMatmultBench.datasizes_N)[2] = 500000;

            JGFSparseMatmultBench.datasizes_nz = new int[3];
            ((int[])JGFSparseMatmultBench.datasizes_nz)[0] = 250000;
            ((int[])JGFSparseMatmultBench.datasizes_nz)[1] = 500000;
            ((int[])JGFSparseMatmultBench.datasizes_nz)[2] = 2500000;
			
			JGFSparseMatmultBench.SPARSE_NUM_ITER = 10;
			JGFSparseMatmultBench.ytotal = 0.0;
        }

        public void JGFsetsize(int size)
        {
            this.size = size;
        }

        public void JGFinitialise()
        {
            R = new Random(1010);

            x = RandomVector(((int[])JGFSparseMatmultBench.datasizes_N)[(int)size], (Random)R);
            y = new double[((int[])JGFSparseMatmultBench.datasizes_M)[(int)size]];

            val = new double[((int[])JGFSparseMatmultBench.datasizes_nz)[(int)size]];
            col = new int[((int[])JGFSparseMatmultBench.datasizes_nz)[(int)size]];
            row = new int[((int[])JGFSparseMatmultBench.datasizes_nz)[(int)size]];

            for (int i = 0; i < ((int[])JGFSparseMatmultBench.datasizes_nz)[(int)size]; i++)
            {
                int temp1 = ((Random)R).Next() % ((int[])JGFSparseMatmultBench.datasizes_M)[(int)size];
                int temp2 = ((Random)R).Next() % ((int[])JGFSparseMatmultBench.datasizes_N)[(int)size];
                ((int[])row)[i] = temp1 >= 0 ? temp1 : -1 * temp1;
                ((int[])col)[i] = temp2 >= 0 ? temp2 : -1 * temp2;
                ((double[])val)[i] = ((Random)R).NextDouble();
            }
        }

        public void JGFkernel()
        {
            for (int reps = 0; reps < (int)SPARSE_NUM_ITER; reps = reps + 1)
            {
                for (int i = 0; i < ((double[])val).Length; i = i + 1)
                {
                    ((double[])y)[((int[])row)[i]] = ((double[])y)[((int[])row)[i]] + (((double[])x)[((int[])col)[i]] * ((double[])val)[i]);
                }
            }
            for (int i = 0; i < ((double[])val).Length; i = i + 1)
            {
                ytotal = (double)ytotal + ((double[])y)[((int[])row)[i]];
            }
        }

        public void JGFvalidate()
        {
            double[] refval = new double[3];
            refval[0] = 75.02484945753453;
            refval[1] = 150.0130719633895;
            refval[2] = 749.5245870753752;
            double dev = (double)ytotal - refval[(int)size];
            dev = dev >= 0 ? dev : -1 * dev;
            if (dev > 1.0e-12)
            {
                //Console.WriteLine("Validation failed");
                //Console.WriteLine("ytotal = " + ytotal + "  " + dev + "  " + size);
            }
        }

        private static double[] RandomVector(int N, Random R)
        {
            double[] a = new double[N];
            for (int i = 0; i < N; i = i + 1)
                a[i] = R.NextDouble() * 1e-6;
            return a;
        }

        public void test()
        {
            JGFsetsize(0);
            JGFinitialise();
            JGFkernel();
            //JGFvalidate();
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
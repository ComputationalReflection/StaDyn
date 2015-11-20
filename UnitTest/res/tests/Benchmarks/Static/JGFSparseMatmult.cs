using System;

namespace CSGrande
{
    public class SparseMatmult
    {
        public static double ytotal = 0.0;

        public static void test(double[] y, double[] val, int[] row, int[] col, double[] x, int NUM_ITERATIONS)
        {
            int nz = val.Length;
            for (int reps = 0; reps < NUM_ITERATIONS; reps = reps + 1)
            {
                for (int i = 0; i < nz; i = i + 1)
                {
                    y[row[i]] += x[col[i]] * val[i];
                }
            }
            for (int i = 0; i < nz; i = i + 1)
            {
                ytotal += y[row[i]];
            }
        }
    }

    public class JGFSparseMatmultBench : SparseMatmult
    {
        private int size;

        private static int[] datasizes_M;
        private static int[] datasizes_N;
        private static int[] datasizes_nz;
        private static int SPARSE_NUM_ITER = 200;

        private Random R;

        private double[] x;
        private double[] y;
        private double[] val;
        private int[] col;
        private int[] row;

        public JGFSparseMatmultBench()
        {
            JGFSparseMatmultBench.datasizes_M = new int[3];
            JGFSparseMatmultBench.datasizes_M[0] = 50000;
            JGFSparseMatmultBench.datasizes_M[1] = 100000;
            JGFSparseMatmultBench.datasizes_M[2] = 500000;

            JGFSparseMatmultBench.datasizes_N = new int[3];
            JGFSparseMatmultBench.datasizes_N[0] = 50000;
            JGFSparseMatmultBench.datasizes_N[1] = 100000;
            JGFSparseMatmultBench.datasizes_N[2] = 500000;

            JGFSparseMatmultBench.datasizes_nz = new int[3];
            JGFSparseMatmultBench.datasizes_nz[0] = 250000;
            JGFSparseMatmultBench.datasizes_nz[1] = 500000;
            JGFSparseMatmultBench.datasizes_nz[2] = 2500000;
        }

        public void JGFsetsize(int size)
        {
            this.size = size;
        }

        public void JGFinitialise()
        {
            R = new Random(1010);

            x = RandomVector(datasizes_N[size], R);
            y = new double[datasizes_M[size]];

            val = new double[datasizes_nz[size]];
            col = new int[datasizes_nz[size]];
            row = new int[datasizes_nz[size]];

            for (int i = 0; i < datasizes_nz[size]; i++)
            {
                int temp1 = R.Next() % datasizes_M[size];
                int temp2 = R.Next() % datasizes_N[size];
                row[i] = temp1 >= 0 ? temp1 : -1 * temp1;
                col[i] = temp2 >= 0 ? temp2 : -1 * temp2;
                val[i] = R.NextDouble();
            }
        }

        public void JGFkernel()
        {
            SparseMatmult.test(y, val, row, col, x, SPARSE_NUM_ITER);
        }

        public void JGFvalidate()
        {
            double[] refval = new double[3];
            refval[0] = 75.02484945753453;
            refval[1] = 150.0130719633895;
            refval[2] = 749.5245870753752;
            double dev = ytotal - refval[size];
            dev = dev >= 0 ? dev : -1 * dev;
            if (dev > 1.0e-12)
            {
                //Console.WriteLine("Validation failed");
                Console.WriteLine("ytotal = " + ytotal + "  " + dev + "  " + size);
            }
        }

        public void JGFtidyup()
        {
            System.GC.Collect();
        }

        public void JGFrun()
        {
            JGFrun1(0);
        }

        public void JGFrun1(int size)
        {
            JGFsetsize(size);
            JGFinitialise();
            JGFkernel();
            JGFvalidate();
            JGFtidyup();
        }

        private static double[] RandomVector(int N, Random R)
        {
            double[] a = new double[N];
            for (int i = 0; i < N; i++)
                a[i] = R.NextDouble() * 1e-6;
            return a;
        }

        public static void Main()
        {
            JGFSparseMatmultBench smm = new JGFSparseMatmultBench();
            smm.JGFrun();
			Console.WriteLine("JavaGrande Static SparseMatmult completed!!");
        }
    }
}


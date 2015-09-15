using System;

namespace CSGrande
{
    public class JGFSparseMatmultBench 
    {
        private var size;

        private static var datasizes_M;
        private static var datasizes_N;
        private static var datasizes_nz;
        private static var SPARSE_NUM_ITER = 10;
		public static var ytotal = 0.0;

        private var R;

        private var x;
        private var y;
        private var val;
        private var col;
        private var row;
		
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

        public void JGFsetsize(var size)
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
                var temp1 = R.Next() % datasizes_M[size];
                var temp2 = R.Next() % datasizes_N[size];                            
                row[i] = temp1 >= 0 ? temp1 : -1 * temp1;
                col[i] = temp2 >= 0 ? temp2 : -1 * temp2;
                val[i] = R.NextDouble();                   
            }
        }

        public void JGFkernel()
        {			            
			for (var reps = 0; reps < SPARSE_NUM_ITER; reps = reps + 1)
            {				
                for (var i = 0; i < val.Length ; i = i + 1)
                {										
                    y[row[i]] += x[col[i]] * val[i];
                }
            }							
            for (var i = 0; i < val.Length ; i = i + 1)
            {
				ytotal += y[row[i]];                
            }			
        }

        public void JGFvalidate()
        {
            var refval = new double[3];
            refval[0] = 75.02484945753453;
            refval[1] = 150.0130719633895;
            refval[2] = 749.5245870753752;
            var dev = ytotal - refval[size];
            dev = dev >= 0 ? dev : -1 * dev;
            if (dev > 1.0e-12)
            {
                //Console.WriteLine("Validation failed");
                //Console.WriteLine("ytotal = " + ytotal + "  " + dev + "  " + size);
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

        public void JGFrun1(var size)
        {
            JGFsetsize(size);
            JGFinitialise();
            JGFkernel();
            JGFvalidate();
            JGFtidyup();
        }

        private static var RandomVector(var N, var R)
        {
            var a = new double[N];            
            for (var i = 0; i < N; i = i + 1)
                a[i] = R.NextDouble() * 1e-6;                
            return a;
        }

        public static void Main()
        {			
            var smm = new JGFSparseMatmultBench();
            smm.JGFrun();			
			Console.WriteLine("SparseMatmult completed!!");			
        }
    }
}


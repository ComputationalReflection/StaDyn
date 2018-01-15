using System;

namespace CSGrande
{
    public class FFT
    {

        public static double JDKtotal = 0.0;
        public static double JDKtotali = 0.0;


        public static void transform(double[] data)
        {
            int JDKrange;
            transform_internal(data, -1);
            JDKrange = data.Length;
            for (int i = 0; i < JDKrange; i = i + 1)
            {
                JDKtotal = JDKtotal + data[i];
            }
        }

        public static void inverse(double[] data)
        {
            transform_internal(data, +1);
            int nd = data.Length;
            int n = nd / 2;
            double norm = 1 / ((double)n);
            for (int i = 0; i < nd; i = i + 1)
                data[i] = data[i] * norm;
            for (int i = 0; i < nd; i = i + 1)
                JDKtotali = JDKtotali + data[i];
        }

        public static double test(double[] data)
        {
            int nd = data.Length;

            double[] copy = new double[nd];
            Array.Copy(data, 0, copy, 0, nd);
            transform(data);
            inverse(data);
            double diff = 0.0;
            for (int i = 0; i < nd; i = i + 1)
            {
                double d = data[i] - copy[i];
                diff = diff + (d * d);
            }
            return Math.Sqrt(diff / nd);
        }

        public static double[] makeRandom(int n)
        {
            Random rand = new Random();
            int nd = 2 * n;
            double[] data = new double[nd];
            for (int i = 0; i < nd; i = i + 1)
                data[i] = rand.NextDouble();
            return data;
        }

        protected static int log2(int n)
        {
            int log = 0;
            for (int k = 1; k < n; k = k * 2)
                log = log + 1;
            if (n != (1 << log))
                Console.Error.WriteLine("FFT: Data Length is not a power of 2!: " + n);
            return log;
        }

        protected static void transform_internal(double[] data, int direction)
        {
            int n = data.Length / 2;
            if (n == 1) return;
            int logn = log2(n);
            bitreverse(data);
            int dual = 1;
            for (int bit = 0; bit < logn; bit = bit + 1)
            {                            
                double w_real = 1.0;
                double w_imag = 0.0;
                double theta = 2.0 * direction * 3.14159265 / (2.0 * (double)dual);
                double s = Math.Sin(theta);
                double t = Math.Sin(theta / 2.0);
                double s2 = 2.0 * t * t;
                int iterations = 0;
                while (iterations < n)
                {                    
                    int i = 2 * iterations;
                    int j = 2 * (iterations + dual);
                    double wd_real = data[j];
                    double wd_imag = data[j + 1];                    
                    data[j] = data[i] - wd_real;
                    data[j + 1] = data[i + 1] - wd_imag;
                    data[i] = data[i] + wd_real;
                    data[i + 1] = data[i + 1] + wd_imag;                    
                    iterations = iterations + (2*dual);
                }             
                iterations = 1;
                while (iterations < dual)
                {
                    double tmp_real = w_real - s * w_imag - s2 * w_real;
                    double tmp_imag = w_imag + s * w_real - s2 * w_imag;
                    w_real = tmp_real;
                    w_imag = tmp_imag;

                    int b = 0;
                    while (b < n)
                    {                        
                        int i = 2 * (b + iterations);
                        int j = 2 * (b + iterations + dual);

                        double z1_real = data[j];
                        double z1_imag = data[j + 1];

                        double wd_real = w_real * z1_real - w_imag * z1_imag;
                        double wd_imag = w_real * z1_imag + w_imag * z1_real;

                        data[j] = data[i] - wd_real;
                        data[j + 1] = data[i + 1] - wd_imag;
                        data[i] = data[i] + wd_real;
                        data[i + 1] = data[i + 1] + wd_imag;
                        b = b + (2 * dual);
                    }                    
                    iterations = iterations + 1;
                }                
                dual = dual * 2;
            }
        }


        protected static void bitreverse(double[] data)
        {
            int n = data.Length / 2;
            int j = 0;
            for (int i = 0; i < n - 1; i = i + 1)
            {
                int ii = 2 * i;
                int jj = 2 * j;
                int k = n / 2;
                if (i < j)
                {
                    double tmp_real = data[ii];
                    double tmp_imag = data[ii + 1];
                    data[ii] = data[jj];
                    data[ii + 1] = data[jj + 1];
                    data[jj] = tmp_real;
                    data[jj + 1] = tmp_imag;
                }

                while (k <= j)
                {
                    j = j - k;
                    k = k / 2;
                }
                j = j + k;
            }
        }
    }

    public class JGFFFTBench : FFT
    {

        private int size;
        private int[] datasizes;

        public JGFFFTBench()
        {
            datasizes = new int[3];
            datasizes[0] = 2097152;
            datasizes[1] = 8388608;
            datasizes[2] = 16777216;
        }

        Random R = new Random();

        public void JGFsetsize(int size)
        {
            this.size = size;
        }

        public void JGFinitialise()
        {

        }

        public void JGFkernel()
        {
            double[] x = RandomVector(2 * (datasizes[size]), R);
            FFT.transform(x);
            FFT.inverse(x);
        }

        public void JGFvalidate()
        {
            double[] refval = new double[3];
            refval[0] = 1.726962988395339;
            refval[1] = 6.907851953579193;
            refval[2] = 13.815703907167297;
            double[] refvali = new double[3];
            refvali[0] = 2.0974756152524314;
            refvali[1] = 8.389142211032294;
            refvali[2] = 16.778094422092604;

            double dev = Math.Abs(JDKtotal - refval[size]);
            double devi = Math.Abs(JDKtotali - refvali[size]);
            if (dev > 1.0e-12)
            {
                Console.WriteLine("Validation failed");
                Console.WriteLine("JDKtotal = " + JDKtotal + "  " + dev + "  " + size);
            }
            if (devi > 1.0e-12)
            {
                Console.WriteLine("Validation failed");
                Console.WriteLine("JDKtotalinverse = " + JDKtotali + "  " + dev + "  " + size);
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

        private static double[] RandomVector(int N, System.Random R)
        {
            double[] A = new double[N];
            for (int i = 0; i < N; i = i + 1)
                //A[i] = R.NextDouble() * 1e-6;
                A[i] = 0.38670253957933864 * 1e-6;
            return A;
        }


        public static void Main()
        {
            JGFFFTBench fft = new JGFFFTBench();
            fft.JGFrun();

        }

    }
}
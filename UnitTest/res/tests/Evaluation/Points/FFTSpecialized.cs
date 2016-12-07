using System;

namespace JG
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
            JGFFFTBench test = new JGFFFTBench();						
			chronometer.Start();				
			test.test();
			chronometer.Stop();			
			this.microSeconds = this.microSeconds + chronometer.GetMicroSeconds();
			return null;
		}
	}
	
	public class JGFFFTBench
    {

        private object size;
        private object datasizes;
		private object R;

        public JGFFFTBench()
        {
            datasizes = new int[3];
            //((int[])datasizes)[0] = 2097152;
            ((int[])datasizes)[0] = 65536;
            ((int[])datasizes)[1] = 8388608;
            ((int[])datasizes)[2] = 16777216;
        }        

        public void JGFsetsize(int size)
        {
            this.size = size;
        }

        public void JGFinitialise()
        {
			R = new Random(1010);
			FFT.JDKtotal = 0.0;
			FFT.JDKtotali = 0.0;
        }

        public void JGFkernel()
        {
            double[] x = JGFFFTBench.RandomVector(2 * ((int[])datasizes)[(int)size], (Random)R);
            FFT.transform(x);			
            FFT.inverse(x);						
        }
		
		private static double[] RandomVector(int N, Random R)
        {
            double[] a = new double[N];
            for (int i = 0; i < N; i = i + 1)
                a[i] = R.NextDouble() * 1e-6;        
            return a;
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
            double dev = (double)FFT.JDKtotal - refval[(int)size];
			dev = dev>=0?dev:(-1*dev);
            double devi = (double)FFT.JDKtotali - refvali[(int)size];
			devi = devi>=0?devi:(-1*devi);
            if (dev > 1.0e-12)
            {
                //Console.WriteLine("Validation failed");
                //Console.WriteLine("JDKtotal = {0} {1} {2}",FFT.JDKtotal,dev,size);
            }
            if (devi > 1.0e-12)
            {
                //Console.WriteLine("Validation failed");
                //Console.WriteLine("JDKtotalinverse = {0} {1} {2}",FFT.JDKtotali,devi,size);
            }
        }
		
		public void test()
		{
            JGFsetsize(0);
            JGFinitialise();
            JGFkernel();			
            JGFvalidate();            
        }
    }
	
	public class FFT
    {
        public static object JDKtotal;
        public static object JDKtotali;

        public static void transform(double[] data)
        {			
            int JDKrange;
            transform_internal(data, -1);
            JDKrange = data.Length;
            for (int i = 0; i < JDKrange; i = i + 1)            
                FFT.JDKtotal = ((double)FFT.JDKtotal) + data[i];   					
        }

        public static void inverse(double[] data)
        {
            transform_internal(data, 1);
            int nd = data.Length;
            int n = nd / 2;
            double norm = 1 / ((double)n);
            for (int i = 0; i < nd; i = i + 1)
                data[i] = data[i] * norm;
            for (int i = 0; i < nd; i = i + 1)
                FFT.JDKtotali = ((double)FFT.JDKtotali) + data[i];
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
			int k = 1; 				
			while(k < n)
			{									
				k = k * 2;								
				log = log + 1;								
			}			
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
			int bit = 0;
			while(bit < logn)            
            {                            
                double w_real = 1.0;
                double w_imag = 0.0;
                double theta = 2.0 * direction * 3.14159265 / (2.0 * dual);
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
                    double tmp_real = w_real - (s * w_imag) - (s2 * w_real);
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
				bit = bit + 1;
            }
        }

        protected static void bitreverse(double[] data)
        {			
            int n = data.Length / 2;
            int j = 0;
			int i = 0;					
            while(i < (n - 1))
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
				i = i + 1;												
            }						
        }
    }
	
	public class Program 
	{
		public static void Main(string[] args)
		{		    
			if (args.Length<1) {
				Console.Error.WriteLine("You must pass the number of thousands iterations.");
					System.Environment.Exit(-1);
			}
			int iterations = Convert.ToInt32(args[0]);
            BenchMark arith = new BenchMark(iterations);
			Console.WriteLine(arith.run());
		}
    }
}
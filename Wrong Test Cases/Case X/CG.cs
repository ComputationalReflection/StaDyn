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

		virtual public object runOneIteration() { return null; }
	}
	
	public class ArithmethicBenchmark : BenchMark 
	{
		public ArithmethicBenchmark(int iterations) : base(iterations) { }	
		public override object runOneIteration() 
		{				
			Chronometer chronometer = new Chronometer();
			Test test = new JGFFFTBench();						
			chronometer.Start();				
			test.test();
			chronometer.Stop();			
			this.microSeconds = this.microSeconds + chronometer.GetMicroSeconds();
			return null;
		}
	}
	
	public abstract class Test {
        public abstract void test();
    }

	public class JGFFFTBench : Test
    {

        private var size;
        private var datasizes;
		//private var R;

        public JGFFFTBench()
        {
            datasizes = new int[3];            
			//datasizes[0] = 2097152;
			datasizes[0] = 65536;
            datasizes[1] = 8388608;
            datasizes[2] = 16777216;
        }        

        public void JGFsetsize(var size)
        {
            this.size = size;
        }

        public void JGFinitialise()
        {
			//R = new Random(1010);
        }

        public void JGFkernel()
        {
			var R = new Random(1010);
            var x = JGFFFTBench.RandomVector(2 * (datasizes[size]), R);
            FFT.transform(x);			
            FFT.inverse(x);						
        }
		
		private static var RandomVector(var N, var R)
        {
            var a = new double[N];
            for (var i = 0; i < N; i = i + 1)
                a[i] = R.NextDouble() * 1e-6;        
            return a;
        }

        public void JGFvalidate()
        {			
            var refval = new double[3];
            refval[0] = 1.726962988395339;
            refval[1] = 6.907851953579193;
            refval[2] = 13.815703907167297;
            var refvali = new double[3];
            refvali[0] = 2.0974756152524314;
            refvali[1] = 8.389142211032294;
            refvali[2] = 16.778094422092604;		
            var dev = FFT.JDKtotal - refval[size];
			dev = dev>=0?dev:(-1*dev);
            var devi = FFT.JDKtotali - refvali[size];
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
		
		public override void test()
		{
            JGFsetsize(0);
            JGFinitialise();
            JGFkernel();			
            JGFvalidate();            
        }
    }
	
	public class FFT
    {
        public static var JDKtotal = 0.0;
        public static var JDKtotali = 0.0;

        public static void transform(var data)
        {
            var JDKrange;
            transform_internal(data, -1);
            JDKrange = data.Length;
            for (var i = 0; i < JDKrange; i = i + 1)            
                JDKtotal = JDKtotal + data[i];   					
        }

        public static void inverse(var data)
        {
            transform_internal(data, +1);
            var nd = data.Length;
            var n = nd / 2;
            var norm = 1 / ((double)n);
            for (var i = 0; i < nd; i = i + 1)
                data[i] = data[i] * norm;
            for (var i = 0; i < nd; i = i + 1)
                JDKtotali = JDKtotali + data[i];
        }

        public static var makeRandom(var n)
        {
            var rand = new Random();
            var nd = 2 * n;
            var data = new double[nd];
            for (var i = 0; i < nd; i = i + 1)
                data[i] = rand.NextDouble();
            return data;
        }

        protected static var log2(var n)
        {
            var log = 0;
			var k = 1; 				
			while(k < n)
			{									
				k = k * 2;								
				log = log + 1;								
			}			
            if (n != (1 << log))
                Console.Error.WriteLine("FFT: Data Length is not a power of 2!: " + n);
            return log;
        }

        protected static void transform_internal(var data, var direction)
        {			
            int n = data.Length / 2;
            if (n == 1) return;			
            var logn = log2(n);			
            bitreverse(data);							
            var dual = 1;	
			var bit = 0;
			while(bit < logn)            
            {                            
                var w_real = 1.0;
                var w_imag = 0.0;
                double theta = 2.0 * direction * 3.14159265 / (2.0 * dual);
                var s = Math.Sin(theta);
                var t = Math.Sin(theta / 2.0);
                var s2 = 2.0 * t * t;
                var iterations = 0;											
                while (iterations < n)
                {                    
                    var i = 2 * iterations;
                    var j = 2 * (iterations + dual);
                    var wd_real = data[j];
                    var wd_imag = data[j + 1];                    
                    data[j] = data[i] - wd_real;
                    data[j + 1] = data[i + 1] - wd_imag;
                    data[i] = data[i] + wd_real;
                    data[i + 1] = data[i + 1] + wd_imag;                    
                    iterations = iterations + (2*dual);
                }             
                iterations = 1;
                while (iterations < dual)
                {
                    var tmp_real = w_real - s * w_imag - s2 * w_real;
                    var tmp_imag = w_imag + s * w_real - s2 * w_imag;
                    w_real = tmp_real;
                    w_imag = tmp_imag;

                    var b = 0;
                    while (b < n)
                    {                        
                        var i = 2 * (b + iterations);
                        var j = 2 * (b + iterations + dual);

                        var z1_real = data[j];
                        var z1_imag = data[j + 1];

                        var wd_real = w_real * z1_real - w_imag * z1_imag;
                        var wd_imag = w_real * z1_imag + w_imag * z1_real;

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

        protected static void bitreverse(var data)
        {			
            var n = data.Length / 2;
            var j = 0;
			var i = 0;					
            while(i < (n - 1))
            {			
                var ii = 2 * i;
                var jj = 2 * j;
                var k = n / 2;
                if (i < j)
                {											
                    var tmp_real = data[ii];										
                    var tmp_imag = data[ii + 1];
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
			ArithmethicBenchmark arith = new ArithmethicBenchmark(1);
			Console.WriteLine(arith.run());
		}
    }
}
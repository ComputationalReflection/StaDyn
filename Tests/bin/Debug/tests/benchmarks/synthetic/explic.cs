
using System;
using System.Collections;

class Cronometro {
    private DateTime ticks1, ticks2;
    private bool stopped;

    public void start() {
        ticks1 = DateTime.Now;
        stopped = false;
    }
    public void stop() {
        ticks2 = DateTime.Now;
        stopped = true;
    }

    private static int ticksToMicroSeconds(DateTime t1, DateTime t2) {
        TimeSpan difference = t2.Subtract(t1);
        return (difference.Milliseconds + difference.Seconds * 1000 + difference.Minutes * 60000) * 1000;
    }

    public int getMicroSeconds() {
        if (stopped)
            return ticksToMicroSeconds(ticks1, ticks2);
        return ticksToMicroSeconds(ticks1, DateTime.Now);
    }
}

class BenchMark {
    private int iterations;
    protected int microSeconds;

    public BenchMark(int iterations) {
        this.iterations = iterations;
    }

    public int run() {
        BenchMark self = this;
		this.microSeconds = 0;
        for (int i = 0; i < iterations; i++)
            self.runOneIteration();
        return this.microSeconds;
    }

    virtual public object runOneIteration() { return null; }
}

class ArithmethicBenchmark : BenchMark {

      private Random random;
    public ArithmethicBenchmark(int iterations)
        : base(iterations)
    {
        random = new Random(DateTime.Now.Millisecond);
    }

override public object runOneIteration() { 
		int number = random.Next(1); 
 
		Cronometro cronometro = new Cronometro();
		object value;
		Derived0 obj = new Derived0();
        cronometro.start();
        for (int i = 0; i < 1000; i++)
            value = obj.m();
        cronometro.stop();
        this.microSeconds = this.microSeconds + cronometro.getMicroSeconds();

        return value;
    }

 }  
 
class Program {
	public static void Main(string[] args)
    {
        ArithmethicBenchmark arith = new ArithmethicBenchmark(1000);
        arith.run();        
    }
}

class Interface {
    public virtual string m() { return null; }
}
class Derived0 : Interface  {
        public override string m() {
                return "derived0"; } 
 } 

using System; using System.Collections; class Cronometro {    private DateTime ticks1, ticks2;    private bool stopped;    public void start() {        ticks1 = DateTime.Now;        stopped = false;    }    public void stop() {        ticks2 = DateTime.Now;        stopped = true;    }    private static int ticksToMicroSeconds(DateTime t1, DateTime t2) {        TimeSpan difference = t2.Subtract(t1);        return (difference.Milliseconds + difference.Seconds * 1000 + difference.Minutes * 60000) * 1000;    }    public int getMicroSeconds() {        if (stopped)            return ticksToMicroSeconds(ticks1, ticks2);        return ticksToMicroSeconds(ticks1, DateTime.Now);    }}class BenchMark {    private int iterations;    protected int microSeconds;    public BenchMark(int iterations) {        this.iterations = iterations;    }    public int run() {        BenchMark self = this; this.microSeconds = 0; for (int i = 0; i < iterations; i++)            self.runOneIteration();        return this.microSeconds;    }    virtual public object runOneIteration() { return null; }}class ArithmethicBenchmark : BenchMark {    public ArithmethicBenchmark(int iterations)        : base(iterations) {    }    static private Random random = new Random();override public object runOneIteration() { 
int number = random.Next(5); 
Cronometro cronometro = new Cronometro(); object value; var obj;if (number == 0) obj = new Derived0(); else if (number==1) obj=new Derived1(); else if (number==2) obj=new Derived2(); else if (number==3) obj=new Derived3();		else obj=new Derived4();
cronometro.start();for (int i = 0; i < 1000; i++)value = obj.m(); cronometro.stop();this.microSeconds = this.microSeconds + cronometro.getMicroSeconds();return value;}
 }  
 class Program{	
 	public static void Main(string[] args)
    {
        ArithmethicBenchmark arith = new ArithmethicBenchmark(1000);
        arith.run();        
    }
}

	
	
	
class Derived0 { public string m() {return "derived0"; } 
 } 
class Derived1 { public string m() {return "derived1"; } 
 } 
class Derived2 { public string m() {return "derived2"; } 
 } 
class Derived3 { public string m() {return "derived3"; } 
 } 
class Derived4 { public string m() {return "derived4"; } 
 } 


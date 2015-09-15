
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

    object runWithNoInference(var obj) {
        Cronometro cronometro = new Cronometro();
        object value;
        cronometro.start();
		for (int i=0;i<1000;i++)
	        value = obj.m();
        cronometro.stop();
        this.microSeconds = this.microSeconds + cronometro.getMicroSeconds();
        return value;
    }

override public object runOneIteration() { 
			int number = random.Next(100); 
 
			var obj;
            if (number == 0)
                obj = new Derived0();
            
            else if (number==1)
                obj=new Derived1();

            else if (number==2)
                obj=new Derived2();

            else if (number==3)
                obj=new Derived3();

            else if (number==4)
                obj=new Derived4();

            else if (number==5)
                obj=new Derived5();

            else if (number==6)
                obj=new Derived6();

            else if (number==7)
                obj=new Derived7();

            else if (number==8)
                obj=new Derived8();

            else if (number==9)
                obj=new Derived9();

            else if (number==10)
                obj=new Derived10();

            else if (number==11)
                obj=new Derived11();

            else if (number==12)
                obj=new Derived12();

            else if (number==13)
                obj=new Derived13();

            else if (number==14)
                obj=new Derived14();

            else if (number==15)
                obj=new Derived15();

            else if (number==16)
                obj=new Derived16();

            else if (number==17)
                obj=new Derived17();

            else if (number==18)
                obj=new Derived18();

            else if (number==19)
                obj=new Derived19();

            else if (number==20)
                obj=new Derived20();

            else if (number==21)
                obj=new Derived21();

            else if (number==22)
                obj=new Derived22();

            else if (number==23)
                obj=new Derived23();

            else if (number==24)
                obj=new Derived24();

            else if (number==25)
                obj=new Derived25();

            else if (number==26)
                obj=new Derived26();

            else if (number==27)
                obj=new Derived27();

            else if (number==28)
                obj=new Derived28();

            else if (number==29)
                obj=new Derived29();

            else if (number==30)
                obj=new Derived30();

            else if (number==31)
                obj=new Derived31();

            else if (number==32)
                obj=new Derived32();

            else if (number==33)
                obj=new Derived33();

            else if (number==34)
                obj=new Derived34();

            else if (number==35)
                obj=new Derived35();

            else if (number==36)
                obj=new Derived36();

            else if (number==37)
                obj=new Derived37();

            else if (number==38)
                obj=new Derived38();

            else if (number==39)
                obj=new Derived39();

            else if (number==40)
                obj=new Derived40();

            else if (number==41)
                obj=new Derived41();

            else if (number==42)
                obj=new Derived42();

            else if (number==43)
                obj=new Derived43();

            else if (number==44)
                obj=new Derived44();

            else if (number==45)
                obj=new Derived45();

            else if (number==46)
                obj=new Derived46();

            else if (number==47)
                obj=new Derived47();

            else if (number==48)
                obj=new Derived48();

            else if (number==49)
                obj=new Derived49();

            else if (number==50)
                obj=new Derived50();

            else if (number==51)
                obj=new Derived51();

            else if (number==52)
                obj=new Derived52();

            else if (number==53)
                obj=new Derived53();

            else if (number==54)
                obj=new Derived54();

            else if (number==55)
                obj=new Derived55();

            else if (number==56)
                obj=new Derived56();

            else if (number==57)
                obj=new Derived57();

            else if (number==58)
                obj=new Derived58();

            else if (number==59)
                obj=new Derived59();

            else if (number==60)
                obj=new Derived60();

            else if (number==61)
                obj=new Derived61();

            else if (number==62)
                obj=new Derived62();

            else if (number==63)
                obj=new Derived63();

            else if (number==64)
                obj=new Derived64();

            else if (number==65)
                obj=new Derived65();

            else if (number==66)
                obj=new Derived66();

            else if (number==67)
                obj=new Derived67();

            else if (number==68)
                obj=new Derived68();

            else if (number==69)
                obj=new Derived69();

            else if (number==70)
                obj=new Derived70();

            else if (number==71)
                obj=new Derived71();

            else if (number==72)
                obj=new Derived72();

            else if (number==73)
                obj=new Derived73();

            else if (number==74)
                obj=new Derived74();

            else if (number==75)
                obj=new Derived75();

            else if (number==76)
                obj=new Derived76();

            else if (number==77)
                obj=new Derived77();

            else if (number==78)
                obj=new Derived78();

            else if (number==79)
                obj=new Derived79();

            else if (number==80)
                obj=new Derived80();

            else if (number==81)
                obj=new Derived81();

            else if (number==82)
                obj=new Derived82();

            else if (number==83)
                obj=new Derived83();

            else if (number==84)
                obj=new Derived84();

            else if (number==85)
                obj=new Derived85();

            else if (number==86)
                obj=new Derived86();

            else if (number==87)
                obj=new Derived87();

            else if (number==88)
                obj=new Derived88();

            else if (number==89)
                obj=new Derived89();

            else if (number==90)
                obj=new Derived90();

            else if (number==91)
                obj=new Derived91();

            else if (number==92)
                obj=new Derived92();

            else if (number==93)
                obj=new Derived93();

            else if (number==94)
                obj=new Derived94();

            else if (number==95)
                obj=new Derived95();

            else if (number==96)
                obj=new Derived96();

            else if (number==97)
                obj=new Derived97();

            else if (number==98)
                obj=new Derived98();

            else
                obj=new Derived99();

        return this.runWithNoInference(obj);
    }

 }  
 
class Program {
	public static void Main(string[] args)
    {
        ArithmethicBenchmark arith = new ArithmethicBenchmark(1);
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
class Derived1 : Interface  {
        public override string m() {
                return "derived1"; } 
 } 
class Derived2 : Interface  {
        public override string m() {
                return "derived2"; } 
 } 
class Derived3 : Interface  {
        public override string m() {
                return "derived3"; } 
 } 
class Derived4 : Interface  {
        public override string m() {
                return "derived4"; } 
 } 
class Derived5 : Interface  {
        public override string m() {
                return "derived5"; } 
 } 
class Derived6 : Interface  {
        public override string m() {
                return "derived6"; } 
 } 
class Derived7 : Interface  {
        public override string m() {
                return "derived7"; } 
 } 
class Derived8 : Interface  {
        public override string m() {
                return "derived8"; } 
 } 
class Derived9 : Interface  {
        public override string m() {
                return "derived9"; } 
 } 
class Derived10 : Interface  {
        public override string m() {
                return "derived10"; } 
 } 
class Derived11 : Interface  {
        public override string m() {
                return "derived11"; } 
 } 
class Derived12 : Interface  {
        public override string m() {
                return "derived12"; } 
 } 
class Derived13 : Interface  {
        public override string m() {
                return "derived13"; } 
 } 
class Derived14 : Interface  {
        public override string m() {
                return "derived14"; } 
 } 
class Derived15 : Interface  {
        public override string m() {
                return "derived15"; } 
 } 
class Derived16 : Interface  {
        public override string m() {
                return "derived16"; } 
 } 
class Derived17 : Interface  {
        public override string m() {
                return "derived17"; } 
 } 
class Derived18 : Interface  {
        public override string m() {
                return "derived18"; } 
 } 
class Derived19 : Interface  {
        public override string m() {
                return "derived19"; } 
 } 
class Derived20 : Interface  {
        public override string m() {
                return "derived20"; } 
 } 
class Derived21 : Interface  {
        public override string m() {
                return "derived21"; } 
 } 
class Derived22 : Interface  {
        public override string m() {
                return "derived22"; } 
 } 
class Derived23 : Interface  {
        public override string m() {
                return "derived23"; } 
 } 
class Derived24 : Interface  {
        public override string m() {
                return "derived24"; } 
 } 
class Derived25 : Interface  {
        public override string m() {
                return "derived25"; } 
 } 
class Derived26 : Interface  {
        public override string m() {
                return "derived26"; } 
 } 
class Derived27 : Interface  {
        public override string m() {
                return "derived27"; } 
 } 
class Derived28 : Interface  {
        public override string m() {
                return "derived28"; } 
 } 
class Derived29 : Interface  {
        public override string m() {
                return "derived29"; } 
 } 
class Derived30 : Interface  {
        public override string m() {
                return "derived30"; } 
 } 
class Derived31 : Interface  {
        public override string m() {
                return "derived31"; } 
 } 
class Derived32 : Interface  {
        public override string m() {
                return "derived32"; } 
 } 
class Derived33 : Interface  {
        public override string m() {
                return "derived33"; } 
 } 
class Derived34 : Interface  {
        public override string m() {
                return "derived34"; } 
 } 
class Derived35 : Interface  {
        public override string m() {
                return "derived35"; } 
 } 
class Derived36 : Interface  {
        public override string m() {
                return "derived36"; } 
 } 
class Derived37 : Interface  {
        public override string m() {
                return "derived37"; } 
 } 
class Derived38 : Interface  {
        public override string m() {
                return "derived38"; } 
 } 
class Derived39 : Interface  {
        public override string m() {
                return "derived39"; } 
 } 
class Derived40 : Interface  {
        public override string m() {
                return "derived40"; } 
 } 
class Derived41 : Interface  {
        public override string m() {
                return "derived41"; } 
 } 
class Derived42 : Interface  {
        public override string m() {
                return "derived42"; } 
 } 
class Derived43 : Interface  {
        public override string m() {
                return "derived43"; } 
 } 
class Derived44 : Interface  {
        public override string m() {
                return "derived44"; } 
 } 
class Derived45 : Interface  {
        public override string m() {
                return "derived45"; } 
 } 
class Derived46 : Interface  {
        public override string m() {
                return "derived46"; } 
 } 
class Derived47 : Interface  {
        public override string m() {
                return "derived47"; } 
 } 
class Derived48 : Interface  {
        public override string m() {
                return "derived48"; } 
 } 
class Derived49 : Interface  {
        public override string m() {
                return "derived49"; } 
 } 
class Derived50 : Interface  {
        public override string m() {
                return "derived50"; } 
 } 
class Derived51 : Interface  {
        public override string m() {
                return "derived51"; } 
 } 
class Derived52 : Interface  {
        public override string m() {
                return "derived52"; } 
 } 
class Derived53 : Interface  {
        public override string m() {
                return "derived53"; } 
 } 
class Derived54 : Interface  {
        public override string m() {
                return "derived54"; } 
 } 
class Derived55 : Interface  {
        public override string m() {
                return "derived55"; } 
 } 
class Derived56 : Interface  {
        public override string m() {
                return "derived56"; } 
 } 
class Derived57 : Interface  {
        public override string m() {
                return "derived57"; } 
 } 
class Derived58 : Interface  {
        public override string m() {
                return "derived58"; } 
 } 
class Derived59 : Interface  {
        public override string m() {
                return "derived59"; } 
 } 
class Derived60 : Interface  {
        public override string m() {
                return "derived60"; } 
 } 
class Derived61 : Interface  {
        public override string m() {
                return "derived61"; } 
 } 
class Derived62 : Interface  {
        public override string m() {
                return "derived62"; } 
 } 
class Derived63 : Interface  {
        public override string m() {
                return "derived63"; } 
 } 
class Derived64 : Interface  {
        public override string m() {
                return "derived64"; } 
 } 
class Derived65 : Interface  {
        public override string m() {
                return "derived65"; } 
 } 
class Derived66 : Interface  {
        public override string m() {
                return "derived66"; } 
 } 
class Derived67 : Interface  {
        public override string m() {
                return "derived67"; } 
 } 
class Derived68 : Interface  {
        public override string m() {
                return "derived68"; } 
 } 
class Derived69 : Interface  {
        public override string m() {
                return "derived69"; } 
 } 
class Derived70 : Interface  {
        public override string m() {
                return "derived70"; } 
 } 
class Derived71 : Interface  {
        public override string m() {
                return "derived71"; } 
 } 
class Derived72 : Interface  {
        public override string m() {
                return "derived72"; } 
 } 
class Derived73 : Interface  {
        public override string m() {
                return "derived73"; } 
 } 
class Derived74 : Interface  {
        public override string m() {
                return "derived74"; } 
 } 
class Derived75 : Interface  {
        public override string m() {
                return "derived75"; } 
 } 
class Derived76 : Interface  {
        public override string m() {
                return "derived76"; } 
 } 
class Derived77 : Interface  {
        public override string m() {
                return "derived77"; } 
 } 
class Derived78 : Interface  {
        public override string m() {
                return "derived78"; } 
 } 
class Derived79 : Interface  {
        public override string m() {
                return "derived79"; } 
 } 
class Derived80 : Interface  {
        public override string m() {
                return "derived80"; } 
 } 
class Derived81 : Interface  {
        public override string m() {
                return "derived81"; } 
 } 
class Derived82 : Interface  {
        public override string m() {
                return "derived82"; } 
 } 
class Derived83 : Interface  {
        public override string m() {
                return "derived83"; } 
 } 
class Derived84 : Interface  {
        public override string m() {
                return "derived84"; } 
 } 
class Derived85 : Interface  {
        public override string m() {
                return "derived85"; } 
 } 
class Derived86 : Interface  {
        public override string m() {
                return "derived86"; } 
 } 
class Derived87 : Interface  {
        public override string m() {
                return "derived87"; } 
 } 
class Derived88 : Interface  {
        public override string m() {
                return "derived88"; } 
 } 
class Derived89 : Interface  {
        public override string m() {
                return "derived89"; } 
 } 
class Derived90 : Interface  {
        public override string m() {
                return "derived90"; } 
 } 
class Derived91 : Interface  {
        public override string m() {
                return "derived91"; } 
 } 
class Derived92 : Interface  {
        public override string m() {
                return "derived92"; } 
 } 
class Derived93 : Interface  {
        public override string m() {
                return "derived93"; } 
 } 
class Derived94 : Interface  {
        public override string m() {
                return "derived94"; } 
 } 
class Derived95 : Interface  {
        public override string m() {
                return "derived95"; } 
 } 
class Derived96 : Interface  {
        public override string m() {
                return "derived96"; } 
 } 
class Derived97 : Interface  {
        public override string m() {
                return "derived97"; } 
 } 
class Derived98 : Interface  {
        public override string m() {
                return "derived98"; } 
 } 
class Derived99 : Interface  {
        public override string m() {
                return "derived99"; } 
 } 


using System;
using System.Collections;


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

    public class Vec
    {
        public object x;
        public object y;
        public object z;
        public Vec(double a, double b, double c)
        {
            x = a;
            y = b;
            z = c;
        }

        public Vec()
        {
            x = 0.0;
            y = 0.0;
            z = 0.0;
        }

        public void add1(Vec a)
        {

            x = (double)a.x + (double)this.x;
            y = (double)a.y + (double)this.y;
            z = (double)a.z + (double)this.z;
        }

        public static Vec adds(double s, Vec a, Vec b)
        {
            return new Vec(s * (double)a.x + (double)b.x, s * (double)a.y + (double)b.y, s * (double)a.z + (double)b.z);
        }

        public void adds(double s, Vec b)
        {
            this.x = (double)this.x + (s * (double)b.x);
            this.y = (double)this.y + (s * (double)b.y);
            this.z = (double)this.z + (s * (double)b.z);
        }

        public static Vec sub1(Vec a, Vec b)
        {
            return new Vec((double)a.x - (double)b.x, (double)a.y - (double)b.y, (double)a.z - (double)b.z);
        }

        public void sub2(Vec a, Vec b)
        {
            this.x = (double)a.x - (double)b.x;
            this.y = (double)a.y - (double)b.y;
            this.z = (double)a.z - (double)b.z;
        }

        public static Vec mult1(Vec a, Vec b)
        {
            return new Vec((double)a.x * (double)b.x, (double)a.y * (double)b.y, (double)a.z * (double)b.z);
        }

        public static Vec cross(Vec a, Vec b)
        {
            return new Vec((double)a.y * (double)b.z - (double)a.z * (double)b.y, (double)a.z * (double)b.x - (double)a.x * (double)b.z, (double)a.x * (double)b.y - (double)a.y * (double)b.x);
        }

        public static double dot(Vec a, Vec b)
        {
            return (double)a.x * (double)b.x + (double)a.y * (double)b.y + (double)a.z * (double)b.z;
        }

        public static Vec comb(double a, Vec A, double b, Vec B)
        {
            return new Vec(a * (double)A.x + b * (double)B.x, a * (double)A.y + b * (double)B.y, a * (double)A.z + b * (double)B.z);
        }

        public void comb2(double a, Vec A, double b, Vec B)
        {
            x = a * (double)A.x + b * (double)B.x;
            y = a * (double)A.y + b * (double)B.y;
            z = a * (double)A.z + b * (double)B.z;
        }

        public void scale(double t)
        {
            x = t * (double)x;
            y = t * (double)y;
            z = t * (double)z;
        }

        public void negate()
        {
            x = -1.0 * (double)x;
            y = -1.0 * (double)y;
            z = -1.0 * (double)z;
        }

        public double normalize()
        {
            double len = Math.Sqrt((double)x * (double)x + (double)y * (double)y + (double)z * (double)z);
            while (len > 0.0)
            {
                x = (double)x / len;
                y = (double)y / len;
                z = (double)z / len;
                len = 0.0;
            }
            return len;
        }

        public override String ToString()
        {
            return "<" + (double)x + "," + (double)y + "," + (double)z + ">";
        }
    }

    public class View
    {
        public object fromVec;
        public object atVec;
        public object upVec;
        public object dist;
        public object angle;
        public object aspect;

        public View(Vec fromVec, Vec atVec, Vec upVec, double dist, double angle, double aspect)
        {
            this.fromVec = fromVec;
            this.atVec = atVec;
            this.upVec = upVec;
            this.dist = dist;
            this.angle = angle;
            this.aspect = aspect;
        }
    }

    public class Ray
    {
        public object P;
        public object D;

        public Ray(Vec pnt, Vec dir)
        {
            P = new Vec();
            ((Vec)P).add1(pnt);
            D = new Vec();
            ((Vec)D).add1(dir);
            ((Vec)D).normalize();
        }

        public Ray()
        {
            P = new Vec();
            D = new Vec();
        }

        public Vec point(double t)
        {
            return new Vec((double)((Vec)P).x + (double)((Vec)D).x * t, (double)((Vec)P).y + (double)((Vec)D).y * t, (double)((Vec)P).z + (double)((Vec)D).z * t);
        }

        public override String ToString()
        {
            return "{" + P.ToString() + " -> " + D.ToString() + "}";
        }
    }

    public class Interval
    {
        public object number;
        public object width;
        public object height;
        public object yfrom;
        public object yto;
        public object total;

        public Interval(int number, int width, int height, int yfrom, int yto, int total)
        {
            this.number = number;
            this.width = width;
            this.height = height;
            this.yfrom = yfrom;
            this.yto = yto;
            this.total = total;
        }
    }

    public class Isect
    {
        public object t;
        public object enter;
        public object prim;
        public object surf;

        public override String ToString()
        {
            return "{" + t.ToString() + "," + enter.ToString() + "," + prim.ToString() + "," + surf.ToString() + "}";
        }
    }

    public class Light
    {
        public object pos;
        public object brightness;

        public Light() { }

        public Light(double x, double y, double z, double brightness)
        {
            this.pos = new Vec(x, y, z);
            this.brightness = brightness;
        }
    }

    public class Surface
    {
        public object color;
        public object kd;
        public object ks;
        public object shine;
        public object kt;
        public object ior;

        public Surface()
        {
            color = new Vec(1.0, 0.0, 0.0);
            kd = 1.0;
            ks = 0.0;
            shine = 0.0;
            kt = 0.0;
            ior = 1.0;
        }

        public override String ToString()
        {
            return "Surface { color=" + color.ToString() + "}";
        }
    }

    public abstract class Primitive
    {
        public object surf;

        public Primitive()
        {
            surf = new Surface();
        }

        public void setColor(double r, double g, double b)
        {
            ((Surface)surf).color = new Vec(r, g, b);
        }

        public abstract Isect intersect(Ray ry);
        public abstract Vec normal(Vec pnt);
    }

    public class Sphere : Primitive
    {
        object c;
        object r;
        object r2;
        object v;
        object b;

        public Sphere(Vec center, double radius) : base()
        {
            c = center;
            r = radius;
            r2 = radius * radius;
            v = new Vec();
            b = new Vec();
        }

        public override Isect intersect(Ray ry)
        {
            Vec p = (Vec)ry.P;
            Vec d = (Vec)ry.D;
            ((Vec)v).sub2((Vec)c, p);
            double b = Vec.dot((Vec)v, d);
            double dotVV = Vec.dot((Vec)v, (Vec)v);
            double disc = b * b - dotVV + (double)r2;
            if (disc < 0.0)
                return null;
            disc = Math.Sqrt(disc);
            double t = (b - disc < 1e-6) ? b + disc : b - disc;
            if (t < 1e-6)
                return null;
            Isect ip = new Isect();
            ip.t = t;
            ip.enter = dotVV > (double)r2 + 1e-6 ? 1.0 : 0.0;
            ip.prim = this;
            ip.surf = this.surf;
            return ip;
        }

        public override Vec normal(Vec p)
        {
            Vec r = Vec.sub1(p, (Vec)c);
            r.normalize();
            return r;
        }

        public override String ToString()
        {
            return "Sphere {" + ((Vec)c).ToString() + "," + (double)r + "}";
        }

        public Vec getCenter()
        {
            return (Vec)c;
        }
        public void setCenter(Vec c)
        {
            this.c = c;
        }
    }

    public class Scene
    {
        public object lights;
        public object objects;
        private object view;

        public Scene()
        {
            this.lights = new ArrayList();
            this.objects = new ArrayList();
        }

        public void addLight(Light l)
        {
            ((ArrayList)this.lights).Add(l);
        }

        public void addObject(Primitive myobject)
        {
            ((ArrayList)this.objects).Add(myobject);
        }

        public void setView(View view)
        {
            this.view = view;
        }

        public View getView()
        {
            return (View)this.view;
        }

        public Light getLight(int number)
        {
            return (Light)((ArrayList)this.lights).ToArray()[number];
        }

        public Primitive getObject(int number)
        {
            return (Primitive)((ArrayList)objects).ToArray()[number];
        }

        public int getLights()
        {
            return ((ArrayList)this.lights).Count;
        }

        public int getObjects()
        {
            return ((ArrayList)this.objects).Count;
        }

        public void setObject(Primitive myobject, int pos)
        {
            ((ArrayList)this.objects)[pos] = myobject;
        }
    }

    public class RayTracer
    {
        public object scene;
        public object lights;
        public object prim;
        public object view;
        public object tRay;
        public static object alpha;
        public static object voidVec;
        public object L;
        public object inter;
        public object height;
        public object width;
        public object datasizes;
        public object checksum;
        public object size;
        public object numobjects;

        public RayTracer()
        {
            datasizes = new int[10];
            ((int[])datasizes)[0] = 5;
            ((int[])datasizes)[1] = 10;
            ((int[])datasizes)[2] = 15;
            ((int[])datasizes)[3] = 20;
            ((int[])datasizes)[4] = 25;
            ((int[])datasizes)[5] = 30;
            ((int[])datasizes)[6] = 35;
            ((int[])datasizes)[7] = 40;
            ((int[])datasizes)[8] = 45;
            ((int[])datasizes)[9] = 50;
            RayTracer.alpha = 255 << 24;
            tRay = new Ray();
            RayTracer.voidVec = new Vec();
            L = new Vec();
            inter = new Isect();
            checksum = 0;
        }

        public Scene createScene()
        {
            double x = 0.0;
            double y = 0.0;
            Scene scene = new Scene();
            Primitive p;
            double nx = 4.0;
            double ny = 4.0;
            double nz = 4.0;
            double i = 0.0;
            while (i < nx)
            {
                double j = 0.0;
                while (j < ny)
                {
                    double k = 0.0;
                    while (k < nz)
                    {
                        double xx = 20.0 / (nx - 1.0) * i - 10.0;
                        double yy = 20.0 / (ny - 1.0) * j - 10.0;
                        double zz = 20.0 / (nz - 1.0) * k - 10.0;
                        p = new Sphere(new Vec(xx, yy, zz), 3.0);
                        p.setColor(0.0, 0.0, (((double)(i + j)) / ((double)(nx + ny - 2.0))));
                        ((Surface)p.surf).shine = 15.0;
                        ((Surface)p.surf).ks = 1.5 - 1.0;
                        ((Surface)p.surf).kt = 1.5 - 1.0;
                        scene.addObject(p);
                        k = k + 1.0;
                    }
                    j = j + 1.0;
                }
                i = i + 1.0;
            }

            scene.addLight(new Light(100.0, 100.0, -50.0, 1.0));
            scene.addLight(new Light(-100.0, 100.0, -50.0, 1.0));
            scene.addLight(new Light(100.0, -100.0, -50.0, 1.0));
            scene.addLight(new Light(-100.0, -100.0, -50.0, 1.0));
            scene.addLight(new Light(200.0, 200.0, 0.0, 1.0));
            View v = new View(new Vec(x, 20.0, -30.0), new Vec(x, y, 0.0), new Vec(0.0, 1.0, 0.0), 1.0, 35.0 * 3.14159265 / 180.0, 1.0);
            scene.setView(v);
            return scene;
        }

        public void setScene(Scene scene)
        {
            int nLights = scene.getLights();
            int nObjects = scene.getObjects();
            lights = new Light[nLights];
            prim = new Primitive[nObjects];
            int l = 0;
            while (l < nLights)
            {
                ((Light[])lights)[l] = scene.getLight(l);
                l = l + 1;
            }
            int o = 0;
            while (o < nObjects)
            {
                ((Primitive[])prim)[o] = scene.getObject(o);
                o = o + 1;
            }
            view = scene.getView();
        }

        public bool intersect(Ray r, double tmax)
        {
            int nhits = 0;
            Isect temp = new Isect();
            temp.t = 1e9;
            this.inter = temp;
            int i = 0;
            while (i < ((Primitive[])this.prim).Length)
            {
                Isect tp = ((Primitive[])this.prim)[i].intersect(r);
                bool sw = (tp != null);
                if (sw)
                {
                    Isect isect = new Isect();
                    isect.t = tp.t;
                    isect.prim = tp.prim;
                    isect.surf = tp.surf;
                    isect.enter = tp.enter;
                    this.inter = isect;
                    nhits = nhits + 1;
                }
                i = i + 1;
            }
            return nhits > 0 ? true : false;
        }

        public Vec trace(int level, double weight, Ray r)
        {
            if (level > 6)
                return new Vec();

            bool hit = intersect(r, 1e6);
            if (hit)
            {
                Vec P = r.point((double)((Isect)inter).t);
                Vec N = ((Primitive)((Isect)inter).prim).normal(P);
                if (Vec.dot((Vec)r.D, N) >= 0.0)
                    N.negate();
                //return shade(level, weight, P, N, r.D, inter);
            }
            return (Vec)voidVec;
        }

        public Vec SpecularDirection(Vec I, Vec N)
        {
            double dotIN = Vec.dot(I, N);
            double abs = dotIN > 0 ? dotIN : (-1 * dotIN);
            Vec r = Vec.comb(1.0 / abs, I, 2.0, N);
            r.normalize();
            return r;
        }

        public Vec TransDir(Surface m1, Surface m2, Vec I, Vec N)
        {
            double n1 = m1 == null ? 1.0 : (double)m1.ior;
            double n2 = m2 == null ? 1.0 : (double)m2.ior;
            double eta = n1 / n2;
            double c1 = -Vec.dot(I, N);
            double cs2 = 1.0 - eta * eta * (1.0 - c1 * c1);
            if (cs2 < 0.0)
                return null;
            Vec r = Vec.comb(eta, I, eta * c1 - Math.Sqrt(cs2), N);
            r.normalize();
            return r;
        }

        public int Shadow(Ray r, double tmax)
        {
            if (intersect(r, tmax))
                return 0;
            return 1;
        }

        public void render(Interval interval)
        {
            View localView = (View)this.view;
            Vec viewVec = Vec.sub1((Vec)localView.atVec, (Vec)localView.fromVec);
            viewVec.normalize();
            Vec tmpVec = new Vec((double)viewVec.x, (double)viewVec.y, (double)viewVec.z);
            double dotValue = Vec.dot((Vec)localView.upVec, viewVec);
            tmpVec.scale(dotValue);
            Vec upVec = Vec.sub1((Vec)localView.upVec, tmpVec);
            upVec.normalize();
            Vec leftVec = Vec.cross((Vec)localView.upVec, viewVec);
            leftVec.normalize();
            double frustrumwidth = (double)localView.dist * Math.Tan((double)localView.angle);
            upVec.scale(-frustrumwidth);
            double scaleValue = (double)localView.aspect * frustrumwidth;
            leftVec.scale(scaleValue);
            Ray r = new Ray((Vec)localView.fromVec, (Vec)RayTracer.voidVec);
            int y = (int)interval.yfrom;
            int nhits = 0;
            while (y < (int)interval.yto)
            {
                double ylen = (2.0 * y) / (int)interval.width - 1.0;
                int x = 0;
                while (x < (int)interval.width)
                {
                    double xlen = (2.0 * x) / (int)interval.width - 1.0;
                    r.D = Vec.comb(xlen, leftVec, ylen, upVec);
                    ((Vec)r.D).add1(viewVec);
                    ((Vec)r.D).normalize();
                    bool hit = this.intersect(r, 1e6);
                    if (hit)
                        nhits = nhits + 1;
                    x = x + 1;
                }
                y = y + 1;
            }
            checksum = (int)checksum + nhits;
        }
    }

    public class JGFRayTracerBench : RayTracer
    {
        public void JGFsetsize(int size)
        {
            this.size = size;
        }

        public void JGFinitialise()
        {
            width = height = ((int[])datasizes)[(int)size];
            scene = createScene();
            setScene((Scene)scene);
            numobjects = ((Scene)scene).getObjects();
        }

        public void JGFapplication()
        {
            Interval interval = new Interval(0, (int)width, (int)height, 0, (int)height, 1);
            render(interval);
        }

        public void JGFvalidate()
        {
            int[] refval = new int[10];
            refval[0] = 12;
            refval[1] = 40;
            refval[2] = 104;
            refval[3] = 170;
            refval[4] = 278;
            refval[5] = 390;
            refval[6] = 552;
            refval[7] = 692;
            refval[8] = 894;
            refval[9] = 1084;
            int dev = (int)checksum - refval[(int)size];
            if (dev != 0)
            {
                Console.WriteLine("Validation failed");
                Console.WriteLine("Pixel checksum = " + (int)checksum);
                Console.WriteLine("Reference value = " + refval[(int)size]);
            }
        }

        public void Test()
        {
            JGFsetsize(4);
            JGFinitialise();
            JGFapplication();
            JGFvalidate();
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
            JGFRayTracerBench test = new JGFRayTracerBench();
            chronometer.Start();
            test.Test();
            chronometer.Stop();
            this.microSeconds = this.microSeconds + chronometer.GetMicroSeconds();
            return null;
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
            BenchMark benchmark = new BenchMark(iterations);
            Console.WriteLine(benchmark.run());
        }
    }
}
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
        public dynamic x;
        public dynamic y;
        public dynamic z;
        public Vec(dynamic a, dynamic b, dynamic c)
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

        public void add1(dynamic a)
        {

            x = a.x + this.x;
            y = a.y + this.y;
            z = a.z + this.z;
        }

        public static dynamic adds(dynamic s, dynamic a, dynamic b)
        {
            return new Vec(s * a.x + b.x, s * a.y + b.y, s * a.z + b.z);
        }

        public void adds(dynamic s, dynamic b)
        {
            this.x = this.x + (s * b.x);
            this.y = this.y + (s * b.y);
            this.z = this.z + (s * b.z);
        }

        public static dynamic sub1(dynamic a, dynamic b)
        {
            return new Vec(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public void sub2(dynamic a, dynamic b)
        {
            this.x = a.x - b.x;
            this.y = a.y - b.y;
            this.z = a.z - b.z;
        }

        public static dynamic mult1(dynamic a, dynamic b)
        {
            return new Vec(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public static dynamic cross(dynamic a, dynamic b)
        {
            return new Vec(a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x);
        }

        public static dynamic dot(dynamic a, dynamic b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        public static dynamic comb(dynamic a, dynamic A, dynamic b, dynamic B)
        {
            return new Vec(a * A.x + b * B.x, a * A.y + b * B.y, a * A.z + b * B.z);
        }

        public void comb2(dynamic a, dynamic A, dynamic b, dynamic B)
        {
            x = a * A.x + b * B.x;
            y = a * A.y + b * B.y;
            z = a * A.z + b * B.z;
        }

        public void scale(dynamic t)
        {
            x = t * x;
            y = t * y;
            z = t * z;
        }

        public void negate()
        {
            x = -1.0 * x;
            y = -1.0 * y;
            z = -1.0 * z;
        }

        public dynamic normalize()
        {
            dynamic len = Math.Sqrt(x * x + y * y + z * z);
            //while (len > 0.0)
            //{
                x = (double)(x / len);
                y = (double)(y / len);
                z = (double)(z / len);
                len = 0.0;
            //}
            return len;
        }

        public override String ToString()
        {
            return "<" + x + "," + y + "," + z + ">";
        }
    }

    public class View
    {
        public dynamic fromVec;
        public dynamic atVec;
        public dynamic upVec;
        public dynamic dist;
        public dynamic angle;
        public dynamic aspect;

        public View(dynamic fromVec, dynamic atVec, dynamic upVec, dynamic dist, dynamic angle, dynamic aspect)
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
        public dynamic P;
        public dynamic D;
        
        public Ray()
        {
            P = new Vec();
            D = new Vec();
        }

        public dynamic point(dynamic t)
        {
            return new Vec(this.P.x + this.D.x * t, this.P.y + this.D.y * t, this.P.z + this.D.z * t);
        }

        public override String ToString()
        {
            return "{" + P.ToString() + " -> " + D.ToString() + "}";
        }
    }

    public class Interval
    {
        public dynamic number;
        public dynamic width;
        public dynamic height;
        public dynamic yfrom;
        public dynamic yto;
        public dynamic total;

        public Interval(dynamic number, dynamic width, dynamic height, dynamic yfrom, dynamic yto, dynamic total)
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
        public dynamic t;
        public dynamic enter;
        public dynamic prim;
        public dynamic surf;

        public override String ToString()
        {
            return "{" + t.ToString() + "," + enter.ToString() + "," + prim.ToString() + "," + surf.ToString() + "}";
        }
    }

    public class Light
    {
        public dynamic pos;
        public dynamic brightness;

        public Light() { }

        public Light(dynamic x, dynamic y, dynamic z, dynamic brightness)
        {
            this.pos = new Vec(x, y, z);
            this.brightness = brightness;
        }
    }

    public class Surface
    {
        public dynamic color;
        public dynamic kd;
        public dynamic ks;
        public dynamic shine;
        public dynamic kt;
        public dynamic ior;

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
        public dynamic surf;

        public Primitive()
        {
            surf = new Surface();
        }

        public void setColor(dynamic r, dynamic g, dynamic b)
        {
            surf.color = new Vec(r, g, b);
        }

        public abstract Isect intersect(dynamic ry);
    }

    public class Sphere : Primitive
    {
        dynamic c;
        dynamic r;
        dynamic r2;
        dynamic v;
        dynamic b;

        public Sphere(dynamic center, dynamic radius) : base()
        {
            c = center;
            r = radius;
            r2 = radius * radius;
            v = new Vec();
            b = new Vec();
        }

        public override Isect intersect(dynamic ry)
        {
            dynamic p = ry.P;
            dynamic d = ry.D;
            v.sub2(c, p);
            dynamic b = Vec.dot(v, d);
            dynamic dotVV = Vec.dot(v, v);
            dynamic disc = b * b - dotVV + r2;
            if (disc < 0.0)
                return null;
            disc = Math.Sqrt(disc);
            dynamic t = (b - disc < 1e-6) ? b + disc : b - disc;
            if (t < 1e-6)
                return null;
            dynamic ip = new Isect();
            ip.t = t;
            ip.enter = dotVV > r2 + 1e-6 ? 1.0 : 0.0;
            ip.prim = this;
            ip.surf = this.surf;
            return ip;
        }

        public dynamic normal(dynamic p)
        {
            dynamic r = Vec.sub1(p, c);
            r.normalize();
            return r;
        }

        public override String ToString()
        {
            return "Sphere {" + c.ToString() + "," + r + "}";
        }

        public dynamic getCenter()
        {
            return c;
        }
        public void setCenter(dynamic c)
        {
            this.c = c;
        }
    }

    public class Scene
    {
        public dynamic lights;
        public dynamic objects;
        private dynamic view;

        public Scene()
        {
            this.lights = new ArrayList();
            this.objects = new ArrayList();
        }

        public void addLight(dynamic l)
        {
            this.lights.Add(l);
        }

        public void addObject(dynamic myobject)
        {
            this.objects.Add(myobject);
        }

        public void setView(dynamic view)
        {
            this.view = view;
        }

        public dynamic getView()
        {
            return this.view;
        }

        public dynamic getLight(dynamic number)
        {
            return (Light)this.lights.ToArray()[number];
        }

        public dynamic getObject(dynamic number)
        {
            return (Primitive)objects.ToArray()[number];
        }

        public dynamic getLights()
        {
            return this.lights.Count;
        }

        public dynamic getObjects()
        {
            return this.objects.Count;
        }

        public void setObject(dynamic myobject, dynamic pos)
        {
            ((ArrayList)this.objects)[pos] = myobject;
        }
    }

    public class RayTracer
    {
        public dynamic scene;
        public dynamic lights;
        public dynamic prim;
        public dynamic view;
        public dynamic tRay;
        public static dynamic alpha;
        public static dynamic voidVec;
        public dynamic L;
        public dynamic inter;
        public dynamic height;
        public dynamic width;
        public dynamic datasizes;
        public dynamic checksum;
        public dynamic size;
        public dynamic numobjects;

        public RayTracer()
        {
            datasizes = new int[11];
            datasizes[0] = 5;
            datasizes[1] = 10;
            datasizes[2] = 15;
            datasizes[3] = 20;
            datasizes[4] = 25;
            datasizes[5] = 30;
            datasizes[6] = 35;
            datasizes[7] = 40;
            datasizes[8] = 45;
            datasizes[9] = 50;
            datasizes[10] = 150;
            RayTracer.alpha = 255 << 24;
            tRay = new Ray();
            RayTracer.voidVec = new Vec();
            L = new Vec();
            inter = new Isect();
            checksum = 0;
        }

        public dynamic createScene()
        {
            dynamic x = 0.0;
            dynamic y = 0.0;
            dynamic scene = new Scene();
            dynamic p;
            dynamic nx = 4.0;
            dynamic ny = 4.0;
            dynamic nz = 4.0;
            dynamic i = 0.0;
            //while (i < nx)
            //{
                dynamic j = 0.0;
                //while (j < ny)
                //{
                    dynamic k = 0.0;
                    //while (k < nz)
                    //{
                        dynamic xx = 20.0 / (nx - 1.0) * i - 10.0;
                        dynamic yy = 20.0 / (ny - 1.0) * j - 10.0;
                        dynamic zz = 20.0 / (nz - 1.0) * k - 10.0;
                        p = new Sphere(new Vec(xx, yy, zz), 3.0);
                        p.setColor(0.0, 0.0, (((double)(i + j)) / ((double)(nx + ny - 2.0))));
                        p.surf.shine = 15.0;
                        p.surf.ks = 1.5 - 1.0;
                        p.surf.kt = 1.5 - 1.0;
                        scene.addObject(p);
                        k = k + 1.0;
                    //}
                    j = j + 1.0;
                //}
                i = i + 1.0;
            //}

            scene.addLight(new Light(100.0, 100.0, -50.0, 1.0));
            scene.addLight(new Light(-100.0, 100.0, -50.0, 1.0));
            scene.addLight(new Light(100.0, -100.0, -50.0, 1.0));
            scene.addLight(new Light(-100.0, -100.0, -50.0, 1.0));
            scene.addLight(new Light(200.0, 200.0, 0.0, 1.0));
            dynamic v = new View(new Vec(x, 20.0, -30.0), new Vec(x, y, 0.0), new Vec(0.0, 1.0, 0.0), 1.0, 35.0 * 3.14159265 / 180.0, 1.0);
            scene.setView(v);
            return scene;
        }

        public void setScene(dynamic scene)
        {
            dynamic nLights = scene.getLights();
            dynamic nObjects = scene.getObjects();
            lights = new Light[nLights];
            prim = new Primitive[nObjects];
            dynamic l = 0;
            //while (l < nLights)
            //{
                //lights[l] = scene.getLight(l);
                l = l + 1;
            //}
            dynamic o = 0;
            //while (o < nObjects)
            //{
                //prim[o] = scene.getObject(o);
                o = o + 1;
            //}
            view = scene.getView();
        }

        public dynamic intersect(dynamic r, dynamic tmax)
        {
            dynamic nhits = 0;
            dynamic temp = new Isect();
            temp.t = 1e9;
            this.inter = temp;
            int i = 0;
            //while (i < this.prim.Length)
            //{
                dynamic tp = this.prim[i].intersect(r);
                bool sw = (tp != null);
                if (sw)
                {
                    dynamic isect = new Isect();
                    isect.t = tp.t;
                    isect.prim = tp.prim;
                    isect.surf = tp.surf;
                    isect.enter = tp.enter;
                    this.inter = isect;
                    nhits = nhits + 1;
                }
                i = i + 1;
            //}
            return nhits > 0 ? true : false;
        }

        public dynamic trace(dynamic level, dynamic weight, dynamic r)
        {
            if (level > 6)
                return new Vec();

            dynamic hit = intersect(r, 1e6);
            //if (hit)
            //{
                dynamic P = r.point(inter.t);
                dynamic N = inter.prim.normal(P);
                if (Vec.dot(r.D, N) >= 0.0)
                    N.negate();
                //return shade(level, weight, P, N, r.D, inter);
            //}
            return voidVec;
        }

        public dynamic SpecularDirection(dynamic I, dynamic N)
        {
            dynamic dotIN = Vec.dot(I, N);
            dynamic abs = dotIN > 0 ? dotIN : (-1 * dotIN);
            dynamic r = Vec.comb(1.0 / abs, I, 2.0, N);
            r.normalize();
            return r;
        }

        public dynamic TransDir(dynamic m1, dynamic m2, dynamic I, dynamic N)
        {
            dynamic n1 = m1 == null ? 1.0 : m1.ior;
            dynamic n2 = m2 == null ? 1.0 : m2.ior;
            dynamic eta = n1 / n2;
            dynamic c1 = -Vec.dot(I, N);
            dynamic cs2 = 1.0 - eta * eta * (1.0 - c1 * c1);
            if (cs2 < 0.0)
                return null;
            dynamic r = Vec.comb(eta, I, eta * c1 - Math.Sqrt(cs2), N);
            r.normalize();
            return r;
        }

        public dynamic Shadow(dynamic r, dynamic tmax)
        {
			intersect(r, tmax);
            //if (intersect(r, tmax))
                return 0;
            return 1;
        }

        public void render(dynamic interval)
        {
            dynamic localView = this.view;
            dynamic viewVec = Vec.sub1(localView.atVec, localView.fromVec);
            viewVec.normalize();
            dynamic tmpVec = new Vec(viewVec.x, viewVec.y, viewVec.z);
            dynamic dotValue = Vec.dot(localView.upVec, viewVec);
            tmpVec.scale(dotValue);
            dynamic upVec = Vec.sub1(localView.upVec, tmpVec);
            upVec.normalize();
            dynamic leftVec = Vec.cross(localView.upVec, viewVec);
            leftVec.normalize();
            dynamic frustrumwidth = localView.dist * Math.Tan(localView.angle);
            upVec.scale(-frustrumwidth);
            dynamic scaleValue = localView.aspect * frustrumwidth;
            leftVec.scale(scaleValue);
            dynamic r = new Ray();            
            r.P.add1(localView.fromVec);            
            r.D.add1(RayTracer.voidVec);
            r.D.normalize();
        
            int y = interval.yfrom;
            dynamic nhits = 0;
            //while (y < interval.yto)
            //{
                dynamic ylen = (2.0 * y) / interval.width - 1.0;
                int x = 0;
              //  while (x < interval.width)
                //{
                    dynamic xlen = (2.0 * x) / interval.width - 1.0;
                    r.D = Vec.comb(xlen, leftVec, ylen, upVec);
                    r.D.add1(viewVec);
                    r.D.normalize();
                    dynamic hit = this.intersect(r, 1e6);
                    if (hit)
                        nhits = nhits + 1;
                    x = x + 1;
                //}
                y = y + 1;
            //}
            checksum = checksum + nhits;
        }
    }

    public class JGFRayTracerBench : RayTracer
    {
        public void JGFsetsize(dynamic size)
        {
            this.size = size;
        }

        public void JGFinitialise()
        {
            width = height = datasizes[size];
            scene = createScene();
            setScene(scene);
            numobjects = scene.getObjects();
        }

        public void JGFapplication()
        {
            dynamic interval = new Interval(0, width, height, 0, height, 1);
            render(interval);
        }

        public void JGFvalidate()
        {
            dynamic refval = new int[11];
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
            refval[10] = 9834;
            dynamic dev = checksum - refval[size];
            if (dev != 0)
            {
                Console.WriteLine("Validation failed");
                Console.WriteLine("Pixel checksum = " + checksum);
                Console.WriteLine("Reference value = " + refval[size]);
            }
        }

        public void Test()
        {
            JGFsetsize(10);
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

        virtual public object runOneIteration() { return null; }
    }

    public class ArithmethicBenchmark : BenchMark
    {
        public ArithmethicBenchmark(int iterations) : base(iterations) { }
        public override object runOneIteration()
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
            ArithmethicBenchmark arith = new ArithmethicBenchmark(iterations);
            Console.WriteLine(arith.run());
        }
    }
}
using System;
using System.Collections;

namespace CSGrande
{
    public class Vec
    {
        public double x;
        public double y;
        public double z;
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

            x = a.x + this.x;
            y = a.y + this.y;
            z = a.z + this.z;
        }

        public static Vec adds(double s, Vec a, Vec b)
        {
            return new Vec(s * a.x + b.x, s * a.y + b.y, s * a.z + b.z);
        }

        public void adds(double s, Vec b)
        {
            this.x = this.x + (s * b.x);
            this.y = this.y + (s * b.y);
            this.z = this.z + (s * b.z);
        }

        public static Vec sub1(Vec a, Vec b)
        {
            return new Vec(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public void sub2(Vec a, Vec b)
        {
            this.x = a.x - b.x;
            this.y = a.y - b.y;
            this.z = a.z - b.z;
        }

        public static Vec mult1(Vec a, Vec b)
        {
            return new Vec(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public static Vec cross(Vec a, Vec b)
        {
            return new Vec(a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x);
        }

        public static double dot(Vec a, Vec b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        public static Vec comb(double a, Vec A, double b, Vec B)
        {
            return new Vec(a * A.x + b * B.x, a * A.y + b * B.y, a * A.z + b * B.z);
        }

        public void comb2(double a, Vec A, double b, Vec B)
        {
            x = a * A.x + b * B.x;
            y = a * A.y + b * B.y;
            z = a * A.z + b * B.z;
        }

        public void scale(double t)
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

        public double normalize()
        {
            double len = Math.Sqrt(x * x + y * y + z * z);
            while (len > 0.0)
            {
                x = (double)(x / len);
                y = (double)(y / len);
                z = (double)(z / len);
                len = 0.0;
            }
            return len;
        }

        public override String ToString()
        {
            return "<" + x + "," + y + "," + z + ">";
        }
    }

    public class View
    {
        public Vec fromVec;
        public Vec atVec;
        public Vec upVec;
        public double dist;
        public double angle;
        public double aspect;

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
        public Vec P;
        public Vec D;

        public Ray(Vec pnt, Vec dir)
        {
            P = new Vec();
            P.add1(pnt);
            D = new Vec();
            D.add1(dir);
            D.normalize();
        }

        public Ray()
        {
            P = new Vec();
            D = new Vec();
        }

        public Vec point(double t)
        {
            return new Vec(P.x + D.x * t, P.y + D.y * t, P.z + D.z * t);
        }

        public override String ToString()
        {
            return "{" + P.ToString() + " -> " + D.ToString() + "}";
        }
    }

    public class Interval
    {
        public int number;
        public int width;
        public int height;
        public int yfrom;
        public int yto;
        public int total;

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
        public double t;
        public double enter;
        public Primitive prim;
        public Surface surf;

        public override String ToString()
        {
            return "{" + t.ToString() + "," + enter.ToString() + "," + prim.ToString() + "," + surf.ToString() + "}";
        }
    }

    public class Light
    {
        public Vec pos;
        public double brightness;

        public Light() { }

        public Light(double x, double y, double z, double brightness)
        {
            this.pos = new Vec(x, y, z);
            this.brightness = brightness;
        }
    }

    public class Surface
    {
        public Vec color;
        public double kd;
        public double ks;
        public double shine;
        public double kt;
        public double ior;

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
        public Surface surf;

        public Primitive()
        {
            surf = new Surface();
        }

        public void setColor(double r, double g, double b)
        {
            surf.color = new Vec(r, g, b);
        }
        
        public abstract Vec normal(Vec pnt);
        public abstract Isect intersect(Ray ry);        
        public abstract Vec getCenter();
        public abstract void setCenter(Vec c);
    }

    public class Sphere : Primitive
    {
        Vec c;
        double r;
        double r2;
        Vec v;
        Vec b;

        public Sphere(Vec center, double radius)
            : base()
        {
            c = center;
            r = radius;
            r2 = radius * radius;
            v = new Vec();
            b = new Vec();
        }

        public override Isect intersect(Ray ry)
        {
            Vec p = ry.P;
            Vec d = ry.D;
            v.sub2(c, p);
            double b = Vec.dot(v, d);
            double dotVV = Vec.dot(v, v);
            double disc = b * b - dotVV + r2;
            if (disc < 0.0)
                return null;
            disc = Math.Sqrt(disc);
            double t = (b - disc < 1e-6) ? b + disc : b - disc;
            if (t < 1e-6)
                return null;
            Isect ip = new Isect();
            ip.t = t;
            ip.enter = dotVV > r2 + 1e-6 ? 1.0 : 0.0;
            ip.prim = this;
            ip.surf = this.surf;
            return ip;
        }

        public override Vec normal(Vec p)
        {
            Vec r = Vec.sub1(p, c);
            r.normalize();
            return r;
        }

        public override String ToString()
        {
            return "Sphere {" + c.ToString() + "," + r + "}";
        }

        public override Vec getCenter()
        {
            return c;
        }
        public override void setCenter(Vec c)
        {
            this.c = c;
        }
    }

    public class Scene
    {
        public ArrayList lights;
        public ArrayList objects;
        private View view;

        public Scene()
        {
            this.lights = new ArrayList();
            this.objects = new ArrayList();
        }

        public void addLight(Light l)
        {
            this.lights.Add(l);
        }

        public void addObject(Primitive myobject)
        {
            this.objects.Add(myobject);
        }

        public void setView(View view)
        {
            this.view = view;
        }

        public View getView()
        {
            return this.view;
        }

        public Light getLight(int number)
        {
            return (Light)this.lights.ToArray()[number];
        }

        public Primitive getObject(int number)
        {
            return (Primitive)objects.ToArray()[number];
        }

        public int getLights()
        {
            return this.lights.Count;
        }

        public int getObjects()
        {
            return this.objects.Count;
        }

        public void setObject(Primitive myobject, int pos)
        {
            ((ArrayList)this.objects)[pos] = myobject;
        }
    }

    public class RayTracer
    {
        public Scene scene;
        public Light[] lights;
        public Primitive[] prim;
        public View view;
        public Ray tRay;
        public static int alpha;
        public static Vec voidVec;
        public Vec L;
        public Isect inter;
        public int height;
        public int width;
        public int[] datasizes;
        public int checksum;
        public int size;
        public int numobjects;

        public RayTracer()
        {
            datasizes = new int[3];
            datasizes[0] = 5;
            datasizes[1] = 25;
            datasizes[2] = 125;
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
                        p.surf.shine = 15.0;
                        p.surf.ks = 1.5 - 1.0;
                        p.surf.kt = 1.5 - 1.0;
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
                lights[l] = scene.getLight(l);
                l = l + 1;
            }
            int o = 0;
            while (o < nObjects)
            {
                prim[o] = scene.getObject(o);
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
            while (i < this.prim.Length)
            {
                Isect tp = this.prim[i].intersect(r);
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
                Vec P = r.point(inter.t);
                Vec N = inter.prim.normal(P);
                if (Vec.dot(r.D, N) >= 0.0)
                    N.negate();
                //return shade(level, weight, P, N, r.D, inter);
            }
            return voidVec;
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
            double n1 = m1 == null ? 1.0 : m1.ior;
            double n2 = m2 == null ? 1.0 : m2.ior;
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
            View localView = this.view;
            Vec viewVec = Vec.sub1(localView.atVec, localView.fromVec);
            viewVec.normalize();
            Vec tmpVec = new Vec(viewVec.x, viewVec.y, viewVec.z);
            double dotValue = Vec.dot(localView.upVec, viewVec);
            tmpVec.scale(dotValue);
            Vec upVec = Vec.sub1(localView.upVec, tmpVec);
            upVec.normalize();
            Vec leftVec = Vec.cross(localView.upVec, viewVec);
            leftVec.normalize();
            double frustrumwidth = localView.dist * Math.Tan(localView.angle);
            upVec.scale(-frustrumwidth);
            double scaleValue = localView.aspect * frustrumwidth;
            leftVec.scale(scaleValue);
            Ray r = new Ray(localView.fromVec, RayTracer.voidVec);
            int y = interval.yfrom;
            int nhits = 0;
            while (y < interval.yto)
            {
                double ylen = (2.0 * y) / interval.width - 1.0;
                int x = 0;
                while (x < interval.width)
                {
                    double xlen = (2.0 * x) / interval.width - 1.0;
                    r.D = Vec.comb(xlen, leftVec, ylen, upVec);
                    r.D.add1(viewVec);
                    r.D.normalize();
                    bool hit = this.intersect(r, 1e6);
                    if (hit)
                        nhits = nhits + 1;
                    x = x + 1;
                }
                y = y + 1;
            }
            checksum = checksum + nhits;
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
            width = height = datasizes[size];
            scene = createScene();
            setScene(scene);
            numobjects = scene.getObjects();
        }

        public void JGFapplication()
        {
            Interval interval = new Interval(0, width, height, 0, height, 1);
            render(interval);
        }


        public void JGFvalidate()
        {
            int[] refval = new int[3];
            refval[0] = 12;
            refval[1] = 278;
            refval[2] = 6798;
            int dev = checksum - refval[size];
            if (dev != 0)
            {
                Console.WriteLine("Validation failed");
                Console.WriteLine("Pixel checksum = " + checksum);
                Console.WriteLine("Reference value = " + refval[size]);
            }
        }

        public void JGFtidyup()
        {
            scene = new Scene();
            lights = new Light[0];
            prim = new Primitive[0];
            tRay = new Ray();
            inter = new Isect();
        }

        public void JGFrun()
        {
            JGFrun1(0);
        }

        public void JGFrun1(int size)
        {
            JGFsetsize(size);
            JGFinitialise();
            JGFapplication();
            JGFvalidate();
            JGFtidyup();
        }

        public static void Main()
        {
            
            JGFRayTracerBench rtb = new JGFRayTracerBench();
            rtb.JGFrun();            
            Console.WriteLine("RayTracer completed!!!");            
        }
    }
}
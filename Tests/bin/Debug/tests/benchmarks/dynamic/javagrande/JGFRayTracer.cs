using System;
using System.Collections;

namespace CSGrande
{		
	public class Vec
    {
        public var x;
        public var y;
        public var z;
        public Vec(var a, var b, var c)
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

        public void add1(var a)
        {
		
            x = a.x + this.x;
            y = a.y + this.y;
            z = a.z + this.z;
        }

        public static var adds(var s, var a, var b)
        {
            return new Vec(s * a.x + b.x, s * a.y + b.y, s * a.z + b.z);
        }

        public void adds(var s, var b)
        {
            this.x = this.x + (s * b.x);
            this.y = this.y + (s * b.y);
            this.z = this.z + (s * b.z);
        }

        public static var sub1(var a, var b)
        {							
            return new Vec(a.x - b.x, a.y - b.y, a.z - b.z);										
        }

        public void sub2(var a, var b)
        {			
			this.x = a.x - b.x;
            this.y = a.y - b.y;
            this.z = a.z - b.z;
        }

        public static var mult1(var a, var b)
        {
            return new Vec(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public static var cross(var a, var b)
        {						
			return new Vec(a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x);						
        }

        public static var dot(var a, var b)
        {			
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        public static var comb(var a, var A, var b, var B)
        {			
			return new Vec(a * A.x + b * B.x, a * A.y + b * B.y, a * A.z + b * B.z);
        }

        public void comb2(var a, var A, var b, var B)
        {
            x = a * A.x + b * B.x;
            y = a * A.y + b * B.y;
            z = a * A.z + b * B.z;
        }

        public void scale(var t)
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

        public var normalize()
        {									
            var len = Math.Sqrt(x * x + y * y + z * z);		
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
        public var fromVec;
        public var atVec;
        public var upVec;
        public var dist;
        public var angle;
        public var aspect;

        public View(var fromVec, var atVec, var upVec, var dist, var angle, var aspect)
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
        public var P;
		public var D;

        public Ray(var pnt, var dir)
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

        public var point(var t)
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
        public var number;
        public var width;
        public var height;
        public var yfrom;
        public var yto;
        public var total;

        public Interval(var number, var width, var height, var yfrom, var yto, var total)
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
        public var t;
        public var enter;
        public var prim;
        public var surf;
		
		public override String ToString()
		{
			return "{" + t.ToString() + "," + enter.ToString() + "," + prim.ToString() + "," + surf.ToString() + "}";
		}
    }
	
	public class Light
    {
        public var pos;
        public var brightness;

        public Light() {}

        public Light(var x, var y, var z, var brightness)
        {
            this.pos = new Vec(x,y,z);
            this.brightness = brightness;
        }
    }
	
	public class Surface
    {
        public var color;
        public var kd;
        public var ks;
        public var shine;
        public var kt;
        public var ior;

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
        public var surf;
		
		public Primitive()
		{
			surf = new Surface();
		}

        public void setColor(var r, var g, var b)
        {
            surf.color = new Vec(r, g, b);
        }
		
		public abstract Isect intersect(Ray ry);		
    }
	
	public class Sphere : Primitive
    {
        var c;
        var r;
		var r2;
        var v;
		var b;

        public Sphere(var center, var radius):base()
        {			
            c = center;
            r = radius;
            r2 = radius * radius;
            v = new Vec();
            b = new Vec();
        }

		public override Isect intersect(Ray ry)	
        {		
			var p = ry.P;
			var d = ry.D;       			
			v.sub2(c, p);					
			var b = Vec.dot(v, d);									
			var dotVV = Vec.dot(v, v);
			var disc = b * b - dotVV + r2;				
			if (disc < 0.0)            			
                return null;            											
            disc = Math.Sqrt(disc);									            
			var t = (b - disc < 1e-6) ? b + disc : b - disc;						
            if (t < 1e-6)            
                return null;    			
            var ip = new Isect();
            ip.t = t;						
            ip.enter = dotVV > r2 + 1e-6 ? 1.0 : 0.0;
            ip.prim = this;
            ip.surf = this.surf;
			return ip;					
        }
		
        public var normal(var p)
        {
            var r = Vec.sub1(p, c);
            r.normalize();
            return r;
        }

        public override String ToString()
        {
            return "Sphere {" + c.ToString() + "," + r + "}";
        }

        public var getCenter()
        {
            return c;
        }
        public void setCenter(var c)
        {
            this.c = c;
        }
    }
	
	public class Scene
    {
        public var lights;
        public var objects;
        private var view;

        public Scene()
        {
            this.lights = new ArrayList();
            this.objects = new ArrayList();
        }

        public void addLight(var l)
        {
            this.lights.Add(l);
        }

        public void addObject(var myobject)
        {
            this.objects.Add(myobject);
        }

        public void setView(var view)
        {
            this.view = view;
        }

        public var getView()
        {
            return this.view;
        }

        public var getLight(var number)
        {
            return (Light)this.lights.ToArray()[number];
        }

        public var getObject(var number)
        {
            return (Primitive)objects.ToArray()[number];
        }

        public var getLights()
        {
            return this.lights.Count;
        }

        public var getObjects()
        {
            return this.objects.Count;
        }

        public void setObject(var myobject, var pos)
        {
            ((ArrayList)this.objects)[pos] = myobject;
        }
    }
	
	public class RayTracer
    {
        public var scene;
        public var lights;
        public var prim;
        public var view;
        public var tRay;
        public static var alpha;
        public static var voidVec;
        public var L;
        public var inter;
        public var height;
        public var width;
        public var datasizes;
        public var checksum;
        public var size;
        public var numobjects;
		
        public RayTracer()
        {
            datasizes = new int[10];
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
            RayTracer.alpha = 255 << 24;
            tRay = new Ray();
            RayTracer.voidVec = new Vec();
            L = new Vec();
            inter = new Isect();
            checksum = 0;
        }	
	
		public var createScene()
        {
            var x = 0.0;
            var y = 0.0;
            var scene = new Scene();
            var p;
            var nx = 4.0;
            var ny = 4.0;
            var nz = 4.0;
            var i = 0.0;			
            while (i < nx)
            {
                var j = 0.0;
                while (j < ny)
                {
                    var k = 0.0;
                    while (k < nz)
                    {
                        var xx = 20.0 / (nx - 1.0) * i - 10.0;						
                        var yy = 20.0 / (ny - 1.0) * j - 10.0;
                        var zz = 20.0 / (nz - 1.0) * k - 10.0;												
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
            var v = new View(new Vec(x, 20.0, -30.0), new Vec(x, y, 0.0), new Vec(0.0, 1.0, 0.0), 1.0, 35.0 * 3.14159265 / 180.0, 1.0);
            scene.setView(v);
            return scene;
        }
	
		public void setScene(var scene)
        {			
            var nLights = scene.getLights();
            var nObjects = scene.getObjects();
            lights = new Light[nLights];
            prim = new Primitive[nObjects];			
            var l = 0;			
			while(l < nLights)
			{
                lights[l] = scene.getLight(l);			
				l = l + 1;
			}
			var o = 0;
            while (o < nObjects)
			{
                prim[o] = scene.getObject(o);				
				o = o + 1;
			}
            view = scene.getView();			
        }	
		
		public var intersect(var r, var tmax)
        {			
            var nhits = 0;
            var temp = new Isect();							
			temp.t = 1e9;	
			this.inter = temp;					
			int i = 0;											
			while (i < this.prim.Length)
			{						
				var tp = this.prim[i].intersect(r);															
				bool sw = (tp != null);					
				if(sw)
				{										
					var isect = new Isect();							
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

		public var trace(var level, var weight, var r)
        {                        
            if (level > 6)
                return new Vec();

            var hit = intersect(r, 1e6);
            if (hit)
            {
                var P = r.point(inter.t);
                var N = inter.prim.normal(P);
                if (Vec.dot(r.D, N) >= 0.0)
                    N.negate();
                //return shade(level, weight, P, N, r.D, inter);
            }
            return voidVec;
        }
		
		public var SpecularDirection(var I, var N)
        {
			var dotIN = Vec.dot(I, N);
			var abs = dotIN>0?dotIN:(-1*dotIN);
            var r = Vec.comb(1.0 / abs, I, 2.0, N);
            r.normalize();
            return r;
        }

        public var TransDir(var m1, var m2, var I, var N)
        {            
            var n1 = m1 == null ? 1.0 : m1.ior;
            var n2 = m2 == null ? 1.0 : m2.ior;
            var eta = n1 / n2;
            var c1 = -Vec.dot(I, N);
            var cs2 = 1.0 - eta * eta * (1.0 - c1 * c1);
            if (cs2 < 0.0)
                return null;
            var r = Vec.comb(eta, I, eta * c1 - Math.Sqrt(cs2), N);
            r.normalize();
            return r;
        }
		
		public var Shadow(var r, var tmax)
        {
            if (intersect(r,tmax))
                return 0;
            return 1;
        }
		
		public void render(var interval)
		{			
			var localView = this.view;			
            var viewVec = Vec.sub1(localView.atVec, localView.fromVec);											
			viewVec.normalize();			
			var tmpVec = new Vec(viewVec.x, viewVec.y, viewVec.z);			
			var dotValue = Vec.dot(localView.upVec, viewVec);			
			tmpVec.scale(dotValue);			
			var upVec = Vec.sub1(localView.upVec, tmpVec);
			upVec.normalize();
			var leftVec = Vec.cross(localView.upVec, viewVec);
			leftVec.normalize();
			var frustrumwidth = localView.dist * Math.Tan(localView.angle);						
			upVec.scale(-frustrumwidth);			
			var scaleValue = localView.aspect * frustrumwidth;
            leftVec.scale(scaleValue);            			
			var r = new Ray(localView.fromVec, RayTracer.voidVec);
			int y = interval.yfrom;			
			var nhits = 0;					
			while (y < interval.yto)
            {								
                var ylen = (2.0 * y) / interval.width - 1.0;
				int x = 0;
                while (x < interval.width)
                {								
                    var xlen = (2.0 * x) / interval.width - 1.0;					
					r.D = Vec.comb(xlen, leftVec, ylen, upVec);     					
			        r.D.add1(viewVec); 									
			        r.D.normalize();																
					var hit = this.intersect(r, 1e6);										
					if(hit)
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
        public void JGFsetsize(var size)
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
            var interval = new Interval(0, width, height, 0, height, 1);
            render(interval);
        }


        public void JGFvalidate()
        {
            var refval = new int[10];
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
            var dev = checksum - refval[size];
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

        public void JGFrun1(var size)
        {
            JGFsetsize(size);
            JGFinitialise();
            JGFapplication();
            JGFvalidate();
            JGFtidyup();
        }

        public static void Main()
        {
			var rtb = new JGFRayTracerBench();            
			rtb.JGFrun();			
			Console.WriteLine("RayTracer completed!!!");			
        }
    }
}
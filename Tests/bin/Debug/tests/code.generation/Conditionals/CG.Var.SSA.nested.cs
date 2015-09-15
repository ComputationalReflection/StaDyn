using System;

namespace CG.Var.SSA.Nested {
	public class Rectangle {
        private int x, y, width, height;
        public Rectangle(int x, int y, int width, int height) {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
        public int getX() { return x; }
        public int getY() { return y; }
        public int getWidth() { return width; }
        public int getHeight() { return height; }
    }

    public class Circle {
        private int x, y, radius;
        public Circle(int x, int y, int radius) {
            this.x = x;
            this.y = y;
            this.radius = radius;
        }
        public int getX() { return x; }
        public int getY() { return y; }
        public int getRadius() { return radius; }
    }
	
	public class VarWrap {
        private var attribute;

        public VarWrap(var param) {
            this.attribute = param;
        }
		
        public VarWrap() { }

        public var get() { 
            return attribute; 
        }

        public void set(var param) {
            attribute = param; 
        }
    }	
	
    public class Test{
		public var attribute;
	
		public static void referenceTestWhileIf() {
            int n; char c; double d; string s; bool b;
            var r;
            r = new Circle(1, 1, 10);
			int i = 0;
            while (i<10) {
                r.getX();
                if (true) {
                    r = new Rectangle(1, 1, 10, 20);
                    r.getWidth();
                }
                else {
                    r = new Circle(1, 1, 10);
                    r.getRadius();
                }
                r = new Rectangle(1, 1, 10, 20);
                r.getY();
				i++;
            }
            r.getX() + r.getY();
        }

        public static void referenceTestIfWhile() {
            int n; char c; double d; string s; bool b;
            var r;
            r = new Circle(1, 1, 10);
			int i = 0;
            if (true) {
                r.getRadius();
                while (i<10) {
                    r.getX();
                    r = new Rectangle(1, 1, 10, 20);
					i++;
                }
                r.getX() + r.getY();
            }
            else {
                r.getRadius();
                while (i<10) {
                    r.getX();
                    r = new Rectangle(1, 1, 10, 20);
					i++;
                }
                r.getX() + r.getY();
            }
            r.getX() + r.getY();
        }
		
        public static void objTestWhileIf() {            		
			int n; char c; double d; string s; bool b;
            var r;
            r = new VarWrap('a');
            int i = 0;
            while (i < 10) {
                d = r.get();
                if (true) {
                    d = r.get();
                    r.set(3);
                    n = r.get();
                }
                else {
                    d = r.get();
                    r.set(3.3);
                    d = r.get();
                }
                d = r.get();
                r.set(3.3);
                d = r.get();
				i++;
            }
            d = r.get();			
        }
		
		public static void objTestIfWhile() {
            int n; char c; double d; string s; bool b;
            var r = new VarWrap('a');
			int i = 0;
            if (true) {
                c = r.get();
                while (i < 10) {
                    n = r.get();
                    r.set(20);
                    n = r.get();
					i++;
                }
                n = r.get();
            }
            else {
                c = r.get();
                while (i < 10) {
                    d = r.get();
                    r.set(2.2);
                    d = r.get();
					i++;
                }
                d = r.get();
            }
            d = r.get();
        }
		
		 public void thisTestWhileIf() {
            int n; char c; double d; string s; bool b;
            attribute = new VarWrap('a');
			int i = 0;
            while (i < 10) {
                d = attribute.get();
                if (true) {
                    d = attribute.get();
                    attribute.set(3);
                    n = attribute.get();
                }
                else {
                    d = attribute.get();
                    attribute.set(3.3);
                    d = attribute.get();
                }
                d = attribute.get();
                attribute.set(3.3);
                d = attribute.get();
				i++;
            }
            d = attribute.get();
        }
   
		public void thisTestIfWhile() {
            int n; char c; double d; string s; bool b;
            attribute = new VarWrap('a');
			int i = 0;
            if (true) {
                c = attribute.get();
                while (i < 10) {
                    n = attribute.get();
                    attribute.set(20);
                    n = attribute.get();
					i++;
                }
                n = attribute.get();
            }
            else {
                c = attribute.get();
                while (i < 10) {
                    d = attribute.get();
                    attribute.set(2.2);
                    d = attribute.get();
					i++;
                }
                d = attribute.get();
            }
            d = attribute.get();
        }
		
		public static void runTestThis() {
            double d;
            Test test = new Test();
            test.thisTestWhileIf();
            d = test.attribute.get();   
			Console.WriteLine(d.ToString());
			test.thisTestIfWhile();
            d = test.attribute.get();			
			Console.WriteLine(d.ToString());
        }
   
        public static void Main() {
			Test.referenceTestWhileIf();
			Test.referenceTestIfWhile();
			Test.objTestWhileIf();
			Test.objTestIfWhile();			
			Test.runTestThis();
			Console.WriteLine("Successfull!!");
        }
    }
}
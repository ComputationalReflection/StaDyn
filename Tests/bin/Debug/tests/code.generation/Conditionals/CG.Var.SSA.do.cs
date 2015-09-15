using System;
using Figures;

namespace CG.Var.SSA {
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

    public class Test {

		public var attribute;

        public void setAttribute(var p) {
            this.attribute = p;
        }

        public var getAttribute() {
            return this.attribute;
        }

        public static void testDoVar() {
            var reference;
            int i;
            reference = new Circle(0, 0, 10);
            do {
                i = i + reference.getX();
                reference = new Rectangle(reference.getX(), reference.getY(), i * 2, i * 4); // * Recursive type inference
                reference.getWidth();
				i++;
            } while (i < 10);
            int n = reference.getY() + reference.getX();
        }

        public static void testDoAttributes() {
            Test obj = new Test();
            obj.setAttribute(new Test());
            obj.getAttribute().setAttribute("hello");
            obj.getAttribute().getAttribute().Length;
			int i = 0;
            do {
                obj.getAttribute().getAttribute() + 3;
                obj.getAttribute().setAttribute(3);
                obj.attribute.attribute + '3';
				i++;
            } while (i < 10);
            string s = obj.getAttribute().getAttribute() + "3";
			Console.WriteLine(s);
        }

        public void testDoThisAttributes() {
            this.setAttribute(new Test());
            this.getAttribute().setAttribute("hello");
            this.getAttribute().getAttribute().Length;
			int i = 0;
            do {
                this.getAttribute().getAttribute() + 3;
                this.getAttribute().setAttribute(3);
                this.attribute.attribute + '3';
				i++;
            } while (i < 10);
            string s = this.getAttribute().getAttribute() + "3";
			Console.WriteLine(s);
        }     

        public static void Main() {
			Test.testDoVar();						
			Test.testDoAttributes();
			Test t = new Test();			
			t.testDoThisAttributes();
			Console.WriteLine("Successful!!");
        }
    }
}


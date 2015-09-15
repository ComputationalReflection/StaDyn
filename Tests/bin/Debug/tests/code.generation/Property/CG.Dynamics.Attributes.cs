using System;

namespace Testing.Dynamics {
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

    public class Attributes {
        public var dynAttribute;
        public var staAttribute;

        public void set(bool condition, var param1, var param2) {            
            if (condition) {
                this.dynAttribute = param1;
                this.staAttribute = param1;
            }
            else {
                this.dynAttribute = param2;
                this.staAttribute = param2;
            }   
        }

        // * The return reference is dynamic
        public var getDynAttribute() {
            return dynAttribute;
        }

        // * The return reference is static
        public var getStaAttribute() {
            return staAttribute;
        }

        public static void Main() {
            Attributes obj = new Attributes();
            obj.set(true, new Circle(0, 0, 10), new Rectangle(0, 0, 10, 20));

            obj.dynAttribute.getRadius();
            obj.staAttribute.getX();

            obj.getDynAttribute().getRadius();
            obj.getStaAttribute().getX();
			Console.WriteLine("Successfull!!");
        }
    }
}
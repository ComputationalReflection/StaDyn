using System;

namespace CG.Dynamics.Parameters 
{
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
	
    public class Parameters {
        public static void testDynamic(var dynParameter) {
            // * dynParameter is a dynamic reference
            // dynParameter: Circle\/Rectangle
            dynParameter.getRadius() * dynParameter.getX();
        }

        public static void testStatic(var staParameter) {
            // * staParameter is a static reference
            // staParameter: Circle\/Rectangle
            staParameter.getX() * staParameter.getY();
        }

        public static var circleOrRectangle(bool condition) {
            if (condition)
                return new Circle(1, 1, 10);
            else 
                return new Rectangle(1, 1, 20, 30);
        }

        public static void Main() {
            Parameters.testDynamic(circleOrRectangle(true));
            Parameters.testStatic(circleOrRectangle(false));
			Console.WriteLine("Successfull!!");
        }
    }
}


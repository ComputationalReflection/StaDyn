using System.Text;
using System;
using Figures;

namespace Testing.Var
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
	
	public class Test
	{
		public static void MethodInvocation1()
		{
			var obj = new StringBuilder();
			obj.Insert(0, "hello");
			Console.WriteLine(obj);
		}

		public static void MethodInvocation2(bool cond)
		{
			 var figure;
			 if (cond)
				figure = new Circle(1, 2, 10);
			 else
				figure = new Rectangle(3, 4, 20, 15);
			 Console.WriteLine(figure.getX());
		}

		public static void Main()
		{
			Test.MethodInvocation1();
			Test.MethodInvocation2(true);
			Test.MethodInvocation2(false);
			Console.WriteLine("Successfull!");
		}
	}
}
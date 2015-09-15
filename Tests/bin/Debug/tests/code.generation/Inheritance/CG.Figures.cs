using System;

namespace Figures 
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
		public static void Main() 
		{
			Rectangle r = new Rectangle(1,1,1,1);
			int x = r.getX();
			if (x != 1)
				Environment.Exit(-1);
			int y = r.getY();
			if (y != 1)
				Environment.Exit(-1);
			int width = r.getWidth();
			if (width != 1)
				Environment.Exit(-1);
			int height = r.getHeight();
			if (height != 1)
				Environment.Exit(-1);	
				
			Circle c = new Circle(1,1,1);
			int xx = c.getX();
			if (xx != 1)
				Environment.Exit(-1);
			int yy = c.getY();
			if (yy != 1)
				Environment.Exit(-1);
			int radius = c.getRadius();
			if (radius != 1)
				Environment.Exit(-1);			
			Console.WriteLine("Successfull!!");
		}
	}
}


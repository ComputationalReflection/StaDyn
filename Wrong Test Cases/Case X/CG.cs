using System;

namespace Points
{
	public class Point3D
	{
		public int x;
		public int y;
		public int z;		
		public int dimensions;
		public Point3D(int x, int y, int z, int dimensions)
		{
			this.x = x;
			this.y = y;
			this.z = z;	
			this.dimensions = dimensions;			
		}
	}
	
	public class Point2D
	{		
		public int x;
		public int y;		
		public int dimensions;
		public Point2D(int x, int y, int dimensions)
		{
			this.x = x;
			this.y = y;			
			this.dimensions = dimensions;
		}
	}
	
	public class Test
	{	
		public var createPoint(var dimensions, var x, var y, var z) 
		{
			var point;
			if (dimensions==2)
				point = new Point2D(x,y,2);
			else
				point = new Point3D(x,y,z,3);
			return point;
		}				
    }
	
	public class Program 
	{
		public static void Main(string[] args) 
		{
			Test test = new Test();
			test.createPoint(3,0,0,0);
			Console.Out.WriteLine("Successful!!");
		}
    }
}
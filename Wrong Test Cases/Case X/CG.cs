using System;

namespace Points
{
	public class Node
    {
        public var data;
        public var next;
        public Node(var data, var next)
        {
            this.data = data;
            this.next = next;
        }
		
		public override String ToString()
        {
		    return "Node [data:" + data + ",next:" + next + "]";
		}
    }

	
	public class Point3D
	{
		public var x;
		public var y;
		public var z;		
		public var dimensions;
		public Point3D(var x, var y, var z, var dimensions)
		{
			this.x = x;
			this.y = y;
			this.z = z;	
			this.dimensions = dimensions;			
		}

        public override String ToString()
        {
		    return "3D [x:" + x + ",y:" + y + ",z:" + z + "]";
		}
	}
	
	public class Point2D
	{		
		public var x;
		public var y;		
		public var dimensions;
		public Point2D(var x, var y, var dimensions)
		{
			this.x = x;
			this.y = y;			
			this.dimensions = dimensions;
		}
        
        public override String ToString()
        {
            return "2D [x:" + x + ",y:" + y + "]";
        }
	}
	
	public class Points
	{	
		private var createPoint(var dimensions, var x, var y, var z) 
		{
			var point;
			if (dimensions==2)
				point = new Point2D(x,y,2);
			else
				point = new Point3D(x,y,z,3);
			return point;
		}
		
		private var createPoints(var number)
        {
            var list;
			var point;
			var i;
            point = createPoint(3, 0, 0, 0);
            list = new Node(point, 0);
			i = 1;
			while (i < number)
            {
                point = createPoint(2, 0, 0, 0);
                list = new Node(point, list);
                i = i + 1;
            }
            return list;
        }
					
        public void Run() 
		{          			
            var list = createPoints(3);
            System.Console.WriteLine("Result {0}", list);
        }
    }
	
	public class Program 
	{
		public static void Main(string[] args) 
		{
			Points points = new Points();
			points.Run();
			Console.WriteLine("Successful!!");
		}
    }
}
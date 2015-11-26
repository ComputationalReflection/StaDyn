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
    }

    public class Points
    {
        private var createPoint(var dimensions, var x, var y, var z)
        {
            var point;
            if (dimensions == 2)
                point = new Point2D(x, y, dimensions);
            else
                point = new Point3D(x, y, z, 3);
            return point;
        }


        private var createPoints(var number)
        {
            var i;
            var list, point;

            i = 1;
            point = createPoint(3, 0, 0, 0);
            list = new Node(point, 0);
            while (i < number)
            {
                point = createPoint(i % 2 + 2, number / 2 - i, i, i);
                list = new Node(point, list);
                i = i + 1;
            }
            return list;
        }

        public void Run()
        {
            var numberOfPoints;
            var list, positive, point;
            numberOfPoints = 10000;
            list = createPoints(numberOfPoints);
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
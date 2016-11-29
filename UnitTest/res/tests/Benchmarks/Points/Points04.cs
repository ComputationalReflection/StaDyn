using System;

namespace Points
{
    public class Node
    {
        public dynamic  data;
        public dynamic  next;
        public Node(dynamic  data, dynamic  next)
        {
            this.data = data;
            this.next = next;
        }
        public override string ToString()
        {
            return "Node[data=" + data.ToString() + ",next=" + next.ToString() + "]";
        }
    }

    public class Point3D
    {
        public dynamic  x;
        public dynamic  y;
        public dynamic  z;
        public dynamic  dimensions;
        public Point3D(dynamic  x, dynamic  y, dynamic  z, dynamic  dimensions)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.dimensions = dimensions;
        }
        public override string ToString()
        {
            return "Point3D[x=" + x.ToString() + ",y=" + y.ToString() + ",z=" + z.ToString() + "]";
        }
    }

    public class Point2D
    {
        public dynamic  x;
        public dynamic  y;
        public dynamic  dimensions;
        public Point2D(dynamic  x, dynamic  y, dynamic  dimensions)
        {
            this.x = x;
            this.y = y;
            this.dimensions = dimensions;
        }
        public override string ToString()
        {
            return "Point2D[x=" + x.ToString() + ",y=" + y.ToString() + "]";
        }
    }

    public class Points
    {
        private dynamic  createPoint(dynamic  dimensions, dynamic  x, dynamic  y, dynamic  z)
        {
            dynamic  point;
            if (dimensions == 2)
                point = new Point2D(x, y, dimensions);
            else
                point = new Point3D(x, y, z, 3);
            return point;
        }


        private dynamic  createPoints(dynamic  number)
        {
            dynamic  i;
            dynamic  list, point;

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

        dynamic  positiveX(dynamic  list, dynamic  n)
        {
            dynamic  i;
            dynamic  l, result;
            i = 0;
            result = i;
            l = list;
            while (i < n)
            {
                if (l.data.x >= 0)
                    result = new Node(l.data, result);
                l = l.next;
                i = i + 1;
            }
            return result;
        }

        public void Run()
        {
            dynamic  numberOfPoints;
            dynamic  list, positive, point;
            numberOfPoints = 100;
            list = createPoints(numberOfPoints);
            System.Console.WriteLine("Result {0}", list);
            positive = positiveX(list, numberOfPoints);
            System.Console.WriteLine("Result {0}", positive);
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
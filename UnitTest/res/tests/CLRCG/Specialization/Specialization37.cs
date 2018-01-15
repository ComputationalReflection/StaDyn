using System;

namespace Points
{
    public class Point3D
    {
        public dynamic x;
        public dynamic y;
        public dynamic z;
        public Point3D(dynamic x, dynamic y, dynamic z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public override String ToString()
        {
            return "3D [x:" + x + ",y:" + y + ",z:" + z + "]";
        }
    }

    public class Point2D
    {
        public dynamic x;
        public dynamic y;
        public Point2D(dynamic x, dynamic y)
        {
            this.x = x;
            this.y = y;
        }

        public override String ToString()
        {
            return "2D [x:" + x + ",y:" + y + "]";
        }
    }


    public class Node
    {
        public dynamic data;
        public dynamic next;
        public Node(dynamic data, dynamic next)
        {
            this.data = data;
            this.next = next;
        }

        public override string ToString()
        {
            return "Node[data=" + data.ToString() + ",next=" + next.ToString() + "]";
        }
    }

    public class Program
    {
        public static dynamic Method(dynamic param)
        {
            return param.data.x;
        }

        public static void Main(string[] args)
        {
            dynamic node;
            if (true)
            {
                var point = new Point2D(1, 2);
                node = new Node(point, 0);
            }
            else
            {
                var point = new Point3D(1, 2, 3);
                node = new Node(point, 0);
            }
            dynamic result = Program.Method(node);
            System.Console.WriteLine("Result {0}", result);
        }
    }
}
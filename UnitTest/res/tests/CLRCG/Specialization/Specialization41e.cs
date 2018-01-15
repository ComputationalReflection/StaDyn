using System;

namespace Points
{
    public class Point
    {
        public var x;
        public Point(var x)
        {
            this.x = x;
        }
        public override string ToString()
        {
            return "Point[x=" + x.ToString() + "]";
        }
    }

    public class Program
    {
        public var GetX(var paramPoint)
        {
            var localPoint = paramPoint;
            return paramPoint.x + localPoint.x;
        }
        
        public static void Main(string[] args)
        {
            Program program = new Program();
            var point = new Point(2);
            var x = program.GetX(point);
            System.Console.WriteLine("Result {0}", x);
        }
    }
}
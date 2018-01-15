using System;

namespace Points
{
    public class Chronometer
    {
        private DateTime ticks1, ticks2;
        private bool stopped;

        public void Start()
        {
            ticks1 = DateTime.Now;
            stopped = false;
        }
        public void Stop()
        {
            ticks2 = DateTime.Now;
            stopped = true;
        }

        private static int TicksToMicroSeconds(DateTime t1, DateTime t2)
        {
            return TicksToMiliSeconds(t1, t2) * 1000;
        }

        private static int TicksToMiliSeconds(DateTime t1, DateTime t2)
        {
            TimeSpan difference = t2.Subtract(t1);
            return (difference.Milliseconds + difference.Seconds * 1000 + difference.Minutes * 60000);
        }

        private static int TicksToSeconds(DateTime t1, DateTime t2)
        {
            TimeSpan difference = t2.Subtract(t1);
            return (difference.Seconds + difference.Minutes * 60);
        }

        public int GetMicroSeconds()
        {
            if (stopped)
                return TicksToMicroSeconds(ticks1, ticks2);
            return TicksToMicroSeconds(ticks1, DateTime.Now);
        }

        public int GetMiliSeconds()
        {
            if (stopped)
                return TicksToMiliSeconds(ticks1, ticks2);
            return TicksToMiliSeconds(ticks1, DateTime.Now);
        }

        public int GetSeconds()
        {
            if (stopped)
                return TicksToSeconds(ticks1, ticks2);
            return TicksToSeconds(ticks1, DateTime.Now);
        }
    }

    public class Node
    {
        public object data;
        public object next;
        public Node(object data, object next)
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
        public override string ToString()
        {
            return "Point3D[x=" + x.ToString() + ",y=" + y.ToString() + ",z=" + z.ToString() + "]";
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
        public override string ToString()
        {
            return "Point2D[x=" + x.ToString() + ",y=" + y.ToString() + "]";
        }
    }

    public class Points
    {
        private object createPoint(int dimensions, int x, int y, int z)
        {
            object result;
            if (dimensions == 2)
            {
                Point2D point2D = new Point2D(x, y, 2);
                result = point2D;
            }
            else
            {
                Point3D point3D = new Point3D(x, y, z, 3);
                result = point3D;
            }
            return result;
        }

        private Node createPoints(int number)
        {
            int i = 1;
            object point = this.createPoint(3, 0, 0, 0);
            Node list = new Node(point, 0);
            while (i < number)
            {
                point = this.createPoint(i % 2 + 2, number / 2 - i, i, i);
                list = new Node(point, list);
                i = i + 1;
            }
            return list;
        }

        private object positiveX(Node list, int n)
        {
            int i = 0;
            object l = list;
            object result;
            result = i;
            while (i < n)
            {
                if (l is Node)
                {
                    if (((Node)l).data is Point3D)
                        if (((Point3D)((Node)l).data).x >= 0)
                            result = new Node(((Node)l).data, result);
                        else if (((Node)l).data is Point2D)
                            if (((Point2D)((Node)l).data).x >= 0)
                                result = new Node(((Node)l).data, result);
                    l = ((Node)l).next;
                }
                i = i + 1;
            }
            return result;
        }

        object distance3D(object point)
        {
            object value = 2147483647;
            object dimensions = point.GetType().GetField("dimensions").GetValue(point);
            if (dimensions is int && (int)dimensions == 3)
                return (int)point.GetType().GetField("x").GetValue(point) *
                       (int)point.GetType().GetField("x").GetValue(point) +
                       (int)point.GetType().GetField("y").GetValue(point) *
                       (int)point.GetType().GetField("y").GetValue(point) +
                       (int)point.GetType().GetField("z").GetValue(point) *
                       (int)point.GetType().GetField("z").GetValue(point);
            return value;
        }

        private int distance3D(Point3D point)
        {
            int value = 2147483647;
            if (point.dimensions == 3)
                value = point.x * point.x + point.y * point.y + point.z * point.z;
            return value;
        }

        private object distance3D__1_Point3D_or_Point2D(object point)
        {
            if (point is Point3D)
                return (object)distance3D((Point3D)point);
            return distance3D(point);
        }

        private object closestToOrigin3D(Node list, int n)
        {
            int i, minDistance;
            object point3D = createPoint(3, 0, 0, 0);
            minDistance = 2147483647;
            object l = list;
            i = 0;
            while (i < n)
            {
                if (l is Node)
                {
                    if ((int)distance3D__1_Point3D_or_Point2D(((Node)l).data) < minDistance)
                    {
                        minDistance = (int)distance3D__1_Point3D_or_Point2D(((Node)l).data);
                        point3D = ((Node)l).data;
                    }
                    l = ((Node)l).next;
                }
                i = i + 1;
            }
            return point3D;
        }

        public void test()
        {
            int numberOfPoints = 10000;
            Node list = createPoints(numberOfPoints);
            object positive = positiveX(list, numberOfPoints);
            object point = closestToOrigin3D(list, numberOfPoints);
            //System.Console.WriteLine("Full List: {0}", list);
            //System.Console.WriteLine("Positive X List: {0}", positive);
            //System.Console.WriteLine("Closest Point: {0}", point);
        }
    }

    public class BenchMark
    {
        private int iterations;
        protected int microSeconds;

        public BenchMark(int iterations)
        {
            this.iterations = iterations;
        }

        public int run()
        {
            BenchMark self = this;
            for (int i = 0; i < iterations; i++)
                self.runOneIteration();
            return this.microSeconds;
        }

        public object runOneIteration()
        {
            Chronometer chronometer = new Chronometer();
            Points test = new Points();
            chronometer.Start();
            test.test();
            chronometer.Stop();
            this.microSeconds = this.microSeconds + chronometer.GetMicroSeconds();
            return null;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            BenchMark benchMark = new BenchMark(1);
            Console.WriteLine(benchMark.run());
        }
    }
}
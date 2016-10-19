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
        public override string ToString()
        {
            return "Node[data=" + data.ToString() + ",next=" + next.ToString() + "]";
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var list = new Node(1, new Node(2, 3));
            var size = 2;
            while (size > 0)
            {
                list = list.next;
                size = size - 1;
            }
            System.Console.WriteLine("Result {0}", list);
            Console.WriteLine("Successful!!");
        }
    }
}
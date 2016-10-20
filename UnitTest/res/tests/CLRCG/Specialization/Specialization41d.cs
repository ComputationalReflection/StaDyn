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
        public static var PositiveData(var list)
        {
            return list.data;            
        }

        public static void Main(string[] args)
        {
            var list = new Node(1, new Node(-1, 0));
            var result = Program.PositiveData(list);
            System.Console.WriteLine("Result {0}", result);
            Console.WriteLine("Successful!!");
        }
    }
}
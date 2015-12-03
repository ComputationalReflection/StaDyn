using System;

namespace ProgramSpecialization
{
    public class Node
    {
        public var data;
        public Node(var data)
        {
            this.data = data;
        }
    }

    public class Program
    {
        public var Method(var node)
        {
            return node.data;
        }

        public static void Main(string[] args)
        {
            var node;
            var result;
            node = new Node(1);
            result = Method(node);
            System.Console.WriteLine("Result {0}", result);
            node = new Node("1");
            result = Method(node);
            System.Console.WriteLine("Result {0}", result);
            Console.WriteLine("Successful!!");
        }
    }
}
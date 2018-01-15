using System;

namespace Specialization
{
    public class Node
    {
        public var data;
        public Node(var data)
        {
            this.data = data;
        }
    }

    public class Test
    {
        public var GetData(var node)
        {
            return node.data;
        }
        
        public static void Main(string[] args)
        {
            Test test = new Test();
            var node;
            var result;
            node = new Node(1);
            result = test.GetData(node);
            System.Console.WriteLine("Result {0}", result.ToString());
            node = new Node("One");
            result = test.GetData(node);
            System.Console.WriteLine("Result {0}", result.ToString());
            Console.WriteLine("Successful!!");
        }
    }
}
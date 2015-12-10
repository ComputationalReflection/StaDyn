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
        
        public void Run()
        {
            var node;
            var result;
            if (true)
                node = new Node(1);
            else
                node = new Node("One");
            result = this.GetData(node);
            System.Console.WriteLine("Result {0}", result);
            Console.WriteLine("Successful!!");
        }

        public static void Main(string[] args)
        {
            Test test = new Test();
            test.Run();
        }
    }
}
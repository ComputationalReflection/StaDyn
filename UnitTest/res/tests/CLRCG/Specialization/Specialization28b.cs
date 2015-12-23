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
        public var GetData(var data)
        {
            return data;
        }

        public var GetThisData(var data)
        {
            return data;
        }


        public void Run()
        {
            var node;
            var result;
            node = new Node(1);
            result = this.GetThisData(node.data);
            node = new Node("One");
            result = this.GetThisData(node.data);
        }

        public static void Main(string[] args)
        {
            Test test = new Test();
            var node;
            var result;
            node = new Node(1);
            result = test.GetData(node.data);
            node = new Node("One");
            result = test.GetData(node.data);

        }
    }
}
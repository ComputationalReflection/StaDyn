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

        public override string ToString()
        {
            return "Node[" + this.data.ToString() +"]";
        }
    }

    public class Test
    {
        public static dynamic GetParam(dynamic param)
        {
            return param;
        }

        public static dynamic GetData(dynamic param)
        {
            return param.data;
        }

        public static void Main(string[] args)
        {            
            var node;
            var result;
            node = new Node(1);
            result = Test.GetParam(node);
            System.Console.WriteLine("GetParam Result {0}", result.ToString());
            result = Test.GetData(node);
            System.Console.WriteLine("GetData Result {0}", result.ToString());
            
            node = new Node("One");
            result = Test.GetParam(node);
            System.Console.WriteLine("GetParam Result {0}", result.ToString());
            result = Test.GetData(node);
            System.Console.WriteLine("GetData Result {0}", result.ToString());
        }
    }
}
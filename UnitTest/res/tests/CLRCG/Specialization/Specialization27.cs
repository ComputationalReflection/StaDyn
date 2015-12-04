using System;
using Compiler;

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

    public class SpecializationTest
    {
        public var GetData(var data)
        {
            return data;
        }

        public static void Main(string[] args)
        {
            SpecializationTest program = new SpecializationTest();
            var node;
            var result;
            node = new Node(1);
            result = program.GetData(node.data);
            System.Console.WriteLine("Result {0}", result.ToString());
            node = new Node("One");
            result = program.GetData(node.data);
            System.Console.WriteLine("Result {0}", result.ToString());
            Console.WriteLine("Successful!!");
        }
    }
}
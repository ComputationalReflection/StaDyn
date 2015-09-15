
using System;

namespace CG.CustomStack
{
    public class Node
    {
        public Node next;
        public Object info;
    }

    public class Stack
    {
        private Node start;

        public void Push(object info)
        {
            Node node = new Node();
            node.info = info;
            node.next = start;
            start = node;
        }

        public object Pop()
        {
            if (start == null)
                return null;
            object result = start.info;
            start = start.next;
            return result;
        }

        public override string ToString()
        {
            String result = "";
            Node pointer = this.start;
            while (pointer != null)
            {
                result += pointer.info.ToString();
                pointer = pointer.next;
            }
            return result;
        }

        public static void Main()
        {
            Stack stack = new Stack();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            Console.WriteLine(stack.ToString());
            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.ToString());
			Console.WriteLine("Successful!!");
        }
    }
}
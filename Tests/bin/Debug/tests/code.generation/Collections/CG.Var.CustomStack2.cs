
using System;

namespace CG.Var.CustomStack2
{
    public class Node
    {
        public var next;
        public var info;
    }

    public class Stack
    {
        private var start;

        public void Push(var info)
        {
            var node = new Node();
            node.info = info;
            node.next = start;
            start = node;
        }

        public var Pop()
        {			         			
			if(!IsEmpty())
			{
				var result = start.info;				
				start = start.next;
				return result;
			}
			return start;
        }
		
		public bool IsEmpty()
        {			         
			return this.start == null;
        }

        public override string ToString()
        {
            String result = "";
            var pointer = this.start;
            while (pointer != null)
            {
				var nodeInfo = pointer.info;
                result += nodeInfo.ToString();
                pointer = pointer.next;
            }
            return result;
        }

        public static void Main()
        {
            var stack = new Stack();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            Console.WriteLine(stack.ToString());						
			while(!stack.IsEmpty())
				Console.WriteLine("{0}",stack.Pop());							
			Console.WriteLine("Successful!!");
        }
    }
}
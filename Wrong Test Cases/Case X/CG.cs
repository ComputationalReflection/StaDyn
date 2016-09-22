using System;

namespace Points
{
    public class Node
    {
        public dynamic data;
        public dynamic next;
        public Node(dynamic data, dynamic next)
        {
            this.data = data;
            this.next = next;
        }		
    }
    
    public class Program
    {
        public static void Main(string[] args)
        {            
            dynamic size = 2;
			if (size > 0)
            {
                size = size - 1;
            }            
        }
    }
}
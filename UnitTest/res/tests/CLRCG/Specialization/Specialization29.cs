using System;

namespace Specialization
{
    public class Test
    {
        public dynamic GetData(dynamic data)
        {
            return data;
        }
        
        public static void Main(string[] args)
        {
            Test test = new Test();
            var data;
            var result;
            if (true)
            {
                data = "One";
                result = test.GetData(data);
                System.Console.WriteLine("Result {0}", result);                
            }
            else
            {
                data = 1;
                result = test.GetData(data);
                System.Console.WriteLine("Result {0}", result);
            }
            result = test.GetData(data);
            System.Console.WriteLine("Result {0}", result);
            Console.WriteLine("Successful!!");
        }
    }
}
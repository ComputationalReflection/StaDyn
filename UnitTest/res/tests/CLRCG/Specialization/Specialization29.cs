using System;

namespace Specialization
{
    public class Test
    {
        public var GetData(var data)
        {
            return data;
        }

        public void Run()
        {
            var data;
            var result;
            if (true)
            {
                data = 1;
                result = this.GetData(data);
                System.Console.WriteLine("Result {0}", result);
            }
            else
            {
                data = "1";
                result = this.GetData(data);
                System.Console.WriteLine("Result {0}", result);
            }
            result = this.GetData(data);
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
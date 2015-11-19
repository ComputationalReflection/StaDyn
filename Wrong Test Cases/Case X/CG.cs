using System;

namespace CaseX
{
    public class Test
    {
		public int Metodo()
		{
			return 1;
		}
        public static void Main()
        {
			Test test = new Test();
			test.Metodo();
            Console.Out.WriteLine("Sucessful!!");
        }
    }
}
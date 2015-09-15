using System;
namespace Identification.Attributes
{    
    public class Program
    {
        int[] myArray = new int[] {1, 2, 3, 4, 5};
        public static int constant = 1;


        public static void CallMethod()
        {
            //Error: Instance requiered for non static attributes:  Common Language Runtime detectó un programa no válido.
            Console.WriteLine(myArray[2]);
        }

        public void HelloWorld()
        {
            Console.WriteLine("¡¡¡Hola Mundo!!!");
        }


        public static void Main(string[] args)
        {
            CallMethod();
            Console.WriteLine(constant);
            //Should be an Error. No static methods cannot be called without an instance
            HelloWorld();

            Program p = new Program();
            p.HelloWorld();
        }
    }
}
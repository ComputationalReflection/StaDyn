using System;

namespace ProgramSpecialization
{
    public class Program
    {
        public static void Main(string[] args)
        {
            dynamic i_0 = 0; //i_0 : int
            i_2 = i_0; //Move

            //i_2 = phi(i_0, i_1) //i_2 : \/(int, typeof(i_1))
            while (i_2 < 10)
            {
                i_1 = i_2 + 1; //i_1 : \/(typeof(i_2), int)
                i_2 = i_1; //Move
            }            
            System.Console.WriteLine("Result {0}", i.ToString());            
        }
    }
}


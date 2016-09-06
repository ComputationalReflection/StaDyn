using System;

public class Program
{
	public static void Main(string[] args)
	{
		dynamic sum = 0;
		for (int i = 0; i < 10; i++)
		{
			sum = sum + 1;
		    if (i == 7)
		    {
		        sum = sum + "Hello";
		    }
		}        
		System.Console.WriteLine(sum.ToString()); // 8Hello11
	}
}
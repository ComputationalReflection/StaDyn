using System;

class Test {

    public static void Main() {
        Console.Write("Your age, please: ");
        var age = Console.In.ReadLine();
        Console.WriteLine("You are " + age + " years old.");


        age = Convert.ToInt32(age);
        age++;
        Console.WriteLine("Happy birthday, you are " + age + " years old now.");

        int length = age.Length; // * Compilation error        
    }

}

using System;
//Implementing two interfaces
namespace CG.InterfaceInheritance 
{
    public interface ICanRun {
        void run();
    }

    public interface ICanSwim {
        void swim();
    }
    public class Frog : ICanSwim {
        public void swim() {
            Console.WriteLine("frog swimming...");
        }
    }

    public class Person : ICanRun, ICanSwim {
        public void run() {
            Console.WriteLine("Person running...");
        }
        public void swim() {
            Console.WriteLine("Person swimmimng...");
        }
    }

	public class Test {
        public static void Main() {
            Person p = new Person();
            p.run();
            p.swim();
            ICanSwim f = new Frog();
            f.swim();
        }
    }
}
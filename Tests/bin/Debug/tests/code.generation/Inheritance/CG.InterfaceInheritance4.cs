using System;
//Implementing two interfaces an abstract class and a base class.
namespace CG.InterfaceInheritance {
    public interface ICanRun {
        void run();
    }
    public interface ICanSwim {
        void swim();
    }
    public abstract class Animal {
        public abstract void live();
    }
    public class Person : Animal, ICanRun, ICanSwim {
        public void run() {
            Console.WriteLine("Person running...");
        }
        public void swim() {
            Console.WriteLine("Person swimmimng...");
        }
        public override void live() {
            Console.WriteLine("Person living...");
        }
    }
    public class Test {
        public static void Main() {
            Person p = new Person();
            p.run();
            p.swim();
            p.live();
        }
    }
}
using System;
namespace CG.InterfaceInheritance 
{
    public interface IMovible {
        void move();
    }
    public class Truck : IMovible {
        public void move() {
            Console.WriteLine("Truck moving...");
        }
    }
    public class Car : IMovible {
        public void move() {
            Console.WriteLine("Car moving...");
        }
    }
    public class Test {
        public static void Main() {
            IMovible i1 = new Car(), i2 = new Truck();
            i1.move();
            i2.move();
        }
    }
}
using System;
namespace GC.TestAbstract.SimpleInheritance {
    abstract class Figure {
        public abstract void Draw();
    }
    class Triangule : Figure {
        public override void Draw() {
            Console.WriteLine("Triangule");
        }
    }
    class Square : Figure {
        public override void Draw() {
            Console.WriteLine("Square");
        }
    }
    class Test {
        public static void Main(string[] args) {
            Figure f1 = new Triangule(), f2 = new Square();
            f1.Draw();
            f2.Draw();
        }
    }

}


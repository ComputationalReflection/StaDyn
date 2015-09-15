using System;
namespace GC.TestAbstract.SimpleInheritance {
    abstract class Figure {
        //public Figure() { }
        private int sides;
        public int getSides() {
            return sides;
        }
        public Figure(int sides) {
            this.sides = sides;
        }
        public abstract void Draw();
    }
    class Triangle : Figure {
        public Triangle() : base(3) { }

        public override void Draw() {
            Console.WriteLine(System.String.Format("Triangle: {0} sides", this.getSides()));
        }
    }
    class Test {
        public static void Main(string[] args) {
            Figure f1 = new Triangle();
            f1.Draw();
        }
    }

}


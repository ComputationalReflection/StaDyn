using System;
namespace GC.TestAbstract.SimpleInheritance {
    abstract class Figure {
        //public Figure() { }
        public abstract void Define();
        public abstract void Draw();
    }
    class Triangle :Figure{
        public override void Draw() {
            Console.WriteLine("Triangle");
        }
        public override void Define() {
            Console.WriteLine("General");
        }
    } 
    class Escaleno: Triangle {
        public override void Define() {
            Console.WriteLine("Escaleno");
        }
    }
    class Test {
        public static void Main(string[] args) {
            Figure f1 = new Escaleno();
            f1.Draw();
            f1.Define();
        }
    }

}

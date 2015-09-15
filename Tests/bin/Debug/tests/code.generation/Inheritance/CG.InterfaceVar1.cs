using System;
//Implementing interfaces with var features.
namespace CG.InterfaceVar {
    public interface IMultiplicable {
        double mult(var x);
        void p();
    }
	
    public class RealNumber : IMultiplicable {
        private double number;
        public RealNumber(double number) {
            this.number = number;
        }
        public double mult(var x) {
            return x * this.number;
        }
        public void p( ) {
            System.Console.WriteLine(number);
        }
    }

    public class Test {
        public static void Main() {
            IMultiplicable m = new RealNumber(8.5);
			System.Console.WriteLine(m.mult(2));
			m.p();
        }
    }
}
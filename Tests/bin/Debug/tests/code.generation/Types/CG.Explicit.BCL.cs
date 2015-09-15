using System;

namespace CG.TestBCL {

    class ClassA: Object {
        public void m() {
            // * BCL qualified names resolution
            Console.WriteLine( base.ToString() );
            // * BCL overload
            random.Next();
            random.Next(10);
            random.Next(0,10);
        }

        // * BCL attribute definition
        private Random random = new Random();

        public bool theSame(Object parameter) {
            // * BCL inheritance of static members
            return Equals(this, parameter); // Equals is a static member of System.Object
        }

        public void properties() {
            // * BCL properties
            // read
            string theASCIInameInstalled = System.Text.ASCIIEncoding.ASCII.BodyName;
            // write
            Console.Title="title";
        }
    }

    class ClassB: ClassA, ICloneable, Clonable {
                                       // * BCL arguments
        public Object cloneAICloneable(ICloneable clonable) {
            // * BCL interfaces
            return clonable.Clone();
        }
        public override Object Clone() {
            // * BCL inheritance and protected members
            return base.MemberwiseClone();
        }
    }

    interface Clonable: ICloneable {}


    // * Any class is a subtype of System.Object (except System.Object)
    class ClassC {
        bool theSame(Object other) {
            // * Without implicit object
            Equals(other);
            // * With the this reserved word
            this.Equals(other);
            // * With the base reserved word
            return this.Equals(other);
        }

        public static void Main() { }

    }


}
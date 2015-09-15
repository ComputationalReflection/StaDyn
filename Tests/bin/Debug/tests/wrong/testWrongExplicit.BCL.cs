using System;

namespace Testing.Wrong.TestBCL {


    // * Tests to check semantic errors
    class Wrong {
        public void overload() {
            Random r = new Random();
            // * Method not implemented
            r.Next(0.4);  // * Error
        }

        public void properties() {
            // * Unknown property
            System.Text.ASCIIEncoding.ASCII.UnknownProperty; // * Error
            // * Read only property
            System.Text.ASCIIEncoding.ASCII.BodyName = "nice try"; // * Error
        }

        // * Unknown class
        private UnknownClass random = new UnknownClass(); // * Error

        // * Bad inheritance use
        public void theSame() {
            // * Unknown method signature
            this.Equals(); // * Error
        }
    }

    // * Unknown interface
    interface Interface1 : UnknownInterface { } // * Error

    // * Interfaces can not derive from classes
    interface Interface2 : System.Object { } // * Error

    public class Run {
        public static void Main() {
        }
    }

}
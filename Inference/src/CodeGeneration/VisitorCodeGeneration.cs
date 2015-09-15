/*    class CodeGenerator {
        virtual public void comienzo() { }
    }
    class ILCodeGenerator : CodeGenerator {
        override public void comienzo() { }
        public void etiqueta() { }
    }

    class CSharpCodeGenerator : CodeGenerator {
        override public void comienzo() { }
        public void whileStatment() { }
    }

    abstract class Visitor {
        abstract public void visit();
    }

    class VisitorCodeGeneration<T>:Visitor where T : CodeGenerator {
        protected T codeGenerator;
        public VisitorCodeGeneration(T cg) { this.codeGenerator = cg; }
        public override void visit() { this.codeGenerator.comienzo(); }
    }

    class VisitorILCodeGeneration<T> : VisitorCodeGeneration<T> where T : ILCodeGenerator {
        public VisitorILCodeGeneration(T cg) : base(cg) { }
        public override void visit() {
            this.codeGenerator.etiqueta();

        }
        static void miMain() {
            Visitor visitor =new VisitorILCodeGeneration<ILCodeGenerator>(new ILCodeGenerator());

        }
    }
    class VisitorCSharpCodeGeneration<T> : VisitorCodeGeneration<T> where T : CSharpCodeGenerator {
        public VisitorCSharpCodeGeneration(T cg) : base(cg) { }
        public override void visit() {
            this.codeGenerator.whileStatment();
        }
    }

*/



////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: VisitorCodeGeneration.cs                                             //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
//         Francisco Ortin - francisco.ortin@gmail.com
//         Daniel Zapico   - daniel.zapico.rodriguez@gmail.com
// Description:                                                               //
//    This class walks the AST to obtain the IL code.                         //
//    Inheritance: VisitorAdapter                                             //
//    Implements Visitor pattern [Concrete Visitor].                          //
//    Implements Factory method (the constructor) [Creator].                  //
// -------------------------------------------------------------------------- //
// Create date: 28-05-2007                                                    //
// Modification date: 21-08-2007                                              //
////////////////////////////////////////////////////////////////////////////////



namespace CodeGeneration {
    /// <summary>
    /// This class walks the AST to obtain the IL code.
    /// </summary>
    /// <remarks>
    /// Inheritance: VisitorAdapter.
    /// Implements Visitor pattern [Concrete Visitor].
    /// </remarks>
    abstract class VisitorCodeGeneration<T> : VisitorCodeGenerationBase where T : CodeGenerator {
        /// <summary>
        /// Class used to generate de intermediate code
        /// We implement explicitly covariance in this attribute by means of genericity
        /// </summary>
        internal T codeGenerator;

        #region Properties

        protected virtual T CodeGenerator {
            get { return this.codeGenerator; }
            set { this.codeGenerator = value; }
        }
        #endregion

        public VisitorCodeGeneration(T codeGenerator) {
            this.codeGenerator = codeGenerator;
        }

        public override void Close() {
            codeGenerator.Close();
        }
        public override void AddExceptionCode() {
            this.codeGenerator.WriteCodeOfExceptions();
        }

    }
}

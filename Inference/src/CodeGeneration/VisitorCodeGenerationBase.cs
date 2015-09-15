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
// File: VisitorCodeGenerationBase.cs                                             //
// Author:                   //
// Description:                                                               //
//    This class walks the AST to obtain the IL code.                         //
//    Inheritance: VisitorAdapter                                             //
//    Implements Visitor pattern [Concrete Visitor].                          //
//    Implements Factory method (the constructor) [Creator].                  //
// -------------------------------------------------------------------------- //
// Create date: 28-05-2007                                                    //
// Modification date: 21-08-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using AST;
using Tools;
using TypeSystem;
using ErrorManagement;

namespace CodeGeneration
{
   /// <summary>
   /// This class walks the AST to to obtain intermediate code
   /// Thisis a layer adapter class and is the base classBy means of thisi class we're able to perform covariance in certain attributes between 
   /// visitor and codegeneration clases
   /// </summary>
   /// <remarks>
   /// Inheritance: VisitorAdapter.
   /// Implements Visitor pattern [Concrete Visitor].
   /// </remarks>
   abstract class VisitorCodeGenerationBase : VisitorAdapter {
       /// <summary>
       /// Class used to generate de intermediate code    
       /// We implement explicitly covariance in this attribute by means of genericity
       /// </summary>
        // helper methods
       //public abstract void AddExceptionCode();

       public abstract void Close();
       public abstract void AddExceptionCode();
   }
}

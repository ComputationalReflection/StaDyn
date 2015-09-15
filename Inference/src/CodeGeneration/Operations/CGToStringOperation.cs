using TypeSystem.Operations;
using TypeSystem;
using ErrorManagement;
using AST;
using Tools;
using System.Collections.Generic;
using CodeGeneration.ExceptionManagement;
namespace CodeGeneration.Operations {
    /// <summary>
       ///Generates the code for converting an Object of any Type to a String       

    internal class CGToStringOperation <T>:TypeSystemOperation where T: ILCodeGenerator {

        /// <summary>
        /// streamto to write to
        /// </summary>
        private T codeGenerator;
        /// <summary>
        /// indentation to use
        /// </summary>
        private int indent;

        private bool boxed;
        public CGToStringOperation(T codeGenerator, int indent, bool boxed) {
            this.codeGenerator = codeGenerator;
            this.indent = indent;
            this.boxed = boxed;
          }

        public CGToStringOperation(T codeGenerator, int indent) : this(codeGenerator, indent, false) { }
        /// <summary>
        /// Default operation for any type
        /// Object::ToString() is called.
        /// if the arg argument has a non null value, then a box is needed. It's an extra responsibility added to the method, but
        /// we think is very related
        /// </summary>
        /// <param name="t">TypeExpression to convert To as String</param>
        /// <param name="arg">if has a non null value, then it performs a Box convertion over t</param>
        /// <returns>null</returns>
        public override object Exec(TypeExpression t, object arg) {
            if ( !boxed)
                this.codeGenerator.BoxIfNeeded(this.indent, t);
            this.codeGenerator.WriteLine(this.indent, "callvirt instance string [mscorlib]System.Object::ToString()");

            return null;
        }
        /// <summary>
        ///  We redefine default behaviour for Strings in order to optimize the code generated. That is there's no need of perform a convertion.
        ///  It generates no code and the parameters have no use.
        /// </summary>
        /// <param name="t">not used</param>
        /// <param name="arg">not used</param>
        /// <returns>null</returns>
        public override object Exec(StringType t, object arg) {
            return null;
        }

        public override object ReportError(TypeExpression tE) {
            ErrorManager.Instance.NotifyError(new CodeGenerationError("No se ha Puede realizar ToString sobre" + tE.FullName));
            return null;
        }
    }
}
using TypeSystem.Operations;
using TypeSystem;
using ErrorManagement;
using AST;
using Tools;
using System.Collections.Generic;
using CodeGeneration.ExceptionManagement;
namespace CodeGeneration.Operations {
    /// <summary>
    ///  It generates the code of the start of a class in IL Language.
    ///  </summary>       
    internal class CGClassDefinitionStartOperation<T> : TypeSystemOperation where T : ILCodeGenerator {

        /// <summary>
        /// streamto to write to
        /// </summary>
        private T codeGenerator;
        /// <summary>
        /// indentation to use
        /// </summary>
        private int indent;
        /// <summary>
        ///  node representing the class definition
        /// </summary>
        private ClassDefinition node;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="codeGenerator">stream to write to</param>
        /// <param name="indent">indentation to use</param>
        public CGClassDefinitionStartOperation(T codeGenerator, int indent, ClassDefinition node) {
            this.codeGenerator = codeGenerator;
            this.indent = indent;
            this.node = node;
        }
        /// <summary>
        ///  Writes the starts of the class definition
        /// </summary>
        /// <param name="ct">Type Expression representing the class</param>
        /// <returns>null</returns>
        public override object Exec(ClassType ct, object arg) {
            // In the following call the last argument could beor node.TypeExpr, but is the same of
            // the argument due to the dispatcher call
            this.codeGenerator.WriteLNClassHeader(this.indent, node.Identifier, ct);
            this.codeGenerator.WriteStartBlock(this.indent);
            return null;
        }
        /// <summary>
        ///  The if the class definition is not a class type the default behaviour is an error
        /// </summary>
        /// <param name="t"></param>
        /// <returns>null</returns>
        public override object Exec(TypeExpression t, object arg) {
            ErrorManager.Instance.NotifyError(new UserTypeExpectedError(this.node.Location));
            return null;
        }

    }
}
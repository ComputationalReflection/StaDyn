using TypeSystem.Operations;
using TypeSystem;
using ErrorManagement;
using AST;
using Tools;
using System.Collections.Generic;
using CodeGeneration.ExceptionManagement;
namespace CodeGeneration.Operations {
    /// <summary>
       ///  It generates the code of the start of an interface in IL Language.
       ///  </summary>       
    internal class CGInterfaceDefinitionStartOperation<T> : TypeSystemOperation where T: ILCodeGenerator{

        /// <summary>
        /// streamto to write to
        /// </summary>
        private T codeGenerator;
        /// <summary>
        /// indentation to use
        /// </summary>
        private int indent;
        /// <summary>
        ///  node representing the interface definition
        /// </summary>
        private InterfaceDefinition node;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="codeGenerator">stream to write to</param>
        /// <param name="indent">indentation to use</param>
        public CGInterfaceDefinitionStartOperation(T codeGenerator, int indent, InterfaceDefinition node) {
            this.codeGenerator = codeGenerator;
            this.indent = indent;
            this.node = node;
          }
        /// <summary>
        ///  Writes the starts of the interface definition
        /// </summary>
        /// <param name="ct">Type Expression representing the interface</param>
        /// <returns>null</returns>
        public override object Exec(InterfaceType it, object arg) {
            // In the following call the last argument could beor node.TypeExpr, but is the same of
            // the argument due to the dispatcher call
            this.codeGenerator.WriteInterfaceHeader(this.indent, node.Identifier, it);
            this.codeGenerator.WriteStartBlock(this.indent);
            return null;
        }
        /// <summary>
        ///  If the interface definition node is not a InterfaceType the default behaviour is an error
        /// </summary>
        /// <param name="t"></param>
        /// <returns>null</returns>
        public override object Exec(TypeExpression t, object arg) {
            ErrorManager.Instance.NotifyError(new UserTypeExpectedError(node.Location));
            return null;
        }
        
    }
}
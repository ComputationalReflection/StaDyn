using TypeSystem.Operations;
using TypeSystem;
using ErrorManagement;
using AST;
using Tools;
using System.Collections.Generic;
using CodeGeneration.ExceptionManagement;
namespace CodeGeneration.Operations {
    /// <summary>
    ///  It processess code generation of a field in IL Language.
    ///  </summary>       
    internal class CGProcessMethodOperation<T> : TypeSystemOperation where T : ILCodeGenerator {

        /// <summary>
        /// streamto to write to
        /// </summary>
        private T codeGenerator;
        /// <summary>
        ///  indentation to use
        /// </summary>
        private int indentation;
        /// <summary>
        ///  argument
        /// </summary>
        
        private MethodDeclaration node;
        private object argument;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="codeGenerator">stream to write to</param>
        /// <param name="indentation">indentation to use</param>
        /// <param name="node">node of the Method to process</param>
        /// <param name="argument">argument</param>
        public CGProcessMethodOperation(T codeGenerator, int indentation, MethodDeclaration node, object argument) {
            this.codeGenerator = codeGenerator;
            this.indentation = indentation;
            this.node = node;
            this.argument = argument;
        }
        /// <summary>
        ///  Process the method header
        /// </summary>
        /// <param name="n">Type Expression representing the method to process</param>
        /// <returns>null</returns>
        public override object Exec(MethodType m, object arg) {
            this.codeGenerator.WriteLNMethodHeader(this.indentation, m.MemberInfo.MemberIdentifier, m);
            return null;
        }
        /// <summary>
        ///  the default behaviour is an error
        /// </summary>
        /// <param name="t"></param>
        /// <returns>null</returns>
        public override object Exec(TypeExpression t, object arg) {
            ErrorManager.Instance.NotifyError(new MemberTypeExpectedError(node.Location));
            return null;
        }

    }
}
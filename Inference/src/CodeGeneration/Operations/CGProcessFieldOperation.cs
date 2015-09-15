using TypeSystem.Operations;
using TypeSystem;
using ErrorManagement;
using AST;
using Tools;
using System.Collections.Generic;
using CodeGeneration.ExceptionManagement;
namespace CodeGeneration.Operations {
    /// <summary>
    ///  It processess code generation of a Field in IL Language.
    ///  </summary>     
    internal class CGProcessFieldOperation<T> : TypeSystemOperation where T : ILCodeGenerator {

        /// <summary>
        /// streamto to write to
        /// </summary>
        private T codeGenerator;
        
        /// <summary>
        ///  indentation to use
        /// </summary>
        private int indentation;
        /// <summary>
        /// node containing the Field declaration
        /// </summary>
        private FieldDeclaration node;
        /// <summary>
        /// indicates wether the field is constant or not
        /// </summary>
        private bool constantField;
        /// <summary>
        ///  argument
        /// </summary>
        private object argument;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="codeGenerator">stream to write to</param>
        /// <param name="indentation">indentation to use</param>
        /// <param name="node"><param name="node">node representing the field to process</param></param>
        /// <param name="constantField">indicates wether the field is constant or not</param>
        /// <param name="argument">argument</param>
        public CGProcessFieldOperation(T codeGenerator, int indentation, FieldDeclaration node,
                        bool constantField, object argument) {
            this.codeGenerator = codeGenerator;
            this.indentation = indentation;
            this.node = node;
            this.constantField = constantField;
            this.argument = argument;
        }
        /// <summary>
        ///  Process the field
        /// </summary>
        /// <param name="f">Type Expression representing the field to process</param>
        /// <returns>null</returns>
        public override object Exec(FieldType f, object arg) {
            this.codeGenerator.ProcessField(this.indentation, this.node, this.argument, this.constantField);
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
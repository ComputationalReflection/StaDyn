using TypeSystem.Operations;
using TypeSystem;
using ErrorManagement;
using AST;
using Tools;
using System.Collections.Generic;
using CodeGeneration.ExceptionManagement;
namespace CodeGeneration.Operations {
    /// <summary>
       ///  It generates the code of the initialization of a constant field in IL Language.
       ///  </summary>       
    internal class CGConstantFieldDefinitionInitializationOperation<T> : TypeSystemOperation where T: ILCodeGenerator{

        /// <summary>
        /// streamto to write to
        /// </summary>
        private T codeGenerator;
        /// <summary>
        ///  node representing the constant field definition
        /// </summary>
        private ConstantFieldDefinition node;
        /// <summary>
        ///  argument
        /// </summary>
        private object argument;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="codeGenerator">stream to write to</param>
        /// <param name="node">node representing the constant field definition</param>
        /// <param name="argument">argument</param>
        public CGConstantFieldDefinitionInitializationOperation(T codeGenerator, ConstantFieldDefinition node, object argument) {
            this.codeGenerator = codeGenerator;
            this.node = node;
            this.argument = argument;
          }
        /// <summary>
        ///  Writes the intialization of a with a NullType
        /// </summary>
        /// <param name="n">Type Expression representing a NullType</param>
        /// <returns>null</returns>
        public override object Exec(NullType n, object arg) {
            this.codeGenerator.WriteLNFieldInitialization(" = nullref");
            return null;
        }
        /// <summary>
        ///  The if the initialization is not a NullType the default behaviour is an error
        /// </summary>
        /// <param name="t"></param>
        /// <returns>null</returns>
        public override object Exec(TypeExpression t, object arg) {
            // In the following call the  argument could be node.Init.ILTypeExpression, but is the same of
            // the argument due to the dispatcher call
            this.codeGenerator.WriteLNFieldInitialization(t);
            this.codeGenerator.WriteLNFieldInitialization((string)this.node.Init.Accept(new VisitorCodeGeneration2(), this.argument));
            this.codeGenerator.WriteEndOfField();
            return null;
        }
        
    }
}
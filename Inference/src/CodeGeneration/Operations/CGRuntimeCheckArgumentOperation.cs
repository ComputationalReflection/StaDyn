using TypeSystem.Operations;
using TypeSystem;
using ErrorManagement;
using System.Collections.Generic;
namespace CodeGeneration.Operations {
    /// <summary>
    ///  It typechecks the runtime arguments, embeded in a method call, with the parametes of this method.
    ///  </summary>       
    public class CGRuntimeCheckArgumentOperation <T> : TypeSystemOperation where T: ILCodeGenerator{
        /// <summary>
        ///  List of labels
        /// </summary>
        private List<string> nextMethod;
        /// <summary>
        /// streamto to write to
        /// </summary>
        private T codeGenerator;
        /// <summary>
        /// indentation to use
        /// </summary>
        private int indent;
        /// <summary>
        /// Constructor of CGRuntimeCheckArgumentOperation
        /// </summary>
        /// <param name="indent">Indentation to use</param>
        /// <param name="codeGenerator">Stream to write to</param>
        /// <param name="nextMethod">list of labels</param>
        public CGRuntimeCheckArgumentOperation(int indent, T codeGenerator, List<string> nextMethod) {
            this.indent = indent;
            this.nextMethod = nextMethod;
            this.codeGenerator = codeGenerator;
        }
        /// <summary>
        /// Checks if the parameter passed (operand) matches with the proper argument.
        /// In this method only processes non TypeValue parameters. 
        /// </summary>
        /// <param name="operand">parameter to check</param>
        /// <returns></returns>
        public override object Exec(TypeExpression operand, object arg) {
            if (operand.IsValueType())
                return null;
            this.codeGenerator.dup(this.indent);
            this.codeGenerator.isinst(this.indent, operand);
            // if check fail then check nextMethod
            this.nextMethod.Add(this.codeGenerator.NewLabel);
            this.codeGenerator.brfalse(this.indent, nextMethod[nextMethod.Count - 1]);
            return null;
        }
        /// <summary>
        /// Groups several kind of calls.
        /// Checks if the parameter passed (operand) matches with the proper argument.
        /// Finally, if the operand is a value type it perforns an unbox.any over the the operand
        /// </summary>
        /// <param name="operand">parameter to check</param>
        /// <returns></returns>
        private object MultiTypeExec(TypeExpression operand, object arg) {
            this.codeGenerator.dup(this.indent);
            this.codeGenerator.isinst(this.indent, operand);
            // if check fail then check nextMethod
            this.nextMethod.Add(this.codeGenerator.NewLabel);
            this.codeGenerator.brfalse(this.indent, nextMethod[nextMethod.Count - 1]);
            if (operand.IsValueType())
                this.codeGenerator.UnboxAny(this.indent, operand);
            return null;
        }
        /// <summary>
        /// Checks if the CharType parameter passed (operand) matches with the proper argument.
        /// Finally, it perforns an unbox.any over the the operand
        /// </summary>
        /// <param name="operand">CharType parameter to check</param>
        /// <returns></returns>
        public override object Exec(CharType operand, object arg) {
            return this.MultiTypeExec(operand, arg);
        }
        /// <summary>
        /// Checks if the BoolType parameter passed (operand) matches with the proper argument.
        /// Finally, it perforns an unbox.any over the the operand
        /// </summary>
        /// <param name="operand">BoolType parameter to check</param>
        /// <returns></returns>
        public override object Exec(BoolType operand, object arg) {
            return this.MultiTypeExec(operand, arg);
        }

        /// <summary>
        /// Checks if the IntType parameter passed (operand) matches with the proper argument.
        /// It makes to 2 calls to CGRuntimeCheckTypeExpressionOperation operation. One using a CharType object, and the 
        /// other with IntType object, with no try to convert to double (toDouble parameter = false)
        /// </summary>
        /// <param name="operand">IntType parameter to check</param>
        /// <returns></returns>
        public override object Exec(IntType operand, object arg) {
            string nextArgument = this.codeGenerator.NewLabel;
            CharType.Instance.AcceptOperation(new CGRuntimeCheckTypeExpressionOperation<T>(this.indent, this.codeGenerator, nextArgument, false), arg);
            IntType.Instance.AcceptOperation(new CGRuntimeCheckTypeExpressionOperation<T>(this.indent, this.codeGenerator, nextArgument, false), arg);

            this.nextMethod.Add(this.codeGenerator.NewLabel);
            this.codeGenerator.br(this.indent, this.nextMethod[this.nextMethod.Count - 1]);
            this.codeGenerator.WriteLabel(this.indent, nextArgument);
            return null;
        }
        /// <summary>
        /// Checks if the Double Type parameter passed (operand) matches with the proper argument.
        /// It makes to 3calls to CGRuntimeCheckTypeExpressionOperation operation. One using a CharType object, second
        /// with IntType object, and last usint a double type, all of them trying a convert to double (toDouble parameter = true)
        /// </summary>
        /// <param name="operand">DoubleType parameter to check</param>
        /// <returns></returns>
        public override object Exec(DoubleType operand, object arg) {
            string nextArgument = this.codeGenerator.NewLabel;

            CharType.Instance.AcceptOperation(new CGRuntimeCheckTypeExpressionOperation<T>(this.indent, this.codeGenerator, nextArgument, true), arg);
            IntType.Instance.AcceptOperation(new CGRuntimeCheckTypeExpressionOperation<T>(this.indent, this.codeGenerator, nextArgument, true), arg);
            DoubleType.Instance.AcceptOperation(new CGRuntimeCheckTypeExpressionOperation<T>(this.indent, this.codeGenerator, nextArgument, true), arg);

            nextMethod.Add(this.codeGenerator.NewLabel);
            this.codeGenerator.br(this.indent, this.nextMethod[this.nextMethod.Count - 1]);
            this.codeGenerator.WriteLabel(this.indent, nextArgument);
            return null;
        }
        public override object ReportError(TypeExpression tE) {
            ErrorManager.Instance.NotifyError(new CodeGenerationError("No se ha definido la operación solicitada"));
            return null;
        }

    }
}
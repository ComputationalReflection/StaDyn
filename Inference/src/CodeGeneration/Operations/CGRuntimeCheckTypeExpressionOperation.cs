using TypeSystem.Operations;
using TypeSystem;
using ErrorManagement;
namespace CodeGeneration.Operations {
    /// <summary>
    /// It tries to convert the top of the stack to a Type Expression indicated by operand.
    /// Firstly it duplicates the top of the stack
    /// Next check if the top of he stack is the same type of argument 'operand'. 
    /// If the conversion is wrong. In a execution context we'd've finished so it's necessary to write 
    /// an opcode jump, brfalse, to the end of this code (endLabel).
    /// Next it unboxes the result the top of the stack to the desired type.
    /// If a Convertion to double is requested, (member this.toDouble), it generates convToDouble
    /// If it success it jumps to the rest of code else it generates the label to jump if isinst fails.
    /// </summary>
    /// <returns></returns>

    public class CGRuntimeCheckTypeExpressionOperation <T>: TypeSystemOperation where T: ILCodeGenerator {
        /// <summary>
        /// Label to go if the type conversion is correct
        /// </summary>
        private string endLabel;
        /// <summary>
        /// True if it is necessary to convert to a double. ( DoubleType.IlType)
        /// </summary>
        private bool toDouble;
        private int indent;
        private T codeGenerator;

        public CGRuntimeCheckTypeExpressionOperation(int indent, T codeGenerator, string endLabel, bool toDouble) {
            this.indent = indent;
            this.codeGenerator = codeGenerator;
            this.endLabel = endLabel;
            this.toDouble = toDouble;
        }

        #region TypeExpression ...
        /// <summary>
        /// It tries to convert the top of the stack to a generic Type Expression indicated by operand.
        /// </summary>
        /// <param name="operand">Type we want to convert to</param>
        /// <returns></returns>
        public override object Exec(TypeExpression operand, object arg) {
            string notThisType = this.codeGenerator.NewLabel;
            this.codeGenerator.dup(this.indent);
            this.codeGenerator.isinst(this.indent, operand);
            this.codeGenerator.brfalse(this.indent, notThisType);
            this.codeGenerator.UnboxAny(this.indent, operand);
            if (this.toDouble)
                this.codeGenerator.convToDouble(this.indent);
            this.codeGenerator.br(this.indent, endLabel);
            this.codeGenerator.WriteLabel(this.indent, notThisType);

            return null;
        }
        #endregion

        #region DoubleType...
        /// <summary>
        /// It tries to convert the top of the stack to a generic DoubleType indicated by operand.
        /// </summary>
        /// <param name="operand">DoubleType we want to convert to</param>
        /// <returns></returns>
        public override object Exec(DoubleType operand, object arg) {
            string notThisType = this.codeGenerator.NewLabel;

            this.codeGenerator.dup(this.indent);
            this.codeGenerator.isinst(this.indent, operand);
            this.codeGenerator.brfalse(this.indent, notThisType);
            this.codeGenerator.UnboxAny(this.indent, operand);
            this.codeGenerator.br(this.indent, endLabel);
            this.codeGenerator.WriteLabel(this.indent, notThisType);

            return null;
        }

        #endregion

        public override object ReportError(TypeExpression tE) {
            ErrorManager.Instance.NotifyError(new CodeGenerationError("No se ha definido la operación solicitada"));
            return null;
        }
    }
}
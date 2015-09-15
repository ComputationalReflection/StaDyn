using TypeSystem.Operations;
using TypeSystem;
using ErrorManagement;
namespace CodeGeneration.Operations {
    /// <summary>
    /// Converts a fresh type variable on the stack (object) to another type
    /// </summary>
    public class CGRuntimeFreshTEPromotionOperation<T> : TypeSystemOperation where T : ILCodeGenerator {

        private int indent;
        private T codeGenerator;

        public CGRuntimeFreshTEPromotionOperation(int indent, T codeGenerator) {
            this.indent = indent;
            this.codeGenerator = codeGenerator;
        }

        #region genInteger
        /// <summary>
        /// Tries a promotion to a integer. As a char can be promotioned to int, it generates the proper code.
        /// </summary>
        /// <param name="operand"></param>
        /// <returns></returns>
        public override object Exec(IntType operand, object arg) {
            // * char to int
            // * If we must promote to an integer, a Char could be on the stack
            this.codeGenerator.dup(this.indent);
            this.codeGenerator.isinst(this.indent, CharType.Instance);

            string notACharLabel = this.codeGenerator.NewLabel;

            this.codeGenerator.brfalse(this.indent, notACharLabel);
            this.codeGenerator.UnboxAny(this.indent, CharType.Instance);

            string endLabel = this.codeGenerator.NewLabel;

            this.codeGenerator.br(this.indent, endLabel);
            this.codeGenerator.WriteLabel(this.indent, notACharLabel);
            this.codeGenerator.UnboxAny(this.indent, IntType.Instance);
            this.codeGenerator.WriteLabel(this.indent, endLabel);
            return null;
        }

        #endregion

        #region Double ...
        /// <summary>
        /// Tries a promotion to a double. We have to generate the promotion code for a char and a int, as both can be
        /// promoted to a double
        /// </summary>
        /// <param name="operand"></param>
        /// <returns></returns>
        public override object Exec(DoubleType operand, object arg) {
            // * char to double or int to double
            // * If we must promote to an double, a Char or a Int32 could be on the stack
            this.codeGenerator.dup(this.indent);
            this.codeGenerator.isinst(this.indent, CharType.Instance);

            string notACharLabel = this.codeGenerator.NewLabel;

            this.codeGenerator.brfalse(this.indent, notACharLabel);
            this.codeGenerator.UnboxAny(this.indent, CharType.Instance);
            this.codeGenerator.convToDouble(this.indent);

            string endLabel = codeGenerator.NewLabel;

            this.codeGenerator.br(this.indent, endLabel);
            this.codeGenerator.WriteLabel(this.indent, notACharLabel);
            this.codeGenerator.dup(this.indent);
            this.codeGenerator.isinst(this.indent, IntType.Instance);

            string notAInt32Label = this.codeGenerator.NewLabel;

            this.codeGenerator.brfalse(this.indent, notAInt32Label);
            this.codeGenerator.UnboxAny(this.indent, IntType.Instance);
            this.codeGenerator.convToDouble(this.indent);
            this.codeGenerator.br(this.indent, endLabel);
            this.codeGenerator.WriteLabel(this.indent, notAInt32Label);
            this.codeGenerator.UnboxAny(this.indent, DoubleType.Instance);
            this.codeGenerator.WriteLabel(this.indent, endLabel);

            return null;
        }

        #endregion

        #region TypeVariable  ...
        /// <summary>
        /// It delegates the operation in several types.
        /// </summary>
        /// <param name="operand"></param>
        /// <returns></returns>
        public override object Exec(TypeVariable operand, object arg) {
            if (!operand.IsValueType())
                return null;
            if (TypeExpression.Is<IntType>(operand))
                return this.Exec(TypeExpression.As<IntType>(operand), arg);
            if (TypeExpression.Is<DoubleType>(operand))
                return this.Exec(TypeExpression.As<DoubleType>(operand), arg);
            return this.Exec((TypeExpression)operand, arg);
        }

        #endregion

        #region TypeExpression ...
        /// <summary>
        /// Invoked when the fresh variable cannot be promoted to a double int, or char type.
        /// </summary>
        /// <param name="operand"></param>
        /// <returns></returns>
        public override object Exec(TypeExpression operand, object arg) {
            if (!operand.IsValueType())
                return null;
            // * If not promotion is needed, we simply Unbox
            codeGenerator.UnboxAny(this.indent, operand);
            return null;
        }

        #endregion
        #region String...
        /// <summary>
        /// Invoked when the fresh variable could be promoted to a string.
        /// </summary>
        /// <param name="operand"></param>
        /// <returns></returns>
        public override object Exec(StringType s, object arg) {
            codeGenerator.WriteLine(this.indent, "callvirt\tinstance string [mscorlib]System.Object::ToString()");
            return null;
        }

        #endregion
        public override object ReportError(TypeExpression tE) {
            ErrorManager.Instance.NotifyError(new CodeGenerationError("No se ha definido la operación solicitada"));
            return null;
        }

    }

}
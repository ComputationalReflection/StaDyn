using TypeSystem.Operations;
using TypeSystem;
using ErrorManagement;
using AST;
using Tools;
using System.Collections.Generic;
using CodeGeneration.ExceptionManagement;
namespace CodeGeneration.Operations {
    /// <summary>
    /// Checks if the type variable on the stack is a specified type or can be to promotion
    /// </summary>
    public class CGRuntimeIsInstructionOperation <T> : TypeSystemOperation where T:ILCodeGenerator {
        private T codeGenerator;
        private int indent;
        public CGRuntimeIsInstructionOperation(int indent, T codeGenerator) {
            this.indent = indent;
            this.codeGenerator = codeGenerator;
        }
        /// <summary>
        /// The promotion could be to a IntType
        /// </summary>
        /// <param name="operand"></param>
        /// <returns></returns>
        public override object Exec(IntType operand, object arg) {
            string notACharLabel = this.codeGenerator.NewLabel;
            string endLabel = this.codeGenerator.NewLabel;

            // * char to int
            // * If we could promote to an integer, a Char could be on the stack
            this.codeGenerator.dup(this.indent);
            this.codeGenerator.isinst(this.indent, CharType.Instance);
            this.codeGenerator.dup(this.indent);
            this.codeGenerator.brfalse(this.indent, notACharLabel);
            this.codeGenerator.br(this.indent, endLabel);
            this.codeGenerator.WriteLabel(this.indent, notACharLabel);
            this.codeGenerator.pop(this.indent);
            this.codeGenerator.dup(this.indent);
            this.codeGenerator.isinst(this.indent, IntType.Instance);
            this.codeGenerator.WriteLabel(this.indent, endLabel);
            return null;
        }
        /// <summary>
        /// The promotion could be to a DoubleType
        /// </summary>
        /// <param name="operand"></param>
        /// <returns></returns>
        public override object Exec(DoubleType operand, object arg) {
            string notACharLabel = this.codeGenerator.NewLabel;
            string notAInt32Label = this.codeGenerator.NewLabel;
            string endLabel = this.codeGenerator.NewLabel;

            // * char to double or int to double
            // * If we could promote to an double, a Char or a Int32 could be on the stack
            this.codeGenerator.dup(this.indent);
            this.codeGenerator.isinst(this.indent, CharType.Instance);
            this.codeGenerator.dup(this.indent);
            this.codeGenerator.brfalse(this.indent, notACharLabel);
            this.codeGenerator.br(indent, endLabel);
            this.codeGenerator.WriteLabel(this.indent, notACharLabel);
            this.codeGenerator.pop(this.indent);
            this.codeGenerator.dup(this.indent);
            this.codeGenerator.isinst(this.indent, IntType.Instance);
            this.codeGenerator.dup(this.indent);
            this.codeGenerator.brfalse(this.indent, notAInt32Label);
            this.codeGenerator.br(this.indent, endLabel);
            this.codeGenerator.WriteLabel(this.indent, notAInt32Label);
            this.codeGenerator.pop(this.indent);
            this.codeGenerator.dup(this.indent);
            this.codeGenerator.isinst(this.indent, DoubleType.Instance);
            this.codeGenerator.WriteLabel(this.indent, endLabel);

            return null;
        }
        /// <summary>
        /// The promotion coul be to other type, not IntType nor DoubleType
        /// </summary>
        /// <param name="operand"></param>
        /// <returns></returns>
        public override object Exec(TypeExpression operand, object arg) {
            // * If not promotion is needed, we simply check the stack
            // * Nothing to do if it is not a value type
            this.codeGenerator.dup(this.indent);
            this.codeGenerator.isinst(this.indent, operand);
            return null;
        }
        /// <summary>
        /// It delegates the trying to promotion to other methods.
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


        public override object ReportError(TypeExpression tE) {
            ErrorManager.Instance.NotifyError(new CodeGenerationError("No se ha definido la operación solicitada. El código generado es incorrecto"));
            return null;
        }

    }

}
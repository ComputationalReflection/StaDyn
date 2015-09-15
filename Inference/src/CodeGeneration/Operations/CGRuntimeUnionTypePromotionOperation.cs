using TypeSystem.Operations;
using TypeSystem;
using ErrorManagement;
using AST;
using Tools;
using System.Collections.Generic;
using CodeGeneration.ExceptionManagement;
namespace CodeGeneration.Operations {
    /// <summary>
    /// Tries a promotion where the top of the stack contains a union type.
    /// </summary>
    public class CGRuntimeUnionTypePromotionOperation<T> : TypeSystemOperation where T : ILCodeGenerator {
        UnionType union;
        private int indent;
        private T codeGenerator;

        public CGRuntimeUnionTypePromotionOperation(int indent, T ilCodeGenerator, UnionType union) {
            this.indent = indent;
            this.codeGenerator = ilCodeGenerator;
            this.union = union;
        }


        #region TypeExpression ...

        public override object Exec(TypeExpression operand, object arg) {
            if (this.union.Count == 1)
                return null;

            string notThisType = this.codeGenerator.NewLabel;
            string endLabel = this.codeGenerator.NewLabel;

            operand.AcceptOperation(new CGRuntimeIsInstructionOperation<T>(this.indent, this.codeGenerator), arg);
            this.codeGenerator.brfalse(this.indent, notThisType);
            operand.AcceptOperation(new CGRuntimeFreshTEPromotionOperation<T>(this.indent, this.codeGenerator), arg);
            this.codeGenerator.br(this.indent, endLabel);
            this.codeGenerator.WriteLabel(this.indent, notThisType);
            // As the expected type is not an String, we throw an exception
            this.codeGenerator.WriteThrowException(this.indent, new WrongDynamicTypeExceptionManager());
            this.codeGenerator.AddExceptionCode(new WrongDynamicTypeExceptionManager());
            this.codeGenerator.WriteLabel(this.indent, endLabel);
            return null;
        }

        #endregion
        public override object Exec(TypeVariable operand, object arg) { return null; }
        #region StringType ...

        public override object Exec(StringType operand, object arg) {
            if (this.union.Count == 1)
                return null;

            string notThisType = this.codeGenerator.NewLabel;
            string endLabel = this.codeGenerator.NewLabel;

            operand.AcceptOperation(new CGRuntimeIsInstructionOperation<T>(this.indent, this.codeGenerator), arg);
            this.codeGenerator.brfalse(this.indent, notThisType);
            operand.AcceptOperation(new CGRuntimeFreshTEPromotionOperation<T>(this.indent, this.codeGenerator), arg);
            this.codeGenerator.br(this.indent, endLabel);
            this.codeGenerator.WriteLabel(this.indent, notThisType);
            // As the expected type is an String, we must call its ToString method
            this.codeGenerator.CallVirt(this.indent, "instance", "string", "[mscorlib]System.Object", "ToString", null);
            this.codeGenerator.WriteLabel(this.indent, endLabel);
            return null;
        }

        #endregion

        public override object ReportError(TypeExpression tE) {
            ErrorManager.Instance.NotifyError(new CodeGenerationError("No se ha definido la operación solicitada"));
            return null;
        }
    }
}
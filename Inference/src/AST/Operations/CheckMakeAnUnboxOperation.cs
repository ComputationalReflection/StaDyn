using AST;
using ErrorManagement;
using System;

namespace AST.Operations {
    /// <summary>
    /// Check if the expression got as parameter in the exec operation is of type 
    /// NewExpression, BaseCallExpression, InvocationExpression
    /// In case of that it checks if it is needed to perform an Unbox operation
    /// We only use An operation BaseCallExpression, because the other ones are automatically called 
    /// basin on its supertype parameter due to the parametric polymorphism granted by the AstOperation
    /// </summary>
    internal class CheckMakeAnUnboxOperation : AstOperation {

        private TypeSystem.MethodType methodType;

        public CheckMakeAnUnboxOperation() {
            this.methodType = null;
        }

        public override object Exec(BaseCallExpression b, object arg) {
            this.CheckActualMethodCalled(b);
            return this.ReturnValue(b);
        }
        /// <summary>
        ///  By default we don check actualmethodcalled in classess differents from
        ///  NewExpression, BaseCallExpression, InvocationExpression
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public override object Exec(Expression node, object arg) {
            return this.ReturnValue(node);
        }

        private bool ReturnValue(Expression expression) {
            if (this.methodType != null)
                return !(this.methodType.Return.IsValueType());

            TypeSystem.FieldType field = expression.ExpressionType as TypeSystem.FieldType;

            if (field != null)
                return (field.FieldTypeExpression is TypeSystem.TypeVariable);

            return false;

        }

        protected void CheckActualMethodCalled(BaseCallExpression bc) {
            this.methodType = bc.ActualMethodCalled as TypeSystem.MethodType;
            if (this.methodType == null) {  // ActualMethodCalled is a TypeVariable or UnionType
                TypeSystem.TypeVariable typeVariable = bc.ActualMethodCalled as TypeSystem.TypeVariable;
                if (typeVariable != null)
                    this.methodType = typeVariable.Substitution as TypeSystem.MethodType;
            }
        }

    }
}
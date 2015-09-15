using TypeSystem;
using AST;
using ErrorManagement;
using DynVarManagement;
using TypeSystem.Constraints;
//TODO: HAbrá que ver que se hace con StringtYPE, con el NULL
namespace TypeSystem.Operations {
    /// <summary>
    /// Class Unary AritmethicalOperation
    /// Implements: Factory method
    /// Role:
    /// </summary>
    public class UnaryArithmeticalOperation : ArithmeticalOperation {
        #region Fields
        protected UnaryOperator unaryOperator;
        protected MethodType methodAnalyzed;
        protected bool showErrorMessage;
        protected Location location;
        #endregion

        #region Constructor T
        public UnaryArithmeticalOperation(UnaryOperator unaryOperator, MethodType methodAnalyzed, bool showErrorMessage, Location location) {
            this.unaryOperator = unaryOperator;
            this.methodAnalyzed = methodAnalyzed;
            this.showErrorMessage = showErrorMessage;
            this.location = location;
        }
        #endregion

        #region +- FieldType
        public override object Exec(FieldType operand, object arg) {
            return operand.FieldTypeExpression.AcceptOperation(this, arg);
        }
        #endregion

        #region +- PropertyType
        public override object Exec(PropertyType operand, object arg) {
            return operand.PropertyTypeExpression.AcceptOperation(this, arg);
        }
        #endregion

        #region +- TypeVariable
        public override object Exec(TypeVariable operand, object arg) {
            if (operand.Substitution != null) {
                DynVarOptions.Instance.AssignDynamism(operand.Substitution, operand.IsDynamic);
                return operand.Substitution.AcceptOperation(this, arg);
            }
            if (methodAnalyzed != null) {
                // * A constraint is added to the method analyzed
                ArithmeticConstraint constraint = new ArithmeticConstraint(operand, unaryOperator, location);
                methodAnalyzed.AddConstraint(constraint);
                return constraint.ReturnType;
            }
            return ReportError(operand);
        }
        #endregion

        #region +- \/...

        public override object Exec(UnionType operand, object arg) {
            // * If all the types in typeset generate a constraint, we simply generate one constraint using the whole union type
            if (operand.IsFreshVariable() && methodAnalyzed != null) {
                // * A constraint is added to the method analyzed
                ArithmeticConstraint constraint = new ArithmeticConstraint(operand, unaryOperator, location);
                methodAnalyzed.AddConstraint(constraint);
                return constraint.ReturnType;
            }
            TypeExpression returnType = null;
            foreach (TypeExpression type in operand.TypeSet) {
                TypeExpression ret = (TypeExpression)type.AcceptOperation(ArithmeticalOperation.Create(unaryOperator, methodAnalyzed, !operand.IsDynamic && showErrorMessage, location), arg);
                if (ret == null && !operand.IsDynamic)
                    return null;
                if (ret != null)
                    returnType = UnionType.collect(returnType, ret);
            }
            // * If there has been some errors, they have not been shown because the type is dynamic, we show it
            if (showErrorMessage && operand.IsDynamic && returnType == null)
                ErrorManager.Instance.NotifyError(new NoTypeAcceptsOperation(operand.FullName, unaryOperator.ToString(), location));
            return returnType;
        }
        #endregion

        #region +- BoolType

        public override object Exec(BoolType operand, object arg) {
            return ReportError(operand);
        }
        #endregion

        #region +- Double

        public override object Exec(DoubleType operand, object arg) {
            return DoubleType.Instance;
        }
        #endregion

        #region +- IntType

        public override object Exec(IntType operand, object arg) {
            return IntType.Instance;
        }
        #endregion

        #region +- CharType

        public override object Exec(CharType operand, object arg) {
            return CharType.Instance;
        }
        #endregion


        #region Report Errors
        // in our case we only notify operations not allowed
        // for other pruposes invoke explicitly another kind of error
        public override object ReportError(TypeExpression tE) {
            if (this.showErrorMessage)
                ErrorManager.Instance.NotifyError(new OperationNotAllowedError(this.unaryOperator.ToString(), tE.FullName, this.location));
            return null;
        }
        #endregion
    }

}


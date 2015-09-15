using TypeSystem;
using AST;
using ErrorManagement;
using System;
using TypeSystem.Constraints;
using DynVarManagement;
namespace TypeSystem.Operations {
    public class RelationalOperation : TypeSystemOperation {

        #region Fields

        protected TypeExpression secondOperand;
        protected RelationalOperator relationalOperator;
        protected MethodType methodAnalyzed;
        protected bool showErrorMessage;
        protected Location location;

        #endregion

        #region Constructor
        public RelationalOperation(TypeExpression secondOperand, RelationalOperator relationalOperator, MethodType methodAnalyzed, bool showErrorMessage, Location location) {
            this.secondOperand = secondOperand;
            this.relationalOperator = relationalOperator;
            this.methodAnalyzed = methodAnalyzed;
            this.showErrorMessage = showErrorMessage;
            this.location = location;
        }
        #endregion

        #region TypeExpression >= this.secondOperand
        public override object Exec(TypeExpression firstOperand, object arg) {
            if (showErrorMessage)
                ErrorManager.Instance.NotifyError(new OperationNotAllowedError(this.relationalOperator.ToString(), firstOperand.FullName, this.secondOperand.fullName, this.location));
            return null;
        }
        #endregion

        #region DoubleType  >= this.secondOperand
        public override object Exec(DoubleType firstOperand, object arg) {
            if ((int)this.secondOperand.AcceptOperation(new PromotionLevelOperation(DoubleType.Instance), arg) != -1)
                return BoolType.Instance;
            return this.secondOperand.AcceptOperation(new RelationalOperation(firstOperand, this.relationalOperator, this.methodAnalyzed, this.showErrorMessage, this.location), arg);
        }
        #endregion

        #region IntType  >= this.secondOperand
        public override object Exec(IntType firstOperand, object arg) {
            if ((int)secondOperand.AcceptOperation(new PromotionLevelOperation(DoubleType.Instance), arg) != -1)
                return BoolType.Instance;
            //? quedaría menos lioso new BinaryAritmeticalOperation(iTE, ...).exec(secondOperand)
            return secondOperand.AcceptOperation(new RelationalOperation(firstOperand, this.relationalOperator, this.methodAnalyzed, this.showErrorMessage, this.location), arg);
        }
        #endregion

        #region CharType  >= this.secondOperand
        public override object Exec(CharType firstOperand, object arg) {
            if ((int)secondOperand.AcceptOperation(new PromotionLevelOperation(DoubleType.Instance), arg) != -1)
                return BoolType.Instance;
            return this.secondOperand.AcceptOperation(new RelationalOperation(firstOperand, this.relationalOperator, this.methodAnalyzed, this.showErrorMessage, this.location), arg);
        }
        #endregion

        #region String  >= this.secondOperand
        public override object Exec(StringType firstOperand, object arg) {
            if (relationalOperator == RelationalOperator.Equal || relationalOperator == RelationalOperator.NotEqual) {
                if ((int)this.secondOperand.AcceptOperation(new PromotionLevelOperation(firstOperand), arg) != -1)
                    return BoolType.Instance;
                if (this.showErrorMessage) {
                    ErrorManager.Instance.NotifyError(new TypePromotionError(this.secondOperand.FullName, firstOperand.FullName, this.relationalOperator.ToString(), this.location));
                    return null;
                }
            }
            return ReportError(firstOperand);
        }
        #endregion

        #region PropertyType  >= this.secondOperand
        public override object Exec(PropertyType firstOperand, object arg) {
            if (firstOperand.PropertyTypeExpression != null)
                return firstOperand.PropertyTypeExpression.AcceptOperation(this, arg);
            return null;
        }
        #endregion

        #region ArrayType  >= this.secondOperand
        public override object Exec(ArrayType firstOperand, object arg) {
            // * Operators >= > <= < are note allowed
            // * Operators >= > <= < are note allowed
            if ((relationalOperator == RelationalOperator.GreaterThan
                        || relationalOperator == RelationalOperator.GreaterThanOrEqual
                        || relationalOperator == RelationalOperator.LessThan
                        || relationalOperator == RelationalOperator.LessThanOrEqual
                 )
                 && showErrorMessage) {
                ErrorManager.Instance.NotifyError(new OperationNotAllowedError(relationalOperator.ToString(), firstOperand.FullName, this.secondOperand.FullName, location));

                return null;
            }
            if (!(secondOperand is NullType))
                ErrorManager.Instance.NotifyError(new OperationNotAllowedError(relationalOperator.ToString(), this.secondOperand.FullName, location)); ;
            return null;
        }
        #endregion

        #region BoolType  >= this.secondOperand
        public override object Exec(BoolType firstOperand, object arg) {
            if (relationalOperator == RelationalOperator.Equal || relationalOperator == RelationalOperator.NotEqual) {
                if ((int)secondOperand.AcceptOperation(new PromotionLevelOperation(firstOperand), arg) != -1)
                    return BoolType.Instance;
                if (showErrorMessage) {
                    ErrorManager.Instance.NotifyError(new TypePromotionError(secondOperand.FullName, firstOperand.FullName, location));
                    return null;
                }
            }
            return ReportError(firstOperand);
        }
        #endregion

        #region UnionType  >= this.secondOperand
        public override object Exec(UnionType firstOperand, object arg) {
            // * If all the types in typeset generate a constraint, we simply generate one constraint using the whole union type
            if (firstOperand.IsFreshVariable() && methodAnalyzed != null) {
                // * A constraint is added to the method analyzed
                RelationalConstraint constraint = new RelationalConstraint(firstOperand, secondOperand, relationalOperator, location);
                methodAnalyzed.AddConstraint(constraint);
                return constraint.ReturnType;
            }
            bool oneCorrectType = false;
            foreach (TypeExpression type in firstOperand.TypeSet) {
                TypeExpression ret = (TypeExpression)type.AcceptOperation(new RelationalOperation(secondOperand, relationalOperator, methodAnalyzed, !firstOperand.IsDynamic && showErrorMessage, location), arg);
                if (ret == null && !firstOperand.IsDynamic)
                    return null;
                if (ret != null)
                    oneCorrectType = true;
            }
            // * If there has been some errors, they have not been shown because the type is dynamic, we show it
            if (showErrorMessage && firstOperand.IsDynamic && !oneCorrectType)
                ErrorManager.Instance.NotifyError(new NoTypeAcceptsOperation(firstOperand.FullName, relationalOperator.ToString(), secondOperand.FullName, location));
            return BoolType.Instance;
        }

        #endregion

        #region TypeVariable  >= this.secondOperand
        public override object Exec(TypeVariable firstOperand, object arg) {
            if (firstOperand.Substitution != null) {
                DynVarOptions.Instance.AssignDynamism(firstOperand.Substitution, firstOperand.IsDynamic);
                return firstOperand.Substitution.AcceptOperation(this, arg);
            }
            if (methodAnalyzed != null) {
                // * A constraint is added to the method analyzed
                RelationalConstraint constraint = new RelationalConstraint(firstOperand, secondOperand, relationalOperator, location);
                methodAnalyzed.AddConstraint(constraint);
                return constraint.ReturnType;
            }
            return ReportError(firstOperand);
        }
        #endregion

        #region FieldType >= this.secondOperand
        public override object Exec(FieldType firstOperand, object arg) {

            if (firstOperand.FieldTypeExpression != null)
                return firstOperand.FieldTypeExpression.AcceptOperation(this, arg);
            return null;
        }
        #endregion

        #region UserType  >= this.secondOperand
        public override object Exec(UserType firstOperand, object arg)
        {
            if (relationalOperator == RelationalOperator.Equal || relationalOperator == RelationalOperator.NotEqual)
            {
                if (secondOperand is NullType)
                    return BoolType.Instance;
                if (this.showErrorMessage)
                {
                    ErrorManager.Instance.NotifyError(new TypePromotionError(this.secondOperand.FullName, firstOperand.FullName, this.relationalOperator.ToString(), this.location));
                    return null;
                }
            }
            return ReportError(firstOperand);
        }
        #endregion

        #region Report Errors
        // in our case we only notify operations not allowed
        // for other pruposes invoke explicitly another kind of error
        public override object ReportError(TypeExpression tE) {
            if (this.showErrorMessage)
                ErrorManager.Instance.NotifyError(new OperationNotAllowedError(this.relationalOperator.ToString(), tE.FullName, this.secondOperand.FullName, this.location));
            return null;
        }
        #endregion
    }

}
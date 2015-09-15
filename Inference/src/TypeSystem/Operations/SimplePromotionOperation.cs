using System;
using System.Collections.Generic;
using System.Text;

using AST;
using ErrorManagement;
using Tools;
using TypeSystem.Operations;
using DynVarManagement;
using TypeSystem.Constraints;
namespace TypeSystem.Operations {
    internal class SimplePromotionOperation : PromotionOperation {
        #region Fields
        protected TypeExpression to;
        protected MethodType methodAnalyzed;
        protected Location location;
        #endregion

        #region Constructor
        internal SimplePromotionOperation(TypeExpression to, MethodType methodAnalyzed, Location location) {
            this.to = to;
            this.methodAnalyzed = methodAnalyzed;
            this.location = location;
        }
        #endregion

        public override KindOfPromotion KindOfPromotion {
            get { return KindOfPromotion.Simple; }
        }

        #region ClassTypeProxy -->...
        public override object Exec(FieldType from, object arg) {
            if (from.FieldTypeExpression != null)
                return from.FieldTypeExpression.AcceptOperation(this, arg);
            return null;
        }
        #endregion

        #region TypeExpression-->...
        public override object Exec(TypeExpression from, object arg) {
            if ((int)from.AcceptOperation(new PromotionLevelOperation(this.to), arg) == -1) {
                return ReportError(from);
            }
            return to;
        }
        #endregion

        #region UnionType-->
        public override object Exec(UnionType from, object arg) {
            return from.AcceptOperation(PromotionOperation.Create(this.to, AssignmentOperator.Assign, this.methodAnalyzed, this.location), arg);
        }
        #endregion

        #region Promotion
        public override object Exec(TypeVariable from, object arg) {
            if (from.Substitution != null) {
                DynVarOptions.Instance.AssignDynamism(from.Substitution, from.IsDynamic);
                return from.Substitution.AcceptOperation(this, arg);
            }
            if (methodAnalyzed != null) {
                // * A constraint is added to the method analyzed
                PromotionConstraint constraint = new PromotionConstraint(from, this.to, this.location);
                this.methodAnalyzed.AddConstraint(constraint);
                return constraint.ReturnType;
            }
            return ReportError(from);
        }

        #endregion

        #region Report Errors
        public override object ReportError(TypeExpression from) {
            ErrorManager.Instance.NotifyError(new TypePromotionError(from.FullName, this.to.FullName, this.location));
            return null;
        }
        #endregion
    }
}
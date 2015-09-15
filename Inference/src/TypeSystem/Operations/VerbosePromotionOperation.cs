using ErrorManagement;
using System;
using TypeSystem.Constraints;
using DynVarManagement;
namespace TypeSystem.Operations {
    /// <summary>
    /// Implements factory method.
    /// Encapsulates a Promotion able to raise errors and to do constraints.
    /// </summary>
    internal class VerbosePromotionOperation : PromotionOperation {

        #region Fields
        private TypeExpression to;
        private Enum op;
        private MethodType methodAnalyzed;
        private Location location;
        #endregion

        #region Constructor
        internal VerbosePromotionOperation (TypeExpression to, Enum op, MethodType methodAnalyzed, Location location) {
            this.to = to;
            this.op = op;
            this.methodAnalyzed = methodAnalyzed;
            this.location = location;
        }
        #endregion
        #region Kind of promotion information
        public override KindOfPromotion KindOfPromotion {
            get { return KindOfPromotion.Verbose; }
        }
        #endregion
        #region ClassTypeProxy --> ...
        public override object Exec(ClassTypeProxy from, object arg) {
            return from.RealType.AcceptOperation(this, arg);
        }
        #endregion

        #region FieldType --> ...

        public override object Exec(FieldType from, object arg) {
            if (from.FieldTypeExpression != null) {
                return from.FieldTypeExpression.AcceptOperation(this, arg);
            }
            return null;
        }
        #endregion

        #region Promotion -->
        public override object Exec(TypeExpression from, object arg) {
            if ((int)from.AcceptOperation (new PromotionLevelOperation(this.to), arg) == -1) {
                return ReportError(from);
            }
            return to;
        }
        #endregion

        public override object Exec(UnionType from, object arg) {
            return this.InternalPromotion(from, arg);
        }
        #region InternalPromotion
        private TypeExpression InternalPromotion(UnionType from, object arg) {
            if (from.IsFreshVariable() && this.methodAnalyzed != null) {
                // * A constraint is added to the method analyzed
                PromotionConstraint constraint = new PromotionConstraint(from, this.to, this.op, this.location);
                this.methodAnalyzed.AddConstraint(constraint);
                return this.to;
            }
            // * Static Behaviour: All the types in typeset must promote 
            // * Dynamic Behaviour: One of the types in typeset must promote
            int aux = 0;
            UnionType dynamicUnionType = new UnionType();
            dynamicUnionType.IsDynamic = true;
            foreach (TypeExpression subType in from.TypeSet) {
                if (from.IsDynamic) {
                    // * Dynamic
                    if (subType.IsFreshVariable())
                        dynamicUnionType.AddType(subType);
                    else {
                        aux = (int) subType.AcceptOperation(new PromotionLevelOperation(this.to), arg);
                        if (aux != -1)
                            return this.to;
                    }
                } else { // * !from.IsDynamic, so it is static
                    aux = (int)subType.AcceptOperation(new PromotionLevelOperation(this.to), arg);
                    if (aux == -1) 
                        return (TypeExpression)ReportError(from);
                  }
            }
            if (dynamicUnionType.Count != 0) {  // * If the union type is dynamic and no type in the type set promotes, then we generate a constraint with one promotion grouping the fresh types in the type set
                PromotionConstraint constraint = new PromotionConstraint(dynamicUnionType, this.to, this.op, this.location);
                this.methodAnalyzed.AddConstraint(constraint);
                return this.to;
            }
            if (from.IsDynamic && aux == -1) {
                // * No promotion at all
                return (TypeExpression)ReportError(from);
                
            }
            return this.to;
        }
        #endregion

        public override object Exec(TypeVariable from, object arg) {
            if (from.Substitution != null) {
                DynVarOptions.Instance.AssignDynamism(from.Substitution, from.IsDynamic);
                return from.Substitution.AcceptOperation(this, arg);
            }
            if (this.methodAnalyzed != null) {
                // * A constraint is added to the method analyzed
                PromotionConstraint constraint = new PromotionConstraint(from , this.to, this.op, this.location);
                this.methodAnalyzed.AddConstraint(constraint);
                return constraint.ReturnType;
            }
            return ReportError(from);
        }


        public override object ReportError(TypeExpression from) {
            ErrorManager.Instance.NotifyError(new TypePromotionError(from.fullName, this.to.FullName, this.op.ToString(), this.location));
            return null;
        }
    }
}
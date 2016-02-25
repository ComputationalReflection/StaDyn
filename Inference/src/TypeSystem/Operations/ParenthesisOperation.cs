using ErrorManagement;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;
using TypeSystem.Constraints;
using DynVarManagement;
namespace TypeSystem.Operations {
    /// <summary>
    /// Implements Double Dispatch Pattern
    /// Role:
    /// </summary>
    public class ParenthesisOperation : TypeSystemOperation {

        #region Fields
        private TypeExpression actualImplicitObject;
        private TypeExpression[] arguments;
        private MethodType methodAnalyzed;
        private SortOfUnification activeSortOfUnification;
        private Location location;
        #endregion

        #region Constructor
        public ParenthesisOperation(TypeExpression actualImplicitObject, TypeExpression[] arguments, MethodType methodAnalyzed, SortOfUnification activeSortOfUnification, Location location) {
            this.actualImplicitObject = actualImplicitObject;
            this.arguments = arguments;
            this.methodAnalyzed = methodAnalyzed;
            this.activeSortOfUnification = activeSortOfUnification;
            this.location = location;
        }
        #endregion

        #region BCLClassType ()
        public override object Exec(BCLClassType caller, object arg) {
            // * Load the constructors
            if (caller.Constructors == null)
                caller.FindConstructor(this.location);

            // * Follows the superclass behaviour
            // * Actually Base Class return the GrandFather Class.

            return caller.BaseClass.AcceptOperation(this, arg);
        }
        #endregion

        #region ClassType ()
        public override object Exec(ClassType caller, object arg) {
            //TODO: comprobar si es el name de caller.
            MethodType method = (MethodType)caller.AcceptOperation(DotOperation.Create(caller.Name, new List<TypeExpression>()), arg);
            if (method == null)
                return null;
            return method.AcceptOperation(this, arg);
        }
        #endregion

        #region ClassTypeProxy ()
        public override object Exec(ClassTypeProxy caller, object arg) {
            return caller.RealType.AcceptOperation(this, arg);
        }
        #endregion

        #region IntersectionType ()
        public override object Exec(IntersectionType caller, object arg) {
            TypeExpression method = caller.overloadResolution(this.arguments, this.location);
            if (method == null)
                return null;
            return method.AcceptOperation(this, arg);
        }
        #endregion

        #region MethodType
        public override object Exec(MethodType caller, object arg) {
            // * Quits if there's some error in the arguments
            if (this.arguments == null)
                return null;

            // * An instance method cannot be call from a static method without using an object
            if ((!caller.MemberInfo.Modifiers.Contains(Modifier.Static)) && this.methodAnalyzed != null &&
                this.methodAnalyzed.MemberInfo.Modifiers.Contains(Modifier.Static) && this.actualImplicitObject == null) {
                ErrorManager.Instance.NotifyError(new InstanceMethodCallFromStaticMethodError(caller.FullName, this.methodAnalyzed.FullName, this.location));
                return null;
            }

            // * Is Unification necessary?
            if (caller.MemberInfo.Class.HasTypeVariables() || caller.HasTypeVariables())
                return MethodType.methodCall(this.actualImplicitObject, caller, this.arguments, this.methodAnalyzed,this.activeSortOfUnification, this.location);            
            // h Otherwise...
            // Check the argument number
            if (this.arguments.GetLength(0) != caller.ParameterListCount) {
                ErrorManager.Instance.NotifyError(new ArgumentNumberError(caller.MemberInfo.MemberIdentifier, this.arguments.GetLength(0), this.location));
                return null;
            }

            int from = (this.methodAnalyzed!=null && this.methodAnalyzed.Constraints != null) ? this.methodAnalyzed.Constraints.Count:0;
            // Check the argument type
            for (int i = 0; i < caller.ParameterListCount; i++)
                this.arguments[i].AcceptOperation(PromotionOperation.Create(caller.getParam(i), this.methodAnalyzed, this.location), arg);
            int to = (this.methodAnalyzed != null && this.methodAnalyzed.Constraints != null) ? this.methodAnalyzed.Constraints.Count : 0;

            if (to > from)
            {
                ConstraintList cl = new ConstraintList();
                for (int i = from; i < to; i++)
                    cl.Add(this.methodAnalyzed.Constraints.Constraints[i]);
                for (int i = to - 1; i >= from; i--)
                    this.methodAnalyzed.Constraints.Constraints.RemoveAt(i);

                InvocationConstraint ic = null;
                foreach (var constraint in this.methodAnalyzed.Constraints.Constraints)
                {
                    ic = constraint as InvocationConstraint;
                    if (ic != null && ic.MethodName.Equals(caller.ToString()))
                    {
                        ic.Add(cl);
                        break;
                    }
                    ic = null;
                }

                if (ic == null)
                {
                    ic = new InvocationConstraint(caller.ToString());
                    ic.Add(cl);
                    this.methodAnalyzed.Constraints.Add(ic);
                }
            }

            // * Returns the return type
            return caller.Return;
        }

        #endregion

        #region TypeVariable ()
        public override object Exec(TypeVariable caller, object arg) {
            if (caller.Substitution != null) {
                DynVarOptions.Instance.AssignDynamism(caller.Substitution, caller.IsDynamic);
                return caller.Substitution.AcceptOperation(this, arg);
            }
            if (this.methodAnalyzed != null) {
                // * A method invocation constraint is added to the method analyzed
                ParenthesisConstraint constraint = new ParenthesisConstraint(caller, this.actualImplicitObject, this.arguments, this.activeSortOfUnification, this.location);
                this.methodAnalyzed.AddConstraint(constraint);
                return constraint.ReturnType;
            }
            return ReportError(caller);
        }
        #endregion

        #region Union ()
        public override object Exec(UnionType caller, object arg) {
            // * If all the types in typeset generate a constraint, we simply generate one constraint using the whole union type
            if (caller.IsFreshVariable() && this.methodAnalyzed != null) {
                // * A constraint is added to the method analyzed
                ParenthesisConstraint constraint = new ParenthesisConstraint(caller, this.actualImplicitObject, this.arguments, this.activeSortOfUnification, this.location);
                this.methodAnalyzed.AddConstraint(constraint);
                return constraint.ReturnType;
            }
            TypeExpression returnType = null;
            foreach (TypeExpression type in caller.TypeSet) {
                TypeExpression ret = (TypeExpression)type.AcceptOperation(this, arg);
                if (ret == null && caller.IsDynamic)
                    return null;
                returnType = UnionType.collect(returnType, ret);
            }
            return returnType;

        }
        #endregion

        // Errors
        #region ReportError
        public override object ReportError(TypeExpression caller) {

            ErrorManager.Instance.NotifyError(new OperationNotAllowedError("()", caller.FullName, this.location));
            return null;
        }
        #endregion
    }
}




using TypeSystem;
using AST;
using ErrorManagement;
using DynVarManagement;
using TypeSystem.Constraints;
using System.Collections.Generic;
//TODO: HAbrá que ver que se hace con StringtYPE, con el NULL
namespace TypeSystem.Operations {
    /// <summary>
    /// This class generatess constraits, and raises error if something is wrong.
    /// Implements a double dispatcher pattern
    /// Implements a factory method.
    /// Role: Product
    /// </summary>
    public class ConstrainedDotOperation : DotOperation {
        /// <summary>
        /// It encapsulates the sort of dot operation we are using. There are tow types
        /// Constrained and Unconstrained.
        /// It implements factory Method.
        /// Member: Product.
        /// </summary>
        public override DotKind kindOfDot {
            get { return DotKind.Constrained; }
        }

        #region Fields
        /// <summary>
        /// Member name, the right side of a dot operation. This is the field in a dot operation
        /// whe want to access.
        /// </summary>
        protected string memberName;
        /// <summary>
        /// The method that is being analyzed when the operation is performed.
        /// </summary>        
        protected MethodType methodAnalyzed;
        /// <summary>To detect infinite loops. The types that have been previously passed the dot message. Used for union types.
        /// </summary>
        protected IList<TypeExpression> previousDot;
        protected bool showErrorMessage;
        /// <summary>
        /// The location (file, line, column) of text being analyzed.
        /// </summary>          
        protected Location location;

        #endregion

        #region Constructors
        /// <summary>
        ///  Constructor of ConstrainedDotOperation
        /// </summary>
        /// <param name="memberName">The selector to applied to the typeExpression</param>
        /// <param name="methodAnalyzed">The actual method</param>
        /// <param name="previousDot">in case we have a previous Dot, we store its value here</param>
        /// <param name="loc">The location (file, line, column) of text being analysed.</param>
        public ConstrainedDotOperation(string memberName, MethodType methodAnalyzed, IList<TypeExpression> previousDot, Location loc) {
            this.memberName = memberName;
            this.methodAnalyzed = methodAnalyzed;
            this.previousDot = previousDot; // * We only save the reference to promote throughput.
            this.location = loc;
        }

        #endregion

        #region arrayType.member
        /// <summary>
        /// Delegate its behaviour to the BCLClassType, chaining its message to the later.
        /// </summary>
        /// <param name="d">An array WriteType to pass the message member</param>
        /// <returns>A typeExpression result of the operation</returns>

        public override object Exec(ArrayType d, object arg) {
            return d.AsClassType().AcceptOperation(this, arg);
        }
        #endregion

        #region BCLClassType.member
        /// <summary>
        /// It check if it's possible the operation accessing BCLClassType and a member, if is not possible
        /// it raises and error
        /// </summary>
        /// <param name="d">A BCLCClassType to pass the message member</param>
        /// <returns>The resulting type expression, if no operation is possible it raises and error.
        /// </returns>
        public override object Exec(BCLClassType d, object arg) {
            TypeExpression member = (TypeExpression)d.AcceptOperation(new UnconstrainedDotOperation(this.memberName, this.previousDot), arg);
            if (member == null)
                ErrorManager.Instance.NotifyError(new UnknownMemberError(this.memberName, this.location));
            return member;
        }
        #endregion

        #region BoolType.member
        /// <summary>
        /// Delegate its behaviour to the BCLClassType, chaining its message to the later.
        /// </summary>
        /// <param name="d">An BoolType to pass the message member</param>
        /// <returns>The resulting type expression, if no operation is possible it raises and error.
        /// </returns>
        
        /// Double Disspatcher
        public override object Exec(BoolType d, object arg) {
            return d.AsClassType().AcceptOperation(this, arg);
        }
        #endregion

        #region CharType.member
        public override object Exec(CharType d, object arg) {
            return d.AsClassType().AcceptOperation(this, arg);
        }
        #endregion

        #region ClassType.member
        public override object Exec(ClassType d, object arg) {
            TypeExpression member = (TypeExpression)d.AcceptOperation(new UnconstrainedDotOperation(memberName, this.previousDot), arg);
            if (member == null) {
                ErrorManager.Instance.NotifyError(new UnknownMemberError(this.memberName, this.location));
                return null;
            }
            if (!d.validAccess(member)) {
                ErrorManager.Instance.NotifyError(new ProtectionLevelError(this.memberName, this.location));
                return null;
            }
            return member;
        }
        #endregion

        #region ClassTypeProxy.member
        public override object Exec(ClassTypeProxy d, object arg) {
            return d.RealType.AcceptOperation(this, arg);
        }
        #endregion

        #region Double.member
        public override object Exec(DoubleType d, object arg) {
            return d.AsClassType().AcceptOperation(this, arg);
        }
        #endregion

        #region FieldType.member

        public override object Exec(FieldType d, object arg) {
            if (d.FieldTypeExpression != null) {
                // * If the field type is a dynamic union type, so it is the union
                UnionType unionType = TypeExpression.As<UnionType>(d.FieldTypeExpression);
                if (unionType != null)
                    DynVarOptions.Instance.AssignDynamism(d.FieldTypeExpression, d.IsDynamic);
                return d.FieldTypeExpression.AcceptOperation(this, arg);
            }
            return null;
        }
        #endregion

        #region IntType.member
        public override object Exec(IntType d, object arg) {
            return d.AsClassType().AcceptOperation(this, arg);
        }
        #endregion

        #region Property.member
        public override object Exec(PropertyType d, object arg) {
            if (d.PropertyTypeExpression != null)
                return d.PropertyTypeExpression.AcceptOperation(this, arg);
            return null;
        }
        #endregion

        #region String.member
        public override object Exec(StringType d, object arg) {
            return d.AsClassType().AcceptOperation(this, arg);
        }
        #endregion

        #region TypeExpression.member
        public override object Exec(TypeExpression d, object arg) {
            return ReportError(d);
        }
        #endregion

        #region InterfaceType.member
        // Try to find the appropriate attribute
        public override object Exec(InterfaceType d, object arg) {
            UnconstrainedDotOperation uo = new UnconstrainedDotOperation(this.memberName, this.previousDot);
            TypeExpression member = (TypeExpression)d.AcceptOperation(uo, arg);
            if (member == null)
                // * Otherwise, error
                ErrorManager.Instance.NotifyError(new UnknownMemberError(this.memberName, this.location));
            return member;
        }

        #endregion

        #region TypeVariable.member
        public override object Exec(TypeVariable d, object arg) {
            if (d.Substitution != null) {
                DynVarOptions.Instance.AssignDynamism(d.Substitution, d.IsDynamic);
                return d.Substitution.AcceptOperation(this, arg);
            }
            if (methodAnalyzed != null) {
                // * A attribute access constraint is added to the method analyzed
                DotConstraint constraint = new DotConstraint(d, this.memberName, this.location);
                methodAnalyzed.AddConstraint(constraint);
                return constraint.ReturnType;
            }
            ErrorManager.Instance.NotifyError(new OperationNotAllowedError(".", d.FullName, this.location));
            return null;
        }
        #endregion

        #region UnionType.member
        public override object Exec(UnionType d, object arg) {
            // * Infinite loop detection
            if (previousDot.Contains(d))
                return new UnionType();
            previousDot.Add(d);

            // * If all the types in typeset generate a constraint, we simply generate one constraint using the whole union type
            if (d.IsFreshVariable() && methodAnalyzed != null) {
                // * A constraint is added to the method analyzed
                DotConstraint constraint = new DotConstraint(d, this.memberName, this.location);
                methodAnalyzed.AddConstraint(constraint);
                return constraint.ReturnType;
            }
            TypeExpression returnType = null;
            foreach (TypeExpression type in d.TypeSet) {
                TypeExpression ret;
                if (!d.IsDynamic) {
                    // * Static Behaviour: All the types must accept the attribute access
                    ret = (TypeExpression)type.AcceptOperation(this, arg);
                    if (ret == null) {
                        return null;
                    }
                } else
                    // * Dynamic Behaviour: Only one type must accept the attribute access
                    ret = (TypeExpression)type.AcceptOperation(new UnconstrainedDotOperation(this.memberName, previousDot), arg);
                if (ret != null)
                    returnType = UnionType.collect(returnType, ret);
            }
            // * If it is a dynamic union, one type must accept the attribute access
            if (returnType == null)
                ErrorManager.Instance.NotifyError(new NoTypeHasMember(d.FullName, this.memberName, this.location));
            return returnType;
        }
        #endregion

        #region Report Errors

        /// <summary>
        /// If showMessages is true it raises and error. It returns null
        /// </summary>
        /// <param name="from">TypeExpression from we want to cast</param>
        /// <returns>null</returns>
        // in our case we only notify operations not allowed
        // for other pruposes invoke explicitly another kind of error

        public override object ReportError(TypeExpression tE) {
            if (this.showErrorMessage)
                ErrorManager.Instance.NotifyError(new OperationNotAllowedError(".", tE.FullName, this.location));
            return null;
        }
        #endregion

    }
}

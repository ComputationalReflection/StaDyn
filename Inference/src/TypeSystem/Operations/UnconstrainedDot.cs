using TypeSystem;
using AST;
using ErrorManagement;
using System.Collections.Generic;
using DynVarManagement;
//TODO: INtentar hacer este fichero con templates
namespace TypeSystem.Operations {

    internal class UnconstrainedDotOperation : DotOperation {

        #region Fields
        protected string memberName;
        protected IList<TypeExpression> previousDot;
        #endregion
        #region Constructor
         internal UnconstrainedDotOperation(string memberName, IList<TypeExpression> previousDot) {
            this.memberName = memberName;
            this.previousDot = previousDot;
        }
        #endregion
        public override DotKind kindOfDot {
            get { return DotKind.Unconstrained; }
        }
        #region Array.
        public override object Exec(ArrayType d, object arg) {
            return d.AsClassType().AcceptOperation(new UnconstrainedDotOperation(this.memberName, this.previousDot), arg);
        }
        #endregion

        #region BCLClass.member

        public override object Exec(BCLClassType d, object arg) {
            // * Has the attribute been previosly found?
            if (d.Members.ContainsKey(this.memberName))
                return d.Members[this.memberName].Type;

            // * Let's try introspection
            TypeExpression member = d.FindMember(this.memberName);
            if (member != null)
                return member;

            // * Search in base class
            if (d.BaseClass != null)
                return d.BaseClass.AcceptOperation(new UnconstrainedDotOperation(this.memberName, this.previousDot), arg);

            return null;
        }
        #endregion

        #region BCLInterfaceType.member
        public override object Exec(BCLInterfaceType d, object arg) {
            // * Has the attribute previously found?
            if (d.Members.ContainsKey(memberName))
                return d.Members[memberName].Type;

            // * Lets use introspection
            TypeExpression memberType = d.FindMember(memberName);
            if (memberType != null)
                return memberType;

            // * Search in the inhirtance tree
            foreach (BCLInterfaceType interfaze in d.InterfaceList) {
                // * Does this interface support this attribute?
                TypeExpression member = (TypeExpression)interfaze.AcceptOperation(this, arg);
                if (member != null)
                    return member;
            }
            // * not found
            return null;
        }

        #endregion

        #region BoolType.member

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
            if (d.Members.ContainsKey(this.memberName))
                return d.Members[memberName].Type;
            if (d.BaseClass != null) // Search in inherit class
                return d.BaseClass.AcceptOperation(this, arg);
            return null;
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
            if (d.FieldTypeExpression != null)
                return d.FieldTypeExpression.AcceptOperation(new UnconstrainedDotOperation(this.memberName, this.previousDot), arg);
            return null;
        }
        #endregion

        #region InterfaceType.member

        public override object Exec(InterfaceType d, object arg) {
            if (d.Members.ContainsKey(this.memberName))
                return d.Members[this.memberName].Type;
            foreach (InterfaceType interfaze in d.InterfaceList) {
                // * Does this interface support this attribute?
                TypeExpression member = (TypeExpression)interfaze.AcceptOperation(this, arg);
                if (member != null)
                    return member;
            }
            // * not found
            return null;
        }
        #endregion

        #region Int.member
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
            return null;
        }
        #endregion

        #region TypeVariable.member
        public override object Exec(TypeVariable d, object arg) {
            if (d.Substitution != null) {
                DynVarOptions.Instance.AssignDynamism(d.Substitution, d.IsDynamic);
                return d.Substitution.AcceptOperation(this, arg);
            }
            return TypeVariable.NewTypeVariable; // * A fresh TV, but no constraint is generated 
        }
        #endregion

        #region UnionType.member
        public override object Exec(UnionType d, object arg) {
            // * Infinite loop detection
            if (this.previousDot.Contains(d))
                return null;
            previousDot.Add(d);

            TypeExpression returnType = null;
            foreach (TypeExpression type in d.TypeSet) {
                TypeExpression ret = (TypeExpression)type.AcceptOperation(this, arg);
                if (ret == null && !d.IsDynamic)
                    return null;
                if (ret != null)
                    returnType = UnionType.collect(returnType, ret);
            }
            return returnType;
        }
        #endregion

        #region Report Errors
        // IN this case no error is reporterd
        public override object ReportError(TypeExpression tE) {
            System.Diagnostics.Debug.Assert(false, "No implementado");
            return null;
        }
        #endregion
    }
}
using ErrorManagement;
using TypeSystem;
using System;


namespace TypeSystem.Operations {
    /// <summary>
    /// This class represent the entry wnen sending a message to an operation object derived from TypeExpression.
    /// </summary>
    public abstract class TypeSystemOperation {
        public virtual bool IsBinary() { return true; }
        public virtual object Exec(ArrayType a, object arg)        { return Exec((TypeExpression)a, arg); }
        public virtual object Exec(BCLClassType b, object arg)     { return Exec((ClassType)b, arg); }
        public virtual object Exec(BCLInterfaceType b, object arg) { return Exec((InterfaceType)b, arg); }
        public virtual object Exec(BoolType b, object arg)         { return Exec((TypeExpression)b, arg); }
        public virtual object Exec(CharType c, object arg)         { return Exec((TypeExpression)c, arg); }
        public virtual object Exec(ClassType c, object arg)        { return Exec((UserType)c, arg); }
        public virtual object Exec(ClassTypeProxy c, object arg)   { return Exec((TypeExpression)c, arg); }
        public virtual object Exec(DoubleType d, object arg)       { return Exec((TypeExpression)d, arg); }
        public virtual object Exec(FieldType g, object arg)        { return Exec((TypeExpression)g, arg); }
        public virtual object Exec(InterfaceType i, object arg)    { return Exec((UserType)i, arg); }
        public virtual object Exec(IntersectionType i, object arg) { return Exec((TypeExpression)i, arg); }
        public virtual object Exec(IntType i, object arg)          { return Exec((TypeExpression)i, arg); }
        public virtual object Exec(MethodType m, object arg)       { return Exec((TypeExpression)m, arg); }
        public virtual object Exec(NullType n, object arg)         { return Exec((TypeExpression)n, arg); }
        public virtual object Exec(PropertyType p, object arg)     { return Exec((TypeExpression)p, arg); }
        public virtual object Exec(StringType s, object arg)       { return Exec((TypeExpression)s, arg); }
        public virtual object Exec(TypeExpression t, object arg)   { return ReportError(t); }
        public virtual object Exec(TypeVariable t, object arg)     { return Exec((TypeExpression)t, arg); }
        public virtual object Exec(UnionType u, object arg)        { return Exec((TypeExpression)u, arg); }
        public virtual object Exec(UserType u, object arg)         { return Exec((TypeExpression)u, arg); }
        public virtual object Exec(VoidType v, object arg)         { return Exec((TypeExpression)v, arg); }

        public virtual object ReportError(String operation, TypeExpression typeExpression, Location location) {
            ErrorManager.Instance.NotifyError(new InternalOperationInterfaceError(operation, typeExpression.FullName, location));
            return null;

        }

        public virtual object ReportError(String operation, TypeExpression typeExpression) {
            return ReportError(operation, typeExpression, new Location());
        }

        public virtual object ReportError(TypeExpression typeExpression) {
            ErrorManager.Instance.NotifyError(new InternalOperationInterfaceError("Unknown TypeSystemOperation", typeExpression.FullName, new Location()));
            return null;
        }

    }
}
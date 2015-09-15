using TypeSystem.Operations;
using TypeSystem;
using ErrorManagement;
using AST;
using Tools;
using System.Collections.Generic;
using CodeGeneration.ExceptionManagement;
namespace CodeGeneration.Operations {
    /// <summary>
       ///  It typechecks the runtime arguments, embeded in a method call, with the parametes of this method.
       ///  </summary>       
    public class CGPlainTypeExpressionOperation : TypeSystemOperation {
        /// <summary>
        ///  List of TypeExpressions
        /// </summary>
        private List<TypeExpression> typeExpressionList;


        internal bool RemoveTypeExpression(TypeExpression t) {
            bool contains = typeExpressionList.Contains(t);
            if ( contains )
                this.typeExpressionList.Remove(t);
            return contains;
        }

        private List<TypeExpression> AddTypeExpression(TypeExpression t) {
            if (!typeExpressionList.Contains(t))
                this.typeExpressionList.Add(t);
            return this.typeExpressionList;
        }
        public CGPlainTypeExpressionOperation() {
            this.typeExpressionList = new List<TypeExpression>(); 
        }

        public List<TypeExpression> TypeExpressionList {
            get { return this.typeExpressionList; }
        }
        // Dispatcher methods
        public override object Exec(ArrayType firstOperand, object arg) {
            return AddTypeExpression(firstOperand);
        }
          //public override object AcceptOperation(BCLClassType firstOperand) { return AcceptOperation((ClassType)firstOperand); }
        //public override object AcceptOperation(BCLInterfaceType firstOperand) { return AcceptOperation((InterfaceType)firstOperand); }

        public override object Exec(BoolType firstOperand, object arg) {
            return AddTypeExpression(firstOperand);
        }
        public override object Exec(CharType firstOperand, object arg) {
            return AddTypeExpression(firstOperand);
        }
        public override object Exec(ClassType firstOperand, object arg) {
            return AddTypeExpression(firstOperand);
        }
        public override object Exec(ClassTypeProxy firstOperand, object arg) {
            return AddTypeExpression(firstOperand.RealType);
        }
        public override object Exec(DoubleType firstOperand, object arg) {
            return AddTypeExpression(firstOperand);
        }

        public override object Exec(FieldType firstOperand, object arg) {
            return firstOperand.MemberInfo.Class.AcceptOperation(this, arg);
        }
        public override object Exec(InterfaceType firstOperand, object arg) {
            return AddTypeExpression(firstOperand);
        }
        public override object Exec(IntersectionType firstOperand, object arg) {
            foreach (TypeExpression type in firstOperand.TypeSet)
                type.AcceptOperation(this, arg);

            return this.typeExpressionList;
        }
        public override object Exec(IntType firstOperand, object arg) {
            return AddTypeExpression(firstOperand);
        }
        public override object Exec(MethodType firstOperand, object arg) {
            return firstOperand.MemberInfo.Class.AcceptOperation(this, arg);
        }

        public override object Exec(NullType firstOperand, object arg) {
            return AddTypeExpression(firstOperand);
        }
        public override object Exec(PropertyType firstOperand, object arg) {
            return firstOperand.MemberInfo.Class.AcceptOperation(this, arg);
        }
        //public override object AcceptOperation(Statement firstOperand) { return ReportError(firstOperand); }
        public override object Exec(StringType firstOperand, object arg) {
            return AddTypeExpression(firstOperand);
        }
        //public override object AcceptOperation(TypeExpression firstOperand) { return ReportError(firstOperand); }
        public override object Exec(TypeVariable firstOperand, object arg) {
            if (!firstOperand.IsFreshVariable())
                return firstOperand.Substitution.AcceptOperation(this, arg);
            return this.typeExpressionList;

        }
        public override object Exec(UnionType firstOperand, object arg) {
            foreach (TypeExpression type in firstOperand.TypeSet)
                type.AcceptOperation(this, arg);

            return this.typeExpressionList;
        }

        public override object Exec(UserType firstOperand, object arg) {
            return AddTypeExpression(firstOperand);
        }
        public override object Exec(VoidType firstOperand, object arg) {
            return AddTypeExpression(firstOperand);
        }
        
        public override object ReportError(TypeExpression tE) {
            ErrorManager.Instance.NotifyError(new CodeGenerationError("No se ha definido la operación solicitada"));
            return null;
        }

    }
}
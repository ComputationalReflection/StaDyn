using TypeSystem;
using AST;
using ErrorManagement;
using System;
using TypeSystem.Constraints;
using DynVarManagement;

namespace TypeSystem.Operations {
    /// <summary>
    /// Implements bracket operation of a type expression.
    /// Implments double dispatcher pattern.
    /// </summary>
    public class SquareBracketOperation : TypeSystemOperation {
        
        #region Fields
        protected TypeExpression index; //* type expression that indexes the type that is using the bracket
        protected MethodType methodAnalyzed;
        protected bool showErrorMessage;
        protected Location location;
#endregion
        
        #region Constructor
        public SquareBracketOperation(TypeExpression index, MethodType methodAnalyzed, bool showErrorMessage, Location location) {
            this.index = index; 
            this.methodAnalyzed = methodAnalyzed;
            this.showErrorMessage = showErrorMessage;
            this.location = location;
        }
        // * We simply check if the index is promotable to an Integer.
        #endregion
        
        
        #region ArrayType []
        public override object Exec(ArrayType a, object arg) {
            if (this.index.AcceptOperation(PromotionOperation.Create(IntType.Instance, ArrayOperator.Indexer, this.methodAnalyzed, this.location), arg) != null)
                return a.ArrayTypeExpression;
           return null;
        }
        #endregion
        
        #region TypeVariable []
        public override object Exec(TypeVariable a, object arg) { // this is a likely array

            TypeExpression subtitutions = a.Substitution;
            if (subtitutions != null) {
                DynVarOptions.Instance.AssignDynamism(subtitutions, a.IsDynamic);
                return subtitutions.AcceptOperation(this, arg);
            }
            if (this.methodAnalyzed != null) {
                // * A bracket constraint is added to the method analyzed
                SquareBracketConstraint bracketConstraint = new SquareBracketConstraint(a, this.index, this.location);
                this.methodAnalyzed.AddConstraint(bracketConstraint);
                // * Also a promotion constriaint of the index to IntType
                //index.Promotion(IntType.Instance, ArrayOperator.Indexer, methodAnalyzed, fileName, line, column);
                return bracketConstraint.ReturnType;
            }
            // We are at this point the operation is invalid, report the error.?
            return ReportError(a);
        }
        #endregion
        
        #region PropertyType []
        public override object Exec(PropertyType a, object arg) {
            if (a.PropertyTypeExpression!= null)
                return a.PropertyTypeExpression.AcceptOperation(this, arg);
            return null;
        }
        #endregion

        #region BCLClassType []
        public override object Exec(BCLClassType a, object arg) {
            // * Brackets are allowed if it is an array nb
            if (a.TypeInfo.IsArray)
                return TypeTable.Instance.GetType(a.TypeInfo.GetElementType().FullName, this.location);
            // * Brackets are allowed if it is an indexer
            if (a.Methods.ContainsKey("get_Item")) {
                MethodType method = a.Methods["get_Item"].Type as MethodType;
                if (method != null && method.ParameterListCount == 1) // && method.GetParameter(0).Equivalent(IntType.Instance))
                    return method.Return;
            }
            // if we are in these point the [] is not applicable simply raise an error.
            return ReportError(a);
        }
        #endregion

        #region UnionType []
        public override object Exec(UnionType a, object arg) {
            // * If all the types in typeset generate a constraint, we simply generate one constraint using the whole union type
            if (a.IsFreshVariable() && this.methodAnalyzed != null) {
                // * A constraint is added to the method analyzed
                SquareBracketConstraint constraint = new SquareBracketConstraint(a, this.index, this.location);
                this.methodAnalyzed.AddConstraint(constraint);
                return constraint.ReturnType;
            }
            TypeExpression returnType = null;
            foreach (TypeExpression type in a.TypeSet) {
                TypeExpression ret = (TypeExpression)type.AcceptOperation(new SquareBracketOperation(this.index, this.methodAnalyzed, !a.IsDynamic && this.showErrorMessage, this.location), arg);
                if (ret == null && !a.IsDynamic)
                    return null;
                if (ret != null)
                    returnType = UnionType.collect(returnType, ret);
            }
            return returnType;
        }

        #endregion
        
        #region FieldType []
        public override object Exec(FieldType a, object arg) {
            if (a.FieldTypeExpression != null)
                return a.FieldTypeExpression.AcceptOperation(this, arg);
            return null;
        }
        #endregion
        
        
        #region Report Errors
        // in our case we only notify operations not allowed
        // for other pruposes invoke explicitly another kind of error

//TODO: Hacer una versión más específica del error
        public override object ReportError(TypeExpression tE) {
            if (this.showErrorMessage)
                ErrorManager.Instance.NotifyError(new OperationNotAllowedError("[]", tE.FullName, this.location));
            return null;
        }
        #endregion
    }

}
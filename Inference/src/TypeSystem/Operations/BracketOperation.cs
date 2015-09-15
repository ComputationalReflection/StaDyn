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
    public class SquareBracketOperation : Operation {
        
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
        public override object Exec(ArrayType a) {
            if (this.index.Exec(PromotionOperation.Create(IntType.Instance, ArrayOperator.Indexer, this.methodAnalyzed, this.location)) != null)
                return a.ArrayTypeExpression;
           return null;
        }
        #endregion
        
        #region TypeVariable []
        public override object Exec(TypeVariable a) { // this is a likely array

            TypeExpression subtitutions = a.Substitution;
            if (subtitutions != null) {
                DynVarOptions.Instance.AssignDynamism(subtitutions, a.IsDynamic);
                return subtitutions.Exec(this);
            }
            if (this.methodAnalyzed != null) {
                // * A bracket constraint is added to the method analyzed
                BracketConstraint bracketConstraint = new BracketConstraint(a, this.index, this.location);
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
        public override object Exec(PropertyType a) {
            if (a.PropertyTypeExpression!= null)
                return a.PropertyTypeExpression.Exec(this);
            return null;
        }
        #endregion

        #region BCLClassType []
        public override object Exec(BCLClassType a) {
            // * Brackets are allowed if it is an array
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
        public override object Exec(UnionType a ) {
            // * If all the types in typeset generate a constraint, we simply generate one constraint using the whole union type
            if (a.IsFreshVariable() && this.methodAnalyzed != null) {
                // * A constraint is added to the method analyzed
                BracketConstraint constraint = new BracketConstraint(a, this.index, this.location);
                this.methodAnalyzed.AddConstraint(constraint);
                return constraint.ReturnType;
            }
            TypeExpression returnType = null;
            foreach (TypeExpression type in a.TypeSet) {
                TypeExpression ret = (TypeExpression)type.Exec(new SquareBracketOperation(a, this.methodAnalyzed, !a.IsDynamic && this.showErrorMessage, this.location));
                if (ret == null && !a.IsDynamic)
                    return null;
                if (ret != null)
                    returnType = UnionType.collect(returnType, ret);
            }
            return returnType;
        }

        #endregion
        
        #region FieldType []
        public override object Exec(FieldType a) {
            if (a.FieldTypeExpression != null)
                return a.FieldTypeExpression.Exec(this);
            return null;
        }
        #endregion
        
        
        #region Report Errors
        // in our case we only notify operations not allowed
        // for other pruposes invoke explicitly another kind of error

//TODO: Hacer una versión mças específica del error
        public override object ReportError(TypeExpression tE) {
            if (this.showErrorMessage)
                ErrorManager.Instance.NotifyError(new OperationNotAllowedError("[]", tE.FullName, this.location));
            return null;
        }
        #endregion
    }

}
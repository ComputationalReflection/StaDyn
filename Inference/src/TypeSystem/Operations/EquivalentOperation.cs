using TypeSystem.Operations;
using TypeSystem;
using ErrorManagement;
using AST;
using Tools;
using System.Collections.Generic;
using TypeSystem.Constraints;
using System;
using DynVarManagement;
namespace TypeSystem.Operations {
    /// <summary>
    /// No location information is provided within this class, because the exec methods invoked by the proper messages always return 
    /// a boolean type. A 
    /// </summary>
    public class EquivalentOperation : TypeSystemOperation {
        #region Fields
        protected TypeExpression operand2;
        #endregion

        #region Constructor
        public EquivalentOperation(TypeExpression operand2) {
            this.operand2 = operand2;
        }
        #endregion

        #region Array.Equivalent(...)
        public override object Exec(ArrayType operand1, object arg) {
            // * Is it an array?
            ArrayType arrayType = TypeExpression.As<ArrayType>(this.operand2);
            if (arrayType != null)
                return operand1.ArrayTypeExpression.AcceptOperation(new EquivalentOperation(arrayType.ArrayTypeExpression), arg);

            // * It can be a System.Array
            BCLClassType bclClassType = TypeExpression.As<BCLClassType>(this.operand2);

            if (bclClassType != null && bclClassType.TypeInfo.IsArray)
            {
                TypeExpression elementType = TypeTable.Instance.GetType(bclClassType.TypeInfo.GetElementType().FullName, new Location());
                return elementType.AcceptOperation(new EquivalentOperation(operand1), arg);
            }

            return false;
        }

        #endregion

        #region BCLClassType.Equivalent(...)
        public override object Exec(BCLClassType operand1, object arg) {
            if (this.operand2 == null)
                return false;

            if (operand1.FullName.Equals(this.operand2.FullName))
                return true;

            if ( (bool)BCLClassType.BCLtoTypeSystemMapping.ContainsKey(operand1.FullName) && (bool)BCLClassType.BCLtoTypeSystemMapping[operand1.FullName].AcceptOperation(new EquivalentOperation(operand2), null))
                return true;

            if (operand1.TypeInfo.IsArray) {
                Type elementType = operand1.TypeInfo.GetElementType();

                if (this.operand2 is ArrayType)
                    return new BCLClassType(elementType.FullName, elementType).AcceptOperation(new EquivalentOperation(( (ArrayType)this.operand2 ).ArrayTypeExpression), null);

                BCLClassType bclType = TypeExpression.As<BCLClassType>(this.operand2);
                if (bclType != null && bclType.TypeInfo.IsArray) {
                    TypeExpression thisArrayType = TypeTable.Instance.GetType(operand1.TypeInfo.GetElementType().FullName, new Location()),
                        paramArrayType = TypeTable.Instance.GetType(bclType.TypeInfo.GetElementType().FullName, new Location());
                    return thisArrayType.AcceptOperation(new EquivalentOperation(paramArrayType), null);
                }
            }
            return false;
        }
        #endregion


        #region ClassTypeProxy(...)

        public override object Exec(ClassTypeProxy operand1, object arg) {
            return operand1.RealType.AcceptOperation(this, arg);
        }
        #endregion

        #region FieldType (...)

        public override object Exec(FieldType operand1, object arg) {
            FieldType fieldType = this.operand2 as FieldType;
            if (fieldType != null)
                return operand1.FieldTypeExpression.AcceptOperation(new EquivalentOperation(fieldType.FieldTypeExpression), arg);
            return operand1.FieldTypeExpression.AcceptOperation(this, arg);
        }
        #endregion

        #region MethodType.Equivalent(...)

        public override object Exec(MethodType operand1, object arg) {
            if (operand1 == this.operand2)
                return true;

            TypeVariable typeVariable = this.operand2 as TypeVariable;
            if (typeVariable != null)
                return typeVariable.AcceptOperation(new EquivalentOperation(operand1), arg);

            MethodType method = this.operand2 as MethodType;
            // * It must be a method
            if (method == null)
                return false;
            // * Same name
            if (!operand1.MemberInfo.MemberIdentifier.Equals(method.MemberInfo.MemberIdentifier))
                return false;
            // * Same class
            if ( !(bool)operand1.MemberInfo.Class.AcceptOperation(new EquivalentOperation(method.MemberInfo.Class), null) )
                return false;
            // * Same signature
            if (operand1.ParameterListCount != method.ParameterListCount)
                return false;
            for (int i = 0; i < operand1.ParameterListCount; i++)
                if ( !(bool)operand1.GetParameter(i).AcceptOperation(new EquivalentOperation(method.GetParameter(i)), null) )
                    return false;
            return true;
        }
        #endregion

        #region TypeExpression.Equivalent
        public override object Exec(TypeExpression operand1, object arg) {
            if (this.operand2 == operand1)
                return true;
            TypeVariable typeVariable = this.operand2 as TypeVariable;
            if (typeVariable != null)
                return typeVariable.AcceptOperation(new EquivalentOperation(operand1), arg);
            FieldType fieldType = this.operand2 as FieldType;
            if (fieldType != null)
                return fieldType.AcceptOperation(this, arg);
            UnionType unionType = this.operand2 as UnionType;
            if (unionType != null)            
                return this.operand2.FullName.Contains(operand1.FullName);            
            return operand1.FullName.Equals(this.operand2.FullName);
        }

        #endregion

        #region TypeVariable.Equivalent
        public override object Exec(TypeVariable operand1, object arg) {
            if (operand1.Substitution != null) {
                DynVarOptions.Instance.AssignDynamism(operand1.Substitution, operand1.IsDynamic);
                // * If the variable is bounded, the equivalence is the one of its substitution
                return operand1.EquivalenceClass.Substitution.AcceptOperation(this, arg);
            }
            // * A free variable is equivalent to any type
            return true;
        }
        #endregion

        #region UnionType.Equivalent
        public override object Exec(UnionType operand1, object arg) {
            foreach (TypeExpression type in operand1.TypeSet)
                if (!(bool)type.AcceptOperation(this, arg))
                    return false;

            return true;
        }
        #endregion
        public override object ReportError(TypeExpression tE) {
            //if (this.showErrorMessage)
            System.Diagnostics.Debug.Assert(false, "Called Report Error in Equivalent operation. Revisite your code");
            return null;
        }
    }
}

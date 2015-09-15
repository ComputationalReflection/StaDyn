using System.Collections.Generic;
using Tools;
namespace TypeSystem.Operations {
    /// <summary>
    /// Implemeents a double dispatcher pattern
    /// Role:
    /// </summary>
    public class EqualsForOverloadOperation : TypeSystemOperation {
        #region Fields
        private TypeExpression typeExpression;
        #endregion
        #region Constructor
        public EqualsForOverloadOperation(TypeExpression typeExpression) {
            this.typeExpression = typeExpression;
        }
        #endregion

        #region Methods
        #region Equals.TypeExpresion
        public override object Exec(TypeExpression te, object arg) {
            return te.Equals(this.typeExpression);
        }
        #endregion
        #region Equals.MethodType
        public override object Exec(MethodType m, object arg) {
            MethodType method = this.typeExpression as MethodType;
            if (method == null)
                return false;
            if (m.ParameterListCount != method.ParameterListCount)
                return false;
            for (int i = 0; i < method.ParameterListCount; i++)
                if (!(bool)m.GetParameter(i).AcceptOperation(new EqualsForOverloadOperation(method.GetParameter(i)), arg))
                    return false;
            return true;
        }
        #endregion
        #region Equals.ClassTypeProxy
        public override object Exec(ClassTypeProxy ct, object arg) {
            return ct.RealType.AcceptOperation(new EqualsForOverloadOperation(this.typeExpression), arg);
        }
        #endregion
        #region Equals.TypeVariable
        public override object Exec(TypeVariable t, object arg) {
            return this.typeExpression is TypeVariable;
        }
        #endregion
        #region Errors
        public override object ReportError(TypeExpression firstOperand) {
            throw new System.NotImplementedException();
        }
        #endregion
        #endregion
    }
}
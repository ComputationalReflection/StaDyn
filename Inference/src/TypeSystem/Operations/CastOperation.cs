
using ErrorManagement;
using TypeSystem.Constraints;
namespace TypeSystem.Operations {
    /// <summary>
    /// These clase implements cast operation over a type expression that is encapsulated in a message pass to AcceptOperation.
    /// Tels if the caller can cast to CastType
    /// Implements Double dispatcher pattern
    /// </summary>
    public class CastOperation : TypeSystemOperation {
        #region Fields
        /// <summary>
        /// The expected type
        /// </summary>
        protected TypeExpression castType;
        /// <summary>
        /// The method that is being analyzed when the operation is performed.
        /// </summary>
        protected MethodType methodAnalyzed;
        /// <summary>
        /// The location (file, line, column) of text being analysed</param>
        /// </summary>
        protected Location location;
        /// <summary>
        /// Indicates if an error message should be shown (used for dynamic types)
        /// </summary>
        protected bool showErrorMessages;
        #endregion

        #region Constructor
        /// <summary>
        /// A constructor for CastOperation objects
        /// </summary>
        /// <param name="castType">The expected type</param>
        /// <param name="methodAnalyzed">The method that is being analyzed when the operation is performed.</param>
        /// <param name="location">The location (file, line, column) of text being analysed</param>
        public CastOperation(TypeExpression castType, MethodType methodAnalyzed, Location location) {
            this.castType = castType;
            this.methodAnalyzed = methodAnalyzed;
            this.location = location;
        }
        #endregion

        #region (TypeExpression) --->castType
        /// <summary>
        /// If the cast is possible it returns the value of the field "to". If there is an error and showMessages is true an error is raised.
        /// </summary>
        /// <param name="from">The type from we want to cast.</param>
        /// <returns>The TypeExpression castType if the casting is possible. Otherwise. If there is an error and showMessages
        /// is true an error is raised.
        /// </returns>
        public override object Exec(TypeExpression from, object arg) {
            if (this.castType == null)
                return null;
            if (((int)castType.AcceptOperation(new PromotionLevelOperation(from), arg) != -1) || ((int)from.AcceptOperation(new PromotionLevelOperation(castType), arg) != -1))
                return castType;
            return ReportError(from);
        }
        #endregion

        #region (TypeVarible) --->castType
        /// <summary>
        /// If the from:TypeVariable has substitution the exec method whi this scope of variables is invoked.
        /// If the cast is possible it returns the value of the field "to". If there is an error and showMessages
        /// is true an error is raised, and a CastConstraint is added, depending the value of showMessages and methodAnalyzed.
        /// 
        /// </summary>
        /// <param name="from">The type from we want to cast.</param>
        /// <returns>The TypeExpression castType if the casting is possible. Otherwise. If there is an error and showMessages
        /// is true, an error is raised.</returns>
        public override object Exec(TypeVariable from, object arg) {
            if (from.Substitution != null) {
                return from.Substitution.AcceptOperation(this, arg);
            }
            if (this.methodAnalyzed != null) {
                // * A constraint is added to the method analyzed
                CastConstraint constraint = new CastConstraint(from, this.castType, this.location);
                this.methodAnalyzed.AddConstraint(constraint);
                return constraint.ReturnType;
            }
            return ReportError(from);
        }
        #endregion

        #region (FieldType) --->castType
        /// <summary>
        /// If the FieldType from is valid then applying the same operation to this field is returned. If there is an error and showMessages is true an error is raised.
        /// </summary>
        /// <param name="from">The type from we want to cast.</param>
        /// <returns>The TypeExpression castType if the casting is possible. Otherwise. If there is an error and showMessages
        /// is true an error is raised.
        /// </returns>
        public override object Exec(FieldType from, object arg) {
            if (from.FieldTypeExpression != null)
                return from.FieldTypeExpression.AcceptOperation(this, arg);
            return null;
        }
        #endregion


        /// <summary>
        /// If showMessages is true it raises and error. It returns null
        /// </summary>
        /// <param name="from">TypeExpression from we want to cast</param>
        /// <returns>null</returns>
        public override object ReportError(TypeExpression from) {
            ErrorManager.Instance.NotifyError(new TypeCastError(from.FullName, this.castType.FullName, this.location));
            return null;
        }
    }
}
using TypeSystem;
using TypeSystem.Operations;
namespace CodeGeneration.Operations {
      /// <summary>
    /// Having two TypeExpression T1 and T2 if T1 [ T2 it returns T2 if  T2 [ T1
    /// otherwise if no conversion is possible in any direction it returns null.
    /// The dynamic behaviour is
    ///     T1\/T2 <= X    iff   T1<=X or T2<=X
    /// </summary>
    /// </summary>
    ///

    public class MajorTypeOperation : TypeSystemOperation {

        #region Fields
        /// <summary>
        /// One of the type expression we want to compare in the Exec methods.
        /// </summary>
        protected TypeExpression secondTypeExpression;
        #endregion

        #region Constructor
        public MajorTypeOperation(TypeExpression secondTypeExpression) {
            this.secondTypeExpression = secondTypeExpression;
        }
        #endregion

        #region Exec Methods


        /// <summary>
        ///  General behaviour of the operation As explained in the Class summary
        /// </summary>
        /// <param name="firstTypeExpression">the TypeExpression to compare with the attribute secondTypeExpression</param>
        /// <param name="arg">not used</param>
        /// <returns>the major type of both type expressions or null if there's no major type.</returns>
        public override object Exec(TypeExpression firstTypeExpression, object arg) {
            if ( (int)firstTypeExpression.AcceptOperation(new PromotionLevelOperation(this.secondTypeExpression), null) >= 0 )
                return this.secondTypeExpression;

            if ( (int)this.secondTypeExpression.AcceptOperation(new PromotionLevelOperation(firstTypeExpression), null) >= 0 )
                return firstTypeExpression;

            return null;
        }

        #endregion
    }
}
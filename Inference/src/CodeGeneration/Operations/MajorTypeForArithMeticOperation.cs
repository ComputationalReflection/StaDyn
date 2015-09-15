using AST;
using TypeSystem;
namespace CodeGeneration.Operations {
    
      /// <summary>
    /// Having two TypeExpression T1 and T2 if T1 [ T2 it returns T2 if  T2 [ T1
    /// otherwise if no conversion is possible in any direction it returns null.
    /// if any of the TypeExpressions is a StringType it Returns String
    /// The dynamic behaviour is
    ///     T1\/T2 <= X    iff   T1<=X or T2<=X or T1==String ---~ X=String or T2 String ---~ X=String
    /// </summary>
    /// </summary>
    ///

    public class MajorTypeForArithMeticOperation : MajorTypeOperation {

        #region Fields
        /// <summary>
        /// Node Containing the arithmetic expressiong, that is the operands and the operator.
        /// </summary>
        ArithmeticExpression node;
        #endregion

        #region Constructor
        public MajorTypeForArithMeticOperation(TypeExpression secondTypeExpression,
                                                     ArithmeticExpression node) : base(secondTypeExpression) {
            this.node = node;
        }
        #endregion

        #region Exec Methods

        /// <summary
        ///  General behaviour of the operation As explained in the Class summary
        /// </summary>
        /// <param name="firstTypeExpression">the TypeExpression to compare with the attribute secondTypeExpression</param>
        /// <param name="arg">not used</param>
        /// <returns>the major type of both type expressions or null if there's no major type.</returns>
        public override object Exec(TypeExpression firstTypeExpression, object arg) {
            if ( this.node.Operator == ArithmeticOperator.Plus
                &&
                    ( TypeExpression.Is<StringType>(this.secondTypeExpression)
                        ||
                     TypeExpression.Is<StringType>(firstTypeExpression)
                     )
                )
                return StringType.Instance;
            //if ( TypeExpression.Is<CharType>(firstTypeExpression) || TypeExpression.Is<CharType>(this.secondTypeExpression) )
            //    return IntType.Instance;
            return firstTypeExpression.AcceptOperation(new MajorTypeOperation(this.secondTypeExpression), arg);
        }
        public override object Exec(CharType firstTypeExpression, object arg) {
            if ( this.node.Operator == ArithmeticOperator.Plus && TypeExpression.Is<StringType>(this.secondTypeExpression) )
                return StringType.Instance;
           
           return TypeExpression.Is<CharType>(this.secondTypeExpression) ? IntType.Instance : firstTypeExpression.AcceptOperation(new MajorTypeOperation(this.secondTypeExpression), arg);
        }

        public override object Exec(StringType firstTypeExpression, object arg) {
            return this.node.Operator == ArithmeticOperator.Plus ? StringType.Instance : firstTypeExpression.AcceptOperation(new MajorTypeOperation(this.secondTypeExpression), arg);
        }


        #endregion
    }
}
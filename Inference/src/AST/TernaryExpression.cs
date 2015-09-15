////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: TernaryExpression.cs                                                 //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a ternary expression of our programming language.          //
//    Inheritance: Expression.                                                //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 05-12-2006                                                    //
// Modification date: 25-04-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using Tools;
using ErrorManagement;

namespace AST
{
    #region TernaryOperator

    /// <summary>
    /// Ternary operators
    /// </summary>
    public enum TernaryOperator
    {
        /// <summary>
        /// op1 ? op2 : op3
        /// </summary>
        Question
    }

    #endregion

    /// <summary>
    /// Encapsulates a ternary expression of our programming language.
    /// </summary>
    /// <remarks>
    /// Inheritance: Expression.
    /// Implements Composite pattern [Composite].
    /// Implements Visitor pattern [Concrete Element].
    /// </remarks>
    public class TernaryExpression : Expression
    {
        #region Fields

        /// <summary>
        /// Represents the first operand of the ternary expression
        /// </summary>
        private Expression operand1;

        /// <summary>
        /// Represents the second of the ternary expression
        /// </summary>
        private Expression operand2;

        /// <summary>
        /// Represents the third operand of the ternary expression
        /// </summary>
        private Expression operand3;

        /// <summary>
        /// Represents the operator of the ternary expression
        /// </summary>
        private TernaryOperator op;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the first operand of the ternary expression
        /// </summary>
        public Expression FirstOperand
        {
            get { return operand1; }
           set { this.operand1 = value; }
        }

        /// <summary>
        /// Gets the second operand of the ternary expression
        /// </summary>
        public Expression SecondOperand
        {
            get { return operand2; }
           set { this.operand2 = value; }   
        }

        /// <summary>
        /// Gets the third operand of the ternary expression
        /// </summary>
        public Expression ThirdOperand
        {
            get { return operand3; }
           set { this.operand3 = value; }
        }

        /// <summary>
        /// Gets the operator of the ternary expression
        /// </summary>
        public TernaryOperator Operator
        {
            get { return op; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of TernaryExpression
        /// </summary>
        /// <param name="operand1">First operand.</param>
        /// <param name="operand2">Second operand.</param>
        /// <param name="operand3">Third operand.</param>
        /// <param name="op">Operator of the ternary expression.</param>
        /// <param name="fileName">File name.</param>
        /// <param name="lineNumber">Line number.</param>
        /// <param name="columnNumber">Column number.</param>
       public TernaryExpression(Expression operand1, Expression operand2, Expression operand3, TernaryOperator op, Location location)
          : base(location)
        {
            this.operand1 = operand1;
            this.operand2 = operand2;
            this.operand3 = operand3;
            this.op = op;
        }

        #endregion

        #region Accept()

        /// <summary>
        /// Accept method of a concrete visitor.
        /// </summary>
        /// <param name="v">Concrete visitor</param>
        /// <param name="o">Optional information to use in the visit.</param>
        /// <returns>Optional information to return</returns>
        public override Object Accept(Visitor v, Object o)
        {
           return v.Visit(this, o);
        }

        #endregion
     }
}

////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: ArithmeticExpression.cs                                              //
// Authors: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                   //
//          Francisco Ortin - francisco.ortin@gmail.com                       //
// Description:                                                               //
//    Encapsulates arithmetic binary expressions.                             //
//    Inheritance: BinaryExpression.                                          //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 05-12-2006                                                    //
// Modification date: 06-04-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using Tools;
using ErrorManagement;

namespace AST {
    #region ArithmeticOperator

    /// <summary>
    /// Arithmetic binary operators
    /// </summary>
    public enum ArithmeticOperator {
        /// <summary>
        /// a - b
        /// </summary>Plus,
        Minus = 1,

        /// <summary>
        /// a + b
        /// </summary>
        Plus = 2,

        /// <summary>
        /// a * b
        /// </summary>
        Mult = 3,

        /// <summary>
        /// a / b
        /// </summary>
        Div = 4,

        /// <summary>
        /// a % b
        /// </summary>
        Mod = 5,
    }
    #endregion

    /// <summary>
    /// Encapsulates arithmetic binary expressions.
    /// </summary>
    /// <remarks>
    /// Inheritance: BinaryExpression.
    /// Implements Composite pattern [Component].
    /// Implements Visitor pattern [Concrete Element].
    /// </remarks>
    public class ArithmeticExpression : BinaryExpression {
        #region Fields

        /// <summary>
        /// Operator of the arithmetic expresion
        /// </summary>
        private ArithmeticOperator op;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the operator of the binary expression
        /// </summary>
        public ArithmeticOperator Operator {
            get { return op; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor de la clase ArithmeticExpression
        /// </summary>
        /// <param name="operand1">First operand.</param>
        /// <param name="operand2">Second operand.</param>
        /// <param name="op">Operator of the binary expression.</param>
        /// <param name="fileName">File name.</param>
        /// <param name="lineNumber">Line number.</param>
        /// <param name="columnNumber">Column number.</param>
        public ArithmeticExpression(Expression operand1, Expression operand2, ArithmeticOperator op, Location location)
            : base(operand1, operand2, location) {
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
        public override Object Accept(Visitor v, Object o) {
            return v.Visit(this, o);
        }

        #endregion

        #region ToString()
        public override string ToString() {
            return this.op.ToString();
        }
        #endregion
    }
}

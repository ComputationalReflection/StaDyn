////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: UnaryExpression.cs                                                   //
// Authors: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                   //
//          Francisco Ortin - francisco.ortin@gmail.com                       //
// Description:                                                               //
//    Encapsulates a unary expression of our programming language.            //
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
   #region UnaryOperator

   /// <summary>
   /// Unary operators
   /// </summary>
   public enum UnaryOperator
   {
      /// <summary>
      /// Prefix increment operator: ++i
      /// </summary>
      PrefixIncrement,

      /// <summary>
      /// Prefix decrement operator: --i
      /// </summary>
      PrefixDecrement,

      /// <summary>
      /// Postfix increment operator: i++ 
      /// </summary>
      PostfixIncrement,

      /// <summary>
      /// Postfix decrement operator: i-- 
      /// </summary>
      PostfixDecrement,

      /// <summary>
      /// Logical negation operator: !i 
      /// </summary>
      Not,

      /// <summary>
      /// Bitwise complement operator: ~i
      /// </summary>
      BitwiseNot,

      /// <summary>
      /// Negation operator: -i
      /// </summary>
      Minus,

      /// <summary>
      /// Plus operator: +i
      /// </summary>
      Plus
   }

   #endregion

   /// <summary>
   /// Encapsulates a unary expression of our programming language.
   /// </summary>
   /// <remarks>
   /// Inheritance: Expression.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class UnaryExpression : Expression
   {
      #region Fields

      /// <summary>
      /// Represents the operand of the unary expression.
      /// </summary>
      private Expression operand;

      /// <summary>
      /// Represents the operation of the unary expression.
      /// </summary>
      private UnaryOperator op;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the operand expression of the unary operation
      /// </summary>
      public Expression Operand
      {
         get { return operand; }
         set { this.operand = value; }
      }

      /// <summary>
      /// Gets the operator of the unary operation
      /// </summary>
      public UnaryOperator Operator
      {
         get { return op; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of UnaryExpression.
      /// </summary>
      /// <param name="operand">Operand expression to the unary expression.</param>
      /// <param name="op">Operator to the unary expression.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public UnaryExpression(Expression operand, UnaryOperator op, Location location)
         : base(location)
      {
         this.operand = operand;
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

      #region ToString()
      public override string ToString() {
          return this.op.ToString();
      }
      #endregion
   }
}

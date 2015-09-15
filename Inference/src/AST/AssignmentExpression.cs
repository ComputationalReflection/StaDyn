////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: AssignmentExpression.cs                                              //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates assignment binary expressions.                             //
//    Inheritance: BinaryExpression.                                          //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 05-12-2006                                                    //
// Modification date: 09-04-2006                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using Tools;
using ErrorManagement;

namespace AST
{
   #region AssignmentOperator

   /// <summary>
   /// Assignment binary operators
   /// </summary>
   public enum AssignmentOperator
   {
      /// <summary>
      /// a = b
      /// </summary>
      Assign = 1,

      /// <summary>
      /// a += b
      /// </summary>
      PlusAssign,

      /// <summary>
      /// a -= b
      /// </summary>
      MinusAssign,

      /// <summary>
      /// a *= b
      /// </summary>
      MultAssign,

      /// <summary>
      /// a /= b
      /// </summary>
      DivAssign,

      /// <summary>
      /// a %= b
      /// </summary>
      ModAssign,

      /// <summary>
      /// a >>= b
      /// </summary>
      ShiftRightAssign,

      /// <summary>
      /// a <<= b
      /// </summary>
      ShiftLeftAssign,

      /// <summary>
      /// a &= b
      /// </summary>
      BitwiseAndAssign,

      /// <summary>
      /// a ^= b
      /// </summary>
      BitwiseXOrAssign,

      /// <summary>
      /// a |= b
      /// </summary>
      BitwiseOrAssign,
   }

   #endregion

   /// <summary>
   /// Encapsulates assignment binary expressions.
   /// </summary>
   /// <remarks>
   /// Inheritance: BinaryExpression.
   /// Implements Composite pattern [Component].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class AssignmentExpression : BinaryExpression
   {
      #region Fields

      /// <summary>
      /// Operator of the assignment expression
      /// </summary>
      private AssignmentOperator op;

      /// <summary>
      /// Move statement associated to the assignment expression.
      /// </summary>
      private MoveStatement moveStat;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the operator of the binary expression
      /// </summary>
      public AssignmentOperator Operator
      {
         get { return op; }
      }

      /// <summary>
      /// Gets or sets a move statement associated to the assignment expression.
      /// </summary>
      public MoveStatement MoveStat
      {
         get { return this.moveStat; }
         set { this.moveStat = value; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor de la clase AssignmentExpression
      /// </summary>
      /// <param name="operand1">First operand.</param>
      /// <param name="operand2">Second operand.</param>
      /// <param name="op">Operator of the binary expression.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public AssignmentExpression(Expression operand1, Expression operand2, AssignmentOperator op, Location location) : base(operand1, operand2, location)
      {
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

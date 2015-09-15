////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: BitwiseExpression.cs                                                 //
// Authors: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                   //
//          Francisco Ortin - francisco.ortin@gmail.com                       //
// Description:                                                               //
//    Encapsulates a bitwise binary expression.                               //
//    Inheritance: BinaryExpression.                                          //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 25-04-2007                                                    //
// Modification date: 25-04-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using Tools;
using ErrorManagement;

namespace AST
{
   #region BitwiseOperator

   /// <summary>
   /// Bitwise binary operators
   /// </summary>
   public enum BitwiseOperator
   {
      /// <summary>
      /// a | b
      /// </summary>
      BitwiseOr = 1,

      /// <summary>
      /// a & b
      /// </summary>
      BitwiseAnd = 2,

      /// <summary>
      /// a ^ b
      /// </summary>
      BitwiseXOr = 3,

      /// <summary>
      /// a << b
      /// </summary>
      ShiftLeft = 4,

      /// <summary>
      /// a >> b
      /// </summary>
      ShiftRight = 5,
   }

   #endregion

   /// <summary>
   /// Encapsulates a bitwise binary expression.
   /// </summary>
   /// <remarks>
   /// Inheritance: BinaryExpression.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class BitwiseExpression : BinaryExpression
   {
      #region Fields

      /// <summary>
      /// Operator of the bitwise expression
      /// </summary>
      private BitwiseOperator op;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the operator of the binary expression
      /// </summary>
      public BitwiseOperator Operator
      {
         get { return op; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of BitwiseExpression
      /// </summary>
      /// <param name="operand1">First operand.</param>
      /// <param name="operand2">Second operand.</param>
      /// <param name="op">Operator of the binary expression.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public BitwiseExpression(Expression operand1, Expression operand2, BitwiseOperator op, Location location)
         : base(operand1, operand2, location)
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

      #region ToString()

      public override string ToString()
      {
         return this.op.ToString();
      }

      #endregion
   }
}

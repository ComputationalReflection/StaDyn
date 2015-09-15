////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: LogicalExpression.cs                                                 //
// Authors: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                   //
//          Francisco Ortin - francisco.ortin@gmail.com                       //
// Description:                                                               //
//    Encapsulates a logical binary expression.                               //
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

namespace AST
{
   #region LogicalOperator

   /// <summary>
   /// Logical binary operators
   /// </summary>
   public enum LogicalOperator
   {
      /// <summary>
      /// a || b
      /// </summary>
      Or,

      /// <summary>
      /// a && b
      /// </summary>
      And,
   }

   #endregion

   /// <summary>
   /// Encapsulates a logical binary expression.
   /// </summary>
   /// <remarks>
   /// Inheritance: BinaryExpression.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class LogicalExpression : BinaryExpression
   {
      #region Fields

      /// <summary>
      /// Operator of the logical expression
      /// </summary>
      private LogicalOperator op;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the operator of the binary expression
      /// </summary>
      public LogicalOperator Operator
      {
         get { return op; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of LogicalExpression
      /// </summary>
      /// <param name="operand1">First operand.</param>
      /// <param name="operand2">Second operand.</param>
      /// <param name="op">Operator of the binary expression.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public LogicalExpression(Expression operand1, Expression operand2, LogicalOperator op, Location location)
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

       public override string ToString() {
           return this.op.ToString();
       }
   }
}

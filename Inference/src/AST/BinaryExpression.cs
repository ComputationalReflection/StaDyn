////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: BinaryExpression.cs                                                  //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a binary expression of our programming language.           //
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
   /// <summary>
   /// Encapsulates a binary expression of our programming language.
   /// </summary>
   /// <remarks>
   /// Inheritance: Expression.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public abstract class BinaryExpression : Expression
   {
      #region Fields

      /// <summary>
      /// Represents the first operand of the binary expression
      /// </summary>
      private Expression operand1;

      /// <summary>
      /// Represents the second of the binary expression
      /// </summary>
      private Expression operand2;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the first operand of the binary expression
      /// </summary>
      public Expression FirstOperand
      {
         get { return operand1; }
         set { operand1 = value; }
      }

      /// <summary>
      /// Gets the second operand of the binary expression
      /// </summary>
      public Expression SecondOperand
      {
         get { return operand2; }
         set { operand2 = value; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of BinaryExpression
      /// </summary>
      /// <param name="operand1">First operand.</param>
      /// <param name="operand2">Second operand.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      protected BinaryExpression(Expression operand1, Expression operand2, Location location)
          : base(location)
      {
         this.operand1 = operand1;
         this.operand2 = operand2;
      }

      #endregion
   }
}

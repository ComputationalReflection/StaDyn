////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: RelationalExpression.cs                                              //
// Authors: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                   //
//          Francisco Ortin - francisco.ortin@gmail.com                       //
// Description:                                                               //
//    Encapsulates a relational binary expression.                            //
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
   #region RelationalOperator

   /// <summary>
   /// Relational binary operators
   /// </summary>
   public enum RelationalOperator
   {
      /// <summary>
      /// a != b
      /// </summary>
      NotEqual,

      /// <summary>
      /// a == b
      /// </summary>
      Equal,

      /// <summary>
      /// a < b
      /// </summary>
      LessThan,

      /// <summary>
      /// a > b
      /// </summary>
      GreaterThan,

      /// <summary>
      /// a <= b
      /// </summary>
      LessThanOrEqual,

      /// <summary>
      /// a >= b
      /// </summary>
      GreaterThanOrEqual,
   }

   #endregion

   /// <summary>
   /// Encapsulates a relational binary expression.
   /// </summary>
   /// <remarks>
   /// Inheritance: BinaryExpression.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class RelationalExpression : BinaryExpression
   {
      #region Fields

      /// <summary>
      /// Operator of the relational expression
      /// </summary>
      private RelationalOperator op;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the operator of the binary expression
      /// </summary>
      public RelationalOperator Operator
      {
         get { return op; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of RelationalExpression
      /// </summary>
      /// <param name="operand1">First operand.</param>
      /// <param name="operand2">Second operand.</param>
      /// <param name="op">Operator of the binary expression.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public RelationalExpression(Expression operand1, Expression operand2, RelationalOperator op, Location location)
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
       public override string  ToString() {
 	 return op.ToString();
}
 
	#endregion

   }
}

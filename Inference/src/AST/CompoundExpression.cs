////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: CompoundExpression.cs                                                //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a set of expressions.                                      //
//    Inheritance: Expression.                                                //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 11-12-2006                                                    //
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
   /// Encapsulates a set of expressions.
   /// </summary>
   /// <remarks>
   /// Inheritance: Expression.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class CompoundExpression : Expression
   {
      #region Fields

      /// <summary>
      /// Represents a list of expressions.
      /// </summary>
      private List<Expression> expressions;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the number of expressions.
      /// </summary>
      public int ExpressionCount
      {
         get { return this.expressions.Count; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of CompoundExpression
      /// </summary>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public CompoundExpression(Location location): base(location) {
         this.expressions = new List<Expression>();
      }

      #endregion

      #region AddExpression()

      /// <summary>
      /// Add a new expression.
      /// </summary>
      /// <param name="exp">Expression to add.</param>
      public void AddExpression(Expression exp)
      {
         this.expressions.Add(exp);
      }

      #endregion

      #region GetExpressionElement()

      /// <summary>
      /// Gets the element stored in the specified index.
      /// </summary>
      /// <param name="index">Index.</param>
      /// <returns>Element stored in the specified index.</returns>
      public Expression GetExpressionElement(int index)
      {
         return this.expressions[index];
      }

      #endregion

      #region SetExpressionElement()

      /// <summary>
      /// Updates the value of the element stored in the specified index.
      /// </summary>
      /// <param name="index">Index.</param>
      public void SetExpressionElement(int index, SingleIdentifierExpression exp)
      {
         this.expressions[index] = exp;
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

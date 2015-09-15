////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: IsExpression.cs                                                      //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a Is expression.                                           //
//    Inheritance: Expression.                                                //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 07-12-2006                                                    //
// Modification date: 25-04-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using Tools;
using TypeSystem;
using ErrorManagement;

namespace AST
{
   /// <summary>
   /// Encapsulates a Is expression.
   /// </summary>
   /// <remarks>
   /// Inheritance: Expression.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class IsExpression : Expression
   {
      #region Fields

      /// <summary>
      /// Represents the expression to check its type.
      /// </summary>
      private Expression expr;

      /// <summary>
      /// Represents the type to check with the expression.
      /// </summary>
      private TypeExpression type;

      /// <summary>
      /// Represents the type identifier to check with the expression.
      /// </summary>
      private string typeId;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the expression.
      /// </summary>
      public Expression Expression
      {
         get { return this.expr; }
         set { this.expr = value; }
      }

      /// <summary>
      /// Gets the type to check with the expression.
      /// </summary>
      public TypeExpression TypeExpr
      {
         get { return this.type; }
         set { this.type = value; }
      }

      /// <summary>
      /// Gets the type identifier to check with the expression.
      /// </summary>
      public string TypeId
      {
         get { return this.typeId; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of IsExpression
      /// </summary>
      /// <param name="expression">Expression to check its type.</param>
      /// <param name="type">TypeExpression to check with the expression.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public IsExpression(Expression expression, string type, Location loc)
         : base(loc)
      {
         this.expr = expression;
         this.typeId = type;
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

////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: CastExpression.cs                                                    //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a cast expression.                                         //
//    Inheritance: Expression.                                                //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 07-12-2006                                                    //
// Modification date: 21-03-2007                                              //
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
   /// Encapsulates a cast expression.
   /// </summary>
   /// <remarks>
   /// Inheritance: Expression.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class CastExpression : Expression
   {
      #region Fields

      /// <summary>
      /// Represents the type to convert the expression.
      /// </summary>
      private TypeExpression type;

      /// <summary>
      /// Identifier of the cast type
      /// </summary>
      private string castTypeId;

      /// <summary>
      /// Represents the expression to convert.
      /// </summary>
      private Expression expression;

      #endregion

      #region Properties

      /// <summary>
      /// Gets or sets the type to convert the expression
      /// </summary>
      public TypeExpression CastType
      {
         get { return this.type; }
         set { this.type = value; }
      }

      /// <summary>
      /// Gets the cast identifier.
      /// </summary>
      public string CastId
      {
         get { return this.castTypeId; }
      }

      /// <summary>
      /// Gets the expression to convert.
      /// </summary>
      public Expression Expression
      {
         get { return this.expression; }
         set { this.expression = value; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of CastExpression
      /// </summary>
      /// <param name="castType">WriteType expression to convert the expression.</param>
      /// <param name="exp">Expression to cast.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public CastExpression(string castType, Expression exp, Location location) : base(location)
      {
         this.castTypeId = castType;
         this.expression = exp;
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

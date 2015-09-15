////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: QualifiedIdentifierExpression.cs                                     //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates the qualified identifier expression.                       //
//    Inheritance: Expression.                                                //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 26-12-2006                                                    //
// Modification date: 26-12-2006                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using Tools;
using ErrorManagement;

namespace AST
{
   /// <summary>
   /// Encapsulates the qualified name expression.
   /// </summary>
   /// <remarks>
   /// Inheritance: Expression.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class QualifiedIdentifierExpression : IdentifierExpression
   {
      #region Fields

      /// <summary>
      /// Represents the expression to compose the name.
      /// </summary>
      private IdentifierExpression expression;

      /// <summary>
      /// Represents the first name of the expression.
      /// </summary>
      private SingleIdentifierExpression idName;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the expression to compose the name.
      /// </summary>
      public IdentifierExpression IdExpression
      {
         get { return this.expression; }
      }

      /// <summary>
      /// Gets the first name to the expression.
      /// </summary>
      public SingleIdentifierExpression IdName
      {
         get { return this.idName; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of QualifiedIdentifierExpression
      /// </summary>
      /// <param name="exp">Expression to compose the name.</param>
      /// <param name="fieldName">First name.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public QualifiedIdentifierExpression(SingleIdentifierExpression name, IdentifierExpression exp, Location location)
          : base(location)
      {
         this.expression = exp;
         this.idName = name;
         this.Identifier = name.Identifier + "." + exp.Identifier;
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

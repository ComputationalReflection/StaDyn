////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: StringLiteralExpression.cs                                           //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a string literal expression.                               //
//    Inheritance: Expression.                                                //
//    Implements Composite pattern [Leaf].                                    //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 05-12-2006                                                    //
// Modification date: 12-12-2006                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using Tools;
using ErrorManagement;

namespace AST
{
   /// <summary>
   /// Encapsulates a string literal expression.
   /// </summary>
   /// <remarks>
   /// Inheritance: Expression.
   /// Implements Composite pattern [Leaf].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class StringLiteralExpression : Expression
   {
      #region Fields

      /// <summary>
      /// Represents a string literal expression.
      /// </summary>
      private string stringValue;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the string value
      /// </summary>
      public string StringValue
      {
         get { return stringValue; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of StringLiteralExpression
      /// </summary>
      /// <param name="stringLiteral">string value.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public StringLiteralExpression(string stringLiteral, Location location)
          : base(location) 
      {
         this.stringValue = stringLiteral;
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

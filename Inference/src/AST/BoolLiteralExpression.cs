////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: BoolLiteralExpression.cs                                             //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a boolean literal expression.                              //
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
   /// Encapsulates a boolean literal expression.
   /// </summary>
   /// <remarks>
   /// Inheritance: Expression.
   /// Implements Composite pattern [Leaf].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class BoolLiteralExpression : Expression
   {
      #region Fields

      /// <summary>
      /// Represents a boolean value
      /// </summary>
      private bool boolValue;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the boolean value
      /// </summary>
      public bool BoolValue
      {
         get { return boolValue; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of BoolLiteralExpressoin
      /// </summary>
      /// <param name="boolLiteral">Boolean value.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public BoolLiteralExpression(bool boolLiteral, Location location)
          : base(location)
      {
         this.boolValue = boolLiteral;
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

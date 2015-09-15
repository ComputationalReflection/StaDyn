////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: IntLiteralExpression.cs                                              //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a integer literal expression.                              //
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
   /// Encapsulates a integer literal expression.
   /// </summary>
   /// <remarks>
   /// Inheritance: Expression.
   /// Implements Composite pattern [Leaf].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class IntLiteralExpression : Expression
   {
      #region Fields

      /// <summary>
      /// Represents a int value.
      /// </summary>
      private int intValue;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the int value
      /// </summary>
      public int IntValue
      {
         get { return intValue; }
      }

      /// <summary>
      /// Gets the int value in string form
      /// </summary>
      public string ILValue
      {
         get { return this.intValue.ToString(); }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of IntLiteralExpression
      /// </summary>
      /// <param name="intLiteral">int value.</param> 
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public IntLiteralExpression(int intLiteral, Location location) : base(location)
      {
         this.intValue = intLiteral;
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

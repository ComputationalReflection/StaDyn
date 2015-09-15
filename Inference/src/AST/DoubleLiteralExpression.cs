////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: DoubleLiteralExpression.cs                                           //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a double literal expression.                               //
//    Inheritance: Expression.                                                //
//    Implements Composite pattern [Leaf].                                    //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 05-12-2006                                                    //
// Modification date: 11-06-2006                                              //
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
   public class DoubleLiteralExpression : Expression
   {
      #region Fields

      /// <summary>
      /// Stores a double value
      /// </summary>
      private double doubleValue;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the double value
      /// </summary>
      public double DoubleValue
      {
         get { return doubleValue; }
      }

      /// <summary>
      /// Gets the double value in string form
      /// </summary>
      public string ILValue
      {
         get
         {
            System.Globalization.NumberFormatInfo nfi = new System.Globalization.NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            return Convert.ToString(this.doubleValue, nfi);
         }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of DoubleLiteralExpression
      /// </summary>
      /// <param name="doubleLiteral">double value.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public DoubleLiteralExpression(double doubleLiteral, Location location)
          : base(location) 
      {
         this.doubleValue = doubleLiteral;
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

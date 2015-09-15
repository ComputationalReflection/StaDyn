////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: CharLiteralExpression.cs                                             //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a character literal expression.                            //
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
   /// Encapsulates a character literal expression.
    /// </summary>
    /// <remarks>
   /// Inheritance: Expression.
   /// Implements Composite pattern [Leaf].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
    public class CharLiteralExpression : Expression
    {
        #region Fields

        /// <summary>
        /// Represents a character value.
        /// </summary>
        private char charValue;

        #endregion

        #region Properties
        
        /// <summary>
        /// Gets the character value
        /// </summary>
        public char CharValue
        {
            get { return charValue; }
        }
                
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of CharLiteralExpression
        /// </summary>
        /// <param name="charLiteral">character value.</param>
        /// <param name="fileName">File name.</param>
        /// <param name="lineNumber">Line number.</param>
        /// <param name="columnNumber">Column number.</param>
       public CharLiteralExpression(char charLiteral, Location location):base(location)
        {
            this.charValue = charLiteral;
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

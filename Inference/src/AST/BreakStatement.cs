////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: BreakStatement.cs                                                    //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a Break statement of our programming languages.            //
//    Inheritance: Statement.                                                 //
//    Implements Composite pattern [Leaf].                                    //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 05-12-2006                                                    //
// Modification date: 13-12-2006                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using Tools;
using ErrorManagement;

namespace AST
{
   /// <summary>
   /// Encapsulates a Break statement of our programming languages.
   /// </summary>
   /// <remarks>
   /// Inheritance: Statement.
   /// Implements Composite pattern [Leaf].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class BreakStatement : Statement
   {
      #region Constructor

      /// <summary>
      /// Constructor of BreakStatement.
      /// </summary>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
       public BreakStatement(Location location)
           : base(location)
      {
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

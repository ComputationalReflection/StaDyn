////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: ForeachStatement.cs                                                  //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a Foreach statement of our programming languages.          //
//    Inheritance: Statement.                                                 //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 12-01-2007                                                    //
// Modification date: 03-04-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using Tools;
using ErrorManagement;

namespace AST
{
   /// <summary>
   /// Encapsulates a Foreach statement of our programming languages.
   /// </summary>
   /// <remarks>
   /// Inheritance: Statement.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class ForeachStatement : Statement
   {
      #region Fields

      /// <summary>
      /// Declaration of foreach statement.
      /// </summary>
      private Declaration decl;

      /// <summary>
      /// Represents a expression of the foreach statement.
      /// </summary>
      private Expression expression;

      /// <summary>
      /// Represents a block of Foreach statement.
      /// </summary>
      private Statement block;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the declaration of the foreach statement.
      /// </summary>
      public Declaration ForEachDeclaration
      {
         get { return this.decl; }
      }

      /// <summary>
      /// Gets the expression of the foreach statement.
      /// </summary>
      public Expression ForeachExp
      {
         get { return this.expression; }
      }

      /// <summary>
      /// Gets the block of Foreach statement.
      /// </summary>
      public Statement ForeachBlock
      {
         get { return this.block; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of ForeachStatement
      /// </summary>
      /// <param name="type">WriteType of Foreach loop.</param>
      /// <param name="id">Name of Foreach loop.</param>
      /// <param name="expr">Expression of Foreach loop.</param>
      /// <param name="stats">Block executed in the Foreach loop.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public ForeachStatement(string type, SingleIdentifierExpression id, Expression expr, Statement stats, Location location)
        :base(location)
      {
         this.decl = new IdDeclaration(id, type, location);
         this.expression = expr;
         this.block = stats;
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

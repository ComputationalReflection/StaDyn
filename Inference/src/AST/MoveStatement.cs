////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: MoveStatement.cs                                                     //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a Move instruction to use in SSA algorithm.                //
//    Inheritance: Statement.                                                 //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 06-04-2007                                                    //
// Modification date: 30-07-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using Tools;
using ErrorManagement;

namespace AST
{
   /// <summary>
   /// Encapsulates a Move instruction to use in SSA algorithm
   /// </summary>
   /// <remarks>
   /// Inheritance: Statement.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class MoveStatement : Statement
   {
      #region Fields

      /// <summary>
      /// Represents the expression of the left part of Move instruccion
      /// </summary>
      private SingleIdentifierExpression leftExp;

      /// <summary>
      /// Represents the expression of the right part of Move instruccion
      /// </summary>
      private SingleIdentifierExpression rightExp;

      /// <summary>
      /// Move statement associated to the current move statement
      /// </summary>
      private MoveStatement moveStatement;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the left expression
      /// </summary>
      public SingleIdentifierExpression LeftExp
      {
         get { return this.leftExp; }
      }

      /// <summary>
      /// Gets the right expression
      /// </summary>
      public SingleIdentifierExpression RightExp
      {
         get { return this.rightExp; }
      }

      /// <summary>
      /// Gets or sets a move statement associated to the current move statement.
      /// </summary>
      public MoveStatement MoveStat
      {
         get { return this.moveStatement; }
         set { this.moveStatement = value; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of MoveStatement
      /// </summary>
      /// <param name="leftExpression">Expression of the left part of Move instruccion</param>
      /// <param name="rightExpression">Expression of the right part of Move instruccion</param>
      /// <param name="fileName">File name.</param>
      /// <param name="line">Line number.</param>
      public MoveStatement(SingleIdentifierExpression leftExpression, SingleIdentifierExpression rightExpression, string fileName, int line)
         : base(new Location(fileName, line, 0))
      {
         this.leftExp = leftExpression;
         this.rightExp = rightExpression;
         this.moveStatement = null;
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

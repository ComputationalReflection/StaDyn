////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: DoStatement.cs                                                       //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a Do statement of our programming language.                //
//    Inheritance: Statement.                                                 //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 05-12-2006                                                    //
// Modification date: 25-04-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using Tools;
using ErrorManagement;

namespace AST
{
   /// <summary>
   /// Encapsulates a Do statement of our programming language.
   /// </summary>
   /// <remarks>
   /// Inheritance: Statement.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class DoStatement : Statement
   {
      #region Fields

      /// <summary>
      /// Represents a condition to do loop
      /// </summary>
      private Expression condition;

      /// <summary>
      /// Represents a block of Do statements.
      /// </summary>
      private Statement doStatement;

      /// <summary>
      /// Represents the statements to init do loop.
      /// </summary>
      private List<MoveStatement> initDo;

      /// <summary>
      /// Represents the statements before do body.
      /// </summary>
      private List<ThetaStatement> beforeBody;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the condition expression of Do Statement.
      /// </summary>
      public Expression Condition
      {
         get { return condition; }
         set { condition = value; }
      }

      /// <summary>
      /// Gets the block executed when the condition is true.
      /// </summary>
      public Statement Statements
      {
         get { return doStatement; }
      }

      /// <summary>
      /// Gets or sets the statements to use at the init of the do loop.
      /// </summary>
      public List<MoveStatement> InitDo
      {
         get { return this.initDo; }
         set { this.initDo = value; }
      }

      /// <summary>
      /// Gets or sets the statements before do body.
      /// </summary>
      public List<ThetaStatement> BeforeBody
      {
         get { return this.beforeBody; }
         set { this.beforeBody = value; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of DoStatement
      /// </summary>
      /// <param name="statements">Block executed in the Do loop.</param>
      /// <param name="cond">Condition of the Do loop.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public DoStatement(Statement statements, Expression cond, Location location) : base(location) {
         this.condition = cond;
         this.doStatement = statements;
         this.beforeBody = new List<ThetaStatement>();
         this.initDo = new List<MoveStatement>();
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

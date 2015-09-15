////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: WhileStatement.cs                                                    //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a While statement of our programming languages.            //
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
   /// Encapsulates a While statement of our programming languages.
   /// </summary>
   /// <remarks>
   /// Inheritance: Statement.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class WhileStatement : Statement
   {
      #region Fields

      /// <summary>
      /// Represents a condition to while loop
      /// </summary>
      private Expression condition;

      /// <summary>
      /// Represents a block of While statements.
      /// </summary>
      private Statement whileStatement;

      /// <summary>
      /// Represents the statements to init while loop.
      /// </summary>
      private List<MoveStatement> initWhile;

      /// <summary>
      /// Represents the statements before condition.
      /// </summary>
      private List<ThetaStatement> beforeCondition;

      /// <summary>
      /// Represents the statements after condition.
      /// </summary>
      private List<MoveStatement> afterCondition;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the condition expression of While Statement.
      /// </summary>
      public Expression Condition
      {
         get { return condition; }
         set { this.condition = value; }
      }

      /// <summary>
      /// Gets the block executed when the condition is true.
      /// </summary>
      public Statement Statements
      {
         get { return whileStatement; }
      }

      /// <summary>
      /// Gets or sets the statements to use at the init of the while loop.
      /// </summary>
      public List<MoveStatement> InitWhile
      {
         get { return this.initWhile; }
         set { this.initWhile = value; }
      }

      /// <summary>
      /// Gets or sets the statements after condition.
      /// </summary>
      public List<MoveStatement> AfterCondition
      {
         get { return this.afterCondition; }
         set { this.afterCondition = value; }
      }

      /// <summary>
      /// Gets or sets the statements before condition.
      /// </summary>
      public List<ThetaStatement> BeforeCondition
      {
         get { return this.beforeCondition; }
         set { this.beforeCondition = value; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of WhileStatement
      /// </summary>
      /// <param name="cond">Condition of the While loop.</param>
      /// <param name="statements">Block executed in the While loop.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public WhileStatement(Expression cond, Statement statements, Location location)
          : base(location) 
      {
         this.condition = cond;
         this.whileStatement = statements;
         this.initWhile = new List<MoveStatement>();
         this.beforeCondition = new List<ThetaStatement>();
         this.afterCondition = new List<MoveStatement>();
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

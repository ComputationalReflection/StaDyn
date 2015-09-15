////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: ForStatement.cs                                                      //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a For statement of our programming languages.              //
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
   /// Encapsulates a For statement of our programming languages.
   /// </summary>
   /// <remarks>
   /// Inheritance: Statement.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class ForStatement : Statement
   {
      #region Fields

      /// <summary>
      /// Represents the initialization of variables of For statement.
      /// </summary>
      private List<Statement> initializer;

      /// <summary>
      /// Represents a condition to For loop
      /// </summary>
      private Expression condition;

      /// <summary>
      /// Represents the increment (or decrement) of the iterator to For statement.
      /// </summary>
      private List<Statement> iterator;

      /// <summary>
      /// Represents a block of For statements.
      /// </summary>
      private Statement forStatement;

      /// <summary>
      /// Represents the statements after initialization.
      /// </summary>
      private List<MoveStatement> afterInit;

      /// <summary>
      /// Represents the statements before condition.
      /// </summary>
      private List<ThetaStatement> beforeCondition;

      /// <summary>
      /// Represents the statements after condition.
      /// </summary>
      private List<MoveStatement> afterCondition;

      /// <summary>
      /// Statement block to store new variable declaration.
      /// </summary>
      private Block auxiliarInit;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the number of initializer expressions of For statement.
      /// </summary>
      public int InitializerCount
      {
         get { return this.initializer.Count; }
      }

      /// <summary>
      /// Gets the condition expression of For Statement.
      /// </summary>
      public Expression Condition
      {
         get { return condition; }
         set { condition = value; }
      }

      /// <summary>
      /// Gets the number of iterator expression of For loop.
      /// </summary>
      public int IteratorCount
      {
         get { return this.iterator.Count; }
      }

      /// <summary>
      /// Gets the block executed while the condition is true.
      /// </summary>
      public Statement Statements
      {
         get { return forStatement; }
      }

      /// <summary>
      /// Gets or sets the statements to use after initialization
      /// </summary>
      public List<MoveStatement> AfterInit
      {
         get { return this.afterInit; }
         set { this.afterInit = value; }
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

      /// <summary>
      /// Gets the auxiliar block of variable declarations
      /// </summary>
      public Block AuxInitializer
      {
         get { return this.auxiliarInit = new Block(location); }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of ForStatement
      /// </summary>
      /// <param name="init">Inicialization of loop variables.</param>
      /// <param name="cond">Condition of For loop. Could be null.</param>
      /// <param name="iter">Iterator expression of For loop.</param>
      /// <param name="statements">Block executed in the For loop.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public ForStatement(List<Statement> init, Expression cond, List<Statement> iter, Statement statements, Location location)
         : base(location)
      {
         if (init != null)
            this.initializer = init;
         else
            this.initializer = new List<Statement>();

         this.condition = cond;

         if (iter != null)
            this.iterator = iter;
         else
            this.iterator = new List<Statement>();

         this.forStatement = statements;

         this.afterInit = new List<MoveStatement>();
         this.beforeCondition = new List<ThetaStatement>();
         this.afterCondition = new List<MoveStatement>();
      }

      #endregion

      #region GetInitializerElement()

      /// <summary>
      /// Gets the element stored in the specified index.
      /// </summary>
      /// <param name="index">Index.</param>
      /// <returns>Element stored in the specified index.</returns>
      public Statement GetInitializerElement(int index)
      {
         return getElement(this.initializer, index);
      }

      #endregion

      #region GetIteratorElement()

      /// <summary>
      /// Gets the element stored in the specified index.
      /// </summary>
      /// <param name="index">Index.</param>
      /// <returns>Element stored in the specified index.</returns>
      public Statement GetIteratorElement(int index)
      {
         return getElement(this.iterator, index);
      }

      #endregion

      #region getElement()

      /// <summary>
      /// Gets the element stored in the specified index.
      /// </summary>
      /// <param name="index">Index.</param>
      /// <returns>Element stored in the specified index.</returns>
      private Statement getElement(List<Statement> list, int index)
      {
         return list[index];
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

      #region UpdateInitializer()

      /// <summary>
      /// Updates the initializer block of for statement with the new variable declaration information.
      /// </summary>
      internal void UpdateInitializer()
      {
         for (int i = 0; i < this.auxiliarInit.StatementCount; i++)
         {
            this.initializer.Insert(0, this.auxiliarInit.GetStatementElement(i));
         }
         this.auxiliarInit = null;
      }

      #endregion
   }
}

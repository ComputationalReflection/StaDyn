////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: Block.cs                                                             //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a block of statements.                                     //
//    Inheritance: Statement.                                                 //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 11-12-2006                                                    //
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
   /// Encapsulates a block of statements.
   /// </summary>
   /// <remarks>
   /// Inheritance: Statement.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class Block : Statement
   {
      #region Fields

      /// <summary>
      /// Represents a list of statements.
      /// </summary>
      private List<Statement> statements;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the number of statements.
      /// </summary>
      public int StatementCount
      {
         get { return this.statements.Count; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of Block
      /// </summary>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public Block(Location location):base(location)
      {
         this.statements = new List<Statement>();
      }

      /// <summary>
      /// Constructor of Block
      /// </summary>
      /// <param name="stats">List of statements of the block.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public Block(List<Statement> stats, Location location):base(location)
      {
         this.statements = stats;
      }

      #endregion

      #region AddStatement()

      /// <summary>
      /// Add a new statement to the end of the block.
      /// </summary>
      /// <param name="statement">Statements to add.</param>
      public void AddStatement(Statement statement) {
          if (statement != null)
              this.statements.Add(statement);
      }

      #endregion

      #region AddStatementToTheBeginning()

      /// <summary>
      /// Add a new statement.
      /// </summary>
      /// <param name="statement">Statements to add.</param>
      public void AddStatementToTheBeginning(Statement statement)
      {
         this.statements.Insert(0, statement);
      }

      #endregion

      #region AddStatementAtIndex()

      /// <summary>
      /// Add a new statement at the specified index.
      /// </summary>
      /// <param name="statement">Statement to add.</param>
      /// <param name="index">Position to insert.</param>
      public void AddStatementAtIndex(Statement statement, int index)
      {
         this.statements.Insert(index, statement);
      }

      #endregion

      #region SearchPosition()

      /// <summary>
      /// Searches the identifier and returns its position.
      /// </summary>
      /// <param name="id">Identifier to search.</param>
      /// <returns>Returns the position to the identifier.</returns>
      public int SearchPosition(string id)
      {
         for (int i = 0; i < this.statements.Count; i++)
			{
            if (this.statements[i] is IdDeclaration)
            {
               if (((IdDeclaration)this.statements[i]).Identifier.Equals(id))
                  return i;
            }

            if (this.statements[i] is DeclarationSet)
            {
               if (((DeclarationSet)this.statements[i]).ContainsId(id))
                  return i;
            }
			}
         return -1;
      }

      #endregion

      #region GetStatementElement()

      /// <summary>
      /// Gets the element stored in the specified index.
      /// </summary>
      /// <param name="index">Index.</param>
      /// <returns>Element stored in the specified index.</returns>
      public Statement GetStatementElement(int index)
      {
         return this.statements[index];
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

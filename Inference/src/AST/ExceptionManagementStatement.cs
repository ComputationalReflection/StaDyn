////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: ExceptionManagementStatement.cs                                                 //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a Try-Catch statement of our programming languages.        //
//    Inheritance: Statement.                                                 //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 05-12-2006                                                    //
// Modification date: 12-12-2006                                              //
////////////////////////////////////////////////////////////////////////////////


using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using Tools;
using ErrorManagement;
 

namespace AST
{
   /// <summary>
   /// Encapsulates a Try-Catch-finally statement of our programming languages.
   /// As C# states catch blcok and finally block can be optional, but not at the same time.
   /// </summary>
   /// <remarks>
   /// Inheritance: Statement.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class ExceptionManagementStatement : Statement
   {
      #region Fields

      /// <summary>
      /// Represents a block of statements executed in the Try block.
      /// </summary>
      private Block tryBlock;

      /// <summary>
      /// Set of Catch statements.
      /// </summary>
      private List<CatchStatement> catchStatements;

      /// <summary>
      /// Represents a block of statements executed in the Finally block.
      /// </summary>
      private Block finallyBlock;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the statements executed in Try block.
      /// </summary>
      public Block TryBlock
      {
         get { return this.tryBlock; }
         set { this.tryBlock = value;}
      }

      /// <summary>
      /// Gets the statements executed in Finally block.
      /// if there is no finally statementes can be null
      /// </summary>
      public Block FinallyBlock
      {
         get { return this.finallyBlock; }
         set { this.finallyBlock = value; }
      }

      /// <summary>
      /// Gets the number of catch blocks.
      /// </summary>
      public int CatchCount
      {
         get { return this.catchStatements.Count; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of ExceptionManagementStatement
      /// </summary>
      /// <param name="tryStats">Statements to execute in Try block.</param>
      /// <param name="finallyStats">Statements asociated to the finally block.</param>
      /// <param name="location">Location of the statement.</param>
      public ExceptionManagementStatement(Block tryBlock, List<CatchStatement> catchStatements, Block finallyBlock, Location location)
          : base(location) {
         this.tryBlock = tryBlock;
         this.catchStatements = catchStatements;
         this.finallyBlock = finallyBlock;
      }
     
      #endregion


      #region GetCatchElement()

      /// <summary>
      /// Gets the element stored in the specified index.
      /// </summary>
      /// <param name="index">Index.</param>
      /// <returns>Element stored in the specified index.</returns>
      public CatchStatement GetCatchElement(int index)
      {
          return this.catchStatements[index];
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

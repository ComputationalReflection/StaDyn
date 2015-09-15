////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: ReturnStatement.cs                                                   //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a Return statement of our programming languages.           //
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
using TypeSystem;
using ErrorManagement;

namespace AST
{
   /// <summary>
   /// Encapsulates a Return statement of our programming languages.
   /// </summary>
   /// <remarks>
   /// Inheritance: Statement.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class ReturnStatement : Statement
   {
      #region Fields

      /// <summary>
      /// Represents the return expression.
      /// </summary>
      private Expression returnExpression;

      /// <summary>
      /// Represents the assignment statements.
      /// </summary>
      private Block assigns;

       /// <summary>
       /// The method where the return statement appears
       /// </summary>
       private MethodType currentMethodType;
      #endregion

      #region Properties

      /// <summary>
      /// Gets the return expression.
      /// </summary>
      public Expression ReturnExpression
      {
         get { return this.returnExpression; }
         set { this.returnExpression = value; }
      }

      /// <summary>
      /// Gets the assignment statements.
      /// </summary>
      public Block Assigns
      {
         get { return this.assigns; }
      }

      /// <summary>
      /// The method where the return statement appears
      /// </summary>
      public MethodType CurrentMethodType {
          get { return this.currentMethodType; }
          set { this.currentMethodType= value; }
      }
      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of ReturnStatement
      /// </summary>
      /// <param name="returnExp">Return expression.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public ReturnStatement(Expression returnExp, Location location)
          : base(location) 
      {
         this.returnExpression = returnExp;
         this.assigns = new Block(this.Location);
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

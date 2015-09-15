////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: AssertStatement.cs                                                   //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a Assert statement of our programming languages.           //
//    Inheritance: Statement.                                                 //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 05-12-2006                                                    //
// Modification date: 05-12-2006                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using Tools;
using ErrorManagement;

namespace AST
{
   /// <summary>
   /// Encapsulates a Assert statement of our programming languages.
   /// </summary>
   /// <remarks>
   /// Inheritance: Statement.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class AssertStatement : Statement
   {
      #region Fields

      /// <summary>
      /// Represents the condition expression associated to the Assert statement.
      /// </summary>
      private Expression condition;

      /// <summary>
      /// Represents a optional expression associated to the Assert statement.
      /// </summary>
      private Expression opExp;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the condition expression associated to the Assert statement.
      /// </summary>
      public Expression Condition
      {
         get { return condition; }
      }

      /// <summary>
      /// Gets the optional expression associated to the Assert statement.
      /// </summary>
      public Expression Expression
      {
         get { return opExp; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of AssertStatement
      /// </summary>
      /// <param name="cond">Condition expression associated to the Assert statement.</param>
      /// <param name="exp">Optional expression associated to the Assert statement.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public AssertStatement(Expression cond, Expression exp, Location location): base(location)
      {
         this.condition = cond;
         this.opExp = exp;
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

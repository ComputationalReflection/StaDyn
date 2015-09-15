////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: IdentifierExpression.cs                                              //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a name expression of our programming language.       //
//    Inheritance: Expression.                                                //
//    Implements Composite pattern [Leaf].                                    //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 05-12-2006                                                    //
// Modification date: 30-04-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using Tools;
using ErrorManagement;
using AST.Operations;

namespace AST
{
   /// <summary>
   /// Encapsulates a name expression of our programming language.
   /// </summary>
   /// <remarks>
   /// Inheritance: Expression.
   /// Implements Composite pattern [Leaf].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public abstract class IdentifierExpression : Expression
   {
      #region Fields

      /// <summary>
      /// Represents the name name.
      /// </summary>
      private string id;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the name name.
      /// </summary>
      public string Identifier
      {
         get { return id; }
         set { this.id = value; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of IdentifierExpression
      /// </summary>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      protected IdentifierExpression(Location location)
         : base(location)
      {
      }

      #endregion
      #region Dispatcher AstOperation
      /// <summary>
      /// Dispatches expressions to the operation passed as argument.
      /// It provokes the execution of op.AcceptOperation(AstNode) with the parameter
      /// resolved polymorfically
      /// </summary>
      /// <param name="op">AstOperation to dispatch</param>
      /// <returns></returns>
      public override object AcceptOperation(AstOperation op, object arg) {
          return op.Exec(this, arg);
      }

      #endregion
   }
}

////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: Expression.cs                                                        //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Abstract class encapsulate a programming language expression.           //
//    Inheritance: Statement.                                                 //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Element].                                   //
// -------------------------------------------------------------------------- //
// Create date: 04-12-2006                                                    //
// Modification date: 09-04-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using Tools;
using TypeSystem;

using ErrorManagement;
using AST.Operations;

namespace AST
{
   /// <summary>
   /// Abstract class encapsulate a programming language expression.
   /// </summary>
   /// <remarks>
   /// Inheritance: Statement.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Element].
   /// </remarks>
   public abstract class Expression : Statement
   {
      #region Fields

      /// <summary>
      /// Represents the type of the expression.
      /// </summary>
      private TypeExpression expressionType;

      /// <summary>
      /// True if the expression is a lvalue. Otherwise, its value is false.
      /// </summary>
      private bool lvalue;

      /// <summary>
      /// True if the expression is allocated in the left part of an assignment, false otherwise.
      /// </summary>
      private bool leftExpression = false;

      /// <summary>
      /// WriteType variable may change its type's substitution (e.g., field type variables)
      /// This attribute saves the type in an specific time (frozen).
      /// If this type's substitution changes, the frozen type does not.
      /// </summary>
      protected TypeExpression frozenTypeExpression;

      #endregion

      #region Properties

      /// <summary>
      /// Gets or sets the type of the expression.
      /// </summary>
      public TypeExpression ExpressionType 
      {
         get { return this.expressionType; }
         set { this.expressionType = value; }
      }

      /// <summary>
      /// Gets or sets the lvalue. 
      /// </summary>
      public bool Lvalue
      {
         get { return this.lvalue; }
         set { this.lvalue = value; }
      }

      /// <summary>
      /// Gets or sets true if the expression is allocated in the left part of an assignment, false otherwise.
      /// </summary>
      public bool LeftExpression
      {
         get { return this.leftExpression; }
         set { this.leftExpression = value; }
      }

      /// <summary>
      /// Gets the type expression to use in code generation.
      /// </summary>
      public virtual TypeExpression ILTypeExpression
      {
         get 
         {
            if (this.frozenTypeExpression != null)
               return this.frozenTypeExpression;
            else
               return this.expressionType;
         }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Protected constructor of Expresion.
      /// </summary>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      protected Expression(Location location):base(location)
      { 
      }

      #endregion

      #region CloneInit()

      /// <summary>
      /// Clones the initialization of the current object.
      /// </summary>
      /// <returns>The reference of the cloned object.</returns>
      public Expression CloneInit()
      {
         return (Expression)this.MemberwiseClone();
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

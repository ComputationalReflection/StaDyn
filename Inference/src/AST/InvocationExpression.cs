////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: InvocationExpression.cs                                              //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a invocation expression.                                   //
//    Inheritance: BaseCallExpression.                                        //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 07-12-2006                                                    //
// Modification date: 25-04-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using Tools;
using TypeSystem;
using ErrorManagement;
using AST.Operations;
using TypeSystem.Operations;

namespace AST
{
   /// <summary>
   /// Encapsulates a invocation expression.
   /// </summary>
   /// <remarks>
   /// Inheritance: BaseCallExpression.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class InvocationExpression : BaseCallExpression
   {
      #region Fields

      /// <summary>
      /// Represents the name of the invocation expression.
      /// </summary>
      private Expression identifier;

      #endregion

      #region Properties
      /// <summary>
      /// Gets the expression to invoke
      /// </summary>
      public Expression Identifier
      {
         get { return this.identifier; }
         set { this.identifier = value; }
      }

      /// <summary>
      /// WriteType variable may change its type's substitution (e.g., field type variables)
      /// This attribute saves the type in an specific time (frozen).
      /// If this type's substitution changes, the frozen type does not.
      /// </summary>
      public TypeExpression FrozenTypeExpression {
           get { return this.frozenTypeExpression; }
           set { this.frozenTypeExpression = value; }
       }
      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of InvocationExpression
      /// </summary>
      /// <param name="name">Name of the expression to invoke.</param>
      /// <param name="arguments">Arguments of the invocation.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public InvocationExpression(Expression identifier, CompoundExpression arguments, Location location)
          : base(arguments,  location) {
         this.identifier = identifier;
      }

      #endregion

      #region Accept()

      /// <summary>
      /// Accept method of a concrete visitor.
      /// </summary>
      /// <param name="v">Concrete visitor</param>
      /// <param name="o">Optional information to use in the visit.</param>
      /// <returns>Optional information to return</returns>
      public override Object Accept(Visitor v, Object o) {
         return v.Visit(this, o);
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

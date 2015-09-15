////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: Declaration.cs                                                       //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a declaration of a concrete type.                          //
//    Inheritance: Statement.                                                 //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 07-12-2006                                                    //
// Modification date: 01-02-2007                                              //
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
   /// Encapsulates a declaration of a concrete type.
   /// </summary>
   /// <remarks>
   /// Inheritance: Statement.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public abstract class Declaration : Statement
   {
      #region Fields

      /// <summary>
      /// TypeExpression associated to the declaration
      /// </summary>
      private TypeExpression type = null;

      /// <summary>
      /// Name of the type expression
      /// </summary>
      private string fullName = "";

      /// <summary>
      /// WriteType variable may change its type's substitution (e.g., field type variables)
      /// This attribute saves the type in an specific time (frozen).
      /// If this type's substitution changes, the frozen type does not.
      /// </summary>
      protected TypeExpression frozenTypeExpression;

      #endregion

      #region Properties

      /// <summary>
      /// Gets or sets the type of the declaration
      /// </summary>
      public TypeExpression TypeExpr
      {
         get { return this.type; }
         set { this.type = value; }
      }

      /// <summary>
      /// Gets or sets the nominal type of the declaration
      /// </summary>
      public string FullName
      {
         get { return this.fullName; }
         set { this.fullName = value; }
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
               return this.type;
         }
      }


      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of Declaration
      /// </summary>
      /// <param name="type">TypeExpression of the declaration.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      protected Declaration(string type, Location location): base(location)
      {
         this.fullName = type;
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

////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: FieldAccessExpression.cs                                             //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates the expression to access a field.                          //
//    Inheritance: Expression.                                                //
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
using TypeSystem.Operations;
using AST.Operations;

namespace AST
{
   /// <summary>
   /// Encapsulates the expression to access a field.
   /// </summary>
   /// <remarks>
   /// Inheritance: Expression.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class FieldAccessExpression : Expression
   {
      #region Fields

      /// <summary>
      /// Represents the expression to access a field.
      /// </summary>
      private Expression expression;

      /// <summary>
      /// Represents the name to the field accessed
      /// </summary>
      private SingleIdentifierExpression field;


       /// <summary>
       /// To know if the visitor type definition has already inferred a type
       /// </summary>
       private bool typeInferredInVisitorTypeDefinition = false;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the expression to access a field.
      /// </summary>
      public Expression Expression
      {
         get { return this.expression; }
         set { this.expression = value; }
      }

      /// <summary>
      /// Gets the name to the field.
      /// </summary>
      public SingleIdentifierExpression FieldName
      {
         get { return this.field; }
         set { this.field = value; }
      }

       /// <summary>
       /// To know if the visitor type definition has already inferred a type
       /// </summary>
      public bool TypeInferredInVisitorTypeDefinition {
          get { return typeInferredInVisitorTypeDefinition; }
          set { typeInferredInVisitorTypeDefinition = value; }
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
      /// Constructor of FieldAccessExpression
      /// </summary>
      /// <param name="exp">Expression to access a field.</param>
      /// <param name="fieldName">Field name.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public FieldAccessExpression(Expression exp, SingleIdentifierExpression fieldName, Location location)
         : base(location)
      {
         this.expression = exp;
         this.field = fieldName;
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

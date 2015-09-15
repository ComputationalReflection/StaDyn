////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: NewExpression.cs                                                     //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a new expression.                                          //
//    Inheritance: BaseCallExpression.                                        //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 07-12-2006                                                    //
// Modification date: 01-01-2007                                              //
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
   /// Encapsulates a new expression.
   /// </summary>
   /// <remarks>
   /// Inheritance: BaseCallExpression.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class NewExpression : BaseCallExpression
   {
      #region Fields

      /// <summary>
      /// Stores the name about the new type
      /// </summary>
      private string typeInfo;

       /// <summary>
       /// The type expression of what is being created
       /// </summary>
       private TypeExpression newType;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the name of the new type
      /// </summary>
      public string TypeInfo
      {
         get { return this.typeInfo; }
      }

              /// <summary>
       /// The type expression of what is being created
       /// </summary>
       public TypeExpression NewType {
           get { return this.newType; }
           set { this.newType = value; }
       }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of NewExpression
      /// </summary>
      /// <param name="type">TypeExpression to create.</param>
      /// <param name="arguments">Arguments of the invocation.</param>
       /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public NewExpression(string type, CompoundExpression arguments, Location location)
         : base(arguments, location)
      {
         this.typeInfo = type;
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
////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: ArgumentExpression.cs                                                //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a argument expression of our programming language.         //
//    Inheritance: Expression.                                                //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 27-12-2006                                                    //
// Modification date: 28-12-2006                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using Tools;
using TypeSystem;
using ErrorManagement;

namespace AST
{
   #region ArgumentOptions // COMMENTED

   ///// <summary>
   ///// Argument type
   ///// </summary>
   //public enum ArgumentOptions
   //{
   //   /// <summary>
   //   /// The argument is neither reference nor output
   //   /// </summary>
   //   Default,

   //   /// <summary>
   //   /// Reference argument
   //   /// </summary>
   //   Ref,

   //   /// <summary>
   //   /// output argument
   //   /// </summary>
   //   Out
   //}

   #endregion

   /// <summary>
   /// Encapsulates a argument expression of our programming language.
   /// </summary>
   /// <remarks>
   /// Inheritance: Expression.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class ArgumentExpression : Expression
   {
      #region Fields

      /// <summary>
      /// Represents the argument
      /// </summary>
      private Expression argument;

      /// <summary>
      /// Represents the option of the argument
      /// </summary>
     // private ArgumentOptions option;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the argument expression
      /// </summary>
      public Expression Argument
      {
         get { return this.argument; }
         set { this.argument = value; }
      }

      /// <summary>
      /// Gets the type expression used in code generation.
      /// </summary>
      public override TypeExpression ILTypeExpression
      {
         get { return this.argument.ILTypeExpression; }
      }

      /// <summary>
      /// Gets the option of the argument
      /// </summary>
      //public ArgumentOptions ArgType
      //{
      //   get { return this.option; }
      //}

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of ArgumentExpression.
      /// </summary>
      /// <param name="arg">Argument expression.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public ArgumentExpression(Expression arg, Location location) : base(location) {
         this.argument = arg;
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

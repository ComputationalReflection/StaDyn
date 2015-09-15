////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: ConstructorDefinition.cs                                             //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a definition of a concrete constructor.                    //
//    Inheritance: MethodDefinition.                                          //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 31-12-2006                                                    //
// Modification date: 01-02-2007                                              //
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
   /// Encapsulates a definition of a concrete constructor.
   /// </summary>
   /// <remarks>
   /// Inheritance: MethodDefinition.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class ConstructorDefinition : MethodDefinition
   {
      #region Fields

      /// <summary>
      /// Initialization in the constructor definition
      /// </summary>
      private InvocationExpression initialization;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the base or this initialization of the constructor definition
      /// </summary>
      public InvocationExpression Initialization
      {
         get { return this.initialization; }
      }

      /// <summary>
      /// Sets the nominal type of the return type
      /// </summary>
      public string SetReturnType
      {
         set { this.ReturnTypeInfo = value; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of ConstructorDefinition.
      /// </summary>
      /// <param name="id">Name of the definition.</param>
      /// <param name="stats">Body associated to the method definition.</param>
      /// <param name="parameters">Parameters information.</param>
      /// <param name="modifiers">Modifiers information.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public ConstructorDefinition(SingleIdentifierExpression id, List<Modifier> modifiers, List<Parameter> parameters, InvocationExpression init, Block stats, Location location)
          : base(id, stats, null, parameters, modifiers, location)
      {
         this.initialization = init;
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

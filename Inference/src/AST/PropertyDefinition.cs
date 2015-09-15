////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: PropertyDefinition.cs                                                //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a property definition.                                     //
//    Inheritance: IdDeclaration.                                             //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 12-01-2007                                                    //
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
   /// Encapsulates a property definition.
   /// </summary>
   /// <remarks>
   /// Inheritance: IdDeclaration.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class PropertyDefinition : IdDeclaration
   {
      #region Fields

      /// <summary>
      /// Represents the statements associated to the get accessor. 
      /// </summary>
      private Statement getBlock;

      /// <summary>
      /// Represents the statements associated to the set accessor. 
      /// </summary>
      private Statement setBlock;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the statements associated to the get accessor. 
      /// </summary>
      public Statement GetBlock
      {
         get { return this.getBlock; }
      }

      /// <summary>
      /// Gets the statements associated to the set accessor. 
      /// </summary>
      public Statement SetBlock
      {
         get { return this.setBlock; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of Definition.
      /// </summary>
      /// <param name="id">Name of the definition.</param>
      /// <param name="type">TypeExpression of the definition.</param>
      /// <param name="get">Statements associated to get accessor.</param>
      /// <param name="set">Statements associated to set accessor.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public PropertyDefinition(SingleIdentifierExpression id, string type, Statement get, Statement set, Location location)
         : base(id, type, location)
      {
         this.getBlock = get;
         this.setBlock = set;
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

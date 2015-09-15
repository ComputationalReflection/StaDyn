////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: FieldDeclarationSet.cs                                               //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a set of field declarations.                               //
//    Inheritance: Declaration.                                               //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 29-01-2007                                                    //
// Modification date: 29-01-2007                                              //
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
   /// Encapsulates a set of field declarations.
   /// </summary>
   /// <remarks>
   /// Inheritance: Declaration.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class FieldDeclarationSet : Declaration
   {
      #region Fields

      /// <summary>
      /// Set of declarations.
      /// </summary>
      private List<FieldDeclaration> declarations;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the number of declarations
      /// </summary>
      public int Count
      {
         get { return this.declarations.Count; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of FieldDeclarationSet
      /// </summary>
      /// <param name="type">WriteType of the declaration.</param>
      /// <param name="decls">Set of declarations with the same type.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public FieldDeclarationSet(string type, List<FieldDeclaration> decls, Location location)
         : base(type, location)
      {
         this.declarations = decls;
      }

      #endregion

      #region GetDeclarationElement()

      /// <summary>
      /// Gets the element stored in the specified index.
      /// </summary>
      /// <param name="index">Index.</param>
      /// <returns>Element stored in the specified index.</returns>
      public FieldDeclaration GetDeclarationElement(int index)
      {
         return this.declarations[index];
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

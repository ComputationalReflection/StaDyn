////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: Namespace.cs                                                         //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a namespace definition.                                    //
//    Inheritance: AstNode.                                                   //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 31-12-2006                                                    //
// Modification date: 09-01-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ErrorManagement;

using Tools;

namespace AST
{
   /// <summary>
   /// Encapsulates a namespace definition.
   /// </summary>
   /// <remarks>
   /// Inheritance: AstNode.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class Namespace : AstNode
   {
      #region Fields

      /// <summary>
      /// Represents the name of the namespace
      /// </summary>
      private IdentifierExpression identifier;
      
      /// <summary>
      /// Stores the declarations of the namespace.
      /// </summary>
      private List<Declaration> namespaceBody;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the name of the namespace.
      /// </summary>
      public IdentifierExpression Identifier
      {
         get { return this.identifier; }
      }

      /// <summary>
      /// Gets the number of declaration in the namespace.
      /// </summary>
      public int NamespaceMembersCount
      {
         get { return this.namespaceBody.Count; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of Namespace
      /// </summary>
      /// <param name="name">Name of the namespace definition.</param>
      /// <param name="declarations">Members of the namespace definition.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public Namespace(IdentifierExpression name, List<Declaration> declarations, Location location) : base(location)
      {
         this.identifier = name;
         this.namespaceBody = declarations;
      }

      #endregion

      #region GetDeclarationElement()

      /// <summary>
      /// Gets the element stored in the specified index.
      /// </summary>
      /// <param name="index">Index.</param>
      /// <returns>Element stored in the specified index.</returns>
      public Declaration GetDeclarationElement(int index)
      {
         return this.namespaceBody[index];
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

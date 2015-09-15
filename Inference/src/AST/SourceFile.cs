////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: SourceFile.cs                                                        //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates the source code.                                           //
//    Inheritance: AstNode.                                                   //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 07-12-2006                                                    //
// Modification date: 09-02-2007                                              //
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
   /// Encapsulates the source code.
   /// </summary>
   /// <remarks>
   /// Inheritance: AstNode.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
   public class SourceFile : AstNode
   {
      #region Fields

      /// <summary>
      /// Stores the names of the included files. 
      /// </summary>
      private List<string> usings;

      /// <summary>
      /// Stores each namespace declaration of source file
      /// </summary>
      private Dictionary<string, List<Namespace>> namespaceDefinitions;

      /// <summary>
      /// Stores each declaration out of a namespace
      /// </summary>
      private List<Declaration> declarations;

      #endregion

      #region Properties


      /// <summary>
      /// Gets the included files.
      /// </summary>
       public List<string> Usings
      {
         get { return this.usings; }
      }

      /// <summary>
      /// Gets the keys of namespace definition.
      /// </summary>
      public Dictionary<string, List<Namespace>>.KeyCollection Namespacekeys
      {
         get { return this.namespaceDefinitions.Keys; }
      }

      /// <summary>
      /// Gets the number of declarations out of namespace definition.
      /// </summary>
      public int DeclarationCount
      {
         get { return this.declarations.Count; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of SourceFile
      /// </summary>
      /// <param name="f">Information of the file stores source code.</param>
      /// <param name="lineNumber">Line number.</param>
      /// <param name="columnNumber">Column number.</param>
      public SourceFile(Location loc)
         : base(loc)
      {
         this.usings = new List<string>();
         this.namespaceDefinitions = new Dictionary<string, List<Namespace>>();
         this.declarations = new List<Declaration>();
      }

      #endregion

      #region AddUsing()

      /// <summary>
      /// Add a new include file.
      /// </summary>
      /// <param name="include">string that represents a external file to include in the current source code.
      /// </param>
      public void AddUsing(string include)
      {
         this.usings.Add(include);
      }

      #endregion

      #region AddDeclaration()

      /// <summary>
      /// Add a new declaration.
      /// </summary>
      /// <param name="declaration">Declaration to add.</param>
      public void AddDeclaration(Declaration declaration)
      {
         this.declarations.Add(declaration);
      }

      #endregion

      #region AddNamespace()

      /// <summary>
      /// Add a new namespace definition.
      /// </summary>
      /// <param name="name">Namespace name.</param>
      /// <param name="declaration">Declaration.</param>
      public void AddNamespace(IdentifierExpression name, List<Declaration> declaration)
      {
         if (!namespaceDefinitions.ContainsKey(name.Identifier))
         {
            this.namespaceDefinitions.Add(name.Identifier, new List<Namespace>());
         }
         this.namespaceDefinitions[name.Identifier].Add(new Namespace(name, declaration, name.Location));
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
         return this.declarations[index];
      }

      #endregion

      #region GetNamespaceDefinitionCount

      /// <summary>
      /// Gets the number of declaration into the specified namespace.
      /// </summary>
      /// <param name="name">Namespace name.</param>
      public int GetNamespaceDefinitionCount(string name)
      {
         if (this.namespaceDefinitions.ContainsKey(name))
            return this.namespaceDefinitions[name].Count;
         return 0;
      }

      #endregion

      #region GetNamespaceDeclarationElement()

      /// <summary>
      /// Gets the element stored in the specified namespace and index.
      /// </summary>
      /// <param name="name">Namespace name.</param>
      /// <param name="index">Index.</param>
      /// <returns>Element stored in the specified namespace and index.</returns>
      public Namespace GetNamespaceDeclarationElement(string name, int index)
      {
         if (this.namespaceDefinitions.ContainsKey(name))
            return this.namespaceDefinitions[name][index];
         return null;
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

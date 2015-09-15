////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: ClassDefinition.cs                                                   //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a definition of a concrete class.                          //
//    Inheritance: IdDeclaration.                                             //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 13-12-2006                                                    //
// Modification date: 124-03-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using Tools;
using TypeSystem;
using ErrorManagement;

namespace AST {
    /// <summary>
    /// Encapsulates a definition of a concrete class.
    /// </summary>
    /// <remarks>
    /// Inheritance: IdDeclaration.
    /// Implements Composite pattern [Composite].
    /// Implements Visitor pattern [Concrete Element].
    /// </remarks>
    public class ClassDefinition : TypeDefinition {
        #region Constructor

        /// <summary>
        /// Constructor of ClassDefinition.
        /// </summary>
        /// <param name="id">Name of the class.</param>
        /// <param name="mods">List of modifier identifiers.</param>
        /// <param name="bases">List of base class identifiers.</param>
        /// <param name="fileName">File name.</param>
        /// <param name="lineNumber">Line number.</param>
        /// <param name="columnNumber">Column number.</param>
        public ClassDefinition(SingleIdentifierExpression id, List<Modifier> mods, List<string> bases, List<Declaration> decls, Location location)
            : base(id, mods, bases, decls, location) {
            bool constructorFound = false;

            if (mods.Contains(Modifier.Static)) // Static class needs static constructor
         {
                // Check default constructor.
                for (int i = 0; i < decls.Count; i++) {
                    if (decls[i] is MethodDeclaration) {
                        if ((((MethodDeclaration)decls[i]).Identifier.Equals(id.Identifier)) && (((MethodDeclaration)decls[i]).ModifiersInfo.Contains(Modifier.Static))) {
                            constructorFound = true;
                            break;
                        }
                    }
                }
            } else // Non static class needs non static constructor
         {
                // Check default constructor.
                for (int i = 0; i < decls.Count; i++) {
                    if (decls[i] is MethodDeclaration) {
                        if (((MethodDeclaration)decls[i]).Identifier.Equals(id.Identifier)) // && (!(((MethodDeclaration)decls[i]).ModifiersInfo.Contains(Modifier.Static))))
                  {
                            constructorFound = true;
                            break;
                        }
                    }
                }
            }

            if (!constructorFound) {
                List<Modifier> m = new List<Modifier>();
                if (mods.Contains(Modifier.Static))
                    m.Add(Modifier.Static);
                else
                    m.Add(Modifier.Public);
                this.members.Add(new ConstructorDefinition(id, m, new List<Parameter>(), null, new Block(location), location));
            }
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
    }
}

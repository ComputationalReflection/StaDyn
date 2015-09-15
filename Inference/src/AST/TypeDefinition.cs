////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: TypeDefinition.cs                                                    //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a definition of a concrete class or interface.             //
//    Inheritance: IdDeclaration.                                             //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 18-01-2007                                                    //
// Modification date: 02-03-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using Tools;
using TypeSystem;
using ErrorManagement;

namespace AST {
    /// <summary>
    /// Encapsulates a definition of a concrete class or interface.
    /// </summary>
    /// <remarks>
    /// Inheritance: IdDeclaration.
    /// Implements Composite pattern [Composite].
    /// Implements Visitor pattern [Concrete Element].
    /// </remarks>
    public class TypeDefinition : IdDeclaration {
        #region Fields

        /// <summary>
        /// Stores the members of the class.
        /// </summary>
        protected List<Declaration> members;

        /// <summary>
        /// List of modifiers
        /// </summary>
        private List<Modifier> modifiers;

        /// <summary>
        /// List of base class
        /// </summary>
        private List<string> baseClass;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of members
        /// </summary>
        public int MemberCount {
            get { return this.members.Count; }
        }

        /// <summary>
        /// Gets the list of modifiers
        /// </summary>
        public List<Modifier> Modifiers {
            get { return this.modifiers; }
        }

        /// <summary>
        /// Gets the list of base class
        /// </summary>
        public List<string> BaseClass {
            get { return this.baseClass; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of TypeDefinition.
        /// </summary>
        /// <param name="id">Name of the type.</param>
        /// <param name="mods">List of modifier identifiers.</param>
        /// <param name="bases">List of base class identifiers.</param>
        /// <param name="fileName">File name.</param>
        /// <param name="lineNumber">Line number.</param>
        /// <param name="columnNumber">Column number.</param>
        protected TypeDefinition(SingleIdentifierExpression id, List<Modifier> mods, List<string> bases, List<Declaration> decls, Location location)
            : base(id, id.Identifier, location) {
            this.modifiers = mods;
            this.baseClass = bases;
            this.members = decls;
        }

        #endregion

        #region AddNewField()

        /// <summary>
        /// Adds a new field declaration.
        /// </summary>
        /// <param name="f">Field to add.</param>
        public void AddNewField(FieldDeclaration f) {
            this.members.Insert(0, f);
        }

        #endregion

        #region GetMemberElement()

        /// <summary>
        /// Gets the element stored in the specified index.
        /// </summary>
        /// <param name="index">Index.</param>
        /// <returns>Element stored in the specified index.</returns>
        public Declaration GetMemberElement(int index) {
            if (index >= 0 && index < this.members.Count)
                return this.members[index];
            else
                ErrorManager.Instance.NotifyError(new ArgumentOutOfRangeError(index, "TypeDefinition.GetMemberElement"));
            
            return null;
        }

        #endregion

        #region RemoveMemberElement()

        /// <summary>
        /// Deletes the element stored in the specified index.
        /// </summary>
        /// <param name="index">Index.</param>
        public void RemoveMemberElement(int index) {
            if (index >= 0 && index < this.members.Count)
                this.members.RemoveAt(index);
            else
                ErrorManager.Instance.NotifyError(new ArgumentOutOfRangeError(index, "TypeDefinition.RemoveMemberElement"));
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

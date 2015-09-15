////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: VisitorTypeLoad.cs                                                   //
// Authors: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                   //
//          Francisco Ortin - francisco.ortin@gmail.com                       //
// Description:                                                               //
//    This class visits the AST doing the following:                          //
//    - It stores the type of each defined class or interface in the          //
//          TypeTable and adds this type to the TypeExpr attribute.           //
//    - It decorates the AST with the attributes representing the IDs         //
//      of Classes, Interfaces, methods, fields and properties.               //
//    - FullName attribute. Is a string with the following format:            //
//          Namespace.Class  for classes and interfaces                       //
//          Namespace.Class.Member for fields, properties and methods         //
//  It does not decorate argument types, nor return types. These types        //
//       have been decorated by the parser as the TypeInfo attribute, and     //
//       each type has been added to the TypeTable (see the string            //
//       representation in the grammar (.g) file.                             //
//  Its second responsability is returning the set of using clauses           //
//       (including the one used by itself)                                   //
//    Inheritance: VisitorAdapter                                             //
//    Implements Visitor pattern [Concrete Visitor].                          //
// -------------------------------------------------------------------------- //
// Create date: 24-01-2007                                                    //
// Modification date: 03-04-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AST;
using ErrorManagement;
using Tools;
using TypeSystem;

namespace Semantic {
    /// <summary>
    /// This class visits the AST to store the type of each defined class or
    /// inteface.
    /// </summary>
    /// <remarks>
    /// Inheritance: VisitorAdapter.
    /// Implements Visitor pattern [Concrete Visitor].
    /// </remarks>
    class VisitorTypeLoad : VisitorAdapter {
        #region Fields

        /// <summary>
        /// Stores the name of the current namespace.
        /// </summary>
        private string currentNamespace;

        /// <summary>
        /// Stores the name of the current class.
        /// </summary>
        private string currentClass;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of VisitorTypeLoad
        /// </summary>
        public VisitorTypeLoad() {
            this.currentNamespace = "";
        }

        #endregion

        #region getIdentifier

        private string getClassIdentifier() {
            StringBuilder aux = new StringBuilder();
            if (this.currentNamespace.Length != 0) {
                aux.Append(this.currentNamespace);
                aux.Append(".");
            }
            aux.Append(this.currentClass);
            return aux.ToString();
        }

        private string getMemberIdentifier(IdDeclaration node) {
            StringBuilder aux = new StringBuilder();
            aux.Append(getClassIdentifier());
            aux.Append(".");
            aux.Append(node.Identifier);
            return aux.ToString();
        }

        #endregion


        #region Visit(Namespace node, Object obj)

        public override Object Visit(Namespace node, Object obj) {

            this.currentNamespace = node.Identifier.Identifier;

            for (int i = 0; i < node.NamespaceMembersCount; i++) {
                node.GetDeclarationElement(i).Accept(this, obj);
            }

            this.currentNamespace = "";

            return null;
        }

        #endregion

        #region Visit(ClassDefinition node, Object obj)

        public override Object Visit(ClassDefinition node, Object obj) {
            this.currentClass = node.Identifier;

            node.FullName = getClassIdentifier();
            ClassType type = new ClassType(node.Identifier, node.FullName, node.Modifiers);
            node.TypeExpr = type;
            TypeTable.Instance.AddType(node.FullName, type, new Location(this.currentFile, node.Location.Line, node.Location.Column));

            for (int i = 0; i < node.MemberCount; i++) {
                node.GetMemberElement(i).Accept(this, obj);
            }

            this.currentClass = "";
            return null;
        }

        #endregion

        #region Visit(InterfaceDefinition node, Object obj)

        public override Object Visit(InterfaceDefinition node, Object obj) {
            this.currentClass = node.Identifier;

            node.FullName = getClassIdentifier();
            InterfaceType type = new InterfaceType(node.Identifier, node.FullName, node.Modifiers);
            node.TypeExpr = type;
            TypeTable.Instance.AddType(node.FullName, type, new Location(this.currentFile, node.Location.Line, node.Location.Column));

            for (int i = 0; i < node.MemberCount; i++) {
                node.GetMemberElement(i).Accept(this, obj);
            }

            this.currentClass = "";
            return null;
        }

        #endregion

        #region Visit(FieldDeclaration node, Object obj)

        public override Object Visit(FieldDeclaration node, Object obj) {
            node.FullName = getMemberIdentifier(node);
            return null;
        }

        #endregion

        #region Visit(FieldDefinition node, Object obj)

        public override Object Visit(FieldDefinition node, Object obj) {
            node.FullName = getMemberIdentifier(node);
            return null;
        }

        #endregion

        #region Visit(ConstantFieldDefinition node, Object obj)

        public override Object Visit(ConstantFieldDefinition node, Object obj) {
            node.FullName = getMemberIdentifier(node);
            return null;
        }

        #endregion

        #region Visit(ConstructorDefinition node, Object obj)

        public override Object Visit(ConstructorDefinition node, Object obj) {
            node.FullName = getClassIdentifier();
            node.SetReturnType = getClassIdentifier();
            return null;
        }

        #endregion

        #region Visit(MethodDeclaration node, Object obj)

        public override Object Visit(MethodDeclaration node, Object obj) {
            node.FullName = getMemberIdentifier(node);
            return null;
        }

        #endregion

        #region Visit(MethodDefinition node, Object obj)

        public override Object Visit(MethodDefinition node, Object obj) {
            node.FullName = getMemberIdentifier(node);
            return null;
        }
        #endregion


    }
}

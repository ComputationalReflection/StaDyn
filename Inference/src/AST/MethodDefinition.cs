////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: MethodDefinition.cs                                                  //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a definition of a concrete method.                         //
//    Inheritance: MethodDeclaration.                                         //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 07-12-2006                                                    //
// Modification date: 31-05-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using Compiler;
using Tools;
using TypeSystem;
using ErrorManagement;

namespace AST {
    /// <summary>
    /// Encapsulates a definition of a concrete method.
    /// </summary>
    /// <remarks>
    /// Inheritance: MethodDeclaration.
    /// Implements Composite pattern [Composite].
    /// Implements Visitor pattern [Concrete Element].
    /// </remarks>
    public class MethodDefinition : MethodDeclaration {
        #region Fields

        /// <summary>
        /// Represents the statements of the method. 
        /// </summary>
        private Block body;

        /// <summary>
        /// True if the method is the entry point. Otherwise, false.
        /// </summary>
        private bool isEntryPoint;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the body of the method
        /// </summary>
        public Block Body {
            get { return this.body; }
        }

        /// <summary>
        /// True if the method definition is a entry point. Otherwise, false.
        /// </summary>
        public bool IsEntryPoint {
            get { return this.isEntryPoint; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of MethodDefinition.
        /// </summary>
        /// <param name="id">Name of the definition.</param>
        /// <param name="stats">Body associated to the method definition.</param>
        /// <param name="returnType">Name of the return type.</param>
        /// <param name="parameters">Parameters information.</param>
        /// <param name="modifiers">Modifiers information.</param>
        /// <param name="fileName">File name.</param>
        /// <param name="lineNumber">Line number.</param>
        /// <param name="columnNumber">Column number.</param>
        public MethodDefinition(SingleIdentifierExpression id, Block stats, string returnType, List<Parameter> parameters, List<Modifier> modifiers, Location location)
            : base(id, returnType, parameters, modifiers, location) {

            this.body = stats;
            if (modifiers.Contains(Modifier.Static) && id.Identifier.Equals("Main")) {
                Program.SetEntryPointFound(this.Location);
                this.isEntryPoint = true;
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

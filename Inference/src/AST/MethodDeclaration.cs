////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: MethodDeclaration.cs                                                 //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a declaration of a concrete method.                        //
//    Inheritance: IdDeclaration.                                             //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 14-01-2007                                                    //
// Modification date: 22-05-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using Tools;
using TypeSystem;
using ErrorManagement;

namespace AST {
    /// <summary>
    /// Encapsulates a declaration of a concrete method.
    /// </summary>
    /// <remarks>
    /// Inheritance: IdDeclaration.
    /// Implements Composite pattern [Composite].
    /// Implements Visitor pattern [Concrete Element].
    /// </remarks>
    public class MethodDeclaration : IdDeclaration {
        #region Fields

        /// <summary>
        /// Parameters information used to obtain its type
        /// </summary>
        private List<Parameter> parametersInfo;

        /// <summary>
        /// Modifiers information used to obtain its type
        /// </summary>
        private List<Modifier> modifiersInfo;

        /// <summary>
        /// Information of the return type
        /// </summary>
        private string returnTypeInfo;

        /// <summary>
        /// True if the return expression is a dynamic reference. Otherwise false.
        /// </summary>
        private bool isReturnDynamic;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the modifiers information used to obtain its type
        /// </summary>
        public List<Modifier> ModifiersInfo {
            get { return this.modifiersInfo; }
        }

        /// <summary>
        /// Gets the parameters information used to obtain its type
        /// </summary>
        public List<Parameter> ParametersInfo {
            get { return this.parametersInfo; }
        }

        /// <summary>
        /// Gets the information of the return type
        /// </summary>
        public string ReturnTypeInfo {
            get { return this.returnTypeInfo; }
            protected set { this.returnTypeInfo = value; }
        }

        /// <summary>
        /// Gets or sets true if the return expression is a dynamic reference. Otherwise false.
        /// </summary>
        public bool IsReturnDynamic {
            get { return this.isReturnDynamic; }
            set { this.isReturnDynamic = value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of MethodDeclaration.
        /// </summary>
        /// <param name="id">Name of the definition.</param>
        /// <param name="parameters">Parameters information.</param>
        /// <param name="modifiers">Modifiers information.</param>
        /// <param name="fileName">File name.</param>
        /// <param name="lineNumber">Line number.</param>
        /// <param name="columnNumber">Column number.</param>
        public MethodDeclaration(SingleIdentifierExpression id, string returnType, List<Parameter> parameters, List<Modifier> modifiers, Location location)
            : base(id, null, location) {
            this.parametersInfo = parameters;
            if (parameters == null)
                this.parametersInfo = new List<Parameter>();
            this.modifiersInfo = modifiers;
            this.returnTypeInfo = returnType;
        }

        #endregion

        #region SearchParam()

        /// <summary>
        /// Searches the specified param in the method parameter list.
        /// </summary>
        /// <param name="param">Parameter identifier to search.</param>
        /// <returns>True if the parameter identifier exists. Otherwise, false.</returns>
        public bool SearchParam(string param) {
            for (int i = 0; i < this.parametersInfo.Count; i++) {
                if (this.parametersInfo[i].Identifier.Equals(param))
                    return true;
            }
            return false;
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

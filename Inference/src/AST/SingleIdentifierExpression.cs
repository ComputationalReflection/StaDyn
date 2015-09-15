////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: SingleIdentifierExpression.cs                                        //
// Authors: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                   //
//          Francisco Ortin - francisco.ortin@gmail.com                       //
// Description:                                                               //
//    Encapsulates a identifier expression of our programming language.       //
//    Inheritance: Expression.                                                //
//    Implements Composite pattern [Leaf].                                    //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 05-12-2006                                                    //
// Modification date: 06-04-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using Symbols;
using Tools;
using TypeSystem;
using ErrorManagement;
using CodeGeneration;
using TypeSystem.Operations;
using AST.Operations;

namespace AST {
    #region enum IdentifierMode

    /// <summary>
    /// Represents how to use the identifier
    /// </summary>
    public enum IdentifierMode {
        Instance,
        UserType,
        NameSpace
    }

    #endregion

    /// <summary>
    /// Encapsulates a identifier expression of our programming language.
    /// </summary>
    /// <remarks>
    /// Inheritance: Expression.
    /// Implements Composite pattern [Leaf].
    /// Implements Visitor pattern [Concrete Element].
    /// </remarks>
    public class SingleIdentifierExpression : IdentifierExpression {
        #region Field

        /// <summary>
        /// Represents how to use the identifier
        /// </summary>
        private IdentifierMode idMode;

        /// <summary>
        /// Represents the symbol of the identifier.
        /// </summary>
        private Symbol symbol;

        /// <summary>
        /// Number associated with SSA algorithm.
        /// </summary>
        private int indexOfSSA;

        /// <summary>
        /// Represents the il identifier.
        /// </summary>
        private string ilName = "";

        private ILReservedWords ilReservedWords;
        #endregion

        #region Properties

        /// <summary>
        /// Gets the mode to use de identifier.
        /// </summary>
        public IdentifierMode IdMode {
            set { this.idMode = value; }
            get { return this.idMode; }
        }

        /// <summary>
        /// Gets or sets the symbol of the identifier.
        /// </summary>
        public Symbol IdSymbol {
            get { return this.symbol; }
            set { this.symbol = value; }
        }

        /// <summary>
        /// Gets or sets the index associated with SSA algorithm
        /// </summary>
        public int IndexOfSSA {
            get { return this.indexOfSSA; }
            set { this.indexOfSSA = value; }
        }

        /// <summary>
        /// WriteType variable may change its type's substitution (e.g., field type variables)
        /// This attribute saves the type in an specific time (frozen).
        /// If this type's substitution changes, the frozen type does not.
        /// </summary>
        public TypeExpression FrozenTypeExpression {
            get { return this.frozenTypeExpression; }
            set { this.frozenTypeExpression = value; }
        }

        #region ILName

        /// <summary>
        /// Gets the IL name associated to the declaration identifier.
        /// </summary>
        public string ILName {

            get {
                // the ilName field is alredy defined, so it is returned
                if (!this.ilName.Equals(""))
                    return this.ilName;
                // ilName is blank
                // if the node is provided by SSA algorithm, the proper subscript is added as a number 
                // to the parameter, and assigned to this.ilName
                if (this.indexOfSSA != -1)
                    SSARename();
                else // assign this.Identifier to ilName, if it's a keyworkd of IL Language the returned string is sourronded by a pair of '
                    this.ilName = this.ilReservedWords.MakeILComplaint(this.Identifier);

                return this.ilName;
            }
        }
        /// <summary>
        /// Renames the ILName to an identifier subscripted by its index of SSA Algorithm.
        /// </summary>
        private void SSARename() {
            StringBuilder aux = new StringBuilder();
            aux.AppendFormat("{0}__{1}", this.Identifier, this.indexOfSSA);
            this.ilName = aux.ToString();
        }

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of SingleIdentifierExpression
        /// </summary>
        /// <param name="idenfifier">Name name.</param>
        /// <param name="fileName">File name.</param>
        /// <param name="lineNumber">Line number.</param>
        /// <param name="columnNumber">Column number.</param>
        public SingleIdentifierExpression(string idenfifier, Location location)
            : base(location) {

            this.Identifier = idenfifier;
            this.idMode = IdentifierMode.Instance;
            this.indexOfSSA = -1;
            this.ilReservedWords = new ILReservedWords();
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

        #region Dispatcher AstOperation
        /// <summary>
        /// Dispatches expressions to the operation passed as argument.
        /// It provokes the execution of op.AcceptOperation(AstNode) with the parameter
        /// resolved polymorfically
        /// </summary>
        /// <param name="op">AstOperation to dispatch</param>
        /// <returns></returns>
        public override object AcceptOperation(AstOperation op, object arg) {
            return op.Exec(this, arg);
        }

        #endregion

        #region Equals&GetHashCode
        public override bool Equals(object obj) {
            if (this.GetHashCode() != obj.GetHashCode())
                return false;
            SingleIdentifierExpression singleId = obj as SingleIdentifierExpression;
            if (singleId == null)
                return false;
            return this.Identifier.Equals(singleId.Identifier) && this.indexOfSSA == singleId.indexOfSSA;
        }
        public override int GetHashCode() {
            return this.Identifier.GetHashCode() * this.indexOfSSA;
        }
        #endregion
    }
}

////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: AstNode.cs                                                           //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Abstract class for all nodes that compounds the abstract syntax tree.   //
//    Implements Composite pattern [Component].                               //
//    Implements Visitor pattern [Element].                                   //
// -------------------------------------------------------------------------- //
// Create date: 04-12-2006                                                    //
// Modification date: 04-12-2006                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using ErrorManagement;

using Tools;
using AST.Operations;


namespace AST {
    /// <summary>
    /// Abstract class for all nodes that compounds the abstract syntax tree.
    /// </summary>
    /// <remarks>
    /// Implements Composite pattern [Component].
    /// Implements Visitor pattern [Element].
    /// </remarks>
    public abstract class AstNode : antlr.CommonAST {
        #region Fields
        /// <summary>
        ///Location: Encapsulates in one object the line, column and filename
        // the ocurrence of an intem in the text program
		/// </summary>
        protected Location location;
        
        #endregion

        #region Properties

        public Location Location {
            get { return this.location; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Protected constructor of NodeAst
        /// </summary>
        /// <param name="location"></param>
        /// localization (file, line, and column) of the node in the text program
        /// 
        protected AstNode(Location location) {
            this.location = location;
        }


        #endregion

        #region Accept()

        /// <summary>
        /// Accept method of a concrete visitor.
        /// </summary>
        /// <param name="v">Concrete visitor</param>
        /// <param name="o">Optional information to use in the visit.</param>
        /// <returns>Optional information to return</returns>
        public abstract Object Accept(Visitor v, Object o);

        #endregion

        #region EqualsAndGetHashCode
        /// <summary>
        /// AntLR compares Nodes with the class name. This is not correct for our purposes.
        /// </summary>
        /// <param name="obj">The other ast node to compare</param>
        /// <returns>Whether they are the same or not</returns>
        public override bool Equals(object obj) {
            AstNode node = obj as AstNode;
            if (obj == null)
                return false;
            return this.Location.Equals(node.Location);
        }
        public override int GetHashCode() {
            return Location.GetHashCode();
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
        public virtual object AcceptOperation(AstOperation op, object arg) {
            return op.Exec(this, arg);
        }

        #endregion

    }
}

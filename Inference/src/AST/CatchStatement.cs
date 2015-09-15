////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: CatchStatement.cs                                                    //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a Catch statement of our programming languages.            //
//    Inheritance: Statement.                                                 //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 05-12-2006                                                    //
// Modification date: 13-12-2006                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using Tools;
using ErrorManagement;

namespace AST {
    /// <summary>
    /// Encapsulates a Catch statement of our programming languages.
    /// </summary>
    /// <remarks>
    /// Inheritance: Statement.
    /// Implements Composite pattern [Composite].
    /// Implements Visitor pattern [Concrete Element].
    /// </remarks>
    public class CatchStatement : Statement {
        #region Fields

        /// <summary>
        /// Stores the exception to catch.
        /// </summary>
        private IdDeclaration exception;

        /// <summary>
        /// Represents the block to execute.
        /// </summary>
        private Block statements;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the exception to catch
        /// </summary>
        public IdDeclaration Exception {
            get { return exception; }
        }

        /// <summary>
        /// Gets the block executed when the exception is caught.
        /// </summary>
        public Block Statements {
            get { return statements; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of CatchStatement
        /// </summary>
        /// <param name="param">Exception to catch.</param>
        /// <param name="stats">Block to execute when the exception is caught.</param>
        /// <param name="fileName">File name.</param>
        /// <param name="lineNumber">Line number.</param>
        /// <param name="columnNumber">Column number.</param>
        public CatchStatement(IdDeclaration param, Block stats, Location location)
            : base(location) {
            this.exception = param;
            this.statements = stats;
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

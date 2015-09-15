////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: IfElseStatement.cs                                                   //
// Authors: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                   //
//         Francisco Ortin - francisco.ortin@gmail.com                        //
// Description:                                                               //
//    Encapsulates a If-Else statement of our programming language.           //
//    Inheritance: Statement.                                                 //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 04-12-2006                                                    //
// Modification date: 07-06-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using Tools;
using TypeSystem;
using ErrorManagement;

namespace AST {
    /// <summary>
    /// Encapsulates a If-Else statement of our programming language.
    /// </summary>
    /// <remarks>
    /// Inheritance: Statement.
    /// Implements Composite pattern [Composite].
    /// Implements Visitor pattern [Concrete Element].
    /// </remarks>
    public class IfElseStatement : Statement {
        #region Fields

        /// <summary>
        /// Represents a logic or compare expression to If-Else statement.
        /// </summary>
        private Expression condition;

        /// <summary>
        /// Represents the statements after condition.
        /// </summary>
        private List<MoveStatement> afterCondition;

        /// <summary>
        /// Represents a block of statements to the true branch of If-Else statement.
        /// </summary>
        private Statement trueBranch;

        /// <summary>
        /// If exists the else block, represents its block of statement.
        /// </summary>
        private Statement falseBranch;

        /// <summary>
        /// Represents a new block of ThetaStatement at the end of if-else statement.
        /// </summary>
        private List<ThetaStatement> thetaStats;

        /// <summary>
        /// The set of references that are used in the if body.
        /// Used for SSA purposes.
        /// </summary>
        private IList<SingleIdentifierExpression> referencesUsedInTrueBranch = new List<SingleIdentifierExpression>();

        /// <summary>
        /// The set of references that are used in the else body.
        /// Used for SSA purposes.
        /// </summary>
        private IList<SingleIdentifierExpression> referencesUsedInFalseBranch = new List<SingleIdentifierExpression>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the condition expression of If-Else statement.
        /// </summary>
        public Expression Condition {
            get { return condition; }
            set { this.condition = value; }
        }

        /// <summary>
        /// Gets or sets the statements after condition.
        /// </summary>
        public List<MoveStatement> AfterCondition {
            get { return this.afterCondition; }
            set { this.afterCondition = value; }
        }

        /// <summary>
        /// Gets the block executed when the condition is true.
        /// </summary>
        public Statement TrueBranch {
            get { return trueBranch; }
        }

        /// <summary>
        /// Gets the block executed when the condition is false.
        /// </summary>
        public Statement FalseBranch {
            get { return falseBranch; }
        }

        /// <summary>
        /// Gets or sets the theta funcion statements 
        /// </summary>
        public List<ThetaStatement> ThetaStatements {
            get { return this.thetaStats; }
            set { this.thetaStats = value; }
        }

        /// <summary>
        /// The set of references that are used in the if body.
        /// Used for SSA purposes.
        /// </summary>
        public IList<SingleIdentifierExpression> ReferencesUsedInTrueBranch {
            get { return this.referencesUsedInTrueBranch; }
        }

        /// <summary>
        /// The set of references that are used in the else body.
        /// Used for SSA purposes.
        /// </summary>
        public IList<SingleIdentifierExpression> ReferencesUsedInFalseBranch {
            get { return this.referencesUsedInFalseBranch; }
        }
        #endregion

        #region Constructors

        /// <summary>
        /// Constructor of IfElseStatement
        /// </summary>
        /// <param name="exp">Condition of the statement.</param>
        /// <param name="trueBranch">Block executed when the condition is true.</param>
        /// <param name="fileName">File name.</param>
        /// <param name="lineNumber">Line number.</param>
        /// <param name="columnNumber">Column number.</param>
        public IfElseStatement(Expression exp, Statement trueBranch, Location location): base(location) {
            this.condition = exp;
            this.afterCondition = new List<MoveStatement>();
            this.trueBranch = trueBranch;
            this.falseBranch = new Block(location); //null?
            this.thetaStats = new List<ThetaStatement>();
        }

        /// <summary>
        /// Constructor of IfElseStatement
        /// </summary>
        /// <param name="exp">Condition of the statement.</param>
        /// <param name="trueBranch">Block executed when the condition is true.</param>
        /// <param name="falseBranch">Block executed when the condition is false.</param>
        /// <param name="fileName">File name.</param>
        /// <param name="lineNumber">Line number.</param>
        /// <param name="columnNumber">Column number.</param>
        public IfElseStatement(Expression exp, Statement trueBranch, Statement falseBranch, Location location)
            : base(location) {
            this.condition = exp;
            this.afterCondition = new List<MoveStatement>();
            this.trueBranch = trueBranch;
            this.falseBranch = falseBranch;
            this.thetaStats = new List<ThetaStatement>();
        }

        #endregion

        #region HaveElseBlock

        /// <summary>
        /// Returns true if the statement has a else block. Otherwise, false.
        /// </summary>
        /// <returns>Returns true if the statement has a else block. Otherwise, false.</returns>
        public bool HaveElseBlock() {
            if ((this.falseBranch is Block) && (((Block)this.falseBranch).StatementCount == 0))
                return false;
            return true;
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

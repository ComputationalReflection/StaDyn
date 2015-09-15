////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: BaseCallExpression.cs                                                //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Encapsulates a invocation expression to base class.                     //
//    Inheritance: Expression.                                                //
//    Implements Composite pattern [Composite].                               //
//    Implements Visitor pattern [Concrete Element].                          //
// -------------------------------------------------------------------------- //
// Create date: 07-12-2006                                                    //
// Modification date: 27-12-2006                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using Tools;
using TypeSystem;
using ErrorManagement;
using TypeSystem.Operations;
using AST.Operations;

namespace AST
{
   /// <summary>
   /// Encapsulates a invocation expression to base class.
   /// </summary>
   /// <remarks>
   /// Inheritance: Expression.
   /// Implements Composite pattern [Composite].
   /// Implements Visitor pattern [Concrete Element].
   /// </remarks>
    public class BaseCallExpression : Expression {
        #region Fields

        /// <summary>
        /// Represents the arguments of the invocation.
        /// </summary>
        private CompoundExpression arguments;

        /// <summary>
        /// The type of the base reference
        /// </summary>
        private TypeExpression baseType;

        /// <summary>
        /// The actual method called once overload has been solved.
        /// </summary>
        private TypeExpression actualMethodCalled;
        #endregion

        #region Properties

        /// <summary>
        /// Gets the arguments to invoke in the expression
        /// </summary>
        public CompoundExpression Arguments {
            get { return this.arguments; }
        }

        /// <summary>
        /// The type of the base reference
        /// </summary>
        public TypeExpression BaseType {
            get { return baseType; }
            set { baseType = value; }
        }

        /// <summary>
        /// The actual method called once overload has been solved.
        /// </summary>
        public TypeExpression ActualMethodCalled {
            get { return this.actualMethodCalled; }
            set { this.actualMethodCalled = value; }
        }
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of BaseCallExpression
        /// </summary>
        /// <param name="arguments">Arguments of the invocation.</param>
        /// <param name="fileName">File name.</param>
        /// <param name="lineNumber">Line number.</param>
        /// <param name="columnNumber">Column number.</param>
        public BaseCallExpression(CompoundExpression arguments, Location location)
            : base(location) {
            if (arguments != null)
                this.arguments = arguments;
            else
                this.arguments = new CompoundExpression(location);
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
    }

}

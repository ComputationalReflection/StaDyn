//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: NoTypeAcceptsOperation.cs                                                
// Author: Francisco Ortin  -  francisco.ortin@gmail.com                    
// Description:                                                               
//   Represents a error produced when a dynamic union type has no valid
//   type to be applied an operation 
// -------------------------------------------------------------------------- 
// Create date: 18-05-2007                                                    
// Modification date: 18-05-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorManagement {
    /// <summary>
    /// Represents a error produced when a dynamic union type has no valid type to be applied an operation 
    /// </summary>
    public class NoTypeAcceptsOperation : ErrorAdapter {
        #region Constructor

        /// <summary>
        /// Constructor of NoTypeAcceptsOperation
        /// </summary>
        /// <param name="type">The type full name.</param>
        /// <param name="op">The operator</param>
        /// <param name="fileName">File name.</param>
        /// <param name="line">Line number.</param>
        /// <param name="column">Column number.</param>
        public NoTypeAcceptsOperation(string type, string op, Location loc) : base(loc) {
            StringBuilder aux = new StringBuilder();
            aux.AppendFormat("The dynamic type '{0}' has no valid type type to apply the '{1}' operation.", type, op);
            this.Description = aux.ToString();
        }

        /// <summary>
        /// Constructor of NoTypeAcceptsOperation
        /// </summary>
        /// <param name="typeOp1">The type full name.</param>
        /// <param name="op">The operator</param>
        /// <param name="typeOp2">The full name of the second operand's type.</param>
        /// <param name="fileName">File name.</param>
        /// <param name="line">Line number.</param>
        /// <param name="column">Column number.</param>
        public NoTypeAcceptsOperation(string typeOp1, string op, string typeOp2, Location loc)
            : base(loc) {
            StringBuilder aux = new StringBuilder();
            aux.AppendFormat("The dynamic type '{0}' has no valid type type to apply the '{1}' operation with a '{2}' operand.", typeOp1, op, typeOp2);
            this.Description = aux.ToString();
        }
        #endregion
    }
}
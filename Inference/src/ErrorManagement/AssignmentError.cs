//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: AssignmentError.cs                                                
// Author: Francisco Ortin  -  francisco.ortin@gmail.com                    
// Description:                                                               
//   Represents a error produced when an assignment is not possible.
// -------------------------------------------------------------------------- 
// Create date: 23-04-2007                                                    
// Modification date: 23-04-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorManagement {
    /// <summary>
    /// Represents a error produced when the attribute identifier is not defined.
    /// </summary>
    public class AssignmentError : ErrorAdapter {

        #region Constructor

        /// <summary>
        /// Constructor of UnknownMemberError
        /// </summary>
        /// <param name="leftType">WriteType of the left part of the assignment.</param>
        /// <param name="rightType">WriteType of the right part of the assignment.</param>
        /// <param name="fileName">File name.</param>
        /// <param name="line">Line number.</param>
        /// <param name="column">Column number.</param>
        public AssignmentError(string rightType, string leftType, Location loc)
            : base(loc) {
            StringBuilder aux = new StringBuilder();
            aux.AppendFormat("Cannot assign an expression of type '{0}' to the '{1}' type.", rightType, leftType);
            this.Description = aux.ToString();
        }

        #endregion
    }
}
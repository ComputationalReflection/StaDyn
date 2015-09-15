//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: UnificationError.cs                                                
// Author: Francisco Ortin  -  francisco.ortin@gmail.com                    
// Description:                                                               
//   Represents a error produced when a method cannot be unified.
// -------------------------------------------------------------------------- 
// Create date: 18-04-2007                                                    
// Modification date: 18-04-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorManagement {
    /// <summary>
    /// Represents a error produced when the attribute identifier is not defined.
    /// </summary>
    public class UnificationError : ErrorAdapter {

        #region Constructor

        /// <summary>
        /// Constructor of UnknownMemberError
        /// </summary>
        /// <param name="memberId">Member identifier.</param>
        /// <param name="fileName">File name.</param>
        /// <param name="line">Line number.</param>
        /// <param name="column">Column number.</param>
        public UnificationError(string methodName, Location loc) : base(loc) {
            StringBuilder aux = new StringBuilder();
            aux.AppendFormat("Could not unify the invocation to the method '{0}'.", methodName);
            this.Description = aux.ToString();
        }
        #endregion

    }
}
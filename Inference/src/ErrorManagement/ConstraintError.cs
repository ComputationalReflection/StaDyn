//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: ConstraintError.cs                                                
// Author: Francisco Ortin  -  francisco.ortin@gmail.com                    
// Description:                                                               
//   Represents a error produced when a constraint has not been satisfied.
// -------------------------------------------------------------------------- 
// Create date: 16-04-2007                                                    
// Modification date: 16-04-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorManagement {
    /// <summary>
    /// Represents a error produced when a constraint has not been satisfied
    /// </summary>
    public class ConstraintError : ErrorAdapter {
        #region Constructor

        /// <summary>
        /// Constructor of UnknownMemberError
        /// </summary>
        /// <param name="memberId">Member identifier.</param>
        /// <param name="fileName">File name.</param>
        /// <param name="line">Line number.</param>
        /// <param name="column">Column number.</param>
        public ConstraintError(Location loc)
            : base(loc) {
            StringBuilder aux = new StringBuilder();
            aux.AppendFormat("The error above has been produced by this method call.");
            this.Description = aux.ToString();
        }

        #endregion
    }
}
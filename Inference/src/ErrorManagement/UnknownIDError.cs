//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: UnknownIDError.cs                                                
// Author: Francisco Ortin  -  francisco.ortin@gmail.com                    
// Description:                                                               
//   Represents a error produced when an identifier has not been solved.
// -------------------------------------------------------------------------- 
// Create date: 16-04-2007                                                    
// Modification date: 16-04-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorManagement {
    /// <summary>
    /// Represents a error produced when the attribute identifier is not defined.
    /// </summary>
    public class UnknownIDError : ErrorAdapter {

        #region Constructor

        /// <summary>
        /// Constructor of UnknownMemberError
        /// </summary>
        /// <param name="memberId">Member identifier.</param>
        /// <param name="fileName">File name.</param>
        /// <param name="line">Line number.</param>
        /// <param name="column">Column number.</param>
        public UnknownIDError(string id, Location loc) : base(loc) {
            StringBuilder aux = new StringBuilder();
            aux.AppendFormat("'{0}': unknown identifier.", id);
            this.Description = aux.ToString();
        }

        #endregion

    }
}
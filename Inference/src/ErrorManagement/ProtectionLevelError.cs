//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: ProtectionLevelError.cs                                                
// Author: Francisco Ortin francisco.ortin@gmail.com
// Description:                                                               
//   Represents a error produced when a member cannot be access due to its
//      protection level.   
// -------------------------------------------------------------------------- 
// Create date: 25-06-2007                                                    
// Modification date: 25-06-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorManagement {
    /// <summary>
    /// Represents a error produced when a member cannot be access due to its protection level.
    /// </summary>
    public class ProtectionLevelError : ErrorAdapter {

        #region Constructor
        /// <summary>
        /// Constructor of ProtectionLevelError
        /// </summary>
        /// <param name="memberId">Member identifier.</param>
        /// <param name="fileName">File name.</param>
        /// <param name="line">Line number.</param>
        /// <param name="column">Column number.</param>
        public ProtectionLevelError(string memberId, Location loc)
            : base(loc) {
            StringBuilder aux = new StringBuilder();
            aux.AppendFormat("The member '{0}' cannot be accessed due to its protection level.", memberId);
            this.Description = aux.ToString();
        }
        #endregion

    }

}
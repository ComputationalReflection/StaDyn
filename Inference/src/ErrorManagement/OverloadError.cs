//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: OverloadError.cs                                                      
// Author: Francisco Ortin francisco.ortin@gmail.com                          
// Description:                                                               
//   Represents a error produced when the same implementation of the
//   method already exists.
// -------------------------------------------------------------------------- 
// Create date: 06-04-2007                                                    
// Modification date: 06-04-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorManagement {
    /// <summary>
    /// Represents a error produced when the ternary expression 
    /// can not be applied to specified expressions
    /// </summary>
    public class OverloadError: ErrorAdapter {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="methodName">The name of the method</param>
        /// <param name="fileName">File name.</param>
        /// <param name="line">Line number.</param>
        /// <param name="column">Column number.</param>
        public OverloadError(string methodName, Location loc)
            : base(loc) {
            StringBuilder aux = new StringBuilder();
            aux.AppendFormat("The method '{0}' already implements this signature.", methodName);
            this.Description = aux.ToString();
        }

        #endregion
    }
}
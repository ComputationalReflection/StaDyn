//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: ReturnExpectedError.cs                                                       
// Author: Francisco Ortin - francisco.ortin@gmail.com                       
// Description:                                                               
//   Represents a error produced when a return statement is expected
//      but not used.
// --------------------------------------------------------------------------
// Create date: 14-04-2007                                                    
// Modification date: 14-04-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorManagement {
    /// <summary>
    /// Represents a error produced when the expression is not a lvalue.
    /// </summary>
    public class ReturnExpectedError : ErrorAdapter {

        #region Constructor
        /// <summary>
        /// Constructor of LValueError
        /// </summary>
        /// <param name="fileName">File name.</param>
        /// <param name="line">Line number.</param>
        /// <param name="column">Column number.</param>
        public ReturnExpectedError(Location loc) : base(loc) {
            StringBuilder aux = new StringBuilder();
            aux.AppendFormat("A return type has been declared, but no return statement has been used.");
            this.Description = aux.ToString();
        }
        #endregion

    }
}
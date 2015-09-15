//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: ExpressionExpectedError.cs                                                       
// Author: Francisco Ortin - francisco.ortin@gmail.com                       
// Description:                                                               
//   Represents a error produced when a return statement requires an
//      expression but it is not used.
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
    public class ExpressionExpectedError : ErrorAdapter {
        #region Constructor

        /// <summary>
        /// Constructor of LValueError
        /// </summary>
        /// <param name="fileName">File name.</param>
        /// <param name="line">Line number.</param>
        /// <param name="column">Column number.</param>
        public ExpressionExpectedError(Location loc)
            : base(loc) {
            StringBuilder aux = new StringBuilder();
            aux.AppendFormat("The return statement of this method requires an expression.");
            this.Description = aux.ToString();
        }

        #endregion
    }
}
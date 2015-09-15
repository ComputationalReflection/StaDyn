//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: TernaryError.cs                                                      
// Author: Francisco Ortin francisco.ortin@gmail.com                          
// Description:                                                               
//   Represents a error produced when the ternary expression 
//       can not be applied to specified expressions.
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
    public class TernaryError : ErrorAdapter {

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="secondArgumentType">The type of the second operand in the ternary expression</param>
        /// <param name="thirdArgumentType">The type of the third operand in the ternary expression</param>
        /// <param name="fileName">File name.</param>
        /// <param name="line">Line number.</param>
        /// <param name="column">Column number.</param>
        public TernaryError(string secondArgumentType, string thirdArgumentType, Location loc) : base(loc) {
            StringBuilder aux = new StringBuilder();
            aux.AppendFormat("Types '{0}' and '{1}' do cannot convert one to the other.", secondArgumentType, thirdArgumentType );
            this.Description = aux.ToString();
        }
        #endregion

    }
}
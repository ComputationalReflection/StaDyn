//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: ThisWordFromStaticMethodError.cs                                                      
// Author: Francisco Ortin francisco.ortin@gmail.com                          
// Description:                                                               
//   Represents a error produced when the this reserved word is used
//       in a static method.
// -------------------------------------------------------------------------- 
// Create date: 28-05-2007                                                    
// Modification date: 28-05-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorManagement {
    /// <summary>
    /// Represents a error produced when the this reserved word is used in a static method
    /// </summary>
    public class ThisWordFromStaticMethodError : ErrorAdapter {

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="staticMethod">The name of the static method</param>
        /// <param name="fileName">File name.</param>
        /// <param name="line">Line number.</param>
        /// <param name="column">Column number.</param>
        public ThisWordFromStaticMethodError(string staticMethod, Location loc) : base(loc) {
            StringBuilder aux = new StringBuilder();
            aux.AppendFormat("The this word cannot be used in the '{0}' static method. " , staticMethod);
            this.Description = aux.ToString();
        }
        #endregion

    }
}    


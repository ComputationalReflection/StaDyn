//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: InstanceMethodCallFromStaticMethodError.cs                                                      
// Author: Francisco Ortin francisco.ortin@gmail.com                          
// Description:                                                               
//   Represents a error produced when an instance method is called from
//       a static one, whithout using an object.
// -------------------------------------------------------------------------- 
// Create date: 25-04-2007                                                    
// Modification date: 25-04-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorManagement {
    /// <summary>
    /// Represents a error produced when an instance method is called from
    /// a static one, whithout using an object.
    /// </summary>
    public class InstanceMethodCallFromStaticMethodError : ErrorAdapter {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="secondArgumentType">The type of the second operand in the ternary expression</param>
        /// <param name="thirdArgumentType">The type of the third operand in the ternary expression</param>
        /// <param name="fileName">File name.</param>
        /// <param name="line">Line number.</param>
        /// <param name="column">Column number.</param>
        public InstanceMethodCallFromStaticMethodError(string instanceMethod, string staticMethod, Location location)
            : base(location) {
            StringBuilder aux = new StringBuilder();
            aux.AppendFormat("The instance method '{0}' has been called from the static method '{1}', whithout using an object. ",
                            instanceMethod, staticMethod);
            this.Description = aux.ToString();
        }

        #endregion
    }
}    


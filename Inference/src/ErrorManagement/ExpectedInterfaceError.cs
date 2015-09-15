//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: ExpectedInterfaceError.cs                                            
// Author: Francisco Ortin -  francisco.ortin@gmail.com                    
// Description:                                                               
//   Represents where an interface is expected and it does not occur.           
// -------------------------------------------------------------------------- 
// Create date: 09-04-2007                                                    
// Modification date: 09-04-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorManagement {
    /// <summary>
    /// Represents a error produced when the used type is not defined.
    /// </summary>
    public class ExpectedInterfaceError : ErrorAdapter {
        #region Constructor

        /// <summary>
        /// Constructor of UnknownTypeError
        /// </summary>
        /// <param name="id">Idenfifier of incorrect type.</param>
        /// <param name="fileName">File name.</param>
        /// <param name="line">Line number.</param>
        /// <param name="column">Column number.</param>
        public ExpectedInterfaceError(string id, Location loc)
            : base(loc) {
            StringBuilder aux = new StringBuilder();
            aux.AppendFormat("The identifier '{0}' is not an interface.", id);
            this.Description = aux.ToString();
        }

        #endregion
    }
}
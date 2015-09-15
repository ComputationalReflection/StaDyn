//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: NoTypeHasMember.cs                                                
// Author: Francisco Ortin  -  francisco.ortin@gmail.com                    
// Description:                                                               
//   Represents a error produced when a dynamic union type has no valid
//   type to access a attribute
// -------------------------------------------------------------------------- 
// Create date: 18-05-2007                                                    
// Modification date: 18-05-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorManagement {
    /// <summary>
    /// Represents a error produced when a dynamic union type has no valid type to be applied an operation
    /// </summary>
    public class NoTypeHasMember : ErrorAdapter {

        #region Constructor
        /// <summary>
        /// Constructor of NoTypeAcceptsOperation
        /// </summary>
        /// <param name="type">The type full name.</param>
        /// <param name="attribute">The attribute name</param>
        /// <param name="fileName">File name.</param>
        /// <param name="line">Line number.</param>
        /// <param name="column">Column number.</param>
        public NoTypeHasMember(string type, string member, Location loc)
            : base(loc) {
            StringBuilder aux = new StringBuilder();
            aux.AppendFormat("The dynamic type '{0}' has no valid type type with '{1}' member.", type, member);
            this.Description = aux.ToString();
        }
        #endregion
    }
}
////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: UnknownTypeError.cs                                                  //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//   Represents a error produced when the used type is not defined.           //
// -------------------------------------------------------------------------- //
// Create date: 24-01-2007                                                    //
// Modification date: 24-01-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorManagement {
    /// <summary>
    /// Represents a error produced when the used type is not defined.
    /// </summary>
    public class UnknownTypeError : ErrorAdapter {
        #region Constructor

        /// <summary>
        /// Constructor of UnknownTypeError
        /// </summary>
        /// <param name="id">Idenfifier of incorrect type.</param>
        /// <param name="fileName">File name.</param>
        /// <param name="line">Line number.</param>
        /// <param name="column">Column number.</param>
        public UnknownTypeError(string id, Location loc) : base(loc) {
            StringBuilder aux = new StringBuilder();
            aux.AppendFormat("The type name '{0}' could not be found. File: {1}", id, loc);
            this.Description = aux.ToString();
        }

        /// <summary>
        /// Constructor of UnknownTypeError
        /// </summary>
        /// <param name="fileName">File name.</param>
        /// <param name="line">Line number.</param>
        /// <param name="column">Column number.</param>
        public UnknownTypeError(Location loc) : base(loc) {
            StringBuilder aux = new StringBuilder();
            aux.AppendFormat("Unknown type after the new operator.");
            this.Description = aux.ToString();
        }

        #endregion
    }
}
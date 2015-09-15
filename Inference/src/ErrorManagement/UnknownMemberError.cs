////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: UnknownMemberError.cs                                                //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//   Represents a error produced when the attribute identifier is not defined.   //
// -------------------------------------------------------------------------- //
// Create date: 21-02-2007                                                    //
// Modification date: 21-02-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorManagement {
    /// <summary>
    /// Represents a error produced when the attribute identifier is not defined.
    /// </summary>
    public class UnknownMemberError : ErrorAdapter {

        #region Constructor
        /// <summary>
        /// Constructor of UnknownMemberError
        /// </summary>
        /// <param name="id">Idenfifier of type.</param>
        /// <param name="memberId">Member identifier.</param>
        /// <param name="fileName">File name.</param>
        /// <param name="line">Line number.</param>
        /// <param name="column">Column number.</param>
        public UnknownMemberError(string typeId, string memberId, Location loc) : base(loc) {
            StringBuilder aux = new StringBuilder();
            aux.AppendFormat("'{0}' does not contain a definition for '{1}'.", typeId, memberId);
            this.Description = aux.ToString();
        }

        /// <summary>
        /// Constructor of UnknownMemberError
        /// </summary>
        /// <param name="memberId">Member identifier.</param>
        /// <param name="fileName">File name.</param>
        /// <param name="line">Line number.</param>
        /// <param name="column">Column number.</param>
        public UnknownMemberError(string memberId, Location loc) : base(loc) {
            StringBuilder aux = new StringBuilder();
            aux.AppendFormat("'{0}': no suitable member found.", memberId);
            this.Description = aux.ToString();
        }

        /// <summary>
        /// Constructor of UnknownMemberError
        /// </summary>
        /// <param name="memberId">Member identifier.</param>
        /// <param name="fileName">File name.</param>
        /// <param name="line">Line number.</param>
        /// <param name="column">Column number.</param>
        public UnknownMemberError(Location loc) : base(loc) {
            StringBuilder aux = new StringBuilder();
            aux.AppendFormat("'No suitable member found.");
            this.Description = aux.ToString();
        }
        #endregion

    }
}
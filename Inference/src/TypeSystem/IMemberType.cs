////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: MemberType.cs                                                        //
// Authors: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                   //
//          Francisco Ortin - francisco.ortin@gmail.com                       //
// Description:                                                               //
//    Represents a class attribute (fields or methods).                          //
//    Inheritance: TypeExpression.                                            //
//    Implements Composite pattern [Composite].                               //
// -------------------------------------------------------------------------- //
// Create date: 22-10-2006                                                    //
// Modification date: 09-03-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using ErrorManagement;

namespace TypeSystem {
    /// <summary>
    /// Representa a class attribute (fields or methods).
    /// </summary>
    /// <remarks>
    /// Inheritance: TypeExpression.
    /// Implements Composite pattern [Composite].
    /// </remarks>
    public interface IMemberType {

        #region Properties
        /// <summary>
        /// Gets or sets the attribute information of method type
        /// </summary>
        AccessModifier MemberInfo {
            get;
            set;
        }

        #endregion


    }
}

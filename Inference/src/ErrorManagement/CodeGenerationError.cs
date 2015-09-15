////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: ClassTypeInfoError.cs                                                //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Represents a error produced when a MethodType has class information and //
// tries to assign other class information.                                   //
// -------------------------------------------------------------------------- //
// Create date: 21-11-2006                                                    //
// Modification date: 14-02-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorManagement {
    /// <summary>
    /// Represents a error produced when a MethodType has class information and
    /// tries to assign other class information.
    /// </summary>
    public class CodeGenerationError : ErrorAdapter {
        #region Constructor

        /// <summary>
        /// Constructor of ClassTypeInfoError
        /// </summary>
        /// <param name="type1"></param>
        /// <param name="type2"></param>
        public CodeGenerationError(string str) {
            this.Description = str;

        #endregion
        }
    }
}
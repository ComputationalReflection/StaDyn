//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: BCLNameSpaceType.cs                                                     
// Author: Francisco Ortin - francisco.ortin@gmail.com                       
// Description:                                                               
//    Represents a type obtained when explicitely using namespaces IDs.
//    This is an special type of NameScpaceType that must search its data
//        in the BCL. 
//        Eg: "System.Diagnostics".Debug
//    It has not been checked that the namespace exists, because this service
//        is not provided by the .Net Framework. Thus, it can generate
//        errors of not existing types. E.g: "System.UnknownType"
//    Inheritance: NameSpaceType.                                            
//    Implements Composite pattern [Leaf].                               
// -------------------------------------------------------------------------- 
// Create date: 06-04-2007                                                    
// Modification date: 06-04-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using TypeSystem.Operations;
//VISTO
namespace TypeSystem {
    /// <summary>
    /// WriteType of a namespace 
    /// </summary>
    class BCLNameSpaceType : NameSpaceType {

        #region Fields
        /// <summary>
        /// To delegate all the functionalities
        /// </summary>
        //private Introspection introspection;

        #endregion



        #region Constructors
        public BCLNameSpaceType(string name) : base(name) { }
        #endregion


        #region Dispatcher
        public override object AcceptOperation(TypeSystemOperation op, object arg) { return op.Exec(this, arg); }
        #endregion
    }
}
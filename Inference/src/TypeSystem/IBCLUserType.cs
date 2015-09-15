//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: IBCLUserType.cs                                                      
// Authors: Francisco Ortin - francisco.ortin@gmail.com                       
// Description:                                                               
//    Represents the interface that all the classes and interfaces of the
//       BCL must implement
// -------------------------------------------------------------------------- 
// Create date: 07-04-2007                                                    
// Modification date: 07-04-2007                                              
//////////////////////////////////////////////////////////////////////////////
//visto
using System;
using System.Collections.Generic;
using System.Text;
using ErrorManagement;
using TypeSystem.Operations;

namespace TypeSystem {
    public interface IBCLUserType {

        /// <summary>
        /// Returns the real introspective type
        /// </summary>
        Type TypeInfo { get; }

        /// <summary>
        /// Gets and sets the attribute list
        /// </summary>
        Dictionary<string, AccessModifier> Members { get; }

        /// <summary>
        /// Gets the constructor list
        /// </summary>
        AccessModifier Constructors { get; }

        /// <summary>
        /// Class identifier;
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Adds a new base class to the class type
        /// </summary>
        /// <param name="baseClass">The new base class</param>
        void AddBaseClass(ClassType baseClass, Location location);


        /// <summary>
        /// Adds a new base interface to the class or interface type
        /// </summary>
        /// <param name="baseInterface">The new base interface</param>
        void AddBaseInterface(InterfaceType baseInterface, Location location);

                /// <summary>
        /// Use allways this method to add any attribute. It takes into account if that attribute 
        /// is a constructor, method, field or property and sets it correctly
        /// DO NOT modify the directly the fields 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="accessModifier"></param>
        void AddMember(string name, AccessModifier accessModifier);


    }
}

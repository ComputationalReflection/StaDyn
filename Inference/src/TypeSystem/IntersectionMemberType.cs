////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: FieldType.cs                                                         //
// Author:  Francisco Ortin - francisco.ortin@gmail.com                       //
// Description:                                                               //
//    Represents a field type.                                                //
//    Inheritance: MemberType.                                                //
//    Implements Composite pattern [Composite].                               //
// -------------------------------------------------------------------------- //
// Create date: 04-04-2007                                                    //
// Modification date: 04-04-2007                                              //
////////////////////////////////////////////////////////////////////////////////

//visto
using System;
using System.Collections.Generic;
using System.Text;

using ErrorManagement;
using TypeSystem.Operations;

namespace TypeSystem {
    /// <summary>
    /// A class that makes possible to have intersection types as class members (overload)
    /// </summary>
    class IntersectionMemberType : IntersectionType, IMemberType {

        #region Fields

        /// <summary>
        /// Links to attribute information (modifiers and its class type)
        /// </summary>
        private AccessModifier memberInfo;
        #endregion

        
        #region Properties

        /// <summary>
        /// Gets or sets the attribute information of method type
        /// </summary>
        public AccessModifier MemberInfo {
            get { return this.memberInfo; }
            set {
                if (this.memberInfo == null) {
                    this.memberInfo = value;
                }
                else
                    ErrorManager.Instance.NotifyError(new ClassTypeInfoError(this.memberInfo.MemberIdentifier, this.memberInfo.Class.FullName, value.Class.FullName));
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor of Intersection
        /// </summary>
        public IntersectionMemberType(TypeExpression te) : base(te) { }
        public IntersectionMemberType() { }

        #region AddMethod
        /// <summary>
        /// Adds a new method for overload.
        /// </summary>
        /// <param name="type">WriteType expression to add.</param>
        /// <returns>If the type has been added</returns>
    
          
          public bool AddMethod(MethodType type) {
            Predicate<TypeExpression> predicate = delegate(TypeExpression te2) {
                return (bool)te2.AcceptOperation(new EqualsForOverloadOperation(type), null);
            };
            if (typeSet.Find(predicate) == null) {
                this.typeSet.Add(type);
                return true;
            }
            return false;
        }
        #endregion

        #endregion
    
        #region Dispatcher
          public override object AcceptOperation(TypeSystemOperation op, object arg) { return op.Exec(this, arg); }
        #endregion
    }
}

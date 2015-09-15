////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: UserType.cs                                                          //
// Authors: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                   //
//          Francisco Ortin - francisco.ortin@gmail.com                       //
// Description:                                                               //
//    Represents a class or interface type.                                   //
//    Inheritance: TypeExpression.                                            //
//    Implements Composite pattern [Composite].                               //
// -------------------------------------------------------------------------- //
// Create date: 26-01-2007                                                    //
// Modification date: 29-01-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;

using ErrorManagement;
using TypeSystem.Operations;

namespace TypeSystem {
    /// <summary>
    /// Represents a class or interface type.
    /// </summary>
    /// <remarks>
    /// Inheritance: TypeExpression.
    /// Implements Composite pattern [Composite].
    /// </remarks>
    public abstract class UserType : TypeExpression {
        #region Fields

        /// <summary>
        /// Represents all class members(fields and methods). 
        /// List of members is used to allow overload methods
        /// </summary>
        private Dictionary<string, AccessModifier> memberList = new Dictionary<string, AccessModifier>();

        /// <summary>
        /// Represents all class fields. 
        /// </summary>
        protected Dictionary<string, AccessModifier> fieldList = new Dictionary<string, AccessModifier>();

        /// <summary>
        /// Represents all class constructos (with intersection types).
        /// </summary>
        private AccessModifier constructorList;

        /// <summary>
        /// Represents all class methods. 
        /// </summary>
        private Dictionary<string, AccessModifier> methodList = new Dictionary<string, AccessModifier>();

        /// <summary>
        /// Represents the identifiers of the interfaces
        /// </summary>
        protected List<InterfaceType> interfaceList;

        /// <summary>
        /// Stores the modifiers belong to the class type
        /// </summary>
        protected List<Modifier> modifierList;

        /// <summary>
        /// Stores a mask with the modifier information
        /// </summary>
        protected Modifier modifierMask;

        /// <summary>
        /// Class identifier;
        /// </summary>
        protected string name;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the field list
        /// </summary>
        public Dictionary<string, AccessModifier> Fields {
            get { return this.fieldList; }
        }

        /// <summary>
        /// Gets the constructor list
        /// </summary>
        public AccessModifier Constructors {
            get { return this.constructorList; }
        }

        /// <summary>
        /// Gets the method list
        /// </summary>
        public Dictionary<string, AccessModifier> Methods {
            get { return this.methodList; }
        }

        /// <summary>
        /// Gets and sets the attribute list
        /// </summary>
        public Dictionary<string, AccessModifier> Members {
            get { return this.memberList; }
            set {
                this.memberList = value;
                foreach (KeyValuePair<string, AccessModifier> pair in value) {
                    TypeExpression memberType = pair.Value.Type;
                    if (memberType is FieldType)
                        fieldList[pair.Key] = pair.Value;
                    else if (pair.Key.Equals(pair.Value.Class.Name)||pair.Key.Equals(pair.Value.Class.FullName))
                        if (this.constructorList != null)
                            System.Diagnostics.Debug.Assert(false, "Use intersection types for multiple constructors.");
                        else this.constructorList = pair.Value;
                    else
                        methodList[pair.Key] = pair.Value;
                }
            }
        }


        /// <summary>
        /// Gets and sets the attribute modifiers
        /// </summary>
        public List<Modifier> Modifiers {
            set {
                for (int i = 0; i < value.Count; i++) {
                    this.modifierMask |= value[i];
                }
                if ((this.modifierMask & Modifier.AccessLevel) == 0) {
                    value.Add(Modifier.Internal);
                    this.modifierMask |= Modifier.Internal;
                }
                this.modifierList = value;
            }
        }

        /// <summary>
        /// Class identifier;
        /// </summary>
        public string Name {
            get { return this.name; }
        }

       /// <summary>
       /// Gets the modifier information
       /// </summary>
       public Modifier ModifierMask
       {
          get { return this.modifierMask; }
       }

       /// <summary>
       /// Gets the list of interfaces
       /// </summary>
       public List<InterfaceType> InterfaceList
       {
          get { return this.interfaceList; }
       }
       
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of UserType
        /// </summary>
        /// <param name="name">Class or interface name</param>
        protected UserType(string identifier) {
            this.fullName = identifier;
            this.interfaceList = new List<InterfaceType>();
        }

        #endregion

        #region Dispatcher
        public override object AcceptOperation(TypeSystemOperation op, object arg) { return op.Exec(this, arg); }
        #endregion

        #region AddMember()
        /// <summary>
        /// Use allways this method to add any attribute. It takes into account if that attribute 
        /// is a constructor, method, field or property and sets it correctly
        /// DO NOT modify the directly the fields 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="accessModifier"></param>
        public void AddMember(string name, AccessModifier accessModifier) {
            TypeExpression memberType = accessModifier.Type;
            memberList[name] = accessModifier;
            if (memberType is FieldType)
                fieldList[name] = accessModifier;
            else if (memberType is MethodType && name.Equals(accessModifier.Class.Name))
                if (this.constructorList != null)
                    System.Diagnostics.Debug.Assert(false, "Use intersection types for multiple constructors.");
                else this.constructorList = accessModifier;
            else
                methodList[name] = accessModifier;
        }
        #endregion

        #region AddBaseClass()
        /// <summary>
        /// Adds a new inherited class
        /// </summary>
        /// <param name="inheritedClass">Information about inherited type.</param>
        /// <param name="fileName">File name.</param>
        /// <param name="line">Line number.</param>
        /// <param name="column">Column number.</param>
        public abstract void AddBaseClass(ClassType inheritedType, Location location);
        #endregion

        #region AddBaseInterface()
        /// <summary>
        /// Adds a new inherited interface
        /// </summary>
        /// <param name="inheritedClass">Information about inherited interface.</param>
        /// <param name="fileName">File name.</param>
        /// <param name="line">Line number.</param>
        /// <param name="column">Column number.</param>
        public void AddBaseInterface(InterfaceType inheritedType, Location location) {
                this.interfaceList.Add((InterfaceType)inheritedType);
        }
        #endregion

        #region HasTypeVariables()
        /// <summary>
        /// To know if the type expression has some type variables and requieres unification
        /// The default implementation is return false
        /// </summary>
        /// <returns>If it has any type variable</returns>
        public override bool HasTypeVariables() {
            return HasTypeVariables(new List<String>());
        }

        public bool HasTypeVariables(IList<String> evaluated)
        {
            if (this.validHasTypeVariables)
                return this.hasTypeVariablesCache;
            bool toReturn = false;
            foreach (AccessModifier am in this.Fields.Values)
            {                
                if (evaluated.Contains(am.Type.FullName))
                    continue;
                else
                {
                    evaluated.Add(am.Type.FullName);
                    bool result;
                    if (am.Type is FieldType)
                        result = ((FieldType)am.Type).HasTypeVariables(evaluated);
                    else
                        result = am.Type.HasTypeVariables();                                       
                    if (result)                    
                        toReturn = true;                    
                }
                if (toReturn)
                    break;
            }
            this.validHasTypeVariables = true;
            return this.hasTypeVariablesCache = toReturn;
        }
        #endregion

        #region InheritsFrom()
        /// <summary>
        /// Tells if the implicit object is a subtype of the superType
        /// </summary>
        /// <param name="superType">The super type</param>
        /// <returns>if the implicit object is a subtype of the superType</returns>
        public virtual bool InheritsFrom(UserType superType) {
            // * A class does not inherit from itself
            if ((bool)this.AcceptOperation(new EquivalentOperation(superType), null))
                return false;
            // * Depth first search
            foreach (InterfaceType interfaze in this.interfaceList)
                if (interfaze == superType || (bool)interfaze.AcceptOperation(new EquivalentOperation(superType), null))
                    return true;
            foreach (InterfaceType interfaze in this.interfaceList)
                if (interfaze.InheritsFrom(superType))
                    return true;
            // * No inheritance
            return false;
        }
        #endregion

    }
}


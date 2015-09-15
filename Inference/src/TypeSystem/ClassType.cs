////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: ClassType.cs                                                         //
// Authors: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                   //
//          Francisco Ortin - francisco.ortin@gmail.com                       //
// Description:                                                               //
//    Represents a class type.                                                //
//    Inheritance: UserType.                                                  //
//    Implements Composite pattern [Composite].                               //
// -------------------------------------------------------------------------- //
// Create date: 22-10-2006                                                    //
// Modification date: 05-06-2007                                              //
////////////////////////////////////////////////////////////////////////////////
//visto
using System;
using System.Collections.Generic;
using System.Text;

using AST;
using TypeSystem.Constraints;
using ErrorManagement;
using Tools;
using TypeSystem.Operations;

namespace TypeSystem {
    /// <summary>
    /// Represents a class type.
    /// </summary>
    /// <remarks>
    /// Inheritance: UserType.
    /// Implements Composite pattern [Composite].
    /// </remarks>
    public class ClassType : UserType {
        #region Fields

        /// <summary>
        /// If exists, represents the base class (simple inheritance)
        /// </summary>
        protected ClassType baseClass;

        /// <summary>
        /// Indicates if the class holds a concrete type. Opposite to an abstract type,
        /// a concrete type holds the type of an object, after creating it with a new.
        /// </summary>
        protected bool isConcreteType = false;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the base type (null if not exists).
        /// </summary>
        public ClassType BaseClass {
            get { return this.baseClass; }
        }

        /// <summary>
        /// Indicates if the class holds a concrete type. Opposite to an abstract type,
        /// a concrete type holds the type of an object, after creating it with a new.
        /// </summary>
        public bool ConcreteType {
            get { return this.isConcreteType; }
            set {
                // * Sets the appropriate value in the hierarchy
                ClassType klass = this;
                while (klass != null) {
                    klass.isConcreteType = value;
                    klass = klass.baseClass;
                }
            }
        }
        #endregion

        #region Constructors

        /// <summary>
        /// Constructor of ClassType
        /// </summary>
        /// <param name="name">Class name</param>
        /// <param name="identifier">Class identifier.</param>
        /// <param name="fullName">Class full identifier.</param>
        /// <param name="modifiers">Modifiers of the class type</param>
        public ClassType(string identifier, string fullName, List<Modifier> modifiers)
            : base(fullName) {
            this.name = identifier;
            this.Modifiers = modifiers;
        }

        /// <summary>
        /// Constructor of ClassType
        /// </summary>
        /// <param name="name">Class name</param>
        public ClassType(string name)
            : base(name) {
            this.name = name;
            this.Modifiers = new List<Modifier>();
        }
        #endregion
        
        
        #region AddBaseClass()
        /// <summary>
        /// Adds a new inherited type
        /// </summary>
        /// <param name="inheritedClass">Information about inherited type.</param>
        /// <param name="fileName">File name.</param>
        /// <param name="line">Line number.</param>
        /// <param name="column">Column number.</param>
        public override void AddBaseClass(ClassType inheritedClass, Location location) {
            if (this.baseClass == null)
                this.baseClass = inheritedClass;
            else
                ErrorManager.Instance.NotifyError(new MultipleBaseClassError(this.fullName, location));
        }
        #endregion

        #region BuildTypeExpressionString()

        /// <summary>
        /// Creates the type expression string.
        /// </summary>
        public override string BuildTypeExpressionString(int depthLevel) {
            if (this.ValidTypeExpression)
                return this.typeExpression;
            if (depthLevel <= 0)
                return this.FullName;
             StringBuilder tE = new StringBuilder();
            // tE: Class(id, modifiers, base, interfaces, members)
            tE.AppendFormat("Class({0},", this.BuildTypeExpressionString(depthLevel - 1));
            // modifiers
            if (this.modifierList.Count != 0) {
                for (int i = 0; i < this.modifierList.Count - 1; i++) {
                    tE.AppendFormat(" {0} x", this.modifierList[i]);
                }
                tE.AppendFormat(" {0}", this.modifierList[this.modifierList.Count - 1]);
            }
            tE.Append(", ");

            // base
            if (this.baseClass != null)
                tE.Append(this.baseClass.FullName);
            tE.Append(", ");

            // interfaces
            if (this.interfaceList.Count != 0) {
                for (int i = 0; i < this.interfaceList.Count - 1; i++) {
                    tE.AppendFormat(" {0} x", this.interfaceList[i].FullName);
                }
                tE.AppendFormat(" {0}", this.interfaceList[this.interfaceList.Count - 1].FullName);
            }
            tE.Append(",");

            // members
            if (this.Members.Count != 0) {
                Dictionary<string, AccessModifier>.KeyCollection keys = this.Members.Keys;
                int i = 0;
                foreach (string key in keys) {
                    tE.Append(this.Members[key].Type.BuildTypeExpressionString(depthLevel - 1));
                    if (i < keys.Count - 1)
                        tE.Append(" x ");
                    i++;
                }
            }
            tE.Append(")");
            this.ValidTypeExpression = true;
            return this.typeExpression = tE.ToString();
        }

        #endregion

        // WriteType inference

        #region Dispatcher
        public override object AcceptOperation(TypeSystemOperation op, object arg) { return op.Exec(this, arg); }
        #endregion

        /// <summary>
        /// Validates the correct access to members, taking into account the information hiding level.
        /// </summary>
        /// <param name="member">The type of the member</param>
        /// <returns>If the access has been correct</returns>
        public bool validAccess(TypeExpression member) {
            // * Union or Intersection type
            IList<TypeExpression> typeSet=null;
            UnionType unionType = member as UnionType;
            if (member is UnionType)
                typeSet=((UnionType)member).TypeSet;
            else if (member is IntersectionType)
                typeSet=((IntersectionType)member).TypeSet;
            if (typeSet!=null) {
                bool anyValid=false;
                foreach (TypeExpression type in typeSet) {
                    if (!this.validAccess(type) && !member.IsDynamic)
                        return false;
                    else anyValid = true;
                    return anyValid;
                }
            }
            // * Member type
            IMemberType memberType = member as IMemberType;
            if (memberType == null || memberType.MemberInfo == null)
                return false;
            // * Public or Internal
            if (memberType.MemberInfo.hasModifier(Modifier.Public) || memberType.MemberInfo.hasModifier(Modifier.Internal))
                return true;
            // * Private
            if (memberType.MemberInfo.hasModifier(Modifier.Private))
                return (bool)this.AcceptOperation(new EquivalentOperation(memberType.MemberInfo.Class), null);
            // * Protected
            return (bool)this.AcceptOperation(new EquivalentOperation(memberType.MemberInfo.Class), null) || this.InheritsFrom(memberType.MemberInfo.Class);
            }

        #region InheritsFrom()
        /// <summary>
        /// Tells if the implicit object is a subtype of the superType
        /// </summary>
        /// <param name="superType">The super type</param>
        /// <returns>if the implicit object is a subtype of the superType</returns>
        public override bool InheritsFrom(UserType superType) {
            // * A class does not inherit from itself
            if ((bool)this.AcceptOperation( new EquivalentOperation(superType), null))
                return false;
            // * Searches in the base classes
            ClassType myBase = this.BaseClass;
            while (myBase != null) {
                if ((bool)myBase.AcceptOperation(new EquivalentOperation(superType), null))
                    return true;
                myBase = myBase.BaseClass;
            }
            // * Interfaces
            return base.InheritsFrom(superType);
        }
        #endregion


        #region AsClassType()
        /// <summary>
        /// Represent a type as a class. It is mainly used to obtain the BCL representation of types
        /// (string=String, int=Int32, []=Array...)
        /// </summary>
        /// <returns>The class type is there is a map, null otherwise</returns>
        public override ClassType AsClassType() {
            return this;
        }
        #endregion

        // WriteType Promotion

        #region PromotionLevel()

        /// <summary>
        /// Returns a value thdat indicates a promotion level.
        /// </summary>
        /// <param name="type">WriteType to promotion.</param>
        /// <returns>Returns a promotion value.</returns>
        //public override int PromotionLevel(TypeExpression type) {
        //    int aux, less = -1;

        //    // * The same type
        //    if (this == type)
        //        return 0;
        //    // * Equivalent types
        //    if ((bool)this.AcceptOperation(new EquivalentOperation(type)))
        //        return 0;

        //    // * Field type and bounded type variable
        //    FieldType fieldType = TypeExpression.As<FieldType>(type);
        //    if (fieldType != null)
        //        return this.PromotionLevel(fieldType.FieldTypeExpression);

        //    // * WriteType variable
        //    TypeVariable typeVariable = type as TypeVariable;
        //    if (typeVariable != null) {
        //        if (typeVariable.Substitution != null)
        //            // * If the variable is bounded, the promotion is the one of its substitution
        //            return this.PromotionLevel(typeVariable.EquivalenceClass.Substitution);
        //        // * A free variable is complete promotion
        //        return 0;
        //    }

        //    // * Inheritance
        //    if (this.BaseClass == null)
        //        // * Object only promotes to object
        //        return -1;
        //    if ((bool)this.baseClass.AcceptOperation(new EquivalentOperation(type)))
        //        return 1;
        //    else {
        //        aux = this.baseClass.PromotionLevel(type);
        //        if (aux != -1)
        //            return aux + 1;
        //    }

        //    // * Interfaces
        //    if (this.interfaceList.Count != 0) {
        //        for (int i = 0; i < this.interfaceList.Count; i++) {
        //            if ((bool)this.interfaceList[i].AcceptOperation( new EquivalentOperation(type))) {
        //                if ((less > 1) || (less == -1))
        //                    less = 1;
        //            }
        //            else {
        //                aux = this.interfaceList[i].PromotionLevel(type);
        //                if (aux != -1) {
        //                    if ((less > (aux + 1)) || (less == -1))
        //                        less = aux + 1;
        //                }
        //            }
        //        }
        //    }
        //    if (less != -1)
        //        return less;

        //    // * Union type
        //    UnionType unionType = TypeExpression.As<UnionType>(type);
        //    if (unionType != null)
        //        return unionType.SuperType(this);

        //    // * No promotion
        //    return -1;
        //}

        #endregion

        // WriteType Unification

        #region Unify
        /// <summary>
        /// This method unifies two type expressions (this and te)
        /// </summary>
        /// <param name="te">The expression to be unfied with this</param>
        /// <param name="unification">Indicates if the kind of unification (equivalent, incremental or override).</param>
        /// <param name="previouslyUnified">To detect infinite loops. The previously unified pairs of type expressions.</param>
        /// <returns>If the unification was successful</returns>
        public override bool Unify(TypeExpression te, SortOfUnification unification, IList<Pair<TypeExpression, TypeExpression>> previouslyUnified) {
            // * Infinite recursion detection
            Pair<TypeExpression, TypeExpression> pair = new Pair<TypeExpression, TypeExpression>(this, te);
            if (previouslyUnified.Contains(pair))
                return true;
            previouslyUnified.Add(pair);

            ClassType ct = te as ClassType;
            bool success = true;
            // * Class WriteType
            if (ct != null) {
                // * Inheritance is taken into account
                if ((int)ct.AcceptOperation(new PromotionLevelOperation(this), null) == -1)
                    return false;
                // * Walk upward in the tree till find the correct class
                while (!(bool)ct.AcceptOperation( new EquivalentOperation(this), null))
                    ct = ct.baseClass;
                // * Now we unify the fields
                foreach (string key in this.Fields.Keys) {
                    FieldType thisField = (FieldType)this.Fields[key].Type,
                        teField = (FieldType)ct.Fields[key].Type;
                    if (thisField.FieldTypeExpression is ClassTypeProxy || teField.FieldTypeExpression is ClassTypeProxy)
                        success = thisField.FieldTypeExpression.FullName.Equals(teField.FieldTypeExpression.FullName);
                    else if (!(thisField.Unify(teField, unification, previouslyUnified)))
                        success = false;
                    if (!success)
                        break;
                }
                if (success && this.baseClass != null)
                    // * The same with the base class
                    this.baseClass.Unify(ct.baseClass, unification, previouslyUnified);
                // * If one of the classes is a concrete type, so it is the other
                if (success)
                    this.ConcreteType = ct.ConcreteType = this.ConcreteType || ct.ConcreteType;
            }
            // * WriteType variable
            else if (te is TypeVariable) {
                TypeVariable typeVariable = (TypeVariable)te;
                if (unification != SortOfUnification.Incremental)
                    // * Incremental is commutative
                    success = typeVariable.Unify(this, unification, previouslyUnified);
                // * Incremental unification (not commutative)
                else if (typeVariable.Substitution != null)
                    // * Class(var) should unify to Var=Class(int)
                    success = this.Unify(typeVariable.Substitution, unification, previouslyUnified);
                else success = false;
            }
            // * Union WriteType
            else if (te is UnionType)
                success = te.Unify(this, unification, previouslyUnified);
            // * Class WriteType Proxy
            else if (te is ClassTypeProxy)
                success = this.Unify(((ClassTypeProxy)te).RealType, unification, previouslyUnified);
            else if (te is FieldType)
                success = this.Unify(((FieldType)te).FieldTypeExpression, unification, previouslyUnified);
            else success = false;
            // * Clears the type expression cache
            this.ValidTypeExpression = false;
            te.ValidTypeExpression = false;
            return success;
        }
        #endregion


        #region CloneType()
        /// <summary>
        /// This method creates a new class type, creating new type variables for
        /// each field. Methods are not cloned.
        /// It these type variables where bounded to types or other
        /// type variables, they are maintained.
        /// </summary>
        /// <param name="typeVariableMappings">Each new type varaiable represent a copy of another existing one.
        /// This parameter is a mapping between them, wher tmpName=old and value=new.</param>
        /// <returns>The new cloned class type</returns>
        public override TypeExpression CloneType(IDictionary<TypeVariable, TypeVariable> typeVariableMappings) {
            if (!this.HasTypeVariables())
                return this;
            // * We clone the members of the ClassType
            IList<EquivalenceClass> equivalenceClasses = new List<EquivalenceClass>();
            UserType newClassType = (UserType)this.CloneTypeVariables(typeVariableMappings, equivalenceClasses, new List<ClassType>());
            // * For each equivalence class we create a new one, 
            //   substituting the old type variables for the new ones
            // * The substitution is not altered
            // * Since equivalence classes and type variables have a bidirectional association,
            //   the new equivalence classes will make type variables update their new equivalence classes
            foreach (EquivalenceClass equivalenceClass in equivalenceClasses)
                equivalenceClass.UpdateEquivalenceClass(typeVariableMappings);
            // * The new class type is returned
            newClassType.ValidTypeExpression = false;
            return newClassType;
        }
        #endregion


        #region CloneTypeVariables()
        /// <summary>
        /// Method that clones each type variable of a type expression.
        /// Equivalence classes are not cloned (but included in the equivalenceClasses parameter).
        /// The default implementation is do nothing (for built-in types).
        /// </summary>
        /// <param name="typeVariableMappings">Each new type varaiable represent a copy of another existing one.
        /// This parameter is a mapping between them, wher tmpName=old and value=new</param>
        /// <param name="equivalenceClasses">Each equivalence class of all the type variables.</param>
        /// <param name="clonedClasses">This parameter collects the set of all cloned classes. It is used to detect infinite recursion.</param>
        /// <returns>The new type expression (itself by default)</returns>
        public override TypeExpression CloneTypeVariables(IDictionary<TypeVariable, TypeVariable> typeVariableMappings, IList<EquivalenceClass> equivalenceClasses, IList<ClassType> clonedClasses) {
            // * Without type variables, no clone is necessary
            if (!this.HasTypeVariables())
                return this;
            // * Let's check recursion
            if (!clonedClasses.Contains(this))
                clonedClasses.Add(this);
            else // * Recursion
                return new ClassTypeProxy(this, typeVariableMappings, equivalenceClasses);
            // * We clone the members of the ClassType
            ClassType newClassType = new ClassType(this.FullName);
            newClassType.ConcreteType = this.ConcreteType;
            newClassType.IsDynamic = this.IsDynamic;
            Dictionary<string, AccessModifier> oldFields = this.Fields;
            newClassType.fieldList = new Dictionary<string, AccessModifier>();
            // * We must create new type variables for fields
            foreach (KeyValuePair<string, AccessModifier> pair in oldFields) {
                // * Every type variable is cloned to a new one, adding both to typesVariables,
                //   inserting its equivalence classes in the equivalenceClasses parameter and updating the
                //   typeVariableMappings dictionary (<oldvariable,newVariable>
                FieldType fieldType = (FieldType)pair.Value.Type.CloneTypeVariables(typeVariableMappings, equivalenceClasses, clonedClasses);
                fieldType.MemberInfo.Class = newClassType;
                newClassType.AddMember(pair.Key, fieldType.MemberInfo);
            }
            // * We must create new attribute info (access modifiers) but without clonning methods
            foreach (KeyValuePair<string, AccessModifier> pair in this.Methods) {
                AccessModifier accesModifier = (AccessModifier)pair.Value.Clone();
                accesModifier.Class = newClassType;
                newClassType.AddMember(pair.Key, accesModifier);
            }
            // * The same with the base class
            newClassType.baseClass = (ClassType)this.baseClass.CloneType(typeVariableMappings);
            newClassType.ValidTypeExpression = false;
            // * The new class type is returned
            return newClassType;
        }

        #endregion

        #region UpdateEquivalenceClass()
        /// <summary>
        /// Replaces the equivalence class of type variables substituting the old type variables for the new ones.
        /// </summary>
        /// <param name="typeVariableMappings">Each new type varaiable represent a copy of another existing one.
        /// This parameter is a mapping between them, wher tmpName=old and value=new.</param>
        /// <param name="previouslyUpdated">To detect infinite loops. Previously updated type expressions.</param>
        public override void UpdateEquivalenceClass(IDictionary<TypeVariable, TypeVariable> typeVariableMappings, IList<TypeExpression> previouslyUpdated) {
            // * Checks infinite loops
            if (previouslyUpdated.Contains(this))
                return;
            previouslyUpdated.Add(this);

            // * Updates the equivalence class
            if (!this.HasTypeVariables())
                return;
            foreach (KeyValuePair<string, AccessModifier> pair in this.fieldList)
                if (pair.Value.Type.HasTypeVariables())
                    pair.Value.Type.UpdateEquivalenceClass(typeVariableMappings, previouslyUpdated);
            this.baseClass.UpdateEquivalenceClass(typeVariableMappings, previouslyUpdated);
            this.ValidTypeExpression = false;
        }
        #endregion

        #region ReplaceTypeVariables()
        /// <summary>
        /// Replaces type variables substituting the old type variables for the new ones.
        /// </summary>
        /// <param name="typeVariableMappings">Each new type varaiable represent a copy of another existing one.
        /// This parameter is a mapping between them, wher tmpName=old and value=new.</param>
        public override void ReplaceTypeVariables(IDictionary<TypeVariable, TypeVariable> typeVariableMappings) {
            if (!this.HasTypeVariables())
                return;
            foreach (KeyValuePair<string, AccessModifier> pair in this.fieldList)
                if (pair.Value.Type.HasTypeVariables())
                    pair.Value.Type.ReplaceTypeVariables(typeVariableMappings);
            this.ValidTypeExpression = false;
        }
        #endregion

        // SSA

        #region Clone()
        /// <summary>
        /// Clones a type to be used in SSA. It must taken into account that:
        /// - In case it has no type variables, no clone is performed
        /// - WriteType variables, equivalence classes and substitutions are cloned
        /// </summary>
        /// <param name="clonedTypeVariables">WriteType variables that have been cloned.</param>
        /// <param name="equivalenceClasses">Equivalence classes of the type cloned variables. These
        /// equivalence classes need to be updated with the new cloned type variables.</param>
        /// <param name="methodAnalyzed">The method that is being analyzed when the operation is performed.</param>
        /// <returns>The cloned type</returns>
        internal override TypeExpression Clone(IDictionary<int, TypeVariable> clonedTypeVariables, IList<EquivalenceClass> equivalenceClasses, MethodType methodAnalyzed) {
            //return this.Clone(clonedTypeVariables, equivalenceClasses, methodAnalyzed, new Dictionary<String, TypeExpression>());           
            if (!this.HasTypeVariables())
                return this;
            // * We clone the ClassType object
            ClassType newClassType = (ClassType)this.MemberwiseClone();           
            // * We clone the fields of the class that has type varrables
            newClassType.fieldList = new Dictionary<string, AccessModifier>();
            newClassType.Members = new Dictionary<string, AccessModifier>();
            foreach (KeyValuePair<string, AccessModifier> pair in this.Fields)
            {
                IMemberType newFieldType = null;
                if (pair.Value.Type is FieldType)
                    newFieldType = (IMemberType)((FieldType)pair.Value.Type).Clone(clonedTypeVariables, equivalenceClasses, methodAnalyzed);
                else
                    newFieldType = (IMemberType)pair.Value.Type.Clone(clonedTypeVariables, equivalenceClasses, methodAnalyzed);
                AccessModifier newAccessModifier = new AccessModifier(pair.Value.Modifiers, pair.Value.MemberIdentifier, newFieldType, false);
                newClassType.fieldList[pair.Key] = newAccessModifier;
                newClassType.Members[pair.Key] = newAccessModifier;
            }
            foreach (KeyValuePair<string, AccessModifier> pair in this.Methods)
                newClassType.Members[pair.Key] = pair.Value;
            // * Clones the base class
            if (this.BaseClass.HasTypeVariables())
                newClassType.baseClass = (ClassType)this.BaseClass.Clone(clonedTypeVariables, equivalenceClasses, methodAnalyzed);
            // * The new class type is returned
            newClassType.ValidTypeExpression = false;
            return newClassType;
        }

        //TODO: To solve Collections/CG.Var.CustomStack.cs test
        internal TypeExpression Clone(IDictionary<int, TypeVariable> clonedTypeVariables, IList<EquivalenceClass> equivalenceClasses, MethodType methodAnalyzed, IDictionary<String, TypeExpression> evaluated)
        {            
            if (!this.HasTypeVariables())
                return this;
            // * We clone the ClassType object
            ClassType newClassType = (ClassType)this.MemberwiseClone();
            if (evaluated.Keys.Contains(this.FullName))                
                return evaluated[this.FullName];
            evaluated.Add(this.FullName, newClassType);
            // * We clone the fields of the class that has type varrables
            newClassType.fieldList = new Dictionary<string, AccessModifier>();
            newClassType.Members = new Dictionary<string, AccessModifier>();
            foreach (KeyValuePair<string, AccessModifier> pair in this.Fields)
            {                             
                IMemberType newFieldType = null;
                if (pair.Value.Type is FieldType)
                    newFieldType = (IMemberType)((FieldType)pair.Value.Type).Clone(clonedTypeVariables, equivalenceClasses, methodAnalyzed, evaluated);                                    
                else
                    newFieldType = (IMemberType)pair.Value.Type.Clone(clonedTypeVariables, equivalenceClasses, methodAnalyzed);                                    
                AccessModifier newAccessModifier = new AccessModifier(pair.Value.Modifiers, pair.Value.MemberIdentifier, newFieldType, false);
                newClassType.fieldList[pair.Key] = newAccessModifier;
                newClassType.Members[pair.Key] = newAccessModifier;
            }
            foreach (KeyValuePair<string, AccessModifier> pair in this.Methods)
                newClassType.Members[pair.Key] = pair.Value;
            // * Clones the base class
            if (this.BaseClass.HasTypeVariables())
                newClassType.baseClass = (ClassType)this.BaseClass.Clone(clonedTypeVariables, equivalenceClasses, methodAnalyzed);
            // * The new class type is returned
            newClassType.ValidTypeExpression = false;
            return newClassType;
        }
        #endregion

        // Loop detection

        #region Equals&GetHashCode()
        /// <summary>
        /// To check loops in recursive types, proxy must be equal that the classes they represent
        /// </summary>
        public override bool Equals(object obj) {
            ClassType parameter = obj as ClassType;
            if (parameter == null) {
                ClassTypeProxy proxy = obj as ClassTypeProxy;
                if (proxy == null)
                    return false;
                parameter = proxy.RealType;
            }
            if (!this.FullName.Equals(parameter.FullName))
                return false;
            foreach (KeyValuePair<string, AccessModifier> pair in this.fieldList)
                if (!pair.Value.Type.FullName.Equals(parameter.fieldList[pair.Key].Type.FullName))
                    return false;
            return true;
        }
        public override int GetHashCode() {
            return this.FullName.GetHashCode();
        }
        #endregion

        // Helper Methods

        #region IsConcreteType()
        /// <summary>
        /// Tells if the parameter is a concrete class type
        /// </summary>
        /// <param name="actualImplicitObject">The object's type</param>
        /// <returns>The appropriate class type if the parameter is a concret object</returns>
        public static ClassType IsConcreteType(TypeExpression actualImplicitObject) {
            TypeVariable typeVariable = actualImplicitObject as TypeVariable;
            if (typeVariable != null)
                actualImplicitObject = typeVariable.Substitution;
            ClassType classType = actualImplicitObject as ClassType;
            if (classType != null && classType.ConcreteType)
                return classType;
            UnionType unionType = actualImplicitObject as UnionType;
            if (unionType != null && unionType.Count > 0)
                return IsConcreteType(unionType.TypeSet[0]);
            return null;
        }
        #endregion

        // Code Generation

        #region ILType()

        /// <summary>
        /// Gets the string type to use in IL code.
        /// </summary>
        /// <returns>Returns the string type to use in IL code.</returns>
        public override string ILType() {
            StringBuilder aux = new StringBuilder();
            aux.AppendFormat("class {0}", this.fullName);
            return aux.ToString();
        }

        #endregion

        #region IsValueType()

        /// <summary>
        /// True if type expression is a ValueType. Otherwise, false.
        /// </summary>
        /// <returns>Returns true if the type expression is a ValueType. Otherwise, false.</returns>
        public override bool IsValueType()
        {
           return false;
        }

        #endregion

    }
}

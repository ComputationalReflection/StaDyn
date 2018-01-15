////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: EquivalenceClass.cs                                                        //
// Author: Francisco Ortin-  francisco.ortin@gmail.com                        //
// Description:                                                               //
//    Represents a equivalence class of type varariables [Aho].              //
// -------------------------------------------------------------------------- //
// Create date: 03-04-2007                                                    //
////////////////////////////////////////////////////////////////////////////////
//visto
using System;
using System.Collections.Generic;
using System.Text;
using Tools;
using TypeSystem.Operations;

namespace TypeSystem {
    public class EquivalenceClass {
        #region Fields
        /// <summary>
        /// All the type variables that must be substituted with the same value
        /// </summary>
        private IDictionary<int, TypeVariable> typeVariables = new Dictionary<int, TypeVariable>();

        /// <summary>
        /// A equivalence class could be bounded to a unique no variable type
        /// </summary>
        private TypeExpression substitution;
        #endregion

        #region Properties
        /// <summary>
        /// All the type variables that must be substituted with the same value
        /// </summary>
        public IDictionary<int, TypeVariable> TypeVariables {
            get { return typeVariables; }
        }

        /// <summary>
        /// A equivalence class could be bounded to a unique no variable type
        /// </summary>
        public TypeExpression Substitution {
            get { return substitution; }
        }
        #endregion


        #region Constructors
        public EquivalenceClass() { }

        public EquivalenceClass(TypeExpression te) {
            if (!(te is TypeVariable))
                throw new ArgumentException("I need a type variable, not a " + te);
            add(te, SortOfUnification.Equivalent, new List<Pair<TypeExpression, TypeExpression>>());
        }
        #endregion

        #region Add
        /// <summary>
        /// To add a new type to the class equivalence.
        /// </summary>
        /// <param name="te">The type to be added</param>
        /// <param name="unification">Indicates if the kind of unification (equivalent, incremental or override).</param>
        /// <param name="previouslyUnified">To detect infinite loops. The previously unified pairs of type expressions.</param>
        /// <returns>If the substitution has been correctly applied</returns>
        public bool add(TypeExpression te, SortOfUnification unification, IList<Pair<TypeExpression, TypeExpression>> previouslyUnified) {
            TypeVariable tv = te as TypeVariable;
            if (tv != null) { // * Another type variable
                // * Tries to add its substitution
                if (tv.Substitution != null) // * If it has a substitution 
                    if (!this.add(tv.EquivalenceClass.Substitution, unification, previouslyUnified)) // * We try to add it to ourselves
                        return false; // * Both susbstitutions are not the same
                // * If no error, we add it to the equivalence class
                this.typeVariables[tv.Variable] = tv;
                if (tv.EquivalenceClass != null) {  // * The parameter already has a equivalence class
                    IList<int> keys = new List<int>(tv.EquivalenceClass.TypeVariables.Keys);
                    foreach (int pairKey in keys) {
                        if (!this.typeVariables.ContainsKey(pairKey)) {
                            // * We recursively add all the element of the equivalence class
                            this.typeVariables[pairKey] = tv.EquivalenceClass.TypeVariables[pairKey];
                            this.add(tv.EquivalenceClass.TypeVariables[pairKey], unification, previouslyUnified);
                        }
                    }
                }
                // * Finally, we update the equivalence class of tv
                tv.EquivalenceClass = this;
                return true;
            }
            // * te is not a type variable
            if (this.substitution != null) {
                // * A substitution already exists
                if (unification == SortOfUnification.Equivalent) {
                    // * They must be equivalent
                    if ( !(bool)this.substitution.AcceptOperation(new EquivalentOperation(te), null) )
                        return false;
                    if (te.HasTypeVariables())
                        // var1=Array(int) must be unified to Array(var1)
                        return te.Unify(this.substitution, unification, previouslyUnified);
                    return true;
                }
                if (unification == SortOfUnification.Incremental) {
                    // * The incremental behaviour implies a union of all the types
                    this.substitution = UnionType.collect(this.substitution, te);
                    return true;
                }
                // * Override unification (the susbstitution is overridden)
                this.substitution = te;
                return true;
            }
            // * We set the type as a susbstitution
            substitution = te;
            return true;
        }
        #endregion

        #region ToString()
        /// <summary>
        /// The ToString of its substitution
        /// </summary>
        /// <param name="except">The id of the parameter is not </param>
        /// <returns>The ToString of the substitution</returns>
        public override string ToString() {
            StringBuilder sb = new StringBuilder("{");
            ICollection<int> keys = typeVariables.Keys;
            int i = 0;
            foreach (int key in keys)
            {
                sb.Append(key);
                if (i < keys.Count - 1)
                    sb.Append(",");
                i++;
            }
            sb.Append("}");
            return sb.ToString();            
        }
        #endregion


        #region UpdateEquivalenceClass()
        /// <summary>
        /// Replaces the equivalence class substituting the old type variables for the new ones.
        /// The substitution is not altered.
        /// Since equivalence classes and type variables have a bidirectional association,
        /// the new equivalence classes will make type variables update their new equivalence classes.
        /// </summary>
        /// <param name="typeVariableMappings">Each new type varaiable represent a copy of another existing one.
        /// This parameter is a mapping between them, wher tmpName=old and value=new.</param>
        public void UpdateEquivalenceClass(IDictionary<TypeVariable, TypeVariable> typeVariableMappings) {
            // * Creates a new equivalence class
            EquivalenceClass newEquivalenceClass = new EquivalenceClass();
            // * We take the existing substitution...
            newEquivalenceClass.substitution = this.substitution;
            //   ... changing old type variables for new ones
            if (newEquivalenceClass.substitution != null)
                newEquivalenceClass.substitution.ReplaceTypeVariables(typeVariableMappings);
            // * We update all the appropiate type variables
            newEquivalenceClass.typeVariables = new Dictionary<int, TypeVariable>();
            foreach (KeyValuePair<int, TypeVariable> oldPair in this.typeVariables) {
                // * Is there a new type variable for the existing one?
                if (typeVariableMappings.ContainsKey(oldPair.Value)) {
                    // * We create a new entry with the new one...
                    TypeVariable newTypeVariable = typeVariableMappings[oldPair.Value];
                    newEquivalenceClass.typeVariables[newTypeVariable.Variable] = newTypeVariable;
                    var previusSubstitution = newTypeVariable.Substitution;
                   
                    // * ... and updates the class equivalence of the new type variable
                    newTypeVariable.EquivalenceClass = newEquivalenceClass;

                    if (previusSubstitution != null && newTypeVariable.Substitution == null)
                        newTypeVariable.EquivalenceClass.add(previusSubstitution, SortOfUnification.Equivalent, new List<Pair<TypeExpression, TypeExpression>>());
                    newTypeVariable.ValidTypeExpression = false;
                }
                else // * Otherwise, the old one is kept
                    newEquivalenceClass.typeVariables[oldPair.Key] = oldPair.Value;
            }
            // * Finally, we update all the equivalence classes of the substitution if its exists
            if (newEquivalenceClass.substitution != null)
                newEquivalenceClass.substitution.UpdateEquivalenceClass(typeVariableMappings, new List<TypeExpression>());
        }
        #endregion

        // SSA

        #region CloneTypeVariables()
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
        internal void CloneTypeVariables(IDictionary<int, TypeVariable> clonedTypeVariables, IList<EquivalenceClass> equivalenceClasses, MethodType methodAnalyzed) {
            // * Checks if the equivalence class has been already cloned
            if (equivalenceClasses.Contains(this))
                return;
            // * Adds class equivalence
            equivalenceClasses.Add(this);
            // * Clones, but not updates, the substitution
            if (this.substitution!=null)
                this.substitution.Clone(clonedTypeVariables, equivalenceClasses, methodAnalyzed);
            // * Clones all the type variables in the equivalence class
            foreach (KeyValuePair<int, TypeVariable> pair in this.typeVariables)
                pair.Value.Clone(clonedTypeVariables, equivalenceClasses, methodAnalyzed);
        }
        #endregion

        // Loop Detection

        /// <summary>
        /// When loops are detected, it is necesary to suppress a new extra variable returned in 
        /// the return type of recursive functions
        /// </summary>
        /// <param name="toRemove">The type variable to remove</param>
        /// <returns>If it has been actually removed</returns>
        public bool Remove(TypeVariable toRemove) {
            return this.typeVariables.Remove(toRemove.Variable);
        }
    }
}

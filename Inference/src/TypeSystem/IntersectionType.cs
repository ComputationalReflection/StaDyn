////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: IntersectionType.cs                                                       //
// Authors: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                   //
//          Francisco Ortin - francisco.ortin@gmail.com                       //
// Description:                                                               //
//    Represents a disjunction set type.                                      //
//    Inheritance: TypeExpression.                                            //
//    Implements Composite pattern [Composite].                               //
// -------------------------------------------------------------------------- //
// Create date: 27-02-2007                                                    //
// Modification date: 21-03-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

using AST;
using ErrorManagement;
using Tools;
using TypeSystem.Operations;

namespace TypeSystem {
    /// <summary>
    /// Representa an intersection type.
    /// </summary>
    /// <remarks>
    /// Inheritance: TypeExpression.
    /// Implements Composite pattern [Composite].
    /// </remarks>
    /// 
    //VISTO
    public class IntersectionType : TypeExpression {
        #region Fields

        /// <summary>
        /// Stores a set of type expression
        /// </summary>
        protected List<TypeExpression> typeSet = new List<TypeExpression>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of type expressions
        /// </summary>
        public int Count {
            get { return this.typeSet.Count; }
        }

        /// <summary>
        /// Gets the list of type expressions
        /// </summary>
        public IList<TypeExpression> TypeSet {
            get { return this.typeSet; }
        }
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of Intersection
        /// </summary>
        public IntersectionType() { }
        public IntersectionType(TypeExpression te) {
            this.AddType(te);
            this.BuildFullName();
        }
        #endregion

        #region Dispatcher
        public override object AcceptOperation(TypeSystemOperation op, object arg) { return op.Exec(this, arg); }
        #endregion

        #region AddType
        /// <summary>
        /// Adds a new type expression.
        /// </summary>
        /// <param name="type">WriteType expression to add.</param>
        /// <returns>If the type has been added</returns>
        public bool AddType(TypeExpression type) {
            bool added = true;
            IntersectionType intersection = TypeExpression.As<IntersectionType>(type);
            if (intersection != null) {
                foreach (TypeExpression t in intersection.typeSet)
                    added = added && this.AddType(t);
                this.ValidTypeExpression = false;
                return added;
            }
            Predicate<TypeExpression> predicate = delegate(TypeExpression te2) {
                return (bool)te2.AcceptOperation(new EquivalentOperation(type), null);
            };
            if (this.typeSet.Find(predicate)== null) {
                this.typeSet.Add(type);
                this.ValidTypeExpression = false;
                return true;
            }
            return false;
        }
        #endregion

        #region ToString()

        /// <summary>
        /// Returns the type expression like a string
        /// </summary>
        /// <returns>string with the type expression associated.</returns>
        public override string BuildTypeExpressionString(int depthLevel) {
            if (this.ValidTypeExpression) return this.typeExpression;
            if (depthLevel <= 0) return this.FullName;

            StringBuilder aux = new StringBuilder();
            aux.Append("/\\(");
            for (int i = 0; i < this.typeSet.Count; i++) {
                aux.AppendFormat("{0}", this.typeSet[i].BuildTypeExpressionString(depthLevel-1));
                if (i < this.typeSet.Count - 1)
                    aux.AppendFormat(" ,");
            }
            aux.Append(")");
            this.typeExpression = aux.ToString();
            this.ValidTypeExpression = true;
            return this.typeExpression;
        }
        #endregion

        #region BuildFullName()
        /// <summary>
        /// Creates/Updates the full name of the type expression
        /// </summary>
        public override void BuildFullName() {
            StringBuilder aux = new StringBuilder();
            aux.Append("/\\(");
            for (int i = 0; i < this.typeSet.Count; i++) {
                aux.AppendFormat("{0}", this.typeSet[i].FullName);
                if (i < this.typeSet.Count - 1)
                    aux.AppendFormat(" ,");
            }
            aux.Append(")");
            this.fullName = aux.ToString();
        }
        #endregion

        // WriteType inference

        #region overloadResolution()
        /// <summary>
        /// A public method for being used as a global overload resolution process
        /// </summary>
        /// <param name="arguments">The ordered types of actual parameters</param>
        /// <param name="fileName"></param>
        /// <param name="line"></param>
        /// <param name="column"></param>
        /// <returns>The actual method called (a union type if more than one is suitable)</returns>
        public TypeExpression overloadResolution(TypeExpression[] arguments, Location location) {
            int aux;
            int min = -1, index = -1, minNumFreeVariables = Int32.MaxValue;

            // * We create a dictionary of <index,promotionValue> to remember the promotion values (they could have
            //   repeated values because of type variables)
            Dictionary<int, int> promotionValues = new Dictionary<int, int>();

            if (this.typeSet.Count == 0) {
                System.Diagnostics.Debug.Assert(false, "There should be no empty intersection types.");
                return null;
            }

            for (int i = 0; i < this.typeSet.Count; i++) {
                MethodType mt = TypeExpression.As<MethodType>(this.typeSet[i]);
                if (mt == null)
                    ErrorManager.Instance.NotifyError(new OperationNotAllowedError("()", mt.FullName, location));
                aux = mt.Promotion(arguments, location);
                if (aux != -1)
                    if ((min >= aux) || (min == -1)) {
                        min = aux;
                        index = i;
                        promotionValues[index] = min;
                    }
            }
            // * No method is suitable
            if (index == -1) {
                ErrorManager.Instance.NotifyError(new UnknownMemberError(location));
                return null;
            }
            index = -1;
            // * Gets the min number of free variables
            foreach (KeyValuePair<int, int> pair in promotionValues)
                if (pair.Value == min) {
                    aux = ((MethodType)this.typeSet[pair.Key]).GetNumberFreeVariables();
                    if (aux < minNumFreeVariables)
                        minNumFreeVariables = aux;
                }
            // * Assigns a union of all the best methods
            TypeExpression bestMethods=null;
            foreach (KeyValuePair<int, int> pair in promotionValues)
                if (pair.Value == min && ((MethodType)this.typeSet[pair.Key]).GetNumberFreeVariables() == minNumFreeVariables)
                    bestMethods = UnionType.collect(bestMethods, this.typeSet[pair.Key]);
            // * We've got'em
            return bestMethods;
        }
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
            Pair<TypeExpression,TypeExpression> pair = new Pair<TypeExpression, TypeExpression>(this, te);
            if (previouslyUnified.Contains(pair))
                return true;
            previouslyUnified.Add(pair);

            // * Clears the type expression cache
            this.ValidTypeExpression = false;
            te.ValidTypeExpression = false;
            // TODO
            return false;
        }
        #endregion


        #region HasTypeVariables()
        /// <summary>
        /// To know if the type expression has some type variables and requieres unification
        /// The default implementation is return false
        /// </summary>
        /// <returns>If it has any type variable</returns>
        public override bool HasTypeVariables() {
            if (this.validHasTypeVariables)
                return this.hasTypeVariablesCache;
            bool toReturn = false;
            foreach (TypeExpression type in this.typeSet)
                if (type.HasTypeVariables()) {
                    toReturn = true;
                    break;
                }
            this.validHasTypeVariables = true;
            return this.hasTypeVariablesCache = toReturn;
        }
        #endregion

        #region CloneTypeVariables()
        /// <summary>
        /// Method that clones each type variable of a type expression.
        /// Equivalence classes are not cloned (but included in the equivalenceClasses parameter.
        /// The default implementation is do nothing (for built-in types).
        /// </summary>
        /// <param name="typeVariableMappings">Each new type varaiable represent a copy of another existing one.
        /// This parameter is a mapping between them, wher tmpName=old and value=new</param>
        /// <param name="equivalenceClasses">Each equivalence class of all the type variables.</param>
        /// <param name="clonedClasses">This parameter collects the set of all cloned classes. It is used to detect infinite recursion.</param>
        /// <returns>The new type expression (itself by default)</returns>
        public override TypeExpression CloneTypeVariables(IDictionary<TypeVariable, TypeVariable> typeVariableMappings, IList<EquivalenceClass> equivalenceClasses, IList<ClassType> clonedClasses) {
            if (!this.HasTypeVariables())
                return this;
            IntersectionType newType = (IntersectionType)this.MemberwiseClone();
            IList<TypeExpression> oldTypeSet = this.typeSet;
            newType.typeSet = new List<TypeExpression>();
            foreach (TypeExpression oldType in oldTypeSet)
                newType.typeSet.Add(oldType.CloneTypeVariables(typeVariableMappings, equivalenceClasses, clonedClasses));
            newType.ValidTypeExpression = false;
            return newType;
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
            if (!this.HasTypeVariables())
                return this;
            IntersectionType newIntersectionType = (IntersectionType)this.MemberwiseClone();
            newIntersectionType.typeSet = new List<TypeExpression>();
            // * Clones all the types in the union
            foreach (TypeExpression type in this.typeSet)
                newIntersectionType.typeSet.Add(type.Clone(clonedTypeVariables, equivalenceClasses, methodAnalyzed));
            return newIntersectionType;
        }
        #endregion

        #region IsValueType()

        /// <summary>
        /// True if type expression is a ValueType. Otherwise, false.
        /// </summary>
        /// <returns>Returns true if the type expression is a ValueType. Otherwise, false.</returns>
        public override bool IsValueType()
        {
           throw new Exception("The method or operation is not implemented.");
        }

        #endregion


    }
}

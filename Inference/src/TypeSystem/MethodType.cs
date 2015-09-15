////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: MethodType.cs                                                        //
// Authors: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                   //
//          Francisco Ortin - francisco.ortin@gmail.com                       //
// Description:                                                               //
//    Represents a method type.                                               //
//    Inheritance: MemberType.                                                //
//    Implements Composite pattern [Composite].                               //
//    Implements Observer pattern [Subject and Observer]                      //
// -------------------------------------------------------------------------- //
// Create date: 15-10-2006                                                    //
// Modification date: 03-04-2007                                              //
////////////////////////////////////////////////////////////////////////////////
//visto
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using AST;
using ErrorManagement;
using TypeSystem.Constraints;
using Tools;
using TypeSystem.Operations;

namespace TypeSystem {
    /// <summary>
    /// Representa a method type.
    /// </summary>
    /// <remarks>
    /// Inheritance: MemberType.
    /// Implements Composite pattern [Composite].
    /// </remarks>
    public class MethodType : TypeExpression, IMemberType {
        #region Fields

        /// <summary>
        /// Represents the type expression the method return.
        /// </summary>
        private TypeExpression ret;

        /// <summary>
        /// Stores the type expressions of the parameters list.
        /// </summary>
        private List<TypeExpression> paramList = new List<TypeExpression>();

        /// <summary>
        /// Links to attribute information (modifiers and its class type)
        /// </summary>
        private AccessModifier memberInfo;

        /// <summary>
        /// A set of constraints to be satisfied by method invocation
        /// </summary>
        private ConstraintList constraints = new ConstraintList();

        /// <summary>
        /// The AST node set by the VisitorTypeDefinition and used in the VisitorTypeInferece for abstract interpretation purposes
        /// </summary>
        private MethodDeclaration astNode;

        /// <summary>
        /// A cache of the GetNumberVariables method
        /// </summary>
        private int numberFreeVariablesCache;

        /// <summary>
        /// To know if the numberFreeVariablesCache value is valid
        /// </summary>
        private bool validNumberFreeVariables;

        /// <summary>
        /// Its true when the type of the method has been completely inferred.
        /// Used to detect loops in the abstract interpretation mechanism.
        /// </summary>
        private bool isTypeInferred = false;

        /// <summary>
        /// The observer set that is waiting for the type inference mechanism to
        /// be completed.
        /// Key (MethodType): The observer that is waiting
        /// Value (TypeVariable): The returned type variable that the observer uses to
        ///       represent the return type of the current method type
        /// </summary>
        private IDictionary<MethodType, TypeVariable> observerCollection;

        #endregion

        #region Properties

        public TypeExpression getParam (int index) {
            return paramList[index]; 
        }
        /// <summary>
        /// Gets type expression method return
        /// </summary>
        public TypeExpression Return {
            get { return this.ret; }
            set {
                this.ret = value;
                this.ValidTypeExpression = this.memberInfo.Class.ValidTypeExpression = false;
            }
        }

        /// <summary>
        /// Gets the number of parameters
        /// </summary>
        public int ParameterListCount {
            get { return this.paramList.Count; }
        }

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

        /// <summary>
        /// A set of constraints to be satisfied
        /// </summary>
        public ConstraintList Constraints {
            get { return this.constraints; }
        }

        /// <summary>
        /// The AST node set by the VisitorTypeDefinition and used in the VisitorTypeInferece for abstract interpretation purposes
        /// </summary>
        public MethodDeclaration ASTNode {
            get { return this.astNode; }
            set { this.astNode = value; }
        }

        /// <summary>
        /// To implement a type expression cache
        /// </summary>
        internal override bool ValidTypeExpression {
            set {
                validTypeExpression = value;
                if (!value) {
                    // * Updates the full name
                    this.BuildFullName();
                    // * Recalculates its class type expression
                    if (this.MemberInfo != null && this.MemberInfo.Class != null)
                        this.MemberInfo.Class.ValidTypeExpression = false;
                }
            }
        }

        /// <summary>
        /// Its true when the type of the method has been completely inferred.
        /// Used to detect loops in the abstract interpretation mechanism.
        /// </summary>
        public bool IsTypeInferred {
            get { return this.isTypeInferred; }
        }
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of MethodType
        /// </summary>
        /// <param name="ret">Return type expression</param>
        public MethodType(TypeExpression ret) {
            this.ret = ret;
            this.BuildFullName();
        }
        #endregion

        #region Dispatcher
        public override object AcceptOperation(TypeSystemOperation op, object arg) { return op.Exec(this, arg); }
        #endregion

        #region AddParameter()
        /// <summary>
        /// Adds a new parameter type.
        /// </summary>
        /// <param name="paramType">Parameter type.</param>
        public void AddParameter(TypeExpression paramType) {
            this.paramList.Add(paramType);
        }
        #endregion

        #region GetParameter()

        /// <summary>
        /// Gets the type expression of the specific parameter
        /// </summary>
        /// <param name="index">Index of the parameter</param>
        /// <returns>WriteType of parameter</returns>
        public TypeExpression GetParameter(int index) {
            if ((index >= 0) && (index < this.paramList.Count))
                return this.paramList[index];
            return null;
        }

        #endregion

        #region AddConstraint()
        /// <summary>
        /// Adds a new constraint to the method.
        /// </summary>
        /// <param name="constraint">The constraint.</param>
        public void AddConstraint(Constraint constraint) {
            //if (constraint is CloneConstraint)
            //{
            //    CloneConstraint cloneConstraint = ((CloneConstraint)constraint);
            //    foreach (var addedContraint in this.constraints.Constraints)
            //    {
            //        if (addedContraint is CloneConstraint)
            //        {
            //            CloneConstraint addedCloneConstraint = ((CloneConstraint)addedContraint);
            //            if (addedCloneConstraint.ReturnType.Equals(cloneConstraint.FirstOperand))
            //            {
            //                if (addedCloneConstraint.FirstOperand is TypeVariable && ((TypeVariable)addedCloneConstraint.FirstOperand).Substitution == null)
            //                {
            //                    this.constraints.Add(new CloneConstraint(addedCloneConstraint.FirstOperand));
            //                    return;
            //                }
            //            }
            //        }

            //    }
            //}
            this.constraints.Add(constraint);
        }
        #endregion

        #region AddConstraint()
        /// <summary>
        /// Removes a constraint to the method.
        /// </summary>
        /// <param name="index">The index of the constraint.</param>
        public void RemoveConstraint(int index) {
            this.constraints.Constraints.RemoveAt(index);
        }
        #endregion

        #region BuildTypeExpressionString()

        /// <summary>
        /// Creates the type expression and the full name string.
        /// </summary>
        public override string BuildTypeExpressionString(int depthLevel) {
            if (this.ValidTypeExpression) return this.typeExpression;
            if (depthLevel <= 0) return this.FullName;

            StringBuilder tE = new StringBuilder();
            // tE: Method(IdClass, IdMethod, mods, params -> ret)
            if (this.MemberInfo != null) {
                tE.AppendFormat("Method({0}, {1},", this.MemberInfo.Class.FullName, this.MemberInfo.MemberIdentifier);
                // modifiers
                if (this.MemberInfo.Modifiers.Count != 0) {
                    for (int i = 0; i < this.MemberInfo.Modifiers.Count - 1; i++)
                        tE.AppendFormat(" {0} x", this.MemberInfo.Modifiers[i]);
                    tE.AppendFormat(" {0}", this.MemberInfo.Modifiers[this.MemberInfo.Modifiers.Count - 1]);
                }
                tE.Append(", ");
            }

            // type
            tE.Append("(");
            if (this.paramList.Count > 0)
                for (int i = 0; i < this.paramList.Count; i++) {
                    tE.AppendFormat("{0}", this.paramList[i].BuildTypeExpressionString(depthLevel - 1));
                    if (i < this.paramList.Count - 1)
                        tE.Append(" x ");
                }
            else
                tE.Append("void");
            tE.Append(")->");
            if (this.ret != null)
                tE.Append(this.ret.BuildTypeExpressionString(depthLevel - 1));
            else
                tE.Append("void");
            // * Constraints
            tE.Append(", ");
            for (int i = 0; i < this.Constraints.Count; i++) {
                tE.Append(this.Constraints[i].ToString());
                if (i < this.Constraints.Count - 1)
                    tE.Append(" x ");
            }
            tE.Append(")");
            this.ValidTypeExpression = true;
            return this.typeExpression = tE.ToString();
        }
        #endregion

        #region BuildFullName()
        /// <summary>
        /// Creates/Updates the full name of the type expression
        /// </summary>
        public override void BuildFullName() {
            StringBuilder fN = new StringBuilder();
            if (this.memberInfo != null)
                fN.AppendFormat("{0}.{1}:", this.MemberInfo.Class.FullName, this.MemberInfo.MemberIdentifier);
            fN.Append("(");
            if (this.paramList.Count > 0)
                for (int i = 0; i < this.paramList.Count; i++) {
                    fN.AppendFormat("{0}", this.paramList[i].FullName);
                    if (i < this.paramList.Count - 1)
                        fN.Append(" x ");
                }
            else fN.Append("void");
            fN.Append(")->");
            if (this.ret != null)
                fN.Append(this.ret.FullName);
            else fN.Append("void");
            fN.Append(")");
            this.fullName = fN.ToString();
        }
        #endregion

        // WriteType inference

        #region Equivalent()
        ///// <summary>
        ///// WriteType equivalence. Tells us if two types are the same 
        ///// </summary>
        ///// <param name="type">The other type</param>
        ///// <returns>True if the represent the same type</returns>
        //public override bool Equivalent(TypeExpression type) {
        //    if (this == type)
        //        return true;
        //    TypeVariable typeVariable = type as TypeVariable;
        //    if (typeVariable != null)
        //        return typeVariable.Equivalent(this);
        //    MethodType method = type as MethodType;
        //    // * It must be a method
        //    if (method == null)
        //        return false;
        //    // * Same name
        //    if (!this.memberInfo.MemberIdentifier.Equals(method.memberInfo.MemberIdentifier))
        //        return false;
        //    // * Same class
        //    if (!this.MemberInfo.Class.Equivalent(method.MemberInfo.Class))
        //        return false;
        //    // * Same signature
        //    if (this.ParameterListCount != method.ParameterListCount)
        //        return false;
        //    for (int i = 0; i < this.ParameterListCount; i++)
        //        if (!this.GetParameter(i).Equivalent(method.GetParameter(i)))
        //            return false;
        //    return true;
        //}
        #endregion


        #region Promotion (Overloaded)

        /// <summary>
        /// Gets the promotion value of the method.
        /// </summary>
        /// <param name="arguments">Arguments of the method.</param>
        /// <param name="fileName">File name.</param>
        /// <param name="line">Line number.</param>
        /// <param name="column">Column number.</param>
        /// <returns>Promotion value.</returns>
        public int Promotion(TypeExpression[] arguments, Location location) {
            int promotion = 0;
            int aux;

            // Has been any error in the arguments?
            if (arguments == null)
                return this.paramList.Count == 0 ? 0 : -1;

            // Check the argument number
            if (arguments.GetLength(0) != this.ParameterListCount)
                return -1;

            // Check the argument type
            for (int i = 0; i < this.ParameterListCount; i++) {
                if ((aux = (int)arguments[i].AcceptOperation( new PromotionLevelOperation(this.paramList[i]), null)) != -1)
                    promotion += aux;
                else
                    return -1;
            }
            return promotion;
        }

        #endregion

        #region EqualsForOverload() 
        /// <summary>
        /// Used to not repeat methods in overload
        /// </summary>
        /// <param name="typeExpression">The other type expression</param>
        /// <returns>If both represent the same type</returns>
        public virtual bool EqualsForOverload(TypeExpression typeExpression) {
            return (bool) this.AcceptOperation(new EqualsForOverloadOperation(typeExpression), null);
        }
        #endregion

        #region GetNumberFreeVariables()
        /// <summary>
        /// To know the number of parameters that has type variables.
        /// This is used for overload resolution.
        /// </summary>
        /// <returns>The number of type variables in its parameters</returns>
        public int GetNumberFreeVariables() {
            if (this.validNumberFreeVariables)
                return this.numberFreeVariablesCache;
            int number = 0;
            foreach (TypeExpression type in this.paramList)
                if (type.HasTypeVariables())
                    number++;
            this.validNumberFreeVariables = true;
            return this.numberFreeVariablesCache = number++;
        }
        #endregion

        // WriteType unification

        #region Unify
        /// <summary>
        /// This method unifies two type expressions (this and te)
        /// </summary>
        /// <param name="te">The expression to be unfied with this</param>
        /// <param name="unification">Indicates if the kind of unification (equivalent, incremental or override).</param>
        /// <param name="previouslyUnified">To detect infinite loops. The previously unified pairs of type expressions.</param>
        /// <returns>If the unification was successful</returns>
        public override bool Unify(TypeExpression te, SortOfUnification unification, IList<Pair<TypeExpression, TypeExpression>> previouslyUnified) {
            MethodType mt = te as MethodType;
            if (mt != null) {
                if (mt.ParameterListCount != this.ParameterListCount)
                    return false;
                bool success = true;
                for (int i = 0; i < this.ParameterListCount; i++)
                    if (!this.paramList[i].Unify(mt.GetParameter(i), unification, previouslyUnified)) {
                        success = false;
                        break;
                    }
                if (success)
                    success = this.Return.Unify(mt.Return, unification, previouslyUnified);
                // * Clears the type expression cache
                this.ValidTypeExpression = false;
                te.ValidTypeExpression = false;
                return success;
            }
            if (te is TypeVariable && unification != SortOfUnification.Incremental)
                // * No incremental unification is commutative
                return te.Unify(this, unification, previouslyUnified);
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
            bool toReturn = this.ret.HasTypeVariables();
            if (!toReturn)
                foreach (TypeExpression type in this.paramList)
                    if (type.HasTypeVariables()) {
                        toReturn = true;
                        break;
                    }
            this.validHasTypeVariables = true;
            return this.hasTypeVariablesCache = toReturn;
        }
        #endregion

        #region CloneMethodType()
        /// <summary>
        /// This method returns a new method type, creating new type variables for
        /// each parametern and the returned value. It also clones its class (without methods).
        /// It these type variables where bounded to types or other
        /// type variables, they are maintained. It also maintain its substitutions.
        /// </summary>
        /// <param name="typeVariableMappings">Each new type varaiable represent a copy of another existing one.
        /// This parameter is a mapping between them, wher tmpName=old and value=new.</param>
        /// <returns>The new cloned method type</returns>
        public MethodType CloneMethodType(IDictionary<TypeVariable, TypeVariable> typeVariableMappings) {
            // * We clone the members of the MethodType
            MethodType methodType = new MethodType(null);
            List<TypeExpression> oldParameterList = this.paramList;
            methodType.paramList = new List<TypeExpression>();
            // * We must create new type variables for parameters
            IList<EquivalenceClass> equivalenceClasses = new List<EquivalenceClass>();
            for (int i = 0; i < this.paramList.Count; i++)
                // * Every type variable is cloned to a new one, adding both to typesVariables,
                //   inserting its equivalence classes in the equivalenceClasses parameter and updating the
                //   typeVariableMappings dictionary (<oldvariable,newVariable>
                methodType.paramList.Add(oldParameterList[i].CloneTypeVariables(typeVariableMappings, equivalenceClasses, new List<ClassType>()));
            // * The same for the returned value
            methodType.ret = this.ret.CloneTypeVariables(typeVariableMappings, equivalenceClasses, new List<ClassType>());
            // * Member info (access modifier)
            AccessModifier accessModifier = (AccessModifier)this.MemberInfo.Clone();
            accessModifier.Type = methodType;
            methodType.MemberInfo = accessModifier;
            // * We also have to clone the constraints
            methodType.constraints = (ConstraintList)this.constraints.CloneTypeVariables(typeVariableMappings, equivalenceClasses);
            // * For each equivalence class we create a new one, 
            //   substituting the old type variables for the new ones
            // * The substitution is not altered
            // * Since equivalence classes and type variables have a bidirectional association,
            //   the new equivalence classes will make type variables update their new equivalence classes
            foreach (EquivalenceClass equivalenceClass in equivalenceClasses)
                equivalenceClass.UpdateEquivalenceClass(typeVariableMappings);
            methodType.ValidTypeExpression = false;
            // * The new method type is returned
            return methodType;
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
            // * Methods are not cloned
            return this;
        }
        #endregion

        // Loop Detection

        // Helper Methods

        #region methodCall()

        /// <summary>
        /// This method does the type inference in a method call including unification.
        /// It requires that a) the method to be invoked has been previously analyzed with this visitor
        /// b) The formalMethod parameter is the result of the overload resolution
        /// </summary>
        /// <param name="actualImplicitObject">The actual implicit object</param>
        /// <param name="formalMethod">The formal method to be called</param>
        /// <param name="args">The ordered types of the actual parameters</param>
        /// <param name="methodAnalyzed">The method that is being analyzed when the operation is performed.</param>
        /// <param name="activeSortOfUnification">The active sort of unification used (Equivalent is the default
        /// one and Incremental is used in the SSA bodies of the while, for and do statements)</param>
        /// <param name="fileName">File name.</param>
        /// <param name="line">Line number.</param>
        /// <param name="column">Column number.</param>
        /// <returns>The type expression of the returned value</returns>
        public static TypeExpression methodCall(TypeExpression actualImplicitObject, MethodType formalMethod, TypeExpression[] args,
                  MethodType methodAnalyzed, SortOfUnification activeSortOfUnification, Location location) {
            UserType userType = formalMethod.MemberInfo.Class;
            MethodType actualMethod = null;
            // * We must create a new type with type variables for the object's attributes (the formal implicit object)
            IDictionary<TypeVariable, TypeVariable> typeVariableMappings = new Dictionary<TypeVariable, TypeVariable>();

            // * If the method is an instance one and the actual object is not this, we create a new implicit object to unify
            if (!formalMethod.MemberInfo.hasModifier(Modifier.Static) && actualImplicitObject != null && actualImplicitObject != methodAnalyzed.memberInfo.Class) {
                // * Unifies the implicit objects (actual and formal)
                UserType formalImplicitObject = (UserType)userType.CloneType(typeVariableMappings);
                if (!actualImplicitObject.Unify(formalImplicitObject, SortOfUnification.Equivalent, new List<Pair<TypeExpression, TypeExpression>>()))
                    // * If the formal implicit object already has substitution (fields declararion with assignments), we override it with a union type
                    formalImplicitObject.Unify(actualImplicitObject, SortOfUnification.Override, new List<Pair<TypeExpression, TypeExpression>>());
                actualImplicitObject.ValidTypeExpression = false;
            }

            // * If "this" is the actual implicit object, the return type is the original return type of the method
            TypeExpression originalReturnType = formalMethod.Return;

            if (formalMethod.HasTypeVariables() || formalMethod.Constraints.Count > 0)
                // * We must also generate a method with fresh variables (formal method)
                //   when it has parameters with type variables or constraints
                formalMethod = formalMethod.CloneMethodType(typeVariableMappings);

            // * If the method has type variables... 
            if (formalMethod.HasTypeVariables()) {
                // * We create the actual method:
                //   1.- The actual return type
                TypeVariable actualReturnType = TypeVariable.NewTypeVariable;
                //   2.- The actual method
                actualMethod = new MethodType(actualReturnType);
                //   3.- The actual parameters
                foreach (TypeExpression arg in args)
                    actualMethod.AddParameter(arg);

                // * Unifies both methods
                if (!actualMethod.Unify(formalMethod, SortOfUnification.Equivalent, new List<Pair<TypeExpression, TypeExpression>>())) {
                    ErrorManager.Instance.NotifyError(new UnificationError(actualMethod.FullName, location));
                    return null;
                }
            }
            // * Otherwise, arguments promotion must be checked
            else {
                if (args.Length != formalMethod.ParameterListCount) {
                    ErrorManager.Instance.NotifyError(new ArgumentNumberError(formalMethod.FullName, args.Length, location));
                    return null;
                }
                for (int i = 0; i < args.Length; i++)
                    if (args[i].AcceptOperation(PromotionOperation.Create(formalMethod.paramList[i], methodAnalyzed, location), null) == null)
                        return null;
            }

            // * Method constraints satisfaction            
            if (formalMethod.Constraints.Count > 0)
                formalMethod.Constraints.Check(methodAnalyzed, actualImplicitObject, true, activeSortOfUnification, location);


            // * The returned type is the the actual method if there has been a unification and
            //   in case the method is a instance method, a concrete object has been used (not this) or 
            //   a different implicit object (the this reference is changed in the SSA algorithm)
            if (actualMethod != null && (formalMethod.MemberInfo.hasModifier(Modifier.Static) ||
                                         ClassType.IsConcreteType(actualImplicitObject) != null ||
                                         actualImplicitObject != formalMethod.MemberInfo.Class)) {
                TypeVariable returnType = (TypeVariable)actualMethod.Return;
                if (returnType.Substitution != null)
                    return returnType.EquivalenceClass.Substitution;
                return returnType;
            }
            // * The original returned type if there has been no unification or the implicit object is "this"
            return originalReturnType;
        }

        #endregion

        #region AddInferredTypeObserver()

        /// <summary>
        /// Adds a new observer that listens to the event of inferred types
        /// </summary>
        /// <param name="method"></param>
        /// <param name="returnType"></param>
        public void addInferredTypeObserver(MethodType method, TypeVariable returnType) {
            if (this.observerCollection == null)
                this.observerCollection = new Dictionary<MethodType, TypeVariable>();
            this.observerCollection[method] = returnType;
        }

        #endregion

        #region TypeInferred()

        /// <summary>
        /// This method is used to control loops (recursion)
        /// </summary>
        public void TypeInferred() {
            this.isTypeInferred = true;
            // * Iterates through all the observers, updating the new type inferred
            if (this.observerCollection != null) {
                foreach (KeyValuePair<MethodType, TypeVariable> pair in this.observerCollection) {
                    // * Once the type is inferred, we add it to the caller (the observer)
                    pair.Key.Return = UnionType.collect(pair.Key.Return, this.Return);
                    // * The type variable used for the return type in the method call is unified
                    //   with the actual type (once inferred), excluding itself
                    this.Return.Remove(pair.Value);
                    //if (pair.Key.Return is TypeVariable)
                    //  this.Return.Remove((TypeVariable)pair.Key.Return);
                    pair.Value.Unify(this.Return, SortOfUnification.Equivalent, new List<Pair<TypeExpression, TypeExpression>>());
                }
            }
        }

        #endregion

        #region IsValueType()

        /// <summary>
        /// True if type expression is a ValueType. Otherwise, false.
        /// </summary>
        /// <returns>Returns true if the type expression is a ValueType. Otherwise, false.</returns>
        public override bool IsValueType() {
            return this.ret.IsValueType();
        }

        #endregion

    }
}

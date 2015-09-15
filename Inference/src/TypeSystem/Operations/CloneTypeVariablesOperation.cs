using System.Collections.Generic;
using Tools;
namespace TypeSystem.Operations {
    public class CloneTypeVariablesOperation : Operation {
        protected IDictionary<TypeVariable, TypeVariable> typeVariableMappings;
        protected IList<EquivalenceClass> equivalenceClasses;
        protected IList<ClassType> clonedClasses;
        public CloneTypeVariablesOperation(IDictionary<TypeVariable, TypeVariable> typeVariableMappings, IList<EquivalenceClass> equivalenceClasses, IList<ClassType> clonedClasses) {
            this.typeVariableMappings = typeVariableMappings; ;
            this.equivalenceClasses = equivalenceClasses;
            this.clonedClasses = clonedClasses;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="te">The type expression which type variables we want to clone</param>
        /// <returns>The new type expression, argument te<returns>
        public override object Exec(TypeExpression te) {
            return te;
        }

        /// <summary>
        /// Method that clones each type variable of a type expression.
        /// Equivalence classes are not cloned (but included in the equivalenceClasses parameter.
        /// The default implementation is do nothing (for built-in types).
        /// </summary>
        public override object Exec(TypeVariable tv) {
            // * If the variable to be cloned already has a map, we return the latter
            if (this.typeVariableMappings.ContainsKey(tv))
                return this.typeVariableMappings[tv];
            TypeVariable newOne = TypeVariable.NewTypeVariable;
            newOne.IsDynamic = tv.IsDynamic;
            // * Sets the mapping between the old and new one in the typeVariableMappings parameter
            this.typeVariableMappings[tv] = newOne;
            // * Add both equivalence classes to the equivalenceClasses parameter
            if (tv.EquivalenceClass != null && !this.equivalenceClasses.Contains(tv.EquivalenceClass))
                this.equivalenceClasses.Add(tv.EquivalenceClass);
            // * Assigns a clone of the substitution when it previously exists
            if (tv.Substitution != null) {
                TypeExpression clonedSubstitution = (TypeExpression)tv.Substitution.Exec(this);
                newOne.addToMyEquivalenceClass(clonedSubstitution, SortOfUnification.Equivalent, new List<Pair<TypeExpression, TypeExpression>>());
            }
            else if (tv.EquivalenceClass != null)
                newOne.EquivalenceClass = tv.EquivalenceClass;
            newOne.ValidTypeExpression = false;
            // * Returns the new type variable
            return newOne;
        }
        public override object Exec(ClassTypeProxy tp) {
            return tp.RealType.Exec(this);
        }

        public override object Exec(IntersectionType it) {
            if (!it.HasTypeVariables())
                return it;
            IntersectionType newType = (IntersectionType)it.MemberwiseClone();
            IList<TypeExpression> oldTypeSet = it.TypeSet;
            newType.TypeSet = new List<TypeExpression>();
            foreach (TypeExpression oldType in oldTypeSet)
                newType.TypeSet.Add((IntersectionType)oldType.Exec(this));
            newType.ValidTypeExpression = false;
            return newType;
        }
        public override object Exec(ArrayType at) {
            if (!at.HasTypeVariables())
                return at;
            ArrayType newType = (ArrayType)at.MemberwiseClone();
            newType.ArrayTypeExpression = (ArrayType)newType.ArrayTypeExpression.Exec(this);
            newType.ValidTypeExpression = false;
            return newType;
        }
        public override object Exec(ClassType ct) {
                        // * Without type variables, no clone is necessary
            if (!ct.HasTypeVariables())
                return ct;
            // * Let's check recursion
            if (!this.clonedClasses.Contains(ct))
                this.clonedClasses.Add(ct);
            else // * Recursion
                return new ClassTypeProxy(ct, this.typeVariableMappings, this.equivalenceClasses);
            // * We clone the members of the ClassType
            ClassType newClassType = new ClassType(ct.FullName);
            newClassType.ConcreteType = ct.ConcreteType;
            newClassType.IsDynamic = ct.IsDynamic;
            Dictionary<string, AccessModifier> oldFields = ct.Fields;
            newClassType.fieldList = new Dictionary<string, AccessModifier>();
            // * We must create new type variables for fields
            foreach (KeyValuePair<string, AccessModifier> pair in oldFields) {
                // * Every type variable is cloned to a new one, adding both to typesVariables,
                //   inserting its equivalence classes in the equivalenceClasses parameter and updating the
                //   typeVariableMappings dictionary (<oldvariable,newVariable>
                FieldType fieldType = (FieldType)pair.Value.Type.Exec(this);
                    fieldType.MemberInfo.Class = newClassType;
                newClassType.AddMember(pair.Key, fieldType.MemberInfo);
            }
            // * We must create new attribute info (access modifiers) but without clonning methods
            foreach (KeyValuePair<string, AccessModifier> pair in ct.Methods) {
                AccessModifier accesModifier = (AccessModifier)pair.Value.Clone();
                accesModifier.Class = newClassType;
                newClassType.AddMember(pair.Key, accesModifier);
            }
            // * The same with the base class
            newClassType.baseClass = (ClassType)ct.baseClass.CloneType(typeVariableMappings);
            newClassType.ValidTypeExpression = false;
            // * The new class type is returned
            return newClassType;
        }
        
        public override object Exec(UnionType ut) {
            if (!ut.HasTypeVariables())
                return ut;
            UnionType newType = (UnionType)ut.MemberwiseClone();
            IList<TypeExpression> oldTypeSet = ut.typeSet;
            newType.typeSet = new List<TypeExpression>();
            foreach (TypeExpression oldType in oldTypeSet)
                newType.typeSet.Add(oldType.Exex(this));
            newType.ValidTypeExpression = false;
            return newType;
        
        }
        public override object Exec(FieldType ft) {

            if (!ft.HasTypeVariables())
                return ft;
            AccessModifier accesModifier = (AccessModifier)ft.MemberInfo.Clone();
            FieldType fieldType = new FieldType(ft.FieldTypeExpression.Exec(this));
            accesModifier.Class = ft.MemberInfo.Class;
            accesModifier.Type = fieldType;
            fieldType.IsDynamic = ft.IsDynamic;
            fieldType.MemberInfo = accesModifier;
            ft.ValidTypeExpression = fieldType.ValidTypeExpression = false;
            return fieldType;
        }

        #region Report Errors
        // in our case we only notify operations not allowed
        // for other pruposes invoke explicitly another kind of error
        public override object ReportError(TypeExpression tE) {
            if (this.showErrorMessage)
                ErrorManager.Instance.NotifyError(new OperationNotAllowedError("hola cara cola", "this.binaryOperator.ToString()W", "tE.FullName", "this.secondOperand.FullName", "this.location"));
            return null;
        }
        #endregion
    }
        }
    
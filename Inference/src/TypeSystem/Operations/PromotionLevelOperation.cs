using System.Collections.Generic;
using DynVarManagement;
using System;
namespace TypeSystem.Operations {
    /// <summary>
    /// Its operation AcceptOperation() returns an integer value that indicates a promotion level between two types.
    /// That is the number of gaps represented as levels between two types 
    /// The static behaviour is
    ///     T1\/T2 <= X    iff   T1<=X and T2<=X
    /// The dynamic behaviour is
    ///     T1\/T2 <= X    iff   T1<=X or T2<=X
    /// </summary>
    /// </summary>
    ///

    public class PromotionLevelOperation : TypeSystemOperation {

        #region Fields
        protected TypeExpression secondOperand;
        #endregion

        #region Constructors
        public PromotionLevelOperation(TypeExpression secondOperand) {
            this.secondOperand = secondOperand;
        }
        #endregion

        #region Array-->#
        //// <summary>
        /// Returns an int value encapsulated in an object item that indicates a promotion level.
        /// </summary>
        /// <param name="firstOperand">An array to promote.</param>
        /// <returns>Returns a promotion value.</returns>
        public override object Exec(ArrayType firstOperand, object arg) {
            // * Array and bounded type variable
            ArrayType array = TypeExpression.As<ArrayType>(this.secondOperand);
            if (array != null)
                return firstOperand.ArrayTypeExpression.Equals(array.ArrayTypeExpression) ? 0 : -1;
            // * A free variable is a complete promotion
            TypeVariable typeVariable = this.secondOperand as TypeVariable;
            if (typeVariable != null && typeVariable.Substitution == null)
                return 0;
            // * Union type and bounded type variable
            UnionType unionType = TypeExpression.As<UnionType>(this.secondOperand);
            if (unionType != null)
                return unionType.SuperType(firstOperand);
            // * Field type and bounded type variable
            FieldType fieldType = TypeExpression.As<FieldType>(this.secondOperand);
            if (fieldType != null)
                return firstOperand.AcceptOperation(new PromotionLevelOperation(fieldType.FieldTypeExpression), arg);
            // * Use the BCL object oriented approach
            return firstOperand.AsClassType().AcceptOperation(this, arg);
        }
        #endregion

        #region Bool-->#
        /// <summary>
        /// Returns a value that indicates a promotion level.
        /// </summary>
        /// <param name="firstOperand">WriteType to promotion.</param>
        /// <returns>Returns a promotion value.</returns>
        public override object Exec(BoolType firstOperand, object arg) {
            // * Bool type and type variable
            if (TypeExpression.As<BoolType>(this.secondOperand) != null)
                return 0;

            // * WriteType variable
            TypeVariable typeVariable = this.secondOperand as TypeVariable;

            if (typeVariable != null && typeVariable.Substitution == null)
                // * A free variable is complete promotion
                return 0;

            // * Union type
            UnionType unionType = TypeExpression.As<UnionType>(this.secondOperand);
            if (unionType != null)
                return unionType.SuperType(firstOperand);

            // * Field type and bounded type variable
            FieldType fieldType = TypeExpression.As<FieldType>(this.secondOperand);
            if (fieldType != null)
                return firstOperand.AcceptOperation(new PromotionLevelOperation(fieldType.FieldTypeExpression), arg);

            // * Use the BCL object oriented approach
            return firstOperand.AsClassType().AcceptOperation(this, arg);
        }
        #endregion

        #region ClasType-->#
        public override object Exec(ClassType firstOperand, object arg) {
            int aux, less = -1;
            EquivalentOperation equivalentType = new EquivalentOperation(this.secondOperand);

            // * The same type
            if (firstOperand == this.secondOperand)
                return 0;
            // * Equivalent types
            if ((bool)firstOperand.AcceptOperation(equivalentType, arg))
                return 0;

            // * Field type and bounded type variable
            FieldType fieldType = TypeExpression.As<FieldType>(this.secondOperand);
            if (fieldType != null)
                return firstOperand.AcceptOperation(new PromotionLevelOperation(fieldType.FieldTypeExpression), arg);

            // * WriteType variable
            TypeVariable typeVariable = this.secondOperand as TypeVariable;
            if (typeVariable != null) {
                if (typeVariable.Substitution != null)
                    // * If the variable is bounded, the promotion is the one of its substitution
                    return firstOperand.AcceptOperation(new PromotionLevelOperation(typeVariable.EquivalenceClass.Substitution), arg);
                // * A free variable is complete promotion
                return 0;
            }

            // * Inheritance
            if (firstOperand.BaseClass == null)
                // * Object only promotes to object
                return -1;
            if ((bool)firstOperand.BaseClass.AcceptOperation(equivalentType, arg))
                return 1;
            else {
                //aux = firstOperand.baseClass.AcceptOperation(new PromotionLevelOperation(this.secondOperand)));
                aux = (int)firstOperand.BaseClass.AcceptOperation(this, arg);
                if (aux != -1)
                    return aux + 1;
            }

            // * Interfaces
            if (firstOperand.InterfaceList.Count != 0) {
                for (int i = 0; i < firstOperand.InterfaceList.Count; i++) {
                    if ((bool)firstOperand.InterfaceList[i].AcceptOperation(equivalentType, arg)) {
                        if (less > 1 || less == -1)
                            less = 1;
                    } else {
                        aux = (int)firstOperand.InterfaceList[i].AcceptOperation(this, arg);
                        if (aux != -1) {
                            if (less > aux + 1 || less == -1)
                                less = aux + 1;
                        }
                    }
                }
            }
            if (less != -1)
                return less;

            // * Union type
            UnionType unionType = TypeExpression.As<UnionType>(this.secondOperand);
            if (unionType != null)
                return unionType.SuperType(firstOperand);

            // * No promotion
            return -1;
        }
        #endregion

        #region ClassTypeProxy-->#
        public override object Exec(ClassTypeProxy firstOperand, object arg) {
            return firstOperand.RealType.AcceptOperation(this, arg); //OJO CON LA RECURSIVIDAD
        }
        #endregion

        #region Char->#
        public override object Exec(CharType firstOperand, object arg) {
            // * Char type and type variable            
            if (TypeExpression.As<CharType>(this.secondOperand) != null)
                return 0;
            // * Int type and type variable
            if (TypeExpression.As<IntType>(this.secondOperand) != null)
                return 1;
            // * Double type and type variable
            if (TypeExpression.As<DoubleType>(this.secondOperand) != null)
                return 2;
            // * Free type variables
            TypeVariable typeVariable = this.secondOperand as TypeVariable;
            if (typeVariable != null && typeVariable.Substitution == null)
                // * A free variable is complete promotion
                return 0;
            // * Union type
            UnionType unionType = TypeExpression.As<UnionType>(this.secondOperand);
            if (unionType != null)
                return unionType.SuperType(firstOperand);
            // * Field type and bounded type variable
            FieldType fieldType = TypeExpression.As<FieldType>(this.secondOperand);
            if (fieldType != null)
                return firstOperand.AcceptOperation(new PromotionLevelOperation(fieldType.FieldTypeExpression), arg);
            // * Use the BCL object oriented approach
            //return firstOperand.BCLType.AcceptOperation(new PromotionLevelOperation(this.secondOperand)));
            return firstOperand.AsClassType().AcceptOperation(this, arg);
        }

        #endregion


        #region Double-->#
        public override object Exec(DoubleType firstOperand, object arg) {
            // * Double type and bounded type variable
            if (TypeExpression.As<DoubleType>(this.secondOperand) != null)
                return 0;
            // * WriteType variable
            TypeVariable typeVariable = this.secondOperand as TypeVariable;
            if (typeVariable != null && typeVariable.Substitution == null)
                // * A free variable is complete promotion
                return 0;
            // * Union type
            UnionType unionType = TypeExpression.As<UnionType>(this.secondOperand);
            if (unionType != null)
                return unionType.SuperType(firstOperand);
            // * Field type and bounded type variable
            FieldType fieldType = TypeExpression.As<FieldType>(this.secondOperand);
            if (fieldType != null)
                return firstOperand.AcceptOperation(new PromotionLevelOperation(fieldType.FieldTypeExpression), arg);
            // * Use the BCL object oriented approach
            return firstOperand.AsClassType().AcceptOperation(this, arg);
        }

        #endregion

        #region FieldType-->#
        public override object Exec(FieldType firstOperand, object arg) {
            if (firstOperand.FieldTypeExpression != null)
                return firstOperand.FieldTypeExpression.AcceptOperation(this, arg);
            return -1;
        }
        #endregion

        #region InterfaceType-->#
        public override object Exec(InterfaceType firstOperand, object arg) {
            int aux, less = -1;
            // * Equivalent types
            if ( (bool)firstOperand.AcceptOperation(new EquivalentOperation(this.secondOperand), arg) )
                less = 0;

            // * WriteType variable
            TypeVariable typeVariable = this.secondOperand as TypeVariable;
            if (typeVariable != null) {
                if (typeVariable.Substitution != null)
                    // * If the variable is bounded, the promotion is the one of its substitution
                    return firstOperand.AcceptOperation(new PromotionLevelOperation(typeVariable.EquivalenceClass.Substitution), arg);
                // * A free variable is complete promotion
                return 0;
            }

            // * Field type and bounded type variable
            FieldType fieldType = TypeExpression.As<FieldType>(this.secondOperand);
            if (fieldType != null)
                return firstOperand.AcceptOperation(new PromotionLevelOperation(fieldType.FieldTypeExpression), arg);

            // * Interface List
            if (firstOperand.InterfaceList.Count != 0) {
                for (int i = 0; i < firstOperand.InterfaceList.Count; i++) {
                    if ( (bool)firstOperand.InterfaceList[i].AcceptOperation(new EquivalentOperation(this.secondOperand), arg) ) {
                        if ((less > 1) || (less == -1))
                            less = 1;
                    } else {
                        aux = (int)firstOperand.InterfaceList[i].AcceptOperation(this, arg);
                        if (aux != -1) {
                            if (less > aux + 1 || less == -1)
                                less = aux + 1;
                        }
                    }
                }
            }
            if (less != -1)
                return less;

            // * Union type
            UnionType unionType = TypeExpression.As<UnionType>(this.secondOperand);
            if (unionType != null)
                return unionType.SuperType(firstOperand);

            // * No promotion
            return -1;
        }
        #endregion

        #region IntType-->#
        public override object Exec(IntType firstOperand, object arg) {
            // * Int type and bounded type variable
            if (TypeExpression.As<IntType>(this.secondOperand) != null)
                return 0;
            // * Double type and bounded type variable
            if (TypeExpression.As<DoubleType>(this.secondOperand) != null)
                return 1;
            // * WriteType variable
            TypeVariable typeVariable = this.secondOperand as TypeVariable;
            if (typeVariable != null && typeVariable.Substitution == null)
                return 0;
            // * Union type
            UnionType unionType = TypeExpression.As<UnionType>(this.secondOperand);
            if (unionType != null)
                return unionType.SuperType(firstOperand);
            // * Field type and bounded type variable
            FieldType fieldType = TypeExpression.As<FieldType>(this.secondOperand);
            if (fieldType != null)
                return firstOperand.AcceptOperation(new PromotionLevelOperation(fieldType.FieldTypeExpression), arg);
            // * Use the BCL object oriented approach
            return firstOperand.AsClassType().AcceptOperation(this, arg);
        }
        #endregion

        #region NullType-->#
        public override object Exec(NullType firstOperand, object arg) {
            // * Built-in types: no promotion, except string
            if (this.secondOperand is BoolType || this.secondOperand is CharType || this.secondOperand is DoubleType || this.secondOperand is IntType || this.secondOperand is VoidType)
                return -1;
            // * BCL Value Types (structs): No promotion
            BCLClassType bclClass = TypeExpression.As<BCLClassType>(this.secondOperand);
            if (bclClass != null) {
                if (bclClass.TypeInfo.IsValueType)
                    return -1;
                // * Correct promotion to classes that are not value types
                return 0;
            }
            // * WriteType variable
            TypeVariable typeVariable = this.secondOperand as TypeVariable;
            if (typeVariable != null) {
                if (typeVariable.Substitution != null)
                    // * If the variable is bounded, the promotion is the one of its substitution
                    return firstOperand.AcceptOperation(new PromotionLevelOperation(typeVariable.EquivalenceClass.Substitution), arg);
                // * A free variable is complete promotion
                return 0;
            }
            // * Union type
            UnionType unionType = TypeExpression.As<UnionType>(this.secondOperand);
            if (unionType != null)
                return unionType.SuperType(firstOperand);
            // * Field type and bounded type variable
            FieldType fieldType = TypeExpression.As<FieldType>(this.secondOperand);
            if (fieldType != null)
                return firstOperand.AcceptOperation(new PromotionLevelOperation(fieldType.FieldTypeExpression), arg);
            // * Correct Promotion
            return 0;
        }
        #endregion

        #region PropertyType-->#
        public override object Exec(PropertyType firstOperand, object arg) {
            if (firstOperand.PropertyTypeExpression != null)
                return firstOperand.PropertyTypeExpression.AcceptOperation(this, arg);
            return -1;
        }

        #endregion

        #region StringType-->#

        public override object Exec(StringType firstOperand, object arg) {
            // * String WriteType and bounded type variables
            if (TypeExpression.As<StringType>(this.secondOperand) != null)
                return 0;
            // * WriteType variable
            TypeVariable typeVariable = this.secondOperand as TypeVariable;
            if (typeVariable != null && typeVariable.Substitution == null)
                // * A free variable is complete promotion
                return 0;
            // * Union type
            UnionType unionType = TypeExpression.As<UnionType>(this.secondOperand);
            if (unionType != null)
                return unionType.SuperType(firstOperand);
            // * Field type and bounded type variable
            FieldType fieldType = TypeExpression.As<FieldType>(this.secondOperand);
            if (fieldType != null)
                return firstOperand.AcceptOperation(new PromotionLevelOperation(fieldType.FieldTypeExpression), arg);
            // * Use the BCL object oriented approach
            return firstOperand.AsClassType().AcceptOperation(this, arg);
        }

        #endregion


        #region TypeExpression-->#
        public override object Exec(TypeExpression type, object arg) {
            return -1;
        }
        #endregion


        #region TypeVariable-->#
        public override object Exec(TypeVariable firstOperand, object arg) {
            if (firstOperand.Substitution != null) {
                DynVarOptions.Instance.AssignDynamism(firstOperand.Substitution, firstOperand.IsDynamic);
                // * If the variable is bounded, the promotion is the one of its substitution
                return firstOperand.EquivalenceClass.Substitution.AcceptOperation(this, arg);
            }
            // * A free variable is complete promotion
            return 0;
        }
        #endregion

        #region UnionType-->#

        // * To detect recursive promotion level calls of recursive type variable expressions
        private static IList<UnionType> recursivePromotionLevelDetection = new List<UnionType>();

        public override object Exec(UnionType firstOperand, object arg) {
            // * Checks recursion
            if (recursivePromotionLevelDetection.Contains(firstOperand))
                return 0;
            recursivePromotionLevelDetection.Add(firstOperand);
            // * Static Behaviour: Takes the addition of all the promotion level
            // * Dynamic Behaviour: Takes the addition of all the promotion level
            int promotionLevel = firstOperand.IsDynamic ? Int32.MaxValue : 0;
            foreach (TypeExpression subType in firstOperand.TypeSet) {
                int aux = (int)subType.AcceptOperation(this, arg);
                if (firstOperand.IsDynamic && aux != -1) {
                    // * Dynamic
                    promotionLevel = Math.Min(promotionLevel, aux);
                    if (promotionLevel == 0)
                        break;
                }
                if (!firstOperand.IsDynamic) {
                    // * Static
                    if (aux == -1) {
                        promotionLevel = -1;
                        break;
                    }
                    promotionLevel += aux;
                }
            }
            if (firstOperand.IsDynamic && promotionLevel == Int32.MaxValue)
                // * No promotion at all
                promotionLevel = -1;
            // * Clears the loop detection for futures method calls
            recursivePromotionLevelDetection.Clear();
            return promotionLevel;
        }
        #endregion

        #region Void-->#
        public override object Exec(VoidType firstOperand, object arg) {
            return this.secondOperand is VoidType ? 0 : -1;
        }
        #endregion

        #region ReportError()
        public override object ReportError(TypeExpression firstOperand) {
            System.Diagnostics.Debug.Assert(false, "Error en código op1 = " + firstOperand + "OP2=" + this.secondOperand);
            return null;
        }
        #endregion
    }
}

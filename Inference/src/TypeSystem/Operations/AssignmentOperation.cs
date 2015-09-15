using System;
using TypeSystem.Operations;
using TypeSystem;
using ErrorManagement;
using AST;
using Tools;
using System.Collections.Generic;
using TypeSystem.Constraints;
namespace TypeSystem.Operations {
    /// <summary>
    /// Implements Double dispatcher Pattern.
    /// The class encapsulates encapsulates all it's needed to perform an assigment operationt, except the first argument that 
    /// is passed as argument with the message AcceptOperation() and done in the proper method.
    /// </summary>
    public class AssignmentOperation : TypeSystemOperation {
        /// <summary>
        /// The expression to assignate to the first operrand
        /// </summary>
        protected TypeExpression rightOperand;
        /// <summary>
        /// The kind of assignment to perform. That is, =, +=, -=,... etc
        /// </summary>
        protected AssignmentOperator op;
        /// <summary>
        /// The actual method being analysed.
        /// </summary>
        protected MethodType methodAnalyzed;
        /// <summary>
        /// Kind of unification to use: Equivalent, Incremental and Override.
        /// </summary>
        protected SortOfUnification unification;
        /// <summary>
        /// Only suitable when the assignment is executed as a constraint of a method call. In that case,
        ///// this parameter represents the actual object used to pass the message; null otherwise..
        /// </summary>
        protected TypeExpression actualImplicitObject;
        /// <param name="location">The location (file, line, column) of text being analyzed.</param>
        protected Location location;

        #region Constructor
        /// <summary>
        /// Costructor of the assignment operation.
        /// </summary>
        /// <param name="rightOperand">TypeExpression to be assigned to the stored firstOperand operand.</param>
        /// <param name="op">Kind o</param>
        /// <param name="methodAnalyzed">The Actual Method Being Analysed</param>
        /// <param name="unification">Kind of unification to use: Equivalent, Incremental and Override.</param>
        /// <param name="actualImplicitObject">The actual "this" objet we are visiting.</param>
        /// <param name="location">The location (file, line, column) of text being analyzed.</param>
        public AssignmentOperation(TypeExpression rightOperand, AssignmentOperator op, MethodType methodAnalyzed, SortOfUnification unification, TypeExpression actualImplicitObject, Location location) {
            this.rightOperand = rightOperand;
            this.op = op;
            this.methodAnalyzed = methodAnalyzed;
            this.unification = unification;
            this.actualImplicitObject = actualImplicitObject;
            this.location = location;
        }
        #endregion

        #region Array = ...
        /// <summary>
        /// Perform an assigment operation between an array and the typeExpression stored in this.rightOperand.
        /// The type to asign to the array must be esquivalent, and the only allowable operator is AssignmentOperator
        /// </summary>
        /// <param name="leftOperand">An array to use as leftOperand in an assignment operation</param>
        /// <returns>The typeExpression resulting of have been doing the operation. Or an error if there is.</returns>
        public override object Exec(ArrayType leftOperand, object arg) {

            if (this.op == AssignmentOperator.Assign && (bool)this.rightOperand.AcceptOperation(new EquivalentOperation(leftOperand), arg)) {
                if (!leftOperand.HasTypeVariables())
                    return leftOperand;
                // * Unification, meter una ternaria aqúi? igual muy queda la linea muy grande
                if (leftOperand.Unify(this.rightOperand, this.unification, new List<Pair<TypeExpression, TypeExpression>>()))
                    return leftOperand;
                else
                    return ReportError(leftOperand);
            }
            return ReportError(leftOperand);
        }

        #endregion

        #region Bool = ...
        /// <summary>
        /// Performs an assigment operation between an boolean type and the typeExpression stored in this.rightOperand.
        /// The type to asign to the array must be promotable to an BoolType, and the only allowable operator is AssignmentOperator
        /// </summary>
        /// <param name="leftOperand">A BoolType to use as leftOperand in an assignment operation</param>
        /// <returns>The typeExpression resulting of have been doing the operation. Or an error if there is.</returns>
        public override object Exec(BoolType leftOperand, object arg) {
            return this.op == AssignmentOperator.Assign // * if the operator is not assing raise and error, in other case, check if the second operand can be promotable to a BoolType
                ? rightOperand.AcceptOperation(PromotionOperation.Create(leftOperand, this.op, this.methodAnalyzed, this.location), arg)
                : ReportError(leftOperand);
        }

        #endregion


        #region Class = ...
        /// <summary>
        /// Performs an assigment operation between a Class type and the typeExpression stored in this.rightOperand.
        /// The type to asign to the array must be promotable to an ClassType, and the only allowable operator is AssignmentOperator
        /// </summary>
        /// <param name="leftOperand">The left operand of the assignmet operator.</param>
        /// <returns>A type expression one all checks and promotions are tried to make the assignment right.</returns>
        public override object Exec(ClassType leftOperand, object arg) { // the unique allowable operator
            if (this.op == AssignmentOperator.Assign) {
                if ( this.rightOperand.AcceptOperation(PromotionOperation.Create(leftOperand, this.op, this.methodAnalyzed, this.location), arg) == null )
                    return null;
                if (!leftOperand.HasTypeVariables())
                    return leftOperand;
                // * If the left expression of the assignment has type variables, 
                //   we must return the concrete type (not the abstract one)
                // * Then whe unify the concrete types
                FieldType fieldType = TypeExpression.As<FieldType>(this.rightOperand);
                if (fieldType != null)
                    this.rightOperand = fieldType.FieldTypeExpression;
                if (leftOperand.Unify(this.rightOperand, this.unification, new List<Pair<TypeExpression, TypeExpression>>())) {
                    leftOperand.ValidTypeExpression = false;
                    return leftOperand;
                }
            }
            ErrorManager.Instance.NotifyError(new OperationNotAllowedError(this.op.ToString(), leftOperand.FullName, rightOperand.FullName, this.location));
            return null;

        }

        #endregion

        #region ClassTypeProxy = ...
        /// <summary>
        /// Performs an assigment operation between a ClassTypeProxy type and the typeExpression stored in this.rightOperand.
        /// the result of the operation is the resulting operation of doing the AcceptOperation() over the method property RealtType.        
        /// </summary>
        /// <param name="leftOperand">It delegates the operatin to its proprety RealType</param>
        /// <returns>The result of applying the same operation exec, using leftOperand.RealType.AcceptOperation as left operand</returns>
        public override object Exec(ClassTypeProxy leftOperand, object arg) {
            return leftOperand.RealType.AcceptOperation(this, arg);
        }

        #endregion

        #region Char = ...
        /// <summary>
        /// Performs an assigment operation between a CharType type and the typeExpression stored in this.rightOperand.
        /// the result of the operation is the resulting operation of doing PromotionOperation over the rightOperation to a Char type.
        /// </summary>
        /// <param name="leftOperand">CharType assignable to secondOperand</param>
        /// <returns>The result of applying the operation</returns>
        public override object Exec(CharType leftOperand, object arg) {

            return rightOperand.AcceptOperation(PromotionOperation.Create(leftOperand, this.op, this.methodAnalyzed, this.location), arg);
        }

        #endregion
        /// <summary>
        /// Performs an assigment operation between a DoubleType type and the typeExpression stored in this.rightOperand.
        /// The result of the operation is the resulting operation of pass a message to the right operand doing a PromotionOperation with the operation over the class IntType.
        /// </summary>
        /// <param name="leftOperand">Double assignable to secondOperand</param>
        /// <returns>The result of applying the operation</returns>
        #region Double = ...

        public override object Exec(DoubleType leftOperand, object arg) {

            return rightOperand.AcceptOperation(PromotionOperation.Create(leftOperand, this.op, this.methodAnalyzed, this.location), arg);
        }

        #endregion

        #region FieldType = ...
        /// <summary>
        /// Performs an assigment operation between a FieldType type and the typeExpression stored in this.rightOperand.
        /// The result of the operation is the resulting operation of doing PromotionOperation the operation.
        /// </summary>
        /// <param name="leftOperand">A FieldType, the left operand of the assignment/param>
        /// <returns> The result of the operation is the resulting operation of doing The Assignment operation.
       ///</returns>
        public override object Exec(FieldType leftOperand, object arg) {
           
            // * We check if a constraint must be generated. Is it an assignment of the implicit object's field?
            bool found = false;
            // * In case it has free variables and the reference used is this, we add a constraint to the method
            if (leftOperand.HasTypeVariables() && this.methodAnalyzed != null &&
                ClassType.IsConcreteType(this.actualImplicitObject) == null)
            {
                // * They should be the same exact (sub)classes. This represent the same instance, not another instance of the same class.
                ClassType methodSuperClass = (ClassType) this.methodAnalyzed.MemberInfo.Class;
                while (!(found = (leftOperand.MemberInfo.Class == methodSuperClass)) && methodSuperClass != null)
                    methodSuperClass = methodSuperClass.BaseClass;
                if (found)
                {
                    // * An assignment constraint is added, postponing the type inference
                    // * If an actual implicit object is used, we take its field's type
                    FieldType fieldType = leftOperand;
                    ClassType thisType = TypeExpression.As<ClassType>(this.actualImplicitObject);
                    if (thisType == null)
                    {
                        FieldType field = TypeExpression.As<FieldType>(this.actualImplicitObject);
                        if (field != null)
                            thisType = TypeExpression.As<ClassType>(field.FieldTypeExpression);
                    }
                       
                    if (thisType != null)
                    {
                        while (thisType != null && !thisType.Fields.ContainsKey(leftOperand.MemberInfo.MemberIdentifier))
                        {
                            thisType = thisType.BaseClass;
                        }
                        if (thisType != null)
                            fieldType = (FieldType) thisType.Fields[leftOperand.MemberInfo.MemberIdentifier].Type;
                    }
                       
                    this.methodAnalyzed.AddConstraint(new FieldTypeAssignmentConstraint(fieldType, this.rightOperand,
                                                                                        this.unification));
                    this.methodAnalyzed.ValidTypeExpression = false;
                    if (leftOperand.FieldTypeExpression is TypeVariable && rightOperand is ArrayType)
                    {
                        TypeVariable typeVariable = (TypeVariable)leftOperand.FieldTypeExpression;
                        typeVariable.AcceptOperation(new AssignmentOperation(rightOperand, this.op, null, SortOfUnification.Override, null, this.location), arg);
                    }
                    else if (leftOperand.FieldTypeExpression is TypeVariable && rightOperand is TypeVariable && ((TypeVariable)rightOperand).Substitution == null && ((TypeVariable)rightOperand).IsDynamic)
                    {
                        TypeVariable typeVariable = (TypeVariable)leftOperand.FieldTypeExpression;
                        typeVariable.AcceptOperation(new AssignmentOperation(rightOperand, this.op, null, SortOfUnification.Override, null, this.location), arg);
                    }
                    return leftOperand.FieldTypeExpression;
                }
                if (leftOperand.FieldTypeExpression is TypeVariable)
                {
                    TypeVariable typeVariable = (TypeVariable) leftOperand.FieldTypeExpression;
                    if (typeVariable.Substitution != null)
                    {
                        return
                            leftOperand.FieldTypeExpression.AcceptOperation(
                                new AssignmentOperation(this.rightOperand, this.op, null,
                                                        SortOfUnification.Incremental, this.actualImplicitObject,
                                                        this.location), arg);
                    }
                }
            }

            if (leftOperand.FieldTypeExpression != null)
                return
                    leftOperand.FieldTypeExpression.AcceptOperation(
                        new AssignmentOperation(this.rightOperand, this.op, null, this.unification,
                                                this.actualImplicitObject, this.location), arg);

            return null;                     
        }

        #endregion

        #region InterfaceType = ...
        /// <summary>
        /// Performs an assigment operation between a Interface type and the typeExpression stored in this.rightOperand.
        /// the result of the operation is the resulting operation of doing PromotionOperation the operation over the subclass attached to he inteface type.
        /// Only is factible do this operation if op is an AssigmentOperation. In other case it raises an error.
        /// </summary>
        /// <param name="leftOperand">A IntefaceType, the left operand of the assignment/param>
        /// <returns> The result of the operation is the resulting operation of doing The Assignment operation.
        ///</returns>
        public override object Exec(InterfaceType leftOperand, object arg) {
            if (this.op == AssignmentOperator.Assign)
                return rightOperand.AcceptOperation(PromotionOperation.Create(leftOperand, this.op, this.methodAnalyzed, this.location), arg);

            return ReportError(leftOperand);
        }
        #endregion

        /// <summary>
        /// Performs an assigment operation between a IntType type and the typeExpression stored in this.rightOperand.
        /// the result of the operation is the resulting operation of pass a message to the right operand doing a PromotionOperation with the operation over the class IntType.
        /// Only is factible do this operation if op is an AssigmentOperation. In other case it raises an error.
        /// </summary>
        /// <param name="leftOperand">A IntType, the left operand of the assignment/param>
        /// <returns> The result of the operation is the resulting operation of doing The Assignment operation.
        ///</returns>
        public override object Exec(IntType leftOperand, object arg) {
            return rightOperand.AcceptOperation(PromotionOperation.Create(leftOperand, this.op, this.methodAnalyzed, this.location), arg);
        }


        #region NullType = ...
        /// <summary>
        /// It performs a an assigment operation with a NullType as left operand. The operaton can only be done if op attribute is set to Attribute.Assign.
        /// </summary>
        /// <param name="leftOperand">A NullType as left Operarand</param>
        /// <returns>The right operand, if the op == AssignmentOperator.Assign. raises and error in other case.</returns>
        public override object Exec(NullType leftOperand, object arg) {
            return op == AssignmentOperator.Assign ? this.rightOperand : ReportError(leftOperand);
        }
        #endregion


        #region PropertyType = ...
        /// <summary>
        /// It performs a an assigment operation with a PropertyType type as left operand. 
        /// </summary>
        /// <param name="leftOperand">A ProperyType as left operand, the property is not read-only we'll do the operation with the PropertyTypeExpressin, the propoerty encapsulate</param>
        /// <returns>result the evaluation of the operator with these two types if they satisfies the condition embebed in the code.
        /// the property is not read-only we'll do the operation with the PropertyTypeExpressin, the propoerty encapsulate.
        /// </returns>
        public override object Exec(PropertyType leftOperand, object arg) {
            if (!leftOperand.MemberInfo.hasModifier(Modifier.CanWrite)) {
                ErrorManager.Instance.NotifyError(new PropertyWriteError(leftOperand.MemberInfo.MemberIdentifier, this.location));
                return null;
            }
            if (leftOperand.PropertyTypeExpression != null)
                return leftOperand.PropertyTypeExpression.AcceptOperation(new AssignmentOperation(this.rightOperand, this.op, null, this.unification, this.actualImplicitObject, this.location), arg);

            return null;
        }

        #endregion


        #region StringType = ...
        /// <summary>
        /// It performs a an assigment operation with a StringType as left operand.
        /// </summary>
        /// <param name="leftOperand">A StringType as left operand</param>
        /// <returns>result the evaluation of the operator, in this case it can be =, and += operations, with these two types if they satisfies the condition embebed in the code.</returns>
        public override object Exec(StringType leftOperand, object arg) {
            if (op == AssignmentOperator.Assign || op == AssignmentOperator.PlusAssign)
                return rightOperand.AcceptOperation(PromotionOperation.Create(leftOperand, this.op, this.methodAnalyzed, this.location), arg);
            // * se podría aquí hacer un base.ReportError()?;
            ErrorManager.Instance.NotifyError(new OperationNotAllowedError(leftOperand.FullName, rightOperand.FullName, this.location));
            return null;
        }

        #endregion


        #region TypeExpression  = ...
        /// <summary>
        ///  This method never would be called. Is point of double dispatcher. But the dispatch is done with equals methods 
        ///  in children of this clases.
        /// </summary>
        /// <param name="leftOperand"></param>
        /// <returns></returns>

        public override object Exec(TypeExpression leftOperand, object arg) {
            return ReportError(leftOperand);
        }


        #endregion

        #region TypeVariable = ...
        /// <summary>
        /// It performs a an assigment operation with a TypeVariable as left operand.
        /// </summary>
        /// <param name="leftOperand">leftOperand is Typevariable in the left par of the assignment</param>
        /// <returns>The result of the assignment if possible null and error if not.</returns>
        public override object Exec(TypeVariable leftOperand, object arg) {
            // * Bounded variable?
            if (leftOperand.Substitution != null && this.unification == SortOfUnification.Equivalent)
                // * Check promotion to its substitution
                return rightOperand.AcceptOperation(PromotionOperation.Create(leftOperand, this.op, this.methodAnalyzed, this.location), arg);
            // * If the variable its not bounded, we add the parameter to the equivalence list
            if (leftOperand.addToMyEquivalenceClass(rightOperand, this.unification, new List<Pair<TypeExpression, TypeExpression>>()))
                return leftOperand;
            // * If it has not been possible, error
            ErrorManager.Instance.NotifyError(new OperationNotAllowedError(this.op.ToString(), leftOperand.FullName, rightOperand.FullName, this.location));
            return null;
        }


        #endregion

        #region UnionType = ...
        /// <summary>
        /// It performs a an assigment operation with a TypeSpressi0n as left operand. These method never be
        /// invonked, it is, it raises and error
        /// </summary>
        /// <param name="leftOperand">TypeExpression</param>
        /// <returns> An error
        /// </returns>
        public override object Exec(UnionType leftOperand, object arg) {
            // * If the unification is incremental, we must add it to the union typeset
            if (this.unification == SortOfUnification.Incremental) {
                leftOperand.TypeSet.Add(rightOperand);
                return leftOperand;
            }
            foreach (TypeExpression type in leftOperand.TypeSet)
                if ((int)rightOperand.AcceptOperation(new PromotionLevelOperation(type), arg) == -1)
                    return ReportError(leftOperand);

            return leftOperand;
        }

        public override object ReportError(TypeExpression tE) {
            ErrorManager.Instance.NotifyError(new AssignmentError(rightOperand.FullName, tE.FullName, this.location));
            return null; ;
        }
        #endregion
    }

}
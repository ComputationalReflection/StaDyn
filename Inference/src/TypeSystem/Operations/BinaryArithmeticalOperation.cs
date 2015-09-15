using TypeSystem;
using AST;
using ErrorManagement;
using System;
using DynVarManagement;
using TypeSystem.Constraints;
namespace TypeSystem.Operations {
    public class BinaryArithmeticalOperation : ArithmeticalOperation {
        /// <summary>
        /// A conrete class of the abstract class arithmetical operation.
        /// Encapsulates a binary arithmetical operation.
        /// Implements factory method.
        /// Role: Product
        /// Implements double dispatcher pattern
        /// </summary>
        #region Fields
        /// <sumary>
        ///  The second operand in a binary arihtmetical operation.
        /// </sumary>
        protected TypeExpression secondOperand;
        /// <summary>
        /// operand to apply to the operands.
        /// </summary>
         protected Enum binaryOperator;
        /// <summary>
        /// the actual method being analised.
        /// </summary>
        protected MethodType methodAnalyzed;
        /// <summary>
        /// Indicates if an error message should be shown (used for dynamic types)
        /// </summary>
        protected bool showErrorMessage; 
        /// <summary>
        /// The location (file, line, column) of text being analysed.
        /// </summary>
        protected Location location;
        #endregion

        #region Constructor
        /// <summary>
        /// Private constructor of the BinaryArithmeticalOperation.
        /// Implements Factory method.
        /// Role: Product.
        /// Implements a double dispatcher method in its methods AcceptOperation.
        /// </summary>
        /// <param name="secondOperand">The second operand in a binary arihtmetical operation. 
        /// First operand would be came inside the proper message to AcceptOperation methods
        /// </param>
        /// <param name="binaryOperator">operand to apply to the operands.</param>
        /// <param name="methodAnalyzed">the actual method being analised.</param>
        /// <param name="showErrorMessage">Indicates if an error message should be shown (used for dynamic types)</param>
        /// <param name="location">The location (file, line, column) of text being analyses</param>
        public BinaryArithmeticalOperation(TypeExpression secondOperand, Enum binaryOperator, MethodType methodAnalyzed, bool showErrorMessage, Location location) {

            this.secondOperand = secondOperand;
            this.binaryOperator = binaryOperator;
            this.methodAnalyzed = methodAnalyzed;
            this.showErrorMessage = showErrorMessage;
            this.location = location;
        }
        #endregion
 
        #region BoolType +-secondOperand
        /// <summary>
        /// These method is called passing firstOperand a left operand in a binary arithmetical operation.
        /// The second operand is encapsulated as secondOperand, in every instances of the class.
        /// The first Operand is a BoolType so the second must be promotable to an expression where a + or 
        /// - made sense with a boolean value.
        /// Implements a double dispatch pattern.
        /// </summary>
        /// <param name="firstOperand">The left operand of the operation is a BoolType.</param>
        /// <returns>A TypeExpression if the operation makes sense, otherwise if showMessages is true, an error is raised</returns>
        public override object Exec(BoolType firstOperand, object arg) {
            if (this.binaryOperator.Equals(ArithmeticOperator.Plus) && (bool)this.secondOperand.AcceptOperation(new EquivalentOperation(StringType.Instance), arg))
                return StringType.Instance;
            if (this.showErrorMessage) //TODO: REvisar ESta SIgnaTURA
                ErrorManager.Instance.NotifyError(new TypePromotionError(firstOperand.FullName, this.secondOperand.FullName, this.binaryOperator.ToString(), this.location));
            return null;
        }
        #endregion

        #region DoubleType +-secondOperand
        /// <summary>
        /// The first Operand is a Double so the second must be promotable to a DoubleType so the whole operation must have sense.
        /// Implements a double dispatch pattern.
        /// </summary>
        /// <param name="firstOperand">A double type to perform a binary arithmetica operation with. The second operate have to be promotable
        /// to Double, cause is the grater integral value.</param>
        /// <returns>A TypeExpression if the operation makes sense, otherwise if showMessages is true, an error is raised</returns>
        public override object Exec(DoubleType firstOperand, object arg) {
            if ((int)this.secondOperand.AcceptOperation(new PromotionLevelOperation(firstOperand), arg) != -1)
            {                 
                if (secondOperand is TypeVariable)
                {
                    if (methodAnalyzed != null && ((TypeVariable)secondOperand).Substitution == null)
                    {
                        // * A constraint is added to the method analyzed                        
                        ArithmeticConstraint constraint = new ArithmeticConstraint(firstOperand, secondOperand, binaryOperator, location);                            
                        methodAnalyzed.AddConstraint(constraint);
                        return constraint.ReturnType;
                    }
                }
                return DoubleType.Instance;
            }                
            if (this.binaryOperator.Equals(ArithmeticOperator.Plus) && (bool)this.secondOperand.AcceptOperation(new EquivalentOperation(StringType.Instance), arg))
                return StringType.Instance;
            // We rely in arithmetic conmutativiness to perform a cross recursion. Could it be dangerous?
            return secondOperand.AcceptOperation(ArithmeticalOperation.Create(firstOperand, this.binaryOperator, this.methodAnalyzed, this.showErrorMessage, this.location), arg);
        }
        #endregion

        #region IntType +-secondOperand
        /// <summary>
        /// The first Operand is a IntType so the second must be promotable in such way that the whole operation must have sense.
        /// Implements a double dispatch pattern.
        /// </summary>
        /// <param name="firstOperand">an IntType to perform a binary arithmetical operatin with. The second operator must 
        /// be promotable to IntType, StringType, or DoubleType</param>
        /// <returns>A TypeExpression if the operation makes sense, otherwise if showMessages is true, an error is raised</returns>
        public override object Exec(IntType firstOperand, object arg) {
            if ((int)this.secondOperand.AcceptOperation(new PromotionLevelOperation(firstOperand), arg) != -1)
            {
                if (secondOperand is TypeVariable && ((TypeVariable) secondOperand).Substitution == null)
                {
                    if (methodAnalyzed != null)
                    {
                        // * A constraint is added to the method analyzed
                        ArithmeticConstraint constraint = new ArithmeticConstraint(firstOperand, secondOperand,
                                                                                   binaryOperator, location);
                        methodAnalyzed.AddConstraint(constraint);
                        return constraint.ReturnType;
                    }
                }
                return IntType.Instance;
            }
            if (this.binaryOperator.Equals(ArithmeticOperator.Plus) && (bool)this.secondOperand.AcceptOperation(new EquivalentOperation(StringType.Instance), arg))
                return StringType.Instance;
            return this.secondOperand.AcceptOperation(ArithmeticalOperation.Create(firstOperand, this.binaryOperator, this.methodAnalyzed, this.showErrorMessage, this.location), arg);
        }
        #endregion

        #region CharType +-secondOperand
        /// <summary>
        /// The first Operand is a CharType so the second must be promotable in such way that the whole operation must have sense.
        /// Implements a double dispatch pattern.
        /// </summary>
        /// <param name="firstOperand">an CharType to perform a binary arithmetical operatin with. The second operator must 
        /// be promotable to Char, IntType, StringType, or DoubleType</param>
        /// <returns>A TypeExpression if the operation makes sense, otherwise if showMessages is true, an error is raised</returns>
        public override object Exec(CharType firstOperand, object arg) {
            if ((int)this.secondOperand.AcceptOperation(new PromotionLevelOperation(firstOperand), arg) != -1)
            {
                if (secondOperand is TypeVariable && ((TypeVariable)secondOperand).Substitution == null)
                {
                    if (methodAnalyzed != null)
                    {
                        // * A constraint is added to the method analyzed
                        ArithmeticConstraint constraint = new ArithmeticConstraint(firstOperand, secondOperand,
                                                                                   binaryOperator, location);
                        methodAnalyzed.AddConstraint(constraint);
                        return constraint.ReturnType;
                    }
                }
                return IntType.Instance;
            }
            if (this.binaryOperator.Equals(ArithmeticOperator.Plus) && (bool)this.secondOperand.AcceptOperation(new EquivalentOperation(StringType.Instance), arg))
                return StringType.Instance;
            return this.secondOperand.AcceptOperation(ArithmeticalOperation.Create(firstOperand, this.binaryOperator, this.methodAnalyzed, this.showErrorMessage, this.location), arg);
        }
        #endregion

        #region StringType +-secondOperand
        
        //TODO: en la versión original, en vez de promotable to a string se preguntaba por string
        /// <summary>
        /// The first Operand is a StringType so the second must be promotable in such way that the whole operation returns StringType.
        /// Implements a double dispatch pattern.
        /// </summary>
        /// <param name="firstOperand">an CharType to perform a binary arithmetical operatin with. The second operator must 
        /// be promotable to Char, IntType, StringType, or DoubleType</param>
        /// <returns>StringType if the operation makes sense, otherwise if showMessages is true, an error is raised</returns>
        public override object Exec(StringType firstOperand, object arg) {
            if (binaryOperator.Equals(ArithmeticOperator.Plus)) {
                if (promotableToStringInAddition(this.secondOperand))
                    return StringType.Instance;

                // * Commutative
                return secondOperand.AcceptOperation(ArithmeticalOperation.Create(firstOperand, this.binaryOperator, this.methodAnalyzed, this.showErrorMessage, this.location), arg);
            }
            return ReportError(firstOperand);
        }
        #endregion

        #region
        /// <summary>
        ///  This method returns true if the type expression passed as argumen can be a type expression able to promote to a String if it is combined 
        ///  with another typeExpression, ussing as operand: + or +=
        /// </summary>
        /// <param name="tE">tE TypeExpression passed as argument</param>
        /// <returns>true if the type expression passed as argumen can be a type expression able to promote to a String if it is combined 
        ///  with another typeExpression, ussing as operand: + or +=
        ///  false otherwise.
        ///  </returns>
        protected bool promotableToStringInAddition(TypeExpression tE) {
            return (int)tE.AcceptOperation(new PromotionLevelOperation(DoubleType.Instance), null) != -1
               || (bool)tE.AcceptOperation(new EquivalentOperation(CharType.Instance), null)
               || (bool)tE.AcceptOperation(new EquivalentOperation(StringType.Instance), null)
               || (bool)tE.AcceptOperation(new EquivalentOperation(NullType.Instance), null);

        }
        #endregion

        #region NullType +-secondOperand
        /// <summary>
        ///  First operand is NullType Expression, the method return a type expressión 
        ///  if the second operand is promotable to StringType and the operator is PLUS
        /// </summary>
        /// <param name="firstOperand">a NullType TypeExpression</param>
        /// <returns>return a type expressión if the second operand is promotable to StringType
        /// and the operator is PLUS. False otherwise.</returns>
        public override object Exec(NullType firstOperand, object arg) {
            if (this.binaryOperator.Equals(ArithmeticOperator.Plus) && (bool)this.secondOperand.AcceptOperation(new EquivalentOperation(StringType.Instance), arg))
                return StringType.Instance;
            if (showErrorMessage)
                ErrorManager.Instance.NotifyError(new TypePromotionError(this.secondOperand.FullName, firstOperand.FullName, this.binaryOperator.ToString(), this.location));
            return null;
        }
        #endregion

        #region FieldType +-secondOperand
        /// <summary>
        /// This method check whether the firstOperand is valid, and in this case it returns the TypeExpression associated with the field
        /// and perform the operation with this field and the operator and second operand, stored in the TypeSystemOperation Object.
        /// </summary>
        /// <param name="firstOperand">A FieldType TypeExpression, to operate with the operator and second operand </param>
        /// <returns>In case the TypeExpression associated with the field is valid it returns the result of aplying the operation 
        /// with this field and the operator and second operand, stored in the TypeSystemOperation Object.
        /// Null in case the Field does not contain a valid TypeExpression.
        /// </returns>
        public override object Exec(FieldType firstOperand, object arg) {
            if (firstOperand.FieldTypeExpression != null)
                return firstOperand.FieldTypeExpression.AcceptOperation(this, arg);
            return null;
        }
        #endregion

        #region PropertyType +-secondOperand
        /// <summary>
        /// This method check whether the firstOperand is valid, and in this case it returns the TypeExpression associated with the field
        /// and perform the operation with this property and the operator and second operand, stored in the TypeSystemOperation Object.
        /// </summary>
        /// <param name="firstOperand">A PropertyType TypeExpression, to operate with the operator and second operand </param>
        /// <returns>in case the TypeExpression associated with the property is valid it returns the result of aplying the operation 
        /// with this property and the operator and second operand, stored in the TypeSystemOperation Object.
        /// Null in case the PropertyType does not contain a valid TypeExpression.
        /// </returns>
        public override object Exec(PropertyType firstOperand, object arg) {
            if (firstOperand.PropertyTypeExpression != null)
                return firstOperand.PropertyTypeExpression.AcceptOperation(ArithmeticalOperation.Create(firstOperand, this.binaryOperator, this.methodAnalyzed, this.showErrorMessage, this.location), arg);
            return null;

        }
        #endregion

        #region TypeVariable +-secondOperand
        /// <summary>
        /// If there are Substitution in the first operand, it return the result of aplying the operation using the substitution 
        /// as first operand. It is not the case, a constraint is added and if showErrorMessages is true an error is raise.
        /// </summary>
        /// <param name="firstOperand">A TypeVariable</param>
        /// <sumary>The type expression resulting of applying the operand and second operator. 
        /// If somethis is worng and showMessages is true an error is raise.</sumary>
        public override object Exec(TypeVariable firstOperand, object arg) {
            if (firstOperand.Substitution != null) {
                DynVarOptions.Instance.AssignDynamism(firstOperand.Substitution, firstOperand.IsDynamic);
                return firstOperand.Substitution.AcceptOperation(this, arg);
            }
            if (methodAnalyzed != null) {
                // * A constraint is added to the method analyzed
                ArithmeticConstraint constraint = new ArithmeticConstraint(firstOperand, secondOperand, binaryOperator, location);
                methodAnalyzed.AddConstraint(constraint);
                return constraint.ReturnType;
            }
            if (showErrorMessage)
                ErrorManager.Instance.NotifyError(new OperationNotAllowedError(binaryOperator.ToString(), firstOperand.FullName, secondOperand.FullName, location));

            return null;
        }
        #endregion

        #region UnionType +-secondOperand
        /// <summary>
        /// This method check whether the firstOperand is has fresh varuabkes, in wich case it apply the operation
        /// to all the variable in the union
        /// </summary>
        /// <param name="firstOperand">A UnionType TypeExpression, to operate with the operator and second operand </param>
        /// <returns>in this case the TypeExpression associated with the field is valid it returns the result of aplying the operation 
        /// with to the Union the operator and second operand, stored in the TypeSystemOperation Object.
        /// Null in case the Field does not contain a valid TypeExpression.
        /// </returns>
        public override object Exec(UnionType firstOperand, object arg) {
            // * If all the types in typeset generate a constraint, we simply generate one constraint using the whole union type
            if (firstOperand.IsFreshVariable() && methodAnalyzed != null) {
                // * A constraint is added to the method analyzed
                ArithmeticConstraint constraint = new ArithmeticConstraint(firstOperand, secondOperand, binaryOperator, location);
                methodAnalyzed.AddConstraint(constraint);
                return constraint.ReturnType;
            }
            TypeExpression returnType = null;
            foreach (TypeExpression type in firstOperand.TypeSet) {
                TypeExpression ret = (TypeExpression)type.AcceptOperation(ArithmeticalOperation.Create(secondOperand, binaryOperator, methodAnalyzed, !firstOperand.IsDynamic && showErrorMessage, location), arg);
                if (ret == null && !firstOperand.IsDynamic)
                    return null;
                if (ret != null)
                    returnType = UnionType.collect(returnType, ret);
            }
            // * If there has been some errors, they have not been shown because the type is dynamic, we show it
            if (showErrorMessage && firstOperand.IsDynamic && returnType == null)
                ErrorManager.Instance.NotifyError(new NoTypeAcceptsOperation(firstOperand.FullName, this.binaryOperator.ToString(), secondOperand.FullName, location));
            return returnType;
        }
        #endregion

        #region Report Errors

        // in our case we only notify operations not allowed
        // for other pruposes invoke explicitly another kind of error
        
        /// <summary>
        /// If showMessages is true it raises and error. It returns null
        /// </summary>
        /// <param name="tE"></param>
        /// <returns>null</returns>
        public override object ReportError(TypeExpression tE) {
            if (this.showErrorMessage)
                ErrorManager.Instance.NotifyError(new OperationNotAllowedError(this.binaryOperator.ToString(), tE.FullName, this.secondOperand.FullName, this.location));
            return null;
        }
        #endregion

    }
}
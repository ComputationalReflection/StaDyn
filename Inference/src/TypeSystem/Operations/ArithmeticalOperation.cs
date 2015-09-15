using TypeSystem;
using AST;
using ErrorManagement;

namespace TypeSystem.Operations {

    /// <summary>
    /// This class instantiates elements of type UnaryArithmeticalOperation, and BinaryArithmeticalOperation, according to the Create method called.
    /// Implements a factory methos(virtual constructor)
    /// Role: factory
    /// </summary>
    public abstract class ArithmeticalOperation : TypeSystemOperation {
        /// <summary>
        /// A factory Method to create UnaryAritmeticaslOperation objects
        /// Implements a factory method.
        /// Role:factory 
        /// </summary>
        /// <param name="unaryOperator">the operator to be applied</param>
        /// <param name="methodAnalyzed">The actual method</param>
        /// <param name="showErrorMessage">true if is needed to show messages, false in other case</param>
        /// <param name="location">The location (file, line, column) of the text being analyzed.</param>
        /// <returns>An UnaryArithmetialOperation object ready to be used.</returns>
        public static ArithmeticalOperation Create(UnaryOperator unaryOperator, MethodType methodAnalyzed, bool showErrorMessage, Location location) {
            return new UnaryArithmeticalOperation(unaryOperator, methodAnalyzed, showErrorMessage, location);

        }
        /// <summary>
        /// A factory Method to create BinaryAritmeticaslOperation objects
        /// Implements a factory method.
        /// Role:factory 
        /// </summary>
        /// <param name="secondOperand">Second operand in the arithmetical expresion to calcularte. The first operand must be enapsulated in the operation objec</param>
        /// <param name="binaryOperator">the operator to be aplied to both operands</param>
        /// <param name="methodAnalyzed">The actual method</param>
        /// <param name="showErrorMessage">true if is needed to show messages, false in other case</param>
        /// <param name="location">The location (file, line, column) of the text being analyzed.</param>
        /// <returns>A BinaryArithmeticalOperation object ready to be used.</returns>
        public static ArithmeticalOperation Create(TypeExpression secondOperand, System.Enum binaryOperator, MethodType methodAnalyzed, bool showErrorMessage, Location location) {

            return new BinaryArithmeticalOperation(secondOperand, binaryOperator, methodAnalyzed, showErrorMessage, location);
        }

      }

}
using TypeSystem;
using ErrorManagement;
using AST;
using Tools;
using System.Collections.Generic;
using CodeGeneration.ExceptionManagement;
namespace CodeGeneration.Operations {
    /// <summary>
    ///  This class generates code for one type to another.
    ///  
    ///  in every the overloaded Method whom first parameter matches the typeExpression we want to convert from to the second argument of this method
    ///  >That is the convertion will be fromm FirstParameter-->(TypeExpression)SecondParameter
    ///  </summary>       
    internal class CGConvertToOperation<T> : CGTypeSystemOperation where T : ILCodeGenerator {

        /// <summary>
        /// streamto to write to
        /// </summary>
        private T codeGenerator;
        /// indentation to use
        /// </summary>
        private int indent;


        private TypeExpression secondTypeExpression;
        public CGConvertToOperation(TypeExpression secondTypeExpression, T codeGenerator, int indent) {
            this.secondTypeExpression = secondTypeExpression;
            this.codeGenerator = codeGenerator;
            this.indent = indent;
           

        }

        public override object Exec(CharType c, object arg) {

            if ( TypeExpression.Is<DoubleType>(this.secondTypeExpression) )
                this.codeGenerator.convToDouble(this.indent);

            if ( TypeExpression.Is<StringType>(this.secondTypeExpression) )
                return c.AcceptOperation(new CGToStringOperation<ILCodeGenerator>(this.codeGenerator, this.indent), true); // we force a box

            return null;
        }

        public override object Exec(DoubleType d, object arg) {
            
             if ( TypeExpression.Is<StringType>(this.secondTypeExpression))
                return d.AcceptOperation(new CGToStringOperation<ILCodeGenerator>(this.codeGenerator, this.indent), true); // we force a box

            return null;

        }
        public override object Exec(IntType i, object arg) {
            if ( TypeExpression.Is<StringType>(this.secondTypeExpression) )
                return i.AcceptOperation(new CGToStringOperation<ILCodeGenerator>(this.codeGenerator, this.indent), true); // we force a box


            if ( TypeExpression.Is<DoubleType>(this.secondTypeExpression) ) {
                this.codeGenerator.convToDouble(this.indent);
                return null;
            }

            return null;
        }

        // TODO
        // hay conversiones que no sé hacer
        public override object Exec(StringType s, object arg) {
            if ( TypeExpression.Is<StringType>(this.secondTypeExpression) ) {
            }
            return null;
                
        }
        public override object Exec(TypeVariable t, object arg) {
            if ( t.HasFreshVariable() ) {
                ReportError(string.Format("Cannot make convertions from type {0} to {1}.", t.FullName, this.secondTypeExpression.FullName));
                return null;
            }
            return t.Substitution.AcceptOperation(this, null);
        }
    }
}
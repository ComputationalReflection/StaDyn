using TypeSystem.Operations;
using TypeSystem;
using ErrorManagement;
using AST;
using Tools;
using System.Collections.Generic;
using CodeGeneration.ExceptionManagement;
using AST.Operations;
namespace CodeGeneration.Operations {
    /// <summary>
    ///  It typechecks the runtime arguments, embeded in a method call, with the parametes of this method.
    ///  </summary>       
    internal class CGVisitArithmeticalOp<T> : TypeSystemOperation where T : ILCodeGenerator {

        /// <summary>
        /// streamto to write to
        /// </summary>
        private T codeGenerator;
        /// <summary>
        /// indentation to use
        /// </summary>
        private int indent;
        //visitor argument
        private object obj;
        private ArithmeticExpression node;
        private VisitorILCodeGeneration<T> visitor;
        // * TypeExpression alias abvreviate operand1 and operand2
        //private TypeExpression t1, t2;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitor"></param>
        /// <param name="node">node containin both operands and the operator</param>
        /// <param name="codeGenerator"></param>
        /// <param name="indent"></param>
        string globalEnd;
        string FirstOperatorLabel;
        public CGVisitArithmeticalOp(VisitorILCodeGeneration<T> visitor, ArithmeticExpression node, T codeGenerator, int indent, object obj) {
            this.node = node;
            this.codeGenerator = codeGenerator;
            this.indent = indent;
            this.visitor = visitor;
            this.node = node;
            this.obj = obj;
            this.globalEnd = codeGenerator.NewLabel;
            this.FirstOperatorLabel = codeGenerator.NewLabel;

        }
        //// Before refactorizing the class this is the main entry point
        
        //public override object Exec(AstNode a, object arg) {
        //    CheckFirstOperand();

        //}
        //// code generated can be reduced by adding a flag indicating whether is the first time the method 
        //// is called 
        //private void CheckFirstOperand() {
        //    TypeExpression t1 = node.FirstOperand.ExpressionType;
        //    this.node.Accept(this.visitor, this.obj);
        //    if ( TypeExpression.Is<UnionType>(t1) )
        //        FirstOperandAsUnion(TypeExpression.As<UnionType>(t1));
        //    else 
        //        CheckSecondOperand(t1);
            
        //}


        ///// <summary>
        ///// 
        ///// 
        ///// </summary>
        ///// <param name="typeExpression"></param>
        //private void CheckSecondOperand(TypeExpression typeExpression) {
        //    // this accept is done in order to push the second operand in the top of the stack
        //    // but it will be neccesary to pop it out if a promotion in the first operand is needed
        //    // that,is because the first operand is now just behind the second operand in the stack
        //    node.SecondOperand.Accept(this.visitor, this.obj);
        //    TypeExpression t2 = node.FirstOperand.ExpressionType;
        //    if ( TypeExpression.Is<UnionType>(t2) )
        //        SecondOperandAsUnion(t1, TypeExpression.As<UnionType>(t2));
        //    else 
        //        FirstAndSecondOperandAsNonUnionTypes(t1, t2);
        //}

        //private void FirstOperandAsUnion(UnionType ut) {
        //    foreach ( TypeExpression ti in ut.TypeSet )
        //        if ( TypeExpression.Is<UnionType>(ti) )
        //            FirstOperandAsUnion(TypeExpression.As<UnionType>(ti));
        //        else {
        //            string skipFirstOperandLabel = this.codeGenerator.NewLabel;
        //            this.codeGenerator.dup(this.indent);
        //            this.codeGenerator.isinst(this.indent, ti);
        //            this.codeGenerator.brfalse(this.indent, skipFirstOperandLabel);
        //            CheckSecondOperand(ti);
        //            this.codeGenerator.WriteLabel(this.indent, skipFirstOperandLabel);
        //        }
        //}
        //private void SecondOperandAsUnion(TypeExpression t1, UnionType ut) {
        //    foreach ( TypeExpression ti in ut.TypeSet )
        //        if ( TypeExpression.Is<UnionType>(ti) )
        //            SecondOperandAsUnion(TypeExpression.As<UnionType>(ti));
        //        else {
        //            string skipSecondOperandLabel = this.codeGenerator.NewLabel;

        //            this.codeGenerator.dup(this.indent);
        //            this.codeGenerator.isinst(this.indent, ti);
        //            this.codeGenerator.brfalse(this.indent, skipSecondOperandLabel);
        //            // now in the top of the stack there's only First Operand
        //            this.codeGenerator.pop();
        //            t1.AcceptOperation
        //            this.codeGenerator.WriteLabel(this.indent, skipSecondOperandLabel);
        //        }
        //}

        //private TypeExpression CheckSecondOperand(TypeExpression t1, TypeExpression t2) {
            
        //    if ( TypeExpression.Is<UnionType>(t2) ) {
        //        UnionType ut = t1 as UnionType;
        //        foreach ( TypeExpression ti in ut.TypeSet )
        //            CheckSecondOperand(t1, ti);

        //    } else { // iS an integral TYPE 
        //        IntegralTypeAsSecondOperand(t1, t2);
        //        GenerateOperator(t1, t2);
        //    }
        //    return null;
        //}

        //private void GenerateOperator(TypeExpression t1, TypeExpression t2) {
        //    throw new System.NotImplementedException();
        //}

        //public void IntegralTypeAsFirstOperand(TypeExpression t1) {
        //    node.FirstOperand.Accept(this.visitor, obj);
        //    GenerateFirstOperandCode(t1);
        //}
        //public void IntegralTypeAsSecondOperand(TypeExpression t1, TypeExpression t2) {
        //    node.FirstOperand.Accept(this.visitor, obj);
        //    GenerateSecondOperandCode(t1);
        //}
        //private void GenerateFirstOperandCode(TypeExpression t1) {
        //    throw new System.NotImplementedException();
        //}

        //private void GenerateSecondOperandCode(TypeExpression t2) {
        //    throw new System.NotImplementedException();
        //}


    }
}
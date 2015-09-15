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
    internal class CGVisitDefinitionNodeOperation<T> : AstOperation where T : ILCodeGenerator {

        /// <summary>
        /// streamto to write to
        /// </summary>
        private T codeGenerator;
        /// <summary>
        /// indentation to use
        /// </summary>
        private int indent;

        private Definition node;

        private object argument;
        private VisitorILCodeGeneration<T> visitor;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitor"></param>
        /// <param name="node"></param>
        /// <param name="codeGenerator"></param>
        /// <param name="indent"></param>

        public CGVisitDefinitionNodeOperation(VisitorILCodeGeneration<T> visitor, Definition node, T codeGenerator, int indent, object argument) {
            this.node = node;
            this.codeGenerator = codeGenerator;
            this.indent = indent;
            this.visitor = visitor;
            this.argument = argument;
        }

        public override object Exec(InvocationExpression invocationExpression, object arg) {
            this.BeginOfOperation();
            MethodType methodType = invocationExpression.ActualMethodCalled as MethodType;
            if (methodType != null && this.node.Init.ILTypeExpression.IsValueType() && !methodType.Return.IsValueType())
                this.codeGenerator.Promotion(this.indent, methodType.Return, methodType.Return, this.node.TypeExpr, this.node.ILTypeExpression, true, this.visitor.CheckMakeAnUnbox(this.node.Init));
            else
                this.codeGenerator.Promotion(this.indent, this.node.Init.ExpressionType, this.node.Init.ILTypeExpression, this.node.TypeExpr, this.node.ILTypeExpression, true, this.visitor.CheckMakeAnUnbox(this.node.Init));            
            return this.EndOfOperation();
        }
        public override object Exec(AstNode exp, object arg) {
            if (this.node.Init is InvocationExpression)
                return Exec((InvocationExpression)this.node.Init,arg);
            this.BeginOfOperation();
            this.codeGenerator.Promotion(this.indent, this.node.Init.ExpressionType, this.node.Init.ILTypeExpression,this.node.TypeExpr, this.node.ILTypeExpression, true,this.visitor.CheckMakeAnUnbox(this.node.Init));
            return this.EndOfOperation();            
        }
        protected virtual object EndOfOperation() {
            this.codeGenerator.stloc(this.indent, this.node.ILName);
            this.codeGenerator.WriteLine();
            return null;
        }
        protected virtual void BeginOfOperation() {
            this.node.Init.Accept(this.visitor, this.argument);
        }
    }
}


    using AST;
using TypeSystem;
using AST.Operations;
using System;
namespace CodeGeneration.Operations {
    /// <summary>
    /// Generates code for an single method infocation/// </summary>
    internal class CGILInvocationExpressionOperation<T> : AstOperation where T : CLRCodeGenerator {

        /// <summary>
        /// stream to write to
        /// </summary>
        private T codeGenerator;
        /// <summary>
        /// viritor
        /// </summary>

        private VisitorCLRCodeGeneration<T> visitor;
        /// <summary>   
        /// indentation
        /// </summary>
        private int indent;
        /// <summary>
        /// Node providing the invocation information
        /// </summary>
        InvocationExpression node;

        InheritedAttributes inheritedAttributes;

        InheritedAttributes objInv;

        Object objArgs;

         public CGILInvocationExpressionOperation(int indent, VisitorCLRCodeGeneration<T> visitor,
                    T codeGenerator, InvocationExpression node,
                    InheritedAttributes inheritedAttributes, InheritedAttributes objInv, object objArgs) {
            this.indent = indent;
            this.visitor = visitor;
            this.codeGenerator = codeGenerator;
            this.node = node;
            this.inheritedAttributes = inheritedAttributes; // ia simple alias for briefing
            this.objInv = objInv;
            this.objArgs = objArgs;
    
        }
        public override object Exec(FieldAccessExpression f, object arg) {
            // * 1. A message has been sent with the syntax "obj.method(...)"
            TypeExpression caller = this.node.ActualMethodCalled;
            if (caller != null) 
                return caller.AcceptOperation(new CGILMethodInvocationOperation<T>(
                                        this.indent, this.visitor, this.codeGenerator, this.node, 
                                        this.inheritedAttributes, this.objArgs, this.objInv), arg);

            throw new FieldAccessException();
        }
        
        public override object Exec(SingleIdentifierExpression s, object arg) {
            // * 2. A message is sent with without the implicit object "method(...)"
            //// * 2.1 A method is called (not a constructor)
            MethodType actualMethodCalled = TypeExpression.As<MethodType>(node.ActualMethodCalled);
            if ( ( ( (MethodType)node.ActualMethodCalled ).MemberInfo.ModifierMask & Modifier.Static ) == 0 )
                this.codeGenerator.ldarg(this.indent, 0);
            this.node.Arguments.Accept(this.visitor, this.objArgs);
            this.codeGenerator.Call(this.indent, actualMethodCalled, actualMethodCalled.MemberInfo.Class, s.Identifier);
            //If the actualMethodCalled returns a TypeVariable and the InvocationExpression returns a ValueType an unboxing is needed, becasue there is not auto-unboxing from object to a ValueType.
            //if (actualMethodCalled.Return is TypeVariable && actualMethodCalled.Return.IsFreshVariable() && node.ExpressionType.IsValueType())
              //  this.codeGenerator.UnboxAny(this.indent, node.ExpressionType);
            return null;

        }

        public override object Exec(BaseExpression b, object arg) {
            // * 2.2 A constructor is called
            MethodType actualMethodCalled = TypeExpression.As<MethodType>(node.ActualMethodCalled);
            this.node.Arguments.Accept(this.visitor, this.objArgs);
            this.codeGenerator.constructorCall(this.indent, actualMethodCalled, b.ExpressionType, ".ctor");
            
            return null;
        }

        public override object Exec(AstNode a, object arg) {
            this.codeGenerator.WriteLine(this.indent, "//entro por AstNode");
            return null;
        }

        
    }
}


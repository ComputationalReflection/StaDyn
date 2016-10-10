//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project StaDyn                                                             
// -------------------------------------------------------------------------- 
// File: VisitorCLRCodeGeneration.cs                                             
// Author: Francisco Ortin  -  francisco.ortin@gmail.com
//         Daniel Zapico   - daniel.zapico.rodriguez@gmail.com                   
// Description:                                                               
//    This class walks the AST to obtain the CLR IL code . 
//    Inheritance: VisitorCodeGeneration                   
//    Implements Visitor pattern [Concrete Visitor].       
//    Implements Factory method  [Concrete Product].       
// -------------------------------------------------------------------------- 
// Create date: 21-08-2007                                                    
// Modification date: 21-08-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;

using AST;
using TypeSystem;
using CodeGeneration.Operations;

namespace CodeGeneration {
    /// <summary>
    /// This class walks the AST to obtain the IL code.
    /// </summary>
    /// <remarks>
    /// Inheritance: VisitorCodeGeneration                   
    /// Implements Visitor pattern [Concrete Visitor].       
    /// Implements Factory method  [Concrete Product].       
    /// </remarks>
    class VisitorCLRCodeGeneration<T> : VisitorILCodeGeneration<T> where T : CLRCodeGenerator {
        #region Constructor
        /// <summary>
        /// Constructor of VisitorRrotorCodeGeneration
        /// </summary>
        /// <param name="writer">Writer to write the intermediate code.</param>
        /// <param name="nameModule">WriteModule name.</param>
        /// <param name="targetPlatform">The name of the target platform</param>
        public VisitorCLRCodeGeneration(string moduleName, T codeGenerator)
            : base(moduleName, codeGenerator) { }
        #endregion

        #region Visit(InvocationExpression node, Object obj)

        #region Pattern: runtime check union methods

        ///  * ld       <implicit object>
        ///  * <for each method of UnionMethods>
        ///  *    // check the invocation reference
        ///  *    dup
        ///  *    isinst <class type of actualMethod>
        ///  *    brfalse <check NextMethod> (Unbox needed for value types (not Unbox.any))
        ///  *    // check arguments
        ///  *    <for each argument>
        ///  *       dup
        ///  *       isinst <current parameter of actualMethod> (If param is ValueType, is the argument of this type or can promote to this type?)
        ///  *       brfalse <NextMethod> (Box needed for value types)
        ///  *    call <current method>
        ///  *    br <EndLabel>
        ///  *    // check NextMethod
        ///  *    <NextMethod>
        ///  *    <clean elements on the stack>
        ///  *  // There are some mistakes
        ///  *  call <WrongMethodException>
        ///  *  <EndLabel>

        #endregion
        public override Object Visit(InvocationExpression node, Object obj) {
            InheritedAttributes ia = (InheritedAttributes)obj; //Simple cast to shorten thee expression
             Object objArgs = obj;
            //Object o = null;
            InheritedAttributes objInv = new InheritedAttributes(ia.CurrentMethod, ia.Assignment, ia.Reference, ia.ArrayAccessFound, ia.ActualMethodCalled, true);
            MethodType actualMethodCalled = TypeExpression.As<MethodType>(node.ActualMethodCalled);

            if ( actualMethodCalled != null ) 
                objArgs = new InheritedAttributes(ia.CurrentMethod, ia.Assignment, ia.Reference, ia.ArrayAccessFound, actualMethodCalled, true);
            
            return node.Identifier.AcceptOperation(new CGILInvocationExpressionOperation<T>(this.indent, this, this.codeGenerator, node, ia, objInv, objArgs), null);
        }

        #endregion

        internal override void RuntimeCheckArguments(InvocationExpression node, Object objArgs, MethodType actualMethodCalled, List<string> nextMethod) {
            for (int j = 0; j < node.Arguments.ExpressionCount; j++) {
                node.Arguments.GetExpressionElement(j).Accept(this, objArgs);
                actualMethodCalled.GetParameter(j).AcceptOperation(new CGRuntimeCheckArgumentOperation<T>(this.indent, this.codeGenerator, nextMethod), null);
                //if ((!(actualMethodCalled.GetParameter(j).IsValueType()))
                //   || (actualMethodCalled.GetParameter(j) is BoolType) || (actualMethodCalled.GetParameter(j) is CharType))
                //{
                //   this.codeGenerator.dup(this.indent);
                //   this.codeGenerator.isinst(this.indent, actualMethodCalled.GetParameter(j));
                //   // if check fail then check nextMethod
                //   nextMethod.Add(this.codeGenerator.NewLabel);
                //   this.codeGenerator.brfalse(this.indent, nextMethod[nextMethod.Count - 1]);
                //   if (actualMethodCalled.GetParameter(j).IsValueType())
                //      this.codeGenerator.UnboxAny(this.indent, actualMethodCalled.GetParameter(j));
                //}
                //else
                //{
                //   // Param is Int. Is the argument an int?
                //   if (actualMethodCalled.GetParameter(j) is IntType)
                //   {
                //      string nextArgument = this.codeGenerator.NewLabel;
                //      CharType.Instance.AcceptOperation(new CGRuntimeCheckTypeExpressionOperation(this.indent, this.codeGenerator, nextArgument, false));
                //      IntType.Instance.AcceptOperation(new CGRuntimeCheckTypeExpressionOperation(this.indent, this.codeGenerator, nextArgument, false));

                //      nextMethod.Add(this.codeGenerator.NewLabel);
                //      this.codeGenerator.br(this.indent, nextMethod[nextMethod.Count - 1]);
                //      this.codeGenerator.WriteLabel(nextArgument);
                //   }
                //   else
                //   {
                //      // Param is Double. Is the argument a double?
                //      if (actualMethodCalled.GetParameter(j) is DoubleType)
                //      {
                //         string nextArgument = this.codeGenerator.NewLabel;

                //         CharType.Instance.AcceptOperation(new CGRuntimeCheckTypeExpressionOperation(this.indent, this.codeGenerator, nextArgument, true));
                //         IntType.Instance.AcceptOperation(new CGRuntimeCheckTypeExpressionOperation(this.indent, this.codeGenerator, nextArgument, true));
                //         DoubleType.Instance.AcceptOperation(new CGRuntimeCheckTypeExpressionOperation(this.indent, this.codeGenerator, nextArgument, true));

                //         nextMethod.Add(this.codeGenerator.NewLabel);
                //         this.codeGenerator.br(this.indent, nextMethod[nextMethod.Count - 1]);
                //         this.codeGenerator.WriteLabel(nextArgument);
                //      }
                //   }
                //}
            }
        }

        #region runtimeCheckTypeExpression()

        /// <summary>
        /// Checks, at runtime, the type in the top of the stack
        /// </summary>
        /// <param name="endLabel">Label to go if the type is correct.</param>
        /// <param name="type">WriteType to check.</param>
        /// <param name="toDouble">True if it is necessary to convert to double.</param>
        // private void runtimeCheckTypeExpression(string endLabel, TypeExpression type, bool toDouble)
        //{
        //   type.AcceptOperation(new CGRuntimeCheckTypeExpressionOperation(this.indent, this.codeGenerator, endLabel, toDouble));
        //string notThisType = this.codeGenerator.NewLabel;

        //this.codeGenerator.dup(this.indent);
        //this.codeGenerator.isinst(this.indent, type);
        //this.codeGenerator.brfalse(this.indent, notThisType);
        //this.codeGenerator.UnboxAny(this.indent, type);
        //if ((toDouble) && (!(type is DoubleType)))
        //   this.codeGenerator.convToDouble(this.indent);
        //this.codeGenerator.br(this.indent, endLabel);
        //this.codeGenerator.WriteLabel(notThisType);
        //}

        #endregion



        #region IntrospectiveInvocation()
        /// <param name="node">The AST invocation expression node</param>
        /// <param name="obj">The visitor paramenter</param>
        /// <param name="inheritedAttributes">Inherited attributes</param>
        /// <param name="memberName">The name of the member</param>
        internal virtual void IntrospectiveInvocation(InvocationExpression node, Object obj, Object inheritedAttributes, string memberName) {
            InheritedAttributes ia = (InheritedAttributes)obj;
            ia.IsParentNodeAnInvocation = true;
            Object o = node.Identifier.Accept(this, ia);
            this.IntrospectiveGetMethod(node, ia, memberName);
            node.Identifier.Accept(this, ia);
            this.codeGenerator.ldci4(this.indent, node.Arguments.ExpressionCount);
            this.codeGenerator.newarr(this.indent, "[mscorlib]System.Object");
            for (int i = 0; i < node.Arguments.ExpressionCount; i++)
                IntrospectiveInvocationMakeReadyArgument(node, ia, i);
            this.codeGenerator.CallVirt(this.indent, "instance", "object", "[mscorlib]System.Reflection.MethodBase", "Invoke", new string[] { "object", "object[]" });          
        }

        private void IntrospectiveGetMethod(InvocationExpression node, InheritedAttributes ia, String memberName)
        {            
            String end = this.codeGenerator.NewLabel;            
            this.codeGenerator.CallVirt(this.indent, "instance class", "[mscorlib]System.Type", "[mscorlib]System.Object", "GetType", null);            
            this.codeGenerator.ldstr(this.indent, memberName);
            this.codeGenerator.ldci4(this.indent, node.Arguments.ExpressionCount);
            this.codeGenerator.newarr(this.indent, "[mscorlib]System.Type");
            for (int i = 0; i < node.Arguments.ExpressionCount; i++)
            {
                TypeExpression argType = node.Arguments.GetExpressionElement(i).ILTypeExpression;
                this.IntropectiveInvocationLoadArgument(i, argType);
            }
            this.codeGenerator.CallVirt(this.indent, "instance class", "[mscorlib]System.Reflection.MethodInfo", "[mscorlib]System.Type", "GetMethod", new string[] { "string", "class [mscorlib]System.Type[]" });
            this.codeGenerator.dup(this.indent);
            this.codeGenerator.brtrue(this.indent,end);                        
            this.codeGenerator.pop(this.indent);
            node.Identifier.Accept(this, ia);
            this.codeGenerator.CallVirt(this.indent, "instance class", "[mscorlib]System.Type", "[mscorlib]System.Object", "GetType", null);
            this.codeGenerator.ldstr(this.indent, memberName);            
            this.codeGenerator.CallVirt(this.indent, "instance class", "[mscorlib]System.Reflection.MethodInfo", "[mscorlib]System.Type", "GetMethod", new string[] { "string" });            
            this.codeGenerator.WriteLabel(this.indent, end);         
        }

        /// <summary>
        /// Push in the stack the i-argument. It performs an unbox if neccesary
        /// </summary>
        /// <param name="node">Ast node where is the method to invoe</param>
        /// <param name="inheritedAttributes">inherited attributes</param>
        /// <param name="i">index of the argument</param>
        private void IntrospectiveInvocationMakeReadyArgument(InvocationExpression node, Object inheritedAttributes, int i) {
            TypeExpression argType = node.Arguments.GetExpressionElement(i).ILTypeExpression;
            this.codeGenerator.dup(this.indent);
            this.codeGenerator.ldci4(this.indent, i);
            node.Arguments.GetExpressionElement(i).Accept(this, inheritedAttributes);
            this.codeGenerator.BoxIfNeeded(this.indent, argType);
            this.codeGenerator.stelemRef(this.indent);
        }
        /// <summary>
        /// Obtains the type if the current argument by means of introspection
        /// </summary>
        /// <param name="index">index of the current argument</param>
        /// <param name="argType">type of the current argument</param>
        private void IntropectiveInvocationLoadArgument(int index, TypeExpression argType) {
            this.codeGenerator.dup(this.indent);
            this.codeGenerator.ldci4(this.indent, index);
            this.codeGenerator.ldtoken(this.indent, TypeMapping.Instance.GetBCLName(argType.ILType(), true));
            this.codeGenerator.Call(this.indent, "class", "[mscorlib]System.Type", "[mscorlib]System.Type", "GetTypeFromHandle", new string[] { "valuetype [mscorlib]System.RuntimeTypeHandle" });
            this.codeGenerator.stelemRef(this.indent);
        }
       

        
        #endregion
    }
}

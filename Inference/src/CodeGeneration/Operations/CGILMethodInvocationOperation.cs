////////////////////////////////////////////////////////////////////////////////////
// ------------------------------------------------------------------------------ //
// Project Stadyn                                                                 //
// ------------------------------------------------------------------------------ //
// File: CGILMethodInvocationOperation.cs                                         //
// Author: DanielZapico Rodríguez  -  daniel.zapico.rodriguez@gmail.com           //
// Description:                                                                   //
//    This class encapsulates the operations to generate method calss in MSIL.    //
// ------------------------------------------------------------------------------ //
// Create date: 3-3-2010                                                          //
// Modification date: 3-3-2010                                                    //
////////////////////////////////////////////////////////////////////////////////////
using System.Collections.Generic;
using AST;
using ErrorManagement;
using TypeSystem;
using TypeSystem.Operations;
using CodeGeneration;

namespace CodeGeneration.Operations {
    /// <summary>
       ///  It typechecks the runtime arguments, embeded in a method call, with the parametes of this method.
       ///  We'ere a doing a delegate methods with the same signature and an extra parameter.
       ///  if the method is invoked  in its original form its the first time its invoked.
       ///  </summary>       
    internal class CGILMethodInvocationOperation<T>:TypeSystemOperation where T:CLRCodeGenerator {

        /// <summary>
        /// stream to write to
        /// </summary>
        private T codeGenerator;
        /// <summary>
        /// indentation to use
        /// </summary>

        private VisitorCLRCodeGeneration<T> visitor;

        private int indent;
        /// <summary>
        /// Node providing the invocation information
        /// </summary>
        /// 
      
        InvocationExpression node;
        /// <summary>
        /// attributes of the decorated treee
        /// </summary>
        InheritedAttributes inheritedAttributes;
        InheritedAttributes objInv;
        /// <summary>
        /// this contains objects that doesn't access the message we are dealing wiht, 
        /// but can be assigned to a var and pass trough the semantic phase 
        /// if the object is marked as dynamic
         /// </summary>
        List<TypeExpression> nonValidObjectsList;

        private string endLabel;
        private string nonValidObjectsLabel;
        private string reflectionLabel;

        private bool areThereNonValidObjects;
        private bool hasTypeVariable = false; // indi casi hay alguna variable de tipo en la TE
        private bool firstTime = true; // indica si el trozo de expresion de tipo que estoy tratando es
        //la expresion es completa y la primera vez que se accede. En el constructor siempre a true
        object objArgs;

        public CGILMethodInvocationOperation(int indent, VisitorCLRCodeGeneration<T> visitor, 
                T codeGenerator, InvocationExpression node,
                InheritedAttributes inheritedAttributes, object objArgs, InheritedAttributes objInv)
        {
            this.indent = indent;
            this.codeGenerator = codeGenerator;
            this.visitor = visitor;
            this.node = node;
            this.inheritedAttributes = inheritedAttributes;
            this.objArgs = objArgs;
            this.objInv = objInv;
            this.endLabel = this.codeGenerator.NewLabel + "_END_LABEL";
            this.nonValidObjectsLabel = this.codeGenerator.NewLabel + "_NON_VALID_OBJECT_CALLS";
            this.reflectionLabel = this.codeGenerator.NewLabel + "_REFLECTION_CALLS";
            this.areThereNonValidObjects = false;
            this.hasTypeVariable = false;
            this.firstTime = true;
            FieldAccessExpression fieldAccessExpression = node.Identifier as FieldAccessExpression;
            this.nonValidObjectsList = (List<TypeExpression>)fieldAccessExpression.Expression.ExpressionType.AcceptOperation(new CGPlainTypeExpressionOperation(), null);
        }

        
        public override object Exec(MethodType actualMethod, object arg) {
            // * 1.1 The implicit object has a known type and can a lso have candidate methods maybe a candidate objec
            if ( this.firstTime ) {
                this.firstTime = false;
                this.areThereNonValidObjects = TypeExpression.As<UnionType>(((FieldAccessExpression)this.node.Identifier).Expression.ExpressionType) != null;                
                if (areThereNonValidObjects)
                {
                    this.nonValidObjectsLabel = codeGenerator.NewLabel;
                    this.node.Identifier.Accept(this.visitor, this.objArgs);
                    this.codeGenerator.isinst(this.indent, actualMethod.MemberInfo.Class);
                    this.codeGenerator.brfalse(this.indent, nonValidObjectsLabel);
                }
                this.InvokeSingleMethod(actualMethod, arg); 
                if(areThereNonValidObjects)
                {
                    string end = codeGenerator.NewLabel;
                    this.codeGenerator.br(this.indent, end);
                    this.codeGenerator.WriteLabel(this.indent, this.nonValidObjectsLabel);
                    this.node.Identifier.Accept(this.visitor, this.objArgs);
                    this.codeGenerator.WriteThrowMissingMethodException(this.indent, ((AST.FieldAccessExpression)(this.node.Identifier)).FieldName.ILName);                                        
                    this.codeGenerator.WriteLabel(this.indent, end);
                }
                return null;
            }   
             this.InvokeUnionMethod(actualMethod, arg);
             return null;
                
          }

        protected object InvokeSingleMethod(MethodType actualMethod, object arg) {
            object decorationInformation = this.node.Identifier.Accept(this.visitor, this.objArgs);
           // if (!this.nonValidObjectsList.Remove(actualMethod.MemberInfo.Class))
              //   this.codeGenerator.Comment("Removing..."+ actualMethod);
            this.node.Arguments.Accept(this.visitor, this.objArgs);
            this.codeGenerator.MakeCall(this.indent, this.node, decorationInformation, actualMethod, (FieldAccessExpression)this.node.Identifier, arg);
            //If the actualMethod returns a TypeVariable and the InvocationExpression returns a ValueType an unboxing is needed, becasue there is not auto-unboxing from object to a ValueType.
            //if (actualMethod.Return is TypeVariable && actualMethod.Return.IsFreshVariable()  && node.ExpressionType.IsValueType())
              //  this.codeGenerator.UnboxAny(this.indent, node.ExpressionType);            
           // this.codeGenerator.br(this.indent, this.endLabel););
            if (!this.inheritedAttributes.MessagePassed)
                this.inheritedAttributes.MessagePassed = true;

            return decorationInformation;     
        }

        public override object Exec(TypeVariable t, object arg)
        {
           if (!t.IsFreshVariable())
                return t.Substitution.AcceptOperation(this, arg);
           
           this.hasTypeVariable = true;
           if (this.firstTime) {
                this.firstTime = false;
                this.areThereNonValidObjects = TypeExpression.As<UnionType>(((FieldAccessExpression)this.node.Identifier).Expression.ExpressionType) != null;
                this.EndInvocationMethod(false);
            }
        return null;
                       
        }

            
        public override object Exec(UnionType ut, object arg) {
            // No need to ask if is the original Expression (first time)
            //this.areThereNonValidObjects = ut.IsDynamic; // ESTO NO SIRVe

            FieldAccessExpression fa =  this.node.Identifier as  FieldAccessExpression;
            this.areThereNonValidObjects = fa.Expression.ExpressionType.IsDynamic;
            bool oldFirstTime = this.firstTime;
            this.firstTime = false;
            
            // * 1.2.1.1 The implicit object has unknown type (union types)
            Dictionary<MethodInvocationArgument, object> dic = new Dictionary<MethodInvocationArgument, object>();
            arg = dic;
            dic[MethodInvocationArgument.Clean] = false;
            dic[MethodInvocationArgument.MakeBox] = ut.ContainsDifferentReturns(); /// indicates whether is neccesary to make a boxing
            dic[MethodInvocationArgument.DecorationAttributes] = this.node.Identifier.Accept(this.visitor, this.objInv); // object o;
            
            foreach(TypeExpression ti in ut.TypeSet)
                ti.AcceptOperation(this, arg);
            
            if (oldFirstTime) {
                // we leave firstTime in false o maintain coherency in the object status
                //this.codeGenerator.WriteLabel(this.indent, this.methodLabel);              
                this.EndInvocationMethod((bool)dic[MethodInvocationArgument.Clean]);
              }

            return dic[MethodInvocationArgument.DecorationAttributes];
        }

        private object InvokeUnionMethod(MethodType actualMethodCalled, object arg) {
            List<string> nextMethod = new List<string>();
            Dictionary<MethodInvocationArgument, object> dic = (Dictionary<MethodInvocationArgument, object>)arg;
            if (this.nonValidObjectsList.Remove(actualMethodCalled.MemberInfo.Class))
               this.codeGenerator.Comment("Removing..." + actualMethodCalled + " from non suitable objects");
            string nextMethodLabel = this.codeGenerator.NewLabel;
            if ( ( actualMethodCalled.MemberInfo.ModifierMask & Modifier.Static ) == 0 ) {
                // check the invocation reference
                dic[MethodInvocationArgument.Clean] = true;
                this.codeGenerator.dup(this.indent);
                this.codeGenerator.isinst(this.indent, actualMethodCalled.MemberInfo.Class);
                this.codeGenerator.brfalse(this.indent, nextMethodLabel);
                if ( actualMethodCalled.MemberInfo.Class.IsValueType() )
                    this.codeGenerator.Unbox(this.indent, actualMethodCalled.MemberInfo.Class);
            }
            InheritedAttributes ia = this.inheritedAttributes;    
            this.objArgs = new InheritedAttributes(ia.CurrentMethod, ia.Assignment, ia.Reference, ia.ArrayAccessFound, actualMethodCalled, ia.IsParentNodeAnInvocation);
                // checks arguments with the parameters of the current method
                this.visitor.RuntimeCheckArguments(this.node, this.objArgs, actualMethodCalled, nextMethod);
                // call currentMethod
                TypeExpression hasReturn = this.codeGenerator.MakeCall(this.indent, this.node, dic[MethodInvocationArgument.DecorationAttributes], actualMethodCalled, (FieldAccessExpression)this.node.Identifier, arg);
                if ( hasReturn.IsValueType() && (bool) dic[MethodInvocationArgument.MakeBox])
                    this.codeGenerator.Box(this.indent, hasReturn);
                
                this.codeGenerator.br(this.indent, endLabel);
                // Check next method
                for ( int k = nextMethod.Count - 1; k >= 0; k-- ) {
                    this.codeGenerator.WriteLabel(this.indent, nextMethod[k]);
                    this.codeGenerator.pop(this.indent);
                }
                if ( (bool)dic[MethodInvocationArgument.Clean] )
                    this.codeGenerator.WriteLabel(this.indent, nextMethodLabel);
           
            return dic[MethodInvocationArgument.DecorationAttributes];
        }

        private void EndInvocationMethod(bool clean)
        {            
            if ( this.areThereNonValidObjects ) { //&& !this.hasTypeVariable) {
                this.NonValidObjectsCodeGeneration();                
            }
            if (this.hasTypeVariable) {
                this.codeGenerator.Comment(indent, "Reflection Code");
                if (clean)
                    this.codeGenerator.pop(indent);
                this.codeGenerator.WriteLabel(this.indent, this.reflectionLabel);
                InheritedAttributes ia = (InheritedAttributes) this.objArgs;
                ia.IsIntrospectiveInvocation = true;
                this.visitor.IntrospectiveInvocation(this.node, ia, this.inheritedAttributes, ( (FieldAccessExpression)this.node.Identifier ).FieldName.Identifier);
               
            }
            this.codeGenerator.WriteLabel(this.indent, this.endLabel);
        }

        private void NonValidObjectsCodeGeneration() {
    
            if (this.nonValidObjectsList.Count == 0) // It could be that all non suitable objects might be deleteded?
                return;       
            string nextLabel = this.codeGenerator.NewLabel + "_END_NON_SUITABLE_OBJECTS_LABEL";
            this.codeGenerator.Comment(this.indent, "Non suitable objects");
            string te = string.Empty;;
            foreach ( TypeExpression ti in this.nonValidObjectsList ) {
                this.codeGenerator.dup(this.indent);
                this.codeGenerator.Comment(this.indent, ti.FullName);
                this.codeGenerator.isinst(this.indent, ti);
                te = ti.ILType();
                this.codeGenerator.brtrue(this.indent, this.nonValidObjectsLabel);

            }
            this.codeGenerator.br(this.indent, nextLabel);
            this.codeGenerator.WriteLabel(this.indent, this.nonValidObjectsLabel);
            this.codeGenerator.WriteThrowNonSuitableObjectException(this.indent, te, ( (AST.FieldAccessExpression)( this.node.Identifier ) ).FieldName.ILName);
            this.codeGenerator.Comment("Not reached");
            this.codeGenerator.WriteLabel(this.indent, nextLabel); // not reached
        }
        
        public override object Exec(TypeExpression t, object arg) {
            this.codeGenerator.Comment(this.indent, "//entro por type expression");
            return null;
        }
        public override object ReportError(TypeExpression tE) {
            ErrorManager.Instance.NotifyError(new CodeGenerationError("//No se ha definido la operación solicitada. " + tE.FullName));
            return null;
        }

    }
}
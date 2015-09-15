//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: VisitorRrotorCodeGeneration.cs                                             
// Author: Francisco Ortin  -  francisco.ortin@gmail.com                    
// Description:                                                               
//    This class walks the AST to obtain the translation to IL code over
//        the Reflective extension of the SSCLI (Rrotor).
//    Inheritance: VisitorCodeGeneration                   
//    Implements Visitor pattern [Concrete Visitor].       
//    Implements Factory method  [Concrete Product].       
// -------------------------------------------------------------------------- 
// Create date: 21-08-2007                                                    
// Modification date: 21-08-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using AST;
using Tools;
using TypeSystem;
using ErrorManagement;

namespace CodeGeneration {
    /// <summary>
    /// This class walks the AST to obtain the translation to IL code over
    /// the Reflective extension of the SSCLI (Rrotor).
    /// </summary>
    /// <remarks>
    /// Inheritance: VisitorCodeGeneration                   
    /// Implements Visitor pattern [Concrete Visitor].       
    /// Implements Factory method  [Concrete Product].       
    /// </remarks>
    abstract class VisitorRrotorCodeGeneration<T>: VisitorILCodeGeneration <T> where T: RrotorCodeGenerator{



        #region Constructor
        /// <summary>
        /// Constructor of VisitorRrotorCodeGeneration
        /// </summary>
        /// <param name="writer">Writer to write the intermediate code.</param>
        /// <param name="nameModule">WriteModule name.</param>
        /// <param name="targetPlatform">The name of the target platform</param>
        public VisitorRrotorCodeGeneration(string moduleName, T codeGenerator)
            : base(moduleName, codeGenerator) { }

        #endregion

        #region Visit(InvocationExpression node, Object obj)

        public override Object Visit(InvocationExpression node, Object obj) {
            InheritedAttributes ia = (InheritedAttributes)obj;
            Object objArgs = obj;
            MethodType actualMethodCalled = TypeExpression.As<MethodType>(node.ActualMethodCalled);
            if (actualMethodCalled != null)
                objArgs = new InheritedAttributes(ia.CurrentMethod, ia.Assignment, ia.Reference, ia.ArrayAccessFound, actualMethodCalled, ia.IsParentNodeAnInvocation);

            FieldAccessExpression fieldAccessExpression = node.Identifier as FieldAccessExpression;
            if (fieldAccessExpression != null) {
                // * A message has been sent with the syntax "obj.method(...)"
                Object o = node.Identifier.Accept(this, obj);
                node.Arguments.Accept(this, objArgs);
                if ((o is SynthesizedAttributes) && (((SynthesizedAttributes)o).IdentifierExpressionMode == IdentifierMode.Instance)) {
                    TypeExpression klass = null; // * When the implicit object is a fresh var reference
                    if (actualMethodCalled != null)
                        klass = actualMethodCalled.MemberInfo.Class;
                    this.codeGenerator.CallVirt(this.indent, actualMethodCalled, klass, fieldAccessExpression.FieldName.Identifier, node.Arguments);
                }
                else
                    this.codeGenerator.Call(this.indent, actualMethodCalled, actualMethodCalled.MemberInfo.Class, actualMethodCalled.MemberInfo.MemberIdentifier);
            }
            else {
                // * A message is sent with without the implicit object "method(...)"
                if (node.Identifier is SingleIdentifierExpression) {
                    if ((((MethodType)node.Identifier.ExpressionType).MemberInfo.ModifierMask & Modifier.Static) == 0)
                        this.codeGenerator.ldarg(this.indent, 0);
                    node.Arguments.Accept(this, objArgs);
                    this.codeGenerator.Call(this.indent, actualMethodCalled, actualMethodCalled.MemberInfo.Class, ((SingleIdentifierExpression)node.Identifier).Identifier);
                }
                else {
                    if (node.Identifier is BaseExpression) {
                        node.Arguments.Accept(this, objArgs);
                        this.codeGenerator.constructorCall(this.indent, actualMethodCalled, ((BaseExpression)node.Identifier).ExpressionType, ".ctor");
                    }
                }
            }
            //TODO: OJO AQUÏ: MIRAR DONDE SE CORRESPONDE CON CODEGENERATOR, PORQUE GENERAR UNA LÍNEA DESDE EL VISITOR
            // ES un poco chapuzaCHAPUZA
            this.codeGenerator.WriteLine();
            return null;
        }

        #endregion

        #region AddException
        public override void AddExceptionCode() {
            throw new NotImplementedException();
        }
        #endregion
    }
}
